using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Loamen.Net;
using Loamen.PluginFramework;
using ProxyHero.Common;
using ProxyHero.Model;

namespace ProxyHero.Proxy
{
    public class DownloadThread
    {
        #region 变量

        public delegate void DownloadThread_CompletedEventHandler(object sender, EventArgs e);

        private readonly Thread thread;

        public event DownloadThread_CompletedEventHandler DownloadThread_Completed;

        #endregion

        #region 属性

        public string Url { get; set; }

        /// <summary>
        ///     线程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     线程状态
        /// </summary>
        public ThreadState ThreadState
        {
            get
            {
                if (thread == null) return ThreadState.Unstarted;
                return thread.ThreadState;
            }
        }

        /// <summary>
        ///     当前线程执行状态
        /// </summary>
        public bool IsAlive
        {
            get
            {
                if (thread == null) return false;
                return thread.IsAlive;
            }
        }

        public string Description { get; set; }

        #endregion

        #region 构造函数

        public DownloadThread(string url)
        {
            this.Url = url;
            thread = new Thread(DoWork);
            Name = thread.ManagedThreadId.ToString();
            Status = thread.ThreadState.ToString();
        }

        #endregion

        #region 方法

        public void Start()
        {
            thread.Start(this);
        }

        public void Abort()
        {
            Description = "Thread " + thread.ManagedThreadId + " is aborting";
            thread.Abort();
            OnCompleted();
        }

        public void OnCompleted()
        {
            if (null != DownloadThread_Completed)
            {
                DownloadThread_Completed(this, new EventArgs()); //发出警报
            }
        }

        private static void DoWork(object data)
        {
            var downloader = (DownloadThread) data;

            try
            {
                Downloading(downloader);
                downloader.Status = downloader.thread.ThreadState.ToString();
                downloader.Status = "Completed";
            }
            catch (Exception ex)
            {
                // 线程被放弃
                downloader.Status = "Completed";
                Console.WriteLine(ex.Message);
                //WriteException(ex);
            }
            finally
            {
                downloader.OnCompleted();
            }
        }

        /// <summary>
        ///     获取代理列表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static void Downloading(DownloadThread downloader)
        {
            try
            {
                var httpHelper = new HttpHelper();
                httpHelper.IsUseDefaultProxy = false;

                if (Config.LocalSetting.HttpOption != null)
                {
                    httpHelper.HttpOption = Config.LocalSetting.HttpOption;
                }
                else
                {
                    httpHelper.HttpOption.Timeout = 60*1000;
                }
                string result = "";
                if (Config.LocalSetting.IsUseSystemProxy)
                {
                    result = httpHelper.GetHtml(downloader.Url);
                }
                else
                {
                    result = httpHelper.GetHtml(downloader.Url);
                }

                string line = result;
                var regex = new Regex(Config.RegexProxy);
                MatchCollection matches = regex.Matches(line);

                downloader.Description = "The downloading thread " + downloader.thread.ManagedThreadId +
                                         " is downloading.Proxies count:" + matches.Count;
                Config.ConsoleEx.Debug(downloader.Description);

                for (int i = 0; i < matches.Count; i++)
                {
                    var model = new ProxyServer();
                    model.proxy = matches[i].Groups["Proxy"].Value;
                    model.port = int.Parse(matches[i].Groups["Port"].Value);

                    ProxyServer cloudProxy = CloudProxyData.Get(model.proxy, model.port);
                    if (null != cloudProxy)
                    {
                        ProxyDataAddRow(cloudProxy);
                    }
                    else
                    {
                        ProxyDataAddRow(model);
                    }
                }

                downloader.Description = "The downloading thread " + downloader.thread.ManagedThreadId +
                                         " download completed.";
                Config.ConsoleEx.Debug(downloader.Description);
            }
            catch (WebException webEx)
            {
                Config.ConsoleEx.Debug(webEx);
            }
            catch (Exception ex)
            {
                Config.ConsoleEx.Debug(ex);
            }
            finally
            {
            }
        }

        public static void ProxyDataAddRow(ProxyServer proxy)
        {
            try
            {
                var regexProxy =
                    new Regex(
                        @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))");
                if (regexProxy.Matches(proxy.proxy).Count != 1 || proxy.port == 0)
                    return;
                ProxyData.Set(proxy);
            }
            catch (Exception ex)
            {
                Config.ConsoleEx.Debug(ex);
            }
        }

        #endregion
    }
}