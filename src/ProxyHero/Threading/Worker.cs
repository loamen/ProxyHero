using System;
using System.Collections.Generic;

namespace ProxyHero.Threading
{
    /// <summary>
    ///     工作类
    /// </summary>
    public class Worker
    {
        #region 变量及属性

        private object[] _parameters;

        /// <summary>
        ///     线程列表
        /// </summary>
        public List<WorkThread> Threads { get; set; }

        /// <summary>
        ///     工作线程完成数量
        /// </summary>
        public int WorkThreadCompletedCount { get; set; }

        /// <summary>
        ///     传递的参数
        /// </summary>
        public object[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        /// <summary>
        ///     返回值
        /// </summary>
        public object ReturnValue { get; set; }

        #region 完成事件

        /// <summary>
        ///     工作完成委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void Worker_CompletedEventHandler(object sender, EventArgs e);

        /// <summary>
        ///     工作完成事件
        /// </summary>
        public event Worker_CompletedEventHandler Worker_Completed;

        #endregion

        #region 起始事件

        /// <summary>
        ///     工作启动委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="parameters"></param>
        public delegate void Worker_StartEventHandler(object sender, EventArgs e, object[] parameters);

        /// <summary>
        ///     工作启动事件
        /// </summary>
        public event Worker_StartEventHandler Worker_Started;

        #endregion

        #endregion

        #region 启动事件

        /// <summary>
        ///     启动事件
        /// </summary>
        private void OnStarted()
        {
            if (null != Worker_Started)
            {
                Worker_Started(this, new EventArgs(), _parameters); //发出警报
            }
        }

        /// <summary>
        ///     完成事件
        /// </summary>
        public void OnWorkerCompleted()
        {
            if (null != Worker_Completed)
            {
                Worker_Completed(this, new EventArgs()); //发出警报
            }
        }

        #endregion

        #region 构造

        public Worker(object[] parameters)
        {
            _parameters = parameters;
            Threads = new List<WorkThread>();
        }

        #endregion

        #region 方法

        /// <summary>
        ///     启动工作
        /// </summary>
        public void Start()
        {
            OnStarted();
        }

        /// <summary>
        ///     结束工作
        /// </summary>
        public void Stop()
        {
            foreach (WorkThread thread in Threads)
            {
                if (null != thread && thread.IsAlive)
                {
                    thread.Abort();
                }
            }
        }

        #endregion
    }
}