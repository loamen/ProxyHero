using System;
using System.Collections.Generic;
using ProxyHero.Model;

namespace ProxyHero.Proxy
{
    public class Downloader
    {
        public delegate void CompletedEventHandler(object sender, EventArgs e);

        private readonly List<string> webSites;
        private int CompletedCount;
        private List<DownloadThread> threads;

        public Downloader(List<string> _webSites)
        {
            webSites = _webSites;
            threads = new List<DownloadThread>();
        }

        public List<DownloadThread> Threads
        {
            get { return threads; }
            set { threads = value; }
        }

        public event CompletedEventHandler Completed;

        public void OnCompleted()
        {
            if (null != Completed)
            {
                Config.MainForm.WriteDebug("整个线程工作完成");
                Completed(this, new EventArgs()); //发出警报
            }
        }

        public void Start()
        {
            foreach (string website in webSites)
            {
                var thread = new DownloadThread(website);
                thread.DownloadThread_Completed += CheckCompleted;
                thread.Start();
                threads.Add(thread);
            }
        }

        public void Stop()
        {
            foreach (DownloadThread thread in Threads)
            {
                if (thread.IsAlive)
                {
                    Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.AbortingThread + thread.Name);
                    Config.ConsoleEx.Debug(Config.LocalLanguage.Messages.AbortingThread + thread.Name);
                    thread.Abort();
                }
            }
            Completed(Completed, new EventArgs()); //发出警告
        }

        private void CheckCompleted(object obj, EventArgs arg)
        {
            var thread = obj as DownloadThread;
            if (thread.Status == "Completed")
            {
                CompletedCount++;
                string countText = string.Format(Config.LocalLanguage.Messages.NumOfProxiesDownloadedByThreads,
                                                 (threads.Count - CompletedCount), ProxyData.TotalNum);
                Config.MainForm.SetStatusText(countText);
                Config.ConsoleEx.Debug(countText);
            }

            if (CompletedCount == threads.Count)
            {
                Completed(Completed, new EventArgs()); //发出警告
            }
        }
    }
}