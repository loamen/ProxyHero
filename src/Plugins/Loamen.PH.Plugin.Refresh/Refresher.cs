using System;
using System.Collections.Generic;
using System.Text;
using Loamen.RefreshPlugin.Entity;

namespace Loamen.PH.Plugin.Refresh
{
    public class Refresher
    {
        private List<string> urlList;
        private List<object> threads;
        private int sleepTime = 5;
        private int timeOut = 10;
        private int CompletedCount = 0;
        private bool useProxy = true; //是否使用代理
        private string refreshType = "WebBrowser"; //刷新方式
        public delegate void CompletedEventHandler(object sender, EventArgs e);
        public event CompletedEventHandler Completed;

        public int TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }
        /// <summary>
        /// 暂停时间
        /// </summary>
        public int SleepTime
        {
            get { return sleepTime; }
            set { sleepTime = value; }
        }

        public List<object> Threads
        {
            get { return threads; }
            set { threads = value; }
        }
        private List<Proxy> proxyList;

        public Refresher(List<string> urlList, List<Proxy> proxyList, bool useProxy,string refreshType)
        {
            threads = new List<object>();
            this.urlList = urlList;
            this.proxyList = proxyList;
            this.useProxy = useProxy;
            this.refreshType = refreshType;
        }

        public void OnCompleted()
        {
            if (null != Completed)
            {
                this.Completed(this, new EventArgs()); //发出警报
            }
        }

        public void Start()
        {
            foreach (string url in urlList)
            {
                if (this.refreshType == "Quickly")
                {
                    RefreshThread thread = new RefreshThread(proxyList, url, this.SleepTime, this.TimeOut);
                    thread.Completed += new RefreshThread.CompletedEventHandler(this.CheckCompleted);
                    thread.Start();
                    threads.Add(thread);
                }
                else
                {
                    WbRefreshThread thread = new WbRefreshThread(proxyList, url, useProxy, this.TimeOut);
                    thread.Completed += new WbRefreshThread.CompletedEventHandler(this.CheckCompleted);
                    thread.Start();
                    threads.Add(thread);
                }
            }
        }

        public void Stop()
        {
            foreach (object thread in this.Threads)
            {
                if (thread.GetType().Name == "RefreshThread")
                {
                    RefreshThread t = (RefreshThread)thread;
                    t.Abort();
                }

                if (thread.GetType().Name == "WbRefreshThread")
                {
                    WbRefreshThread t = (WbRefreshThread)thread;
                    t.Abort();
                }
            }
            this.Completed(this.Completed, new EventArgs()); //发出警告
        }

        private void CheckCompleted(object obj, EventArgs arg)
        {
            if (obj.GetType().Name == "RefreshThread")
            {
                RefreshThread thread = obj as RefreshThread;
                if (thread.Status == "Completed")
                {
                    this.CompletedCount++;
                }
            }

            if (obj.GetType().Name == "WbRefreshThread")
            {
                WbRefreshThread thread = obj as WbRefreshThread;
                if (thread.Status == "Completed")
                {
                    this.CompletedCount++;
                }
            }

            if (this.CompletedCount == this.threads.Count)
            {
                this.Completed(this.Completed, new EventArgs()); //发出警告
            }
        }
    }
}
