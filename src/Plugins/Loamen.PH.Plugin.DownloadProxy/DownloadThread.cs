using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Loamen.PluginFramework;
using ThreadState = System.Threading.ThreadState;

namespace Loamen.PH.Plugin.DownloadProxy
{
    public class DownloadThread
    {
        #region 变量

        public delegate void CompletedEventHandler(object sender, EventArgs e);

        private readonly IApp app;

        private readonly Thread thread;
        private int timeOut = 10;

        public event CompletedEventHandler Completed;

        #endregion

        #region 属性

        private string description;
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

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

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                if (null != app)
                {
                    app.WriteDebug(description);
                }
            }
        }

        #endregion

        #region 构造函数

        public DownloadThread(string url, IApp app)
        {
            this.app = app;
            this.url = url;
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
            Description = "";
            thread.Abort();
        }

        public void OnCompleted()
        {
            if (null != Completed)
            {
                app.WriteDebug("线程" + thread.ManagedThreadId + "工作完成");
                Completed(this, new EventArgs()); //发出警报
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

        private static void Downloading(DownloadThread downloader)
        {
            GetHtmlString(downloader); //httpWebRequest方式
        }

        public static void GetHtmlString(DownloadThread downloader)
        {
            string result = "";

            if (string.IsNullOrEmpty(downloader.Url))
                return;
            var sw = new Stopwatch();
            sw.Start();


            var r = new Random();
            string url = downloader.url;

            var webRequest = (HttpWebRequest) WebRequest.Create(new Uri(url).ToString());

            try
            {
                downloader.Description = "设置完成，开始访问网页...";

                webRequest.Method = "Get";
                webRequest.Accept = "*/*";
                webRequest.UserAgent = "Mozilla/4.0 (MSIE 6.0; Windows NT " + r.Next(99999) + ")";
                webRequest.Headers["Accept-Language"] = "zh-cn";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Referer = "http://www.loamen.com/404.htm?name=" + Application.ProductName + "&url=" + url;
                webRequest.Timeout = downloader.timeOut*1000;
                webRequest.ReadWriteTimeout = downloader.timeOut*1000;
                webRequest.ServicePoint.ConnectionLimit = 100;

                WebResponse response = webRequest.GetResponse();
                result = GetHtmlString(response, Encoding.Default);
                response.Close();
                string line = result;
                var regex = new Regex(Download.RegexProxy);
                MatchCollection matches = regex.Matches(line);

                int count = 0;
                for (int i = 0; i < matches.Count; i++)
                {
                    string ip = matches[i].Groups["Proxy"].Value;
                    string port = matches[i].Groups["Port"].Value;

                    var proxy = new ProxyServer();

                    proxy.proxy = matches[i].Groups["Proxy"].Value;
                    proxy.port = Convert.ToInt32(matches[i].Groups["Port"].Value);
                    proxy.type = "HTTP";
                    proxy.status = -1;

                    var regexProxy =
                        new Regex(
                            @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))");
                    if (regexProxy.Matches(proxy.proxy).Count != 1 || proxy.port == 0)
                        return;
                    lock (downloader.app.ProxyList)
                    {
                        if (downloader.app.ProxyList.Count(p => p.proxy == proxy.proxy && p.port == proxy.port) == 0)
                        {
                            downloader.app.ProxyList.Add(proxy);
                        }
                        count++;
                    }
                }
                sw.Stop();
                downloader.Description = url + "采集完毕，耗时：" + sw.ElapsedMilliseconds + "毫秒，共" + count + "条";
                downloader.app.SetStatusText(downloader.description);
            }
            catch (Exception ex)
            {
                sw.Stop();
                downloader.Description = url + "出错啦：" + ex.Message + sw.ElapsedMilliseconds;
            }
            finally
            {
                webRequest.Abort();
            }
        }

        public static string GetHtmlString(WebResponse response, Encoding encoding)
        {
            try
            {
                using (var stream = new StreamReader(response.GetResponseStream(), encoding))
                {
                    string code = stream.ReadToEnd();
                    return code;
                }
            }
            catch (Exception ce)
            {
                throw ce;
            }
        }

        #endregion
    }
}