using Loamen.Net;
using Loamen.Net.Entity;
using ProxyHero.Common;
using ProxyHero.Model;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using ThreadState = System.Threading.ThreadState;

namespace ProxyHero.Proxy
{
    public class TestThread
    {
        #region Variable

        /// <summary>
        ///     测试线程委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void TestThreadCompletedEventHandler(object sender, EventArgs e);

        private readonly ManualResetEvent _mSuspendEvent = new ManualResetEvent(true);
        private readonly TestOption _testOption;
        private readonly Thread _thread;
        private readonly int _timeOut;
        private static TestProxyHelper _testProxyHelper;

        /// <summary>
        ///     测试线程事件
        /// </summary>
        public event TestThreadCompletedEventHandler TestThreadCompleted;

        #endregion

        #region property

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
                if (_thread == null) return ThreadState.Unstarted;
                return _thread.ThreadState;
            }
        }

        /// <summary>
        ///     当前线程执行状态
        /// </summary>
        public bool IsAlive
        {
            get
            {
                if (_thread == null) return false;
                return _thread.IsAlive;
            }
        }

        /// <summary>
        ///     验证完毕的代理数量
        /// </summary>
        public int TestedCount { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        #endregion

        /// <summary>
        ///     测试线程
        /// </summary>
        public TestThread()
        {
            _testOption = new TestOption();
            _timeOut = Config.LocalSetting.TestTimeOut;
            if (Config.LocalSetting.DefaultTestOption != null)
                _testOption = Config.LocalSetting.DefaultTestOption;

            _testProxyHelper = new TestProxyHelper(_testOption, _timeOut, Config.LocalSetting.UserAgent);

            _thread = new Thread(DoWork);
            Name = _thread.ManagedThreadId.ToString(CultureInfo.InvariantCulture);
            Status = _thread.ThreadState.ToString();
        }

        /// <summary>
        ///     线程开始
        /// </summary>
        public void Start()
        {
            _thread.Start(this);
        }

        /// <summary>
        ///     线程终止
        /// </summary>
        public void Abort()
        {
            Description = "";
            _thread.Abort();
            TestThreadOnCompleted();
        }

        /// <summary>
        ///     线程暂停
        /// </summary>
        public void Suspend()
        {
            _mSuspendEvent.Reset();
        }

        /// <summary>
        ///     线程恢复
        /// </summary>
        public void Resume()
        {
            _mSuspendEvent.Set();
        }

        /// <summary>
        ///     完成时发出警告
        /// </summary>
        public void TestThreadOnCompleted()
        {
            if (null != TestThreadCompleted)
            {
                TestThreadCompleted(this, new EventArgs()); //发出警报
            }
        }

        /// <summary>
        ///     kaishi
        /// </summary>
        /// <param name="data"></param>
        private static void DoWork(object data)
        {
            var testThread = (TestThread) data;

            try
            {
                while (Tester.TestingQueue.Count > 0)
                {
                    //testThread.m_suspendEvent.WaitOne(Timeout.Infinite);
                    Monitor.Enter(Tester.TestingQueue);
                    HttpProxy proxy = Tester.TestingQueue.Dequeue();
                    Monitor.Exit(Tester.TestingQueue);

                    testThread.Description = proxy.IpAndPort + " Testing...";
                    try
                    {
                        Testing(testThread, proxy);
                    }
                    catch (ThreadAbortException e)
                    {
                        Config.ConsoleEx.Debug(e);
                    }
                    catch (Exception ex)
                    {
                        Config.ConsoleEx.Debug(ex);
                    }
                    finally
                    {
                        testThread.TestedCount++;
                    }
                    testThread.Status = testThread._thread.ThreadState.ToString();
                }

                testThread.Status = "Completed";
            }
            catch (Exception ex)
            {
                // 线程被放弃
                testThread.Status = "Completed";
                Config.ConsoleEx.Debug(ex);
            }
            finally
            {
                testThread.TestThreadOnCompleted();
            }
        }

        /// <summary>
        ///     检测代理
        /// </summary>
        /// <param name="testThread"></param>
        /// <param name="proxy"></param>
        private static void Testing(TestThread testThread, HttpProxy proxy)
        {
            var sw = new Stopwatch();

            sw.Start();
            var result = _testProxyHelper.TestProxy(proxy);
            sw.Stop();

            #region

            var testResult = result.IsAlive;

            testThread.Description = proxy.IpAndPort + (testResult ? " Working " : " Timeout ") + sw.ElapsedMilliseconds +
                                     "ms";

            var model = ProxyData.GetCopy(proxy.Ip, proxy.Port);
            try
            {
                if (model != null)
                {
                    if (testResult)
                    {
                        model.response = int.Parse(sw.ElapsedMilliseconds.ToString("F0"));
                        model.testdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        model.description = string.Format("{0} {1}", model.testdate,
                                                          sw.ElapsedMilliseconds.ToString("F0") + "ms");
                        model.status = 1;
                    }
                    else
                    {
                        model.description = Config.LocalLanguage.Messages.ProxyIsDead +
                                            sw.ElapsedMilliseconds.ToString("F0") + "ms";
                        model.response = 99999;
                        model.status = 0;
                        model.testdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    ProxyData.Set(model);
                }
            }
            catch
            {
                ProxyData.Remove(model);
            }

            #endregion
        }

        ///// <summary>
        /////     为避免挤占CPU, 队列为空时睡觉.
        ///// </summary>
        //private static void SleepWhenQueueIsEmpty(TestThread tester)
        //{
        //    //tester.Status = CrawlerStatusType.Idle;
        //    //tester.Url = string.Empty;
        //    //tester.Flush();

        //    //Thread.Sleep(MemCache.ThreadSleepTimeWhenQueueIsEmptyMs);
        //}
    }
}