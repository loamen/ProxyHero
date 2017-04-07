using System;
using System.Collections.Generic;
using Loamen.PH.Plugin.DownloadProxy.Entity;
using Loamen.PluginFramework;

namespace Loamen.PH.Plugin.DownloadProxy
{
    public class Downloader
    {
        public delegate void CompletedEventHandler(object sender, EventArgs e);

        private readonly IApp app;
        private readonly List<Website> websites;
        private int CompletedCount;
        private List<DownloadThread> threads;

        public Downloader(List<Website> websites, IApp app)
        {
            this.app = app;
            threads = new List<DownloadThread>();
            this.websites = websites;
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
                app.WriteDebug("整个线程工作完成");
                Completed(this, new EventArgs()); //发出警报
            }
        }

        public void Start()
        {
            foreach (Website website in websites)
            {
                var thread = new DownloadThread(website.Url, app);
                thread.Completed += CheckCompleted;
                thread.Start();
                threads.Add(thread);
            }
        }

        public void Stop()
        {
            foreach (DownloadThread thread in Threads)
            {
                thread.Abort();
            }
        }

        private void CheckCompleted(object obj, EventArgs arg)
        {
            var thread = obj as DownloadThread;
            if (thread.Status == "Completed")
            {
                CompletedCount++;
            }

            if (CompletedCount == threads.Count)
            {
                Completed(Completed, new EventArgs()); //发出警告
            }
        }
    }
}