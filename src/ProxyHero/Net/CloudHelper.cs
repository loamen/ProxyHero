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
                Config.ConsoleEx.Debug(ex);
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
                Config.ConsoleEx.Debug(ex);
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
                        Config.MyApiHelper.AddOrUpdateProxy(model);
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
                Config.ConsoleEx.Debug(ex);
                return true;
            }
            finally
            {
                Config.LateUpdateProxyListTime = DateTime.Now;
            }
        }


        private static ProxyHeroEntity GetConfig(string url)
        {
            var result = new ProxyHeroEntity();
            try
            {
                var httpHelper = new HttpHelper();
   
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

                ProxyHeroEntity proxyHeroEntity = JsonHelper.JsonToModel<ProxyHeroEntity>(html);

                result=proxyHeroEntity;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex, "初始化配置异常");
            }

            return result;
        }

        /// <summary>
        ///     获取网络配置
        /// </summary>
        public static void GetNetConfig()
        {
            #region Config

            string url = "https://gitee.com/yangd4/ph/raw/master/Config.json";
            ProxyHeroEntity result = GetConfig(url);


            if (result.Sites == null || result.Sites.Length == 0)
            {
                Config.InitErrorInfo = Config.LocalLanguage.Messages.InitializeFailed;
                throw new Exception(Config.InitErrorInfo);
            }

            Config.ProxyHeroCloudSetting = result;

            if (Config.ProxyHeroCloudSetting == null)
            {
                Config.InitErrorInfo = Config.LocalLanguage.Messages.InitializeFailed;
                throw new Exception(Config.InitErrorInfo);
            }

            #endregion

            #region get proxyPageList

     
            for (int i = 0; i < result.Sites.Length; i++)
            {
                Config.ProxySiteUrlList.Add(result.Sites[i]);
            }

            #endregion
        }
    }
}