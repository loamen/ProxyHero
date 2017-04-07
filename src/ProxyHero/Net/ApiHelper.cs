using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Loamen.Common;
using Loamen.Net;
using Loamen.PluginFramework;
using ProxyHero.Model;
using ProxyHero.Threading;

namespace ProxyHero.Net
{
    public class ApiHelper : HttpHelper
    {
        private ProxyItems _proxyItems;
        private Worker _worker;

        public ApiHelper()
        {
            //不是用默认代理
            IsUseDefaultProxy = false;
        }

        public ApiHelper(OauthKey oauthKey)
        {
            //不是用默认代理
            IsUseDefaultProxy = false;
            Config.MyOauthKey = oauthKey;

            #region

            //var postData = string.Format("{0}username={1}&password={2}", UrlType.GetToken, oauthKey.Username, oauthKey.Passwod);
            //var json = this.GetHtmlNoProxy(postData);
            //var result = JsonHelper.JsonToModel<JsonResult<Token>>(json);
            //if (result.data != null)
            //{
            //    Config.MyOauthKey.Uid = result.data.uid;
            //    Config.MyOauthKey.TokenKey = result.data.token;
            //}

            #endregion
        }

        /// <summary>
        ///     获取时间
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DateTime GetDate(DateType type)
        {
            string json = GetHtml(UrlType.GetDate);
            try
            {
                var result = JsonHelper.JsonToModel<JsonResult<string>>(json);
                if (result.err_code == 0)
                {
                    return Convert.ToDateTime(result.data);
                }
            }
            catch
            {
            }

            return DateTime.Now;
        }

        /// <summary>
        ///     下载代理数据
        /// </summary>
        /// <returns></returns>
        public ProxyItems DownloadProxyList()
        {
            var sw = new Stopwatch();
            var result = new ProxyItems {items = new List<ProxyServer>()};
            _proxyItems = new ProxyItems {items = new List<ProxyServer>()};

            try
            {
                sw.Start();
                var totalCount = GetTotalCount();
                if (totalCount > 0)
                {
                    const int pageSize = 1000; //每次取的数量 
                    var page = totalCount/pageSize;
                    page = totalCount%pageSize > 0 ? page + 1 : page; //总页数

                    #region 单线程

                    //for (var i = 0; i < page; i++)
                    //{
                    //    proxyItems = DownloadProxyList(i * pageSize, pageSize);
                    //    if (null != proxyItems && proxyItems.items != null)
                    //    {
                    //        result.items = result.items.AsEnumerable().Union(proxyItems.items.AsEnumerable()).Distinct().ToList<ProxyServer>();
                    //    }
                    //}

                    #endregion

                    #region 多线程

                    _worker = new Worker(new object[] {page, pageSize});
                    _worker.Worker_Started += WorkStart; //工作开始
                    _worker.Worker_Completed += WorkCompleted; //工作结束
                    _worker.Start();
                    while (_worker.ReturnValue == null)
                    {
                        if (sw.Elapsed.Minutes > 2)
                        {
                            sw.Stop();
                            return result;
                        }
                    }
                    result = (ProxyItems) _worker.ReturnValue;

                    #endregion
                }
                sw.Stop();
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        ///     下载代理数据
        /// </summary>
        /// <returns></returns>
        private ProxyItems DownloadProxyList(int sinceId, int count)
        {
            string postData = string.Format("{0}token={1}&since_id={2}&count={3}", UrlType.GetProxyList,
                                            Config.MyOauthKey.TokenKey, sinceId, count);

            string json = GetHtml(postData);
            if (!json.Contains("\"data\":false"))
            {
                var result = JsonHelper.JsonToModel<JsonResult<ProxyItems>>(json);
                if (result.err_code == 0 && result.data != null)
                {
                    return result.data;
                }
            }
            return null;
        }

        /// <summary>
        ///     获取代理总数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            var postData = string.Format("{0}token={1}", UrlType.GetTotalCount, Config.MyOauthKey.TokenKey);

            try
            {
                var json = GetHtml(postData);
                if (!json.Contains("\"data\":false"))
                {
                    var result = JsonHelper.JsonToModel<JsonResult<int>>(json);
                    if (result.err_code == 0)
                    {
                        return result.data;
                    }
                }
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        ///     新增或插入数据
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public string AddOrUpdate(ProxyServer proxy)
        {
            string postData =
                string.Format(
                    "{0}proxy={1}&port={2}&type={3}&response={4}&proxyusername={5}&proxypassword={6}&anonymity={7}&country={8}&anonymityen={9}&countryen={10}&description={11}&testdate={12}&status={13}&userid={14}&username={15}&userip={16}&isvip={17}&token={18}",
                    UrlType.AddOrUpdateList, proxy.proxy, proxy.port, proxy.type, proxy.response, proxy.proxyusername,
                    proxy.proxypassword, proxy.anonymity, proxy.country, proxy.anonymityen, proxy.countryen,
                    proxy.description, proxy.testdate, proxy.status, proxy.userid, proxy.username, proxy.userip,
                    proxy.isvip, Config.MyOauthKey.TokenKey);

            string json = GetHtml(postData);
            var result = JsonHelper.JsonToModel<JsonResult<string>>(json);
            return result.data;
        }

        /// <summary>
        ///     新增或插入数据列表
        /// </summary>
        /// <param name="proxyList"></param>
        /// <returns></returns>
        public string UpdateList(string proxyList)
        {
            var postData = string.Format("proxyList={0}&token={1}", proxyList, Config.MyOauthKey.TokenKey);
            try
            {
                var json = GetHtml(UrlType.UpdateList, postData);
                var result = JsonHelper.JsonToModel<JsonResult<string>>(json);
                return result.data;
            }
            catch
            {
            }
            return string.Empty;
        }

        #region 工作

        /// <summary>
        ///     开始工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="parameters"></param>
        private void WorkStart(object sender, EventArgs e, object[] parameters)
        {
            var page = (int) parameters[0]; //总页数
            var pageSize = (int) parameters[1]; //每页大小
            for (int i = 0; i < page; i++)
            {
                var thread = new WorkThread(new object[] {i*pageSize, pageSize});
                thread.WorkThread_Completed += WorkThread_Completed;
                thread.WorkThread_DoWork += WorkThread_DoWork; //设定工作内容
                thread.Start(); //启动工作线程
                _worker.Threads.Add(thread);
            }
        }

        /// <summary>
        ///     结束工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkCompleted(object sender, EventArgs e)
        {
            var worker = (Worker) sender;
            List<ProxyServer> proxies = _proxyItems.items.Where(p => p.status == 0).ToList();
            foreach (ProxyServer proxy in proxies)
            {
                proxy.status = -1;
            }
            worker.ReturnValue = _proxyItems;
        }

        #endregion

        #region 工作线程

        /// <summary>
        ///     线程工作内容
        /// </summary>
        /// <param name="WorkThread"></param>
        /// <param name="arg"></param>
        private void WorkThread_DoWork(object WorkThread, EventArgs arg)
        {
            var workThread = (WorkThread) WorkThread;
            var sinceId = (int) workThread.Parameters[0];
            var count = (int) workThread.Parameters[1];
            ProxyItems items = DownloadProxyList(sinceId, count);
            if (null != items && items.items != null)
            {
                _proxyItems.items = items.items.AsEnumerable().Union(_proxyItems.items.AsEnumerable()).Distinct().ToList();
            }
        }

        /// <summary>
        ///     线程结束时发出警告
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="arg"></param>
        private void WorkThread_Completed(object obj, EventArgs arg)
        {
            var thread = obj as WorkThread;
            if (thread != null && thread.Status == "Completed")
            {
                _worker.WorkThreadCompletedCount++;
            }

            if (_worker.WorkThreadCompletedCount == _worker.Threads.Count)
            {
                _worker.OnWorkerCompleted(); //全部工作完成，提醒上一级结束
            }
        }

        #endregion
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