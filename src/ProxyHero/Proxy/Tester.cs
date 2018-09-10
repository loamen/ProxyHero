using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Loamen.Net.Entity;
using Loamen.PluginFramework;

namespace ProxyHero.Proxy
{
    public class Tester
    {
        public delegate void TesterCompletedEventHandler(object sender, EventArgs e);

        private int _completedCount;

        private List<TestThread> _threads;

        public Tester(IEnumerable<ProxyServer> list)
        {
            if (null == TestingQueue)
                TestingQueue = new Queue<HttpProxy>();

            int index = 0;

            foreach (
                HttpProxy httpProxy in
                    list.Select(proxy => new HttpProxy {Ip = proxy.proxy, Port = proxy.port}))
            {
                httpProxy.Id = index;
                index++;
                TestingQueue.Enqueue(httpProxy);
            }
        }

        /// <summary>
        ///     验证代理列表
        /// </summary>
        public static Queue<HttpProxy> TestingQueue { get; set; }

        public List<TestThread> Threads
        {
            get { return _threads; }
            set { _threads = value; }
        }

        public Stopwatch Watch { get; set; }
        public event TesterCompletedEventHandler TesterCompleted;

        public void Start()
        {
            Watch = new Stopwatch();
            Watch.Start();
            int threadCount = Config.LocalSetting.TestThreadsCount;
            threadCount = TestingQueue.Count >= threadCount ? threadCount : TestingQueue.Count;
            Threads = new List<TestThread>();

            for (var i = 0; i < threadCount; i++)
            {
                var testThread = new TestThread();
                testThread.TestThreadCompleted += CheckCompleted;
                testThread.Start();
                Threads.Add(testThread);
            }
        }

        public void Stop()
        {
            if (null == Threads) return;
            Watch.Stop();
            foreach (var thread in Threads)
            {
                if (thread.IsAlive)
                {
                    var message = Config.LocalLanguage.Messages.AbortingThread + thread.Name + "...";
                    Config.MainForm.SetStatusText(message);
                    Config.ConsoleEx.Debug(message);
                    thread.Abort();
                }
                else
                {
                    thread.Status = "Completed";
                }
            }
            _completedCount = Threads.Count;
            TesterCompleted(TesterCompleted, new EventArgs()); //发出警告
        }

        private void CheckCompleted(object obj, EventArgs arg)
        {
            var thread = obj as TestThread;
            if (thread != null && thread.Status == "Completed")
            {
                _completedCount++;
            }

            if (_completedCount == Threads.Count)
            {
                Watch.Stop();
                TesterCompleted(TesterCompleted, new EventArgs()); //发出警告
            }
        }
    }
}