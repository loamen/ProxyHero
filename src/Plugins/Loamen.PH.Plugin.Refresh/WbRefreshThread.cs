using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Loamen.WinControls.UI;
using Loamen.RefreshPlugin.Entity;
using System.Diagnostics;
using System.Net;

namespace Loamen.PH.Plugin.Refresh
{
    public class WbRefreshThread
    {
        #region 变量
        private Thread thread = null;
        private List<Proxy> proxyList;
        private bool useProxy = true; //是否使用代理刷新
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
        public WbRefreshThread(List<Proxy> pList, string url, bool useProxy, int timeOut)
        {
            //wbWebBrowser.Navigated += new WebBrowserNavigatedEventHandler(wbWebBrowser_Navigated);
            //wbWebBrowser.ScriptErrorsSuppressed = true;

            this.timeOut = timeOut;
            this.useProxy = useProxy;
            this.url = url;
            this.ProxyCount = pList.Count;
            this.proxyList = pList;

            thread = new Thread(new ParameterizedThreadStart(WbRefreshThread.DoWork));
            thread  .SetApartmentState(ApartmentState.STA);
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
            WbRefreshThread refresher = (WbRefreshThread)data;

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
                    }
                    catch (Exception exx)
                    {
                        refresher.Description = "出错啦：" + exx.Message;
                        refresher.FailedCount++;
                    }
                    refresher.Status = refresher.thread.ThreadState.ToString();
                }

                refresher.Status = "Completed";
            }
            catch (Exception ex)
            {
                // 线程被放弃
                refresher.Description =  "出错啦：" + ex.Message;
                refresher.FailedCount++;
                refresher.Status = "Completed";
                Console.WriteLine(ex.Message);
                //WriteException(ex);
            }
            finally
            {
                refresher.TotalCount++;
                refresher.OnCompleted();
            }
        }

        private static void Refresh(WbRefreshThread refresher, Proxy proxy)
        {
            WebBrowserEx wbWebBrowser = new WebBrowserEx();
            wbWebBrowser.ScriptErrorsSuppressed = true;

            if (refresher.useProxy)
                wbWebBrowser.ProxyServer = proxy.IpAndPort;
            else
                wbWebBrowser.ProxyServer = "";
            wbWebBrowser.Navigate(refresher.Url);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (wbWebBrowser.ReadyState < WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
                if (sw.ElapsedMilliseconds > refresher.timeOut * 1000)
                {
                    throw new WebException(WebExceptionStatus.Timeout.ToString());
                }
            }
            sw.Stop();
            refresher.SuccessCount++;
            refresher.Description = wbWebBrowser.ProxyServer + "刷新成功！";
        }

        #endregion

        private void wbWebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)  //屏蔽alert等弹框弹窗的情况
        {
            //WebBrowserEx wbWebBrowser = (WebBrowserEx)sender;
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("function alert(){return;}");
            //sb.AppendLine("function confirm(){return;}");
            //sb.AppendLine("function showModalDialog(){return;}");
            //sb.AppendLine("function window.open(){return;}");
            //sb.AppendLine("function prompt(){return;}");
            //string strJS = sb.ToString();
            //IHTMLWindow2 win = (IHTMLWindow2)wbWebBrowser.Document.Window.DomWindow;
            //win.execScript(strJS, "Javascript");
            //win = null;
        }
    }
}
