using System;
using System.Threading;

namespace ProxyHero.Threading
{
    /// <summary>
    ///     工作线程
    /// </summary>
    public class WorkThread
    {
        #region 变量

        /// <summary>
        ///     线程完成委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void WorkThread_CompletedEventHandler(object sender, EventArgs e);

        /// <summary>
        ///     线程工作委托
        /// </summary>
        /// <param name="WorkThread"></param>
        /// <param name="e"></param>
        public delegate void WorkThread_DoWorkEventHandler(object WorkThread, EventArgs e);

        private readonly Thread thread;

        /// <summary>
        ///     线程完成事件
        /// </summary>
        public event WorkThread_CompletedEventHandler WorkThread_Completed;

        /// <summary>
        ///     线程工作事件：工作内容
        /// </summary>
        public event WorkThread_DoWorkEventHandler WorkThread_DoWork;

        #endregion

        #region 属性

        /// <summary>
        ///     传递参数
        /// </summary>
        public object[] Parameters { get; set; }

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

        /// <summary>
        ///     线程描述
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region 构造函数

        public WorkThread(object[] parameters)
        {
            Parameters = parameters;
            thread = new Thread(DoWork);
            Name = thread.ManagedThreadId.ToString();
            Status = thread.ThreadState.ToString();
        }

        #endregion

        #region 方法

        /// <summary>
        ///     线程开始
        /// </summary>
        public void Start()
        {
            thread.Start(this);
        }

        /// <summary>
        ///     终止线程
        /// </summary>
        public void Abort()
        {
            Description = "Thread " + thread.ManagedThreadId + " is aborting";
            if (thread != null && thread.IsAlive)
                thread.Abort();
            OnWorkThreadCompleted();
        }

        /// <summary>
        ///     启动线程完成事件
        /// </summary>
        private void OnWorkThreadCompleted()
        {
            if (null != WorkThread_Completed)
            {
                WorkThread_Completed(this, new EventArgs()); //发出警报
            }
        }

        /// <summary>
        ///     启动线程工作事件
        /// </summary>
        /// <param name="data"></param>
        private void OnWorkThreadDoWork(object data)
        {
            if (null != WorkThread_DoWork)
            {
                WorkThread_DoWork(data, new EventArgs()); //发出警报
            }
        }

        /// <summary>
        ///     开始工作
        /// </summary>
        /// <param name="data"></param>
        private static void DoWork(object data)
        {
            var workThread = (WorkThread) data;

            try
            {
                workThread.OnWorkThreadDoWork(data);
                workThread.Status = workThread.thread.ThreadState.ToString();
                workThread.Status = "Completed";
            }
            catch (Exception ex)
            {
                // 线程被放弃
                workThread.Status = "Completed";
                Console.WriteLine(ex.Message);
                //WriteException(ex);
            }
            finally
            {
                workThread.OnWorkThreadCompleted();
            }
        }

        #endregion
    }
}