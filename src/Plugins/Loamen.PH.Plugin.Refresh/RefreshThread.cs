using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Loamen.RefreshPlugin.Entity;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Loamen.PH.Plugin.Refresh
{
    public class RefreshThread
    {
        #region 变量
        private Thread thread = null;
        private List<Proxy> proxyList;
        private int sleepTime = 5;
        private int timeOut = 10;
        public delegate void CompletedEventHandler(object sender, EventArgs e);
        public event CompletedEventHandler Completed;
        #endregion

        #region 属性
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        /// <summary>
        /// 线程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 线程状态
        /// </summary>
        public System.Threading.ThreadState ThreadState
        {
            get
            {
                if (thread == null) return System.Threading.ThreadState.Unstarted;
                return thread.ThreadState;
            }
        }
        /// <summary>
        /// 当前线程执行状态
        /// </summary>
        public bool IsAlive
        {
            get
            {
                if (thread == null) return false;
                return thread.IsAlive;
            }
        }
        /// <summary>
        /// 代理数量
        /// </summary>
        public int ProxyCount { get; set; }
        /// <summary>
        /// 总刷新数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        public int SuccessCount { get; set; }
        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailedCount { get; set; }
        public string Description { get; set; }
        #endregion

        #region 构造函数
        public RefreshThread(List<Proxy> pList, string url, int sleepTime,int timeOut)
        {
            this.timeOut = timeOut;
            this.sleepTime = sleepTime;
            this.url = url;
            this.ProxyCount = pList.Count;
            this.proxyList = pList;
            thread = new Thread(RefreshThread.DoWork);
            this.Name = thread.ManagedThreadId.ToString();
            this.Status = thread.ThreadState.ToString();
        }
        #endregion

        #region 方法
        public void Start()
        {
            thread.Start(this);
        }

        public void Abort()
        {
            this.Description = "";
            thread.Abort();
            this.OnCompleted();
        }

        public void OnCompleted()
        {
            if (null != Completed)
            {
                this.Completed(this, new EventArgs()); //发出警报
            }
        }

        private static void DoWork(object data)
        {
            RefreshThread refresher = (RefreshThread)data;

            try
            {
                List<Proxy> pList = refresher.proxyList;

                foreach (Proxy proxy in pList)
                {
                    refresher.Description = proxy.IpAndPort + "开始刷新...";
                    try
                    {
                        Refresh(refresher, proxy);
                        RefreshForm fm = (RefreshForm)Application.OpenForms["RefreshForm"];
                        if (null != fm)
                        {
                            fm.UpdateDataGrid();
                        }
                        Thread.Sleep(refresher.sleepTime * 1000);
                    }
                    catch (ThreadAbortException)
                    {
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        refresher.TotalCount++;
                    }
                    refresher.Status = refresher.thread.ThreadState.ToString();
                }

                refresher.Status = "Completed";
            }
            catch (Exception ex)
            {
                // 线程被放弃
                refresher.Status = "Completed";
                Console.WriteLine(ex.Message);
                //WriteException(ex);
            }
            finally
            {
                refresher.OnCompleted();
            }
        }

        private static void Refresh(RefreshThread refresher, Proxy proxy)
        {
            GetHtmlString(proxy, refresher); //httpWebRequest方式
            //GetHtmlString(refresher,proxy.IpAndPort); //webClient方式
        }

        public static string GetHtmlString(Proxy proxy,RefreshThread refresher)
        {
            string result = "";

            if (proxy == null)
                return result;
            Stopwatch sw = new Stopwatch();
            sw.Start();


            Random r = new Random();
            string url = refresher.url;
            //if (url.Contains("?"))
            //{
            //    url = url + "&p=" + r.Next(99999);
            //}
            //else
            //{
            //    url = url + "?p=" + r.Next(99999);
            //}

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url).ToString());

            try
            {
                WebProxy proxyServer = WebProxy.GetDefaultProxy();
                if (!string.IsNullOrEmpty(proxy.IP) && proxy.Port != 0)
                {
                    proxyServer.Address = new Uri("http://" + proxy.IpAndPort + "/");

                }
                webRequest.Proxy = proxyServer;
                refresher.Description = proxy.IpAndPort + "设置完成，开始访问网页...";

                webRequest.Method = "Get";
                webRequest.Accept = "*/*";
                webRequest.UserAgent = "Mozilla/4.0 (MSIE 6.0; Windows NT " + r.Next(99999) + ")";
                webRequest.Headers["Accept-Language"] = "zh-cn";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Referer = "http://www.loamen.com/404.htm?name=refresher&url=" + url;
                webRequest.Timeout = refresher.timeOut * 1000;
                webRequest.ReadWriteTimeout = refresher.timeOut * 1000;
                webRequest.ServicePoint.ConnectionLimit = 100;

                WebResponse response = webRequest.GetResponse();
                result = GetHtmlString(response, Encoding.GetEncoding("GB2312"));
                response.Close();
                sw.Stop();
                refresher.Description = proxy.IpAndPort + "刷新完毕，耗时：" + sw.ElapsedMilliseconds;
                refresher.SuccessCount++;
                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                refresher.FailedCount++;
                refresher.Description = proxy.IpAndPort + "出错啦：" + ex.Message + sw.ElapsedMilliseconds;
                return result;
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
                using (StreamReader stream = new StreamReader(response.GetResponseStream(), encoding))
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

        public static string GetHtmlString(RefreshThread refresher, string proxyServer)
        {
            Stream data = null;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string result = "";

            try
            {

                string html = "";

                WebClient client = new WebClient();
                refresher.Description = proxyServer + "开始设置代理...";
                WebProxy proxy = WebProxy.GetDefaultProxy();
                proxy.Address = new Uri("http://" + proxyServer + "/");
                client.Proxy = proxy;
                refresher.Description = proxyServer + "设置完成，开刷...";
                data = client.OpenRead(refresher.url);
                

                using (StreamReader reader = new StreamReader(data, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    result = reader.ReadLine();
                    while (result != null)
                    {
                        html += result;
                        result = reader.ReadLine();
                    }
                }
                sw.Stop();
                refresher.Description = proxyServer + "搞定，耗时：" + sw.ElapsedMilliseconds;
                refresher.SuccessCount++;
                return html;
            }
            catch (Exception ex)
            {
                sw.Stop();
                refresher.FailedCount++;
                refresher.Description = proxyServer + "有问题：" + ex.Message + sw.ElapsedMilliseconds;
                return result;
            }
            finally
            {
                if (data != null) data.Close();
            }
        }
        #endregion
    }
}
