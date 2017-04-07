using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Loamen.PluginFramework;
using WeifenLuo.WinFormsUI.Docking;
using ThreadState = System.Threading.ThreadState;

namespace Loamen.PH.Plugin.Anonymity
{
    /// <summary>
    ///     实现宿主接口
    /// </summary>
    public class Anonymity : IPlugin
    {
        #region 变量

        public AnonymityForm aForm = null;
        private IApp app;
        private string author = "龙门信息网";
        private string description = "龙门代理公布器使用代理匿名度插件！";
        private ToolStripMenuItem dsmi;
        private string lPHVersion = "1.6.0+";

        private string name = "验证匿名度插件";
        private string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        #endregion

        #region 继承属性 此处不用更改

        public IApp App
        {
            get { return app; }
            set
            {
                app = value;
                InitApp(); //此处不要更改
            }
        }

        /// <summary>
        ///     插件名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        ///     作者名称
        /// </summary>
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        /// <summary>
        ///     插件版本
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        ///     适用于代理公布器的代理
        /// </summary>
        public string LPHVersion
        {
            get { return lPHVersion; }
            set { lPHVersion = value; }
        }

        /// <summary>
        ///     插件描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        ///     判断主窗体上是否有本插件
        /// </summary>
        public bool Exist
        {
            get { return null != app.FindDockContentByClassName("AnonymityForm"); }
        }

        /// <summary>
        ///     初始化宿主
        /// </summary>
        public void InitApp()
        {
            #region

            // 定义一个下拉菜单
            dsmi = App.AddMenuItem("AnonymityPluginMenu", "验证匿名度");
            dsmi.Click += item_Click; //为下拉单添加事件

            //定义一个工具按钮
            ToolStripButton tsb = App.AddToolButton("AnonymityPlugin", "验证匿名度");
            tsb.Click += item_Click; //为工具栏按钮添加事件

            #endregion
        }

        /// <summary>
        ///     显示插件
        /// </summary>
        public void ShowPlugin()
        {
            try
            {
                if (null == aForm)
                {
                    aForm = new AnonymityForm(app, this); //初始化插件窗体
                }

                aForm.Show(app.DockPanel, DockState.Document); //显示在停靠栏中间
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex); //记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        /// <summary>
        ///     隐藏插件
        /// </summary>
        public void HidePlugin()
        {
            try
            {
                if (null != aForm)
                {
                    if (Exist)
                        aForm.Hide();
                    else
                        aForm = null;
                }
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex); //记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            try
            {
                dsmi.Checked = !dsmi.Checked;
                if (dsmi.Checked)
                    ShowPlugin();
                else
                    HidePlugin();
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex); //记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        #endregion
    }


    /// <summary>
    ///     验证代理页面
    /// </summary>
    public sealed class AnonymityForm : DockPage
    {
        private readonly Anonymity _anonymity;
        private readonly Button _btnStart;
        private readonly Button _btnStop;
        private readonly RichTextBox _rbResult;
        private readonly TextBox _txtProxyJudge;
        private IApp _app;
        private Checker _checker;

        public AnonymityForm(IApp app, Anonymity anonymity)
        {
            _anonymity = anonymity;
            App = app;
            Name = "AnonymityForm";
            Text = "验证匿名度";

            var gbOptionUp =
                (GroupBox)
                GUICreateControl("gbOptionUp", "配置", typeof (GroupBox), this, new Point(10, 10), new Size(0, 50), null);
                //创建一个GroupBox
            gbOptionUp.Anchor = (AnchorStyles.Top | (AnchorStyles.Left | AnchorStyles.Right)); //靠左、靠上、靠右

            var lbProxyJudge =
                (Label)
                GUICreateControl("lbProxyJudge", "ProxyJudge网址：", typeof (Label), gbOptionUp, new Point(13, 18),
                                 new Size(110, 0), null); //创建一个标签

            _txtProxyJudge =
                (TextBox)
                GUICreateControl("txtProxyJudge", "http://kojiki.server.ne.jp/etc/pj235.cgi?en", typeof (TextBox),
                                 gbOptionUp, new Point(13 + lbProxyJudge.Width, 15), new Size(280, 0), null); //创建一个文本框

            _btnStart =
                (Button)
                GUICreateControl("btnStart", "验证匿名度", typeof (Button), gbOptionUp,
                                 new Point(15 + lbProxyJudge.Width + _txtProxyJudge.Width, 15), null, null); //创建一个按钮
            _btnStop =
                (Button)
                GUICreateControl("btnStop", "停止验证", typeof (Button), gbOptionUp,
                                 new Point(15 + lbProxyJudge.Width + _txtProxyJudge.Width + _btnStart.Width, 15), null,
                                 null); //创建一个按钮

            GUISetOnEvent(_btnStart, "Click", "btn_Click"); //给按钮注册事件
            GUISetOnEvent(_btnStop, "Click", "btnStop_Click"); //给按钮注册事件

            var gbOptionDown =
                (GroupBox)
                GUICreateControl("gbOptionDown", "输出", typeof (GroupBox), this, new Point(10, 70), null, null);
                //创建一个GroupBox
            gbOptionDown.Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom)
                                      | AnchorStyles.Left)
                                     | AnchorStyles.Right))); //靠左、靠上、靠右、靠下

            _rbResult =
                (RichTextBox)
                GUICreateControl("rbResult", "", typeof (RichTextBox), gbOptionDown, new Point(10, 10), null,
                                 DockStyle.Fill); //创建一个RichTextBox
        }

        [Localizable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public IApp App
        {
            get { return _app; }
            set { _app = value; }
        }

        public void Refresh(string text)
        {
            DelegateUpdate d = UpdateStatus;
            Invoke(d, new object[] {text});
        }

        private void UpdateStatus(string text)
        {
            _rbResult.AppendText(text + "\n");
            if (_rbResult.Lines.Length > 15)
            {
                //this.rbResult.Clear();
            }
        }

        public void btn_Click(object sender, EventArgs e)
        {
            _btnStart.Enabled = false;
            App.EnabledProxyPageUI(false);
            _checker = new Checker(_app.ProxyList, _app, _txtProxyJudge.Text.Trim());
            _checker.CheckerCompleted += CheckCompelted;
            App.SetStatusText("正在验证匿名度，请稍后...");
            _checker.Start();
        }

        public void btnStop_Click(object sender, EventArgs e)
        {
            if (null != _checker)
            {
                var stopThread = new Thread(_checker.Stop);
                stopThread.Start();
            }
            _btnStart.Enabled = true;
        }

        private void CheckCompelted(object sender, EventArgs args)
        {
            var checker = (Checker) sender;
            if (checker.ThreadCompletedCount == checker.Threads.Count &&
                checker.TotalCheckedCount == App.ProxyList.Count)
            {
                _btnStart.Enabled = true;
                Stopwatch sw = checker.Watch;
                var sb = new StringBuilder();
                if (sw.Elapsed.Days > 0)
                    sb.Append(sw.Elapsed.Days + "天");
                if (sw.Elapsed.Hours > 0)
                    sb.Append(sw.Elapsed.Hours + "小时");
                if (sw.Elapsed.Minutes > 0)
                    sb.Append(sw.Elapsed.Minutes + "分");
                sb.Append(sw.Elapsed.Seconds + "秒");
                sb.Append(sw.Elapsed.Milliseconds + "毫秒");
                App.SetStatusText("");
                Refresh("全部操作完成！共" + checker.TotalCheckedCount + "/" + App.ProxyList.Count + "耗时：" + sb);
                App.EnabledProxyPageUI(true);
                App.RefreshCloud();
            }
        }

        private delegate void DelegateUpdate(string text);
    }


    public class Checker
    {
        public delegate void CheckerCompletedEventHandler(object sender, EventArgs e);

        private readonly IList<ProxyServer> _list;
        public string CheckUrl = "http://kojiki.server.ne.jp/etc/pj235.cgi?en";
        private List<CheckThread> _threads;

        public Checker(IList<ProxyServer> list, IApp app, string checkUrl)
        {
            _list = list;
            CheckUrl = checkUrl;
            App = app;
            if (null == CheckingQueue)
                CheckingQueue = new Queue<ProxyServer>();
            foreach (ProxyServer model in list)
            {
                CheckingQueue.Enqueue(model);
            }
        }

        /// <summary>
        ///     线程完成数量
        /// </summary>
        public int ThreadCompletedCount
        {
            get { return Threads.Count(p => p.Status == "Completed"); }
        }

        public IApp App { get; set; }

        /// <summary>
        ///     总完成验证的数量
        /// </summary>
        public int TotalCheckedCount
        {
            get { return _list.Count - CheckingQueue.Count; }
        }

        /// <summary>
        ///     测试代理列表
        /// </summary>
        public static Queue<ProxyServer> CheckingQueue { get; set; }

        public List<CheckThread> Threads
        {
            get { return _threads; }
            set { _threads = value; }
        }

        public Stopwatch Watch { get; set; }
        public event CheckerCompletedEventHandler CheckerCompleted;

        public void Start()
        {
            Watch = new Stopwatch();
            Watch.Start();
            int threadCount = 50;
            threadCount = CheckingQueue.Count >= threadCount ? threadCount : CheckingQueue.Count;
            Threads = new List<CheckThread>();
            var fm = (AnonymityForm) Application.OpenForms["AnonymityForm"];
            if (null != fm)
            {
                fm.Refresh("共开启" + threadCount + "条线程进行匿名度验证" + CheckingQueue.Count + "条代理！");
            }
            for (int i = 0; i < threadCount; i++)
            {
                var testThread = new CheckThread(this);
                testThread.Completed += CheckCompleted;
                testThread.Start();
                Threads.Add(testThread);
            }
        }

        public void Stop()
        {
            if (null != Threads)
            {
                Watch.Stop();
                foreach (CheckThread thread in Threads)
                {
                    if (thread.IsAlive)
                    {
                        App.SetStatusText("正在终止线程" + thread.Name + "...");
                        thread.Abort();
                    }
                }
            }
        }

        private void CheckCompleted(object obj, EventArgs arg)
        {
            if (ThreadCompletedCount == _threads.Count)
            {
                Watch.Stop();
                CheckerCompleted(this, new EventArgs()); //发出警告
            }
        }
    }


    public class CheckThread
    {
        #region Variable

        public delegate void CompletedEventHandler(object sender, EventArgs e);

        private readonly Checker _checker;
        private readonly ManualResetEvent _mSuspendEvent = new ManualResetEvent(true);
        private readonly Thread _thread;
        public event CompletedEventHandler Completed;

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

        public string Description { get; set; }

        #endregion

        public CheckThread(Checker checker)
        {
            _checker = checker;
            _thread = new Thread(DoWork);
            Name = _thread.ManagedThreadId.ToString(CultureInfo.InvariantCulture);
            Status = _thread.ThreadState.ToString();
        }

        public void Start()
        {
            _thread.Start(this);
        }

        public void Abort()
        {
            Description = "";
            _thread.Abort();
            OnCompleted();
        }

        public void Suspend()
        {
            _mSuspendEvent.Reset();
        }

        public void Resume()
        {
            _mSuspendEvent.Set();
        }

        public void OnCompleted()
        {
            if (null != Completed)
            {
                Completed(this, new EventArgs()); //发出警报
            }
        }

        private static void DoWork(object data)
        {
            var checkThread = (CheckThread) data;
            var fm = (AnonymityForm) Application.OpenForms["AnonymityForm"];
            ProxyServer proxy = null;
            string level = "未知";
            try
            {
                while (Checker.CheckingQueue.Count > 0)
                {
                    try
                    {
                        #region 验证匿名度

                        Monitor.Enter(Checker.CheckingQueue);
                        proxy = Checker.CheckingQueue.Dequeue();
                        Monitor.Exit(Checker.CheckingQueue);
                        string html = checkThread._checker.App.GetHtml(checkThread._checker.CheckUrl,
                                                                       proxy.proxy + ":" + proxy.port);
                        if (html.Contains("ProxyJudge"))
                        {
                            level = checkThread._checker.App.GetMidString(html, "AnonyLevel :", "<BR>");
                            level = checkThread._checker.App.GetMidString(level, "<FONT color=\"yellow\">", "</FONT>");
                            switch (level)
                            {
                                case "1":
                                    level = "高匿名代理";
                                    break;
                                case "2":
                                    level = "匿名代理";
                                    break;
                                case "3":
                                    level = "普通匿名代理";
                                    break;
                                case "3?":
                                    level = "透明代理";
                                    break;
                                case "4?":
                                    level = "透明代理";
                                    break;
                                case "5?":
                                    level = "透明代理";
                                    break;
                                case "4":
                                    level = "透明代理";
                                    break;
                                case "5":
                                    level = "透明代理";
                                    break;
                            }
                        }
                        checkThread.Status = "Completed";

                        #endregion
                    }
                    catch
                    {
                        // 线程被放弃
                        checkThread.Status = "Completed";
                        level = "未知";
                    }
                    finally
                    {
                        if (null != proxy)
                        {
                            List<ProxyServer> drs = (from p in checkThread._checker.App.ProxyList
                                                     where p.proxy == proxy.proxy &&
                                                           p.port == proxy.port
                                                     select p).ToList<ProxyServer>();
                            foreach (ProxyServer dr in drs)
                            {
                                dr.anonymity = level;
                            }
                        }

                        if (null != fm)
                        {
                            var sb = new StringBuilder();
                            if (proxy != null) sb.Append(proxy.proxy);
                            sb.Append(":");
                            if (proxy != null) sb.Append(proxy.port);
                            sb.Append(",");
                            sb.Append(level);
                            fm.Refresh(sb.ToString());

                            sb = new StringBuilder();
                            sb.Append(checkThread._checker.TotalCheckedCount);
                            sb.Append("/");
                            sb.Append(checkThread._checker.App.ProxyList.Count);
                            string info = string.Format("正在验证匿名度{0}，请稍后...", sb);
                            checkThread._checker.App.SetStatusText(info);
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                checkThread.OnCompleted();
            }
        }
    }
}