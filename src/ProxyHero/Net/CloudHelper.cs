using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Loamen.Common;
using Loamen.Net;
using Loamen.PluginFramework;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.Model;

namespace ProxyHero.Net
{
    public class CloudHelper
    {
        #region  valiables

        #endregion

        /// <summary>
        ///     下载代理
        /// </summary>
        /// <returns></returns>
        public bool DownloadProxyList()
        {
            try
            {
                CloudProxyData.CloudProxyList.Clear();

                ProxyItems result = Config.MyApiHelper.DownloadProxyList();

                if (result != null && result.items.Count > 0)
                {
                    List<ProxyServer> proxies = result.items;
                    lock (CloudProxyData.CloudProxyList)
                    {
                        CloudProxyData.CloudProxyList = (from row in proxies
                                                         select row).Distinct().ToList();
                    }

                    return true;
                }
                else
                {
                    return false;
                }

               
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                return false;
            }
            finally
            {
                Config.LateUpdateProxyListTime = DateTime.Now;
            }
        }

        /// <summary>
        ///     上传可用代理
        /// </summary>
        /// <returns></returns>
        public bool UploadProxyList(IList<ProxyServer> list)
        {
            try
            {
                if (list.Count > 0)
                {
                    var i = 0;
                    IList<ProxyServer> tempList = new List<ProxyServer>();
                    ThreadPool.SetMaxThreads(50, 50);
                    var threadCount = 0; //线程完成次数
                    const int exeSize = 5000;
                    var exeCount = list.Count/exeSize; //执行次数
                    exeCount = exeCount%exeSize > 0 ? exeCount + 1 : exeCount; //需要总执行次数

                    #region 执行

                    foreach (ProxyServer model in list)
                    {
                        tempList.Add(model);
                        i++;
                        if (i%5000 == 0) //每5000条上传一次
                        {
                            var task = new Task<bool>(() => UploadProxy(tempList));
                            task.Start();
                            task.Wait(); //等待任务执行完成
                            if (task.Result)
                            {
                                threadCount++;
                            }
                            tempList = new List<ProxyServer>();
                        }
                    }

                    if (tempList.Count > 0)
                    {
                        var task = new Task<bool>(() => UploadProxy(tempList));
                        task.Start();
                        task.Wait(); //等待任务执行完成
                        if (task.Result)
                        {
                            threadCount++;
                        }
                    }

                    #endregion

                    if (threadCount == exeCount)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
#if DEBUG
                LogHelper.WriteException(ex);
#else
                LogHelper.WriteLog(ex.Message);
#endif
                return false;
            }
        }

        /// <summary>
        ///     上传代理列表
        /// </summary>
        /// <returns></returns>
        private bool UploadProxy(object state)
        {
            if (state == null) throw new ArgumentNullException("state");
            try
            {
                var list = (IList<ProxyServer>) state;
                if (list.Count == 0) return true;
                var sbProxyList = new StringBuilder();
                foreach (var model in list)
                {
                    string proxy = model.proxy + "";
                    string port = model.port + "";
                    string type = model.type + "";
                   

                    if (!string.IsNullOrEmpty(proxy.Trim()) &&
                        port.Trim() != "0" &&
                        !string.IsNullOrEmpty(port.Trim()))
                    {
                        Config.MyApiHelper.AddOrUpdate(model);
                    }
                }

                if (sbProxyList.ToString().EndsWith(";"))
                {
                    sbProxyList.Remove(sbProxyList.Length - 1, 1);
                }
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                LogHelper.WriteException(ex);
#else
                LogHelper.WriteLog(ex.Message);
#endif
                return true;
            }
            finally
            {
                Config.LateUpdateProxyListTime = DateTime.Now;
            }
        }


        private static string[] GetConfig(string url)
        {
            var result = new[] {"", ""};
            try
            {
                var httpHelper = new HttpHelper();
                string htmlSites;
                string html;

                if (Config.LocalSetting.IsUseSystemProxy)
                {
                    httpHelper.IsUseDefaultProxy = true;
                    html = httpHelper.GetHtml(url, Encoding.GetEncoding("UTF-8"));
                }
                else
                {
                    httpHelper.IsUseDefaultProxy = false;
                    html = httpHelper.GetHtml(url, Encoding.GetEncoding("UTF-8"));
                }

                string xmlSetting = StringHelper.GetMidString(html, "[Config]", "[/Config]");
                xmlSetting = SecurityHelper.DecryptDES(xmlSetting, "Don.Yang");

                htmlSites = StringHelper.GetMidString(html, "[Sites]", "[/Sites]");
                htmlSites = SecurityHelper.DecryptDES(htmlSites, "Don.Yang");

                result[0] = xmlSetting;
                result[1] = htmlSites;
            }
            catch
            {
            }

            return result;
        }

        /// <summary>
        ///     获取网络配置
        /// </summary>
        public static void GetNetConfig()
        {
            #region Config

            string url = UrlType.GetConfig;
            string[] result = GetConfig(url);
            if (string.IsNullOrEmpty(result[0]))
            {
                url = "http://www.cnblogs.com/mops/articles/2377951.html";
                result = GetConfig(url);
            }
            string xmlSetting = result[0];
            string htmlSites = result[1];

            if (string.IsNullOrEmpty(xmlSetting))
            {
                Config.InitErrorInfo = Config.LocalLanguage.Messages.InitializeFailed;
                throw new Exception(Config.InitErrorInfo);
            }

            Config.ProxyHeroCloudSetting =
                XmlHelper.DeserializeXml(xmlSetting, typeof (ProxyHeroEntity)) as ProxyHeroEntity;

            if (Config.ProxyHeroCloudSetting == null)
            {
                Config.InitErrorInfo = Config.LocalLanguage.Messages.InitializeFailed;
                throw new Exception(Config.InitErrorInfo);
            }

            #endregion

            #region get proxyPageList

            string proxyPageListText = htmlSites;

            var regexList = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            MatchCollection matchesProxyList = regexList.Matches(proxyPageListText);
            for (int i = 0; i < matchesProxyList.Count; i++)
            {
                Config.ProxySiteUrlList.Add(matchesProxyList[i].Value);
            }

            #endregion
        }
    }

    internal class UrlType
    {
        /// <summary>
        ///     代理API地址
        /// </summary>
        private static string apiProxyUrl = "http://1.loamen.duapp.com/";

        public static string ApiProxyUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(Config.ProxyHeroCloudSetting.ApiDomain))
                {
                    return Config.ProxyHeroCloudSetting.ApiDomain;
                }

                return apiProxyUrl;
            }
        }

        //private static string apiUserUrl = "2.loamen.sinaapp.com/api/loamen_user";
        /// <summary>
        ///     获取时间URL
        /// </summary>
        public static string GetDate
        {
            get { return ApiProxyUrl + "?a=getprcdate&"; }
        }

        /// <summary>
        ///     获取代理列表URL
        /// </summary>
        public static string GetProxyList
        {
            get { return ApiProxyUrl + "?a=getproxylist&"; }
        }

        /// <summary>
        ///     获取代理总数URL
        /// </summary>
        public static string GetTotalCount
        {
            get { return ApiProxyUrl + "?a=gettotalcount&"; }
        }

        /// <summary>
        ///     获取TotkenURL
        /// </summary>
        public static string GetToken
        {
            get { return ApiProxyUrl + "?a=get_token&"; }
        }

        /// <summary>
        ///     新增和更新URL
        /// </summary>
        public static string AddOrUpdateList
        {
            get { return ApiProxyUrl + "?a=addorupdate&"; }
        }

        /// <summary>
        ///     新增和更新代理列表URL
        /// </summary>
        public static string UpdateList
        {
            get { return ApiProxyUrl + "?a=uploadproxylist&"; }
        }

        /// <summary>
        ///     获取配置
        /// </summary>
        public static string GetConfig
        {
            get { return ApiProxyUrl + "?a=getconfig&"; }
        }
    }
}