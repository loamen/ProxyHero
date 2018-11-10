using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using cn.bmob.io;
using Loamen.Common;
using Loamen.Net;
using Loamen.Net.Entity;
using Loamen.PluginFramework;
using Loamen.WinControls.UI;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.LanguageInformation;
using ProxyHero.Model;
using ProxyHero.Net;
using ProxyHero.Option;
using ProxyHero.Plugin;
using ProxyHero.Properties;
using ProxyHero.TabPages;
using WeifenLuo.WinFormsUI.Docking;
using Timer = System.Timers.Timer;

namespace ProxyHero
{
    public partial class MainForm : Form
    {
        #region Variable
        private readonly HttpHelper _httpHelper = new HttpHelper();
        private readonly ServiceContainer _serviceContainer = new ServiceContainer();
        private bool _hasDockSettingExceptioin;
        private bool _hasViewSettingException;
        public string ClosingControl = "";
        private DeserializeDockContent _deserializeDockContent;
        private int _intIcon;
        private LanguageLoader _languageLoader;
        public Timer TimerAutoChangeIcon;
        public Timer TimerAutoSwitchingProxy;
        //public SnsHelper snsHelper = new SnsHelper();

        private delegate void DelegateSet(string text);

        private delegate void DelegateVoid();

        #endregion

        #region Property

        #region

        private OutputForm _outputPage;
        private ProxyForm _proxyPage;
        private StartForm _startPage;

        /// <summary>
        ///     起始页
        /// </summary>
        private StartForm StartPage
        {
            get { return _startPage ?? (_startPage = new StartForm()); }
        }

        /// <summary>
        ///     信息页
        /// </summary>
        public OutputForm OutputPage
        {
            get { return _outputPage ?? (_outputPage = new OutputForm()); }
            set { _outputPage = value; }
        }

        /// <summary>
        ///     代理页
        /// </summary>
        public ProxyForm ProxyPage
        {
            get { return _proxyPage ?? (_proxyPage = new ProxyForm()); }
            set { _proxyPage = value; }
        }

        /// <summary>
        ///     自动切换状态Label
        /// </summary>
        public ToolStripStatusLabel AutoSwitchingStatus
        {
            get { return AutoSwitchProxyStatus; }
        }

        /// <summary>
        ///     云端代理列表
        /// </summary>
        public IList<ProxyServer> CloudProxyList
        {
            get { return CloudProxyData.CloudProxyList; }
            set { CloudProxyData.CloudProxyList = value; }
        }

        #endregion

        #endregion

        #region init

        public MainForm()
        {
            try
            {
                Hide();
                SplashScreen.BackgroundImage = Resources.splash;
                SplashScreen.FontForeColor = Color.White;
                var splashthread = new Thread(SplashScreen.ShowSplashScreen) {IsBackground = true};
                splashthread.Start();

                Config.LanguageFileName = Config.LocalSetting.LanguageFileName;
                if (System.IO.File.Exists(Config.LanguageFileName))
                {
                    var language = XmlHelper.XmlDeserialize(
                        Config.LanguageFileName,
                        typeof(Language)) as Language;
                    Config.LocalLanguage = language;
                }

                SplashScreen.UpdateStatusText(Config.LocalLanguage.Messages.InitializeComponent);
                InitializeComponent();
            }
            catch (Exception ex)
            {
                SplashScreen.CloseSplashScreen();
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                #region 测试版

//#if DEBUG
//                try
//                {
//                    var apiHelper = new ApiHelper();
//                    DateTime now = apiHelper.GetDate(DateType.SysDate);
//                    if (now > AboutBox.PublishDate.AddMonths(1)) //发布一个月失效
//                    {
//                        SplashScreen.CloseSplashScreen();
//                        MsgBox.ShowErrorMessage("该测试版已失效，请下在最新版！");
//                        OpenIE(Config.ProxyHeroCloudSetting.UpdateUrl);
//                        Exit_Click(Exit, new EventArgs());
//                    }
//                }
//                catch (WebException)
//                {
//                    Config.InitErrorInfo = Config.LocalLanguage.Messages.InitializeFailed + "," +
//                                           Config.LocalLanguage.Messages.PleaseCheckNetworkSettingsAreCorrect;
//                }
//#endif

                #endregion

                #region

                _languageLoader = new LanguageLoader();
                TimerAutoSwitchingProxy = new Timer();
                TimerAutoChangeIcon = new Timer();

                //SplashScreen.UpdateStatusText(Config.LocalLanguage.Messages.InitializeDatabase);

                _deserializeDockContent = GetContentFromPersistString;

                SplashScreen.UpdateStatusText(Config.LocalLanguage.Messages.LoadingLanguages);
                LoadLanguage();
                
                

                #endregion

                #region 初始化配置

                GetNetConfigAndCheckVersion();

                #endregion

                #region 连接云引擎

                try
                {
                    DelegateVoid dv = ConnectCloud;
                    var thred = new Thread(new ThreadStart(dv));
                    thred.Start();
                }
                catch
                {
                    SetCloudStatus(Config.LocalLanguage.Messages.ConnectCloudEngineFailed, Resources.cloudno);
                }

                #endregion

                #region Hotkey

                //Hotkey hotkey = new Hotkey(this.Handle);
                //Hotkey1 = hotkey.RegisterHotkey(System.Windows.Forms.Keys.T, Hotkey.KeyFlags.MOD_CONTROL);
                //hotkey.OnHotkey += new HotkeyEventHandler(OnHotkey);

                #endregion

                #region UI

                MainToolbar.Visible = false;
                MainStatusBar.Items.Insert(2, new ToolStripSeparator());
                MainStatusBar.Items.Insert(4, new ToolStripSeparator());
                MainStatusBar.Items.Insert(6, new ToolStripSeparator());
                MainStatusBar.Items.Insert(8, new ToolStripSeparator());
                MainStatusBar.Items.Insert(10, new ToolStripSeparator());
                tsslVersion.Text = @"Version:" + Assembly.GetExecutingAssembly().GetName().Version;
                if (Config.LocalLanguage != null)
                    CloudStatus.Text = Config.LocalLanguage.Messages.ConnectingCloudEngine;

                _httpHelper.HttpOption.Timeout = 60*1000;

                SetProxyStatusLabel();
                if (Config.LocalLanguage != null)
                    AutoSwitchProxyStatus.Text = Config.LocalLanguage.Messages.AutomaticSwitchingOff;
                StatusLabel.Spring = true;
                SetStatusText(Config.InitErrorInfo);

                #endregion

                #region timer

                TimerAutoSwitchingProxy.Enabled = false;
                TimerAutoSwitchingProxy.Interval = 1000;
                TimerAutoSwitchingProxy.Elapsed += timerAutoSwitchingProxy_Elapsed;

                TimerAutoChangeIcon.Enabled = false;
                TimerAutoChangeIcon.Interval = 1000;
                TimerAutoChangeIcon.Elapsed += timerAutoChangeIcon_Elapsed;

                #endregion

                #region DockPanel

                if (Config.LocalLanguage != null)
                {
                    SplashScreen.UpdateStatusText(Config.LocalLanguage.Messages.InitializeDockPanel);
                    if (System.IO.File.Exists(Config.DockSettingFileName))
                    {
                        try
                        {
                            MainDockPanel.LoadFromXml(Config.DockSettingFileName, _deserializeDockContent);
                        }
                        catch
                        {
                            _hasDockSettingExceptioin = true;
                            if (System.IO.File.Exists(Config.DockSettingFileName))
                            {
                                SplashScreen.CloseSplashScreen();
                                System.IO.File.Delete(Config.DockSettingFileName);
                                MsgBox.ShowErrorMessage(Config.LocalLanguage.Messages.InitializeFailed);
                                Application.Exit();
                            }
                        }
                    }
                    else
                    {
                        #region dock

                        StartPage.Show(MainDockPanel, DockState.Document);
                        OutputPage.Show(MainDockPanel, DockState.DockBottomAutoHide);
                        OutputPage.Hide();
                        ProxyPage.Show(MainDockPanel, DockState.Document);

                        #endregion
                    }
                }

                #endregion

                #region

                LoadViewSetting();

                if (Config.LocalLanguage != null)
                    SplashScreen.UpdateStatusText(Config.LocalLanguage.Messages.CheckUpdate);
                CheckVersionAndDownLoad();

                #region 读取上次代理

                if (System.IO.File.Exists(Config.LastProxyFileName))
                {
                    ProxyData.ProxyList =
                        (List<ProxyServer>)
                        XmlHelper.XmlDeserialize(Config.LastProxyFileName, typeof (List<ProxyServer>));
                    ProxyPage.BindData();
                }

                #endregion

                if (Config.LocalLanguage != null)
                    SplashScreen.UpdateStatusText(Config.LocalLanguage.Messages.LoadingPlugins);
                PluginManager.LoadAllPlugins();
                //如果没有获取代理网页列表，则禁止使用
#if !DEBUG
                if (Config.ProxySiteUrlList.Count == 0)
                    this.ProxyPage.Enabled = false;
#endif

                SplashScreen.CloseSplashScreen();
                StartPage.Activate();
                Activate();
                if (Config.ProxyHeroCloudSetting.EnableCommercialPage == "1") //如果显示弹出广告
                {
#if !DEBUG
                    if (Config.IsChineseOs)
                        this.OpenNewTab(Config.ProxyHeroCloudSetting.CommercialUrl);
                    else
                        this.OpenNewTab(Config.ProxyHeroCloudSetting.EnglishCommercialUrl);
#else
                    OpenNewTab(Config.IsChineseLanguage
                                   ? Config.ProxyHeroCloudSetting.CommercialUrl
                                   : Config.ProxyHeroCloudSetting.EnglishCommercialUrl);
#endif
                }

                #region 用户登录
                //if (BmobUser.CurrentUser == null)
                //{
                //    var userForm = new BmobUserForm();
                //    userForm.ShowDialog();
                //}
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                SplashScreen.CloseSplashScreen();
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        #region 内存回收到虚拟内存

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet =
            CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize,
                                                           int maximumWorkingSetSize);

        public void DisposeGc()
        {
            GC.Collect();
            GC.SuppressFinalize(this);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        #endregion

        #endregion

        #region method

        /// <summary>
        ///     取得当前的浏览器窗口
        /// </summary>
        public IEBrowserForm CurrentBrowserForm
        {
            get
            {
                IEBrowserForm ieForm = null;
                try
                {
                    if (MainDockPanel.DocumentStyle != DocumentStyle.SystemMdi)
                    {
                        if (MainDockPanel.ActiveDocument != null &&
                            MainDockPanel.ActiveDocument.GetType().Name == "IEBrowserForm")
                            ieForm = (IEBrowserForm) MainDockPanel.ActiveDocument;
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.ShowExceptionMessage(ex);
                }

                return ieForm;
            }
        }

        /// <summary>
        ///     用IE打开
        /// </summary>
        /// <param name="url"></param>
        public void OpenIE(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                try
                {
                    Process.Start("iexplore.exe", url);
                }
                catch
                {
                    try
                    {
                        Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe", url);
                    }
                    catch
                    {
                        try
                        {
                            OpenNewTab(url);
                        }
                        catch (Exception ex)
                        {
                            MsgBox.ShowExceptionMessage(ex);
                        }
                    }
                }
            }
        }


        /// <summary>
        ///     超找内容页
        /// </summary>
        /// <returns></returns>
        public IDockContent FindDockContentByTabText(string tabText)
        {
            if (tabText == null) throw new ArgumentNullException("tabText");
            return MainDockPanel.DocumentStyle == DocumentStyle.SystemMdi ? (from form in MdiChildren where form.Text == tabText select form as IDockContent).FirstOrDefault() : MainDockPanel.Documents.FirstOrDefault(content => content.DockHandler.TabText == tabText);
        }

        /// <summary>
        ///     查找停靠窗体
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public IDockContent FindDockContentByClassName(string className)
        {
            return MainDockPanel.DocumentStyle == DocumentStyle.SystemMdi ? (from form in MdiChildren where form.GetType().Name == className select form as IDockContent).FirstOrDefault() : MainDockPanel.Documents.FirstOrDefault(content => content.GetType().Name == className);
        }

        /// <summary>
        ///     建立新标签
        /// </summary>
        /// <param name="url"></param>
        public void OpenNewTab(string url)
        {
            var ieForm = CreateNewIEBrowser(url);
            if (MainDockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                ieForm.MdiParent = this;
                ieForm.Show();
            }
            else
                ieForm.Show(MainDockPanel);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof (StartForm).ToString())
                return StartPage;
            if (persistString == typeof (OutputForm).ToString())
                return OutputPage;
            return persistString == typeof (ProxyForm).ToString() ? ProxyPage : null;
        }

        /// <summary>
        ///     加载窗体语言
        /// </summary>
        public void LoadLanguage()
        {
            Language language = Config.LocalLanguage;

            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.MainPage;
                _languageLoader.Load(model, typeof (MainForm), this);
            }

            Text = Config.IsChineseLanguage ? "龙门代理公布器" : "Loamen Proxy Hero";
            Text = Text + @" " + Application.ProductVersion;

//#if DEBUG
//            Text += @" RC版" + @"(到期时间：" + AboutBox.PublishDate.AddMonths(1).ToShortDateString() + @")";
//#endif
            notifyIconMain.Text = Text;
        }

        /// <summary>
        ///     建立新标签
        /// </summary>
        /// <param name="url"></param>
        private IEBrowserForm CreateNewIEBrowser(string url)
        {
            var ieForm = new IEBrowserForm(url);

            ieForm.Browser.ProxyServer = Config.BuiltinBrowserProxyServer;

            return ieForm;
        }

        /// <summary>
        ///     建立新标签
        /// </summary>
        public void OpenNewTab()
        {
            OpenNewTab("");
        }

        /// <summary>
        ///     连接云引擎,下载数据
        /// </summary>
        public void ConnectCloud()
        {
            TimeSpan ts = DateTime.Now - Config.LateUpdateProxyListTime;
            if (ts.TotalSeconds > 30)
            {
                SetCloudStatus(Config.LocalLanguage.Messages.ConnectingCloudEngine + "...", Resources.loading);

                var cloudHelper = new CloudHelper();
                bool isConnected = cloudHelper.DownloadProxyList();
                if (isConnected)
                {
                    SetCloudStatus(Config.LocalLanguage.Messages.ConnectCloudEngineSuccess, Resources.cloud);
                }
                else
                {
                    SetCloudStatus(Config.LocalLanguage.Messages.ConnectCloudEngineFailed, Resources.cloudno);
                }
            }
        }

        /// <summary>
        ///     获取网络配置并检查更新
        /// </summary>
        private void GetNetConfigAndCheckVersion()
        {
            try
            {
                CloudHelper.GetNetConfig(); //获取网络配置

                #region CheckVersion

                var updateHelper = new UpdateHelper();
                Config.NeedUpdate = updateHelper.CheckVersion();

                #endregion
            }
            catch (WebException)
            {
                Config.InitErrorInfo = Config.LocalLanguage.Messages.InitializeFailed + "," +
                                       Config.LocalLanguage.Messages.PleaseCheckNetworkSettingsAreCorrect;
                SplashScreen.UpdateStatusText(Config.InitErrorInfo);
            }
            catch (Exception ex)
            {
                SplashScreen.UpdateStatusText(ex.Message);
                Config.InitErrorInfo = ex.Message;
            }
        }

        /// <summary>
        ///     设置当前代理状态
        /// </summary>
        public void SetProxyStatusLabel()
        {
            string[] res = ProxyHelper.GetIEProxy();
            if (res[0] == "1" && res[1] != "")
            {
                string location = "";
                try
                {
                    var ih = new IpHelper(res[1], Config.IsChineseLanguage);
                    location = " " + ih.IpAddress;
                }
                catch (Exception ex)
                {
                    Config.ConsoleEx.Debug(ex);
                }
                ProxyStatus.Text = Config.LocalLanguage.Messages.CurrentProxy + @":" + res[1] + location;
                ProxyStatus.Image = Resources.aused;
                SetToolTipText(ProxyStatus.Text);
            }
            else if (res[0] == "0")
            {
                ProxyStatus.Text = Config.LocalLanguage.Messages.NotUseProxy;
                ProxyStatus.Image = Resources.aunuse;
            }
        }

        private void Donate_Click(object sender, EventArgs e)
        {
            OpenIE("https://github.com/loamen/ProxyHero/blob/master/README.zh-CN.md#捐赠");
        }

        /// <summary>
        ///     设置自动切换标签
        /// </summary>
        /// <param name="enable"></param>
        public void SetAutoSwitchingStatus(bool enable)
        {
            AutomaticSwitching.Checked = enable;
            if (enable)
            {
                AutoSwitchProxyStatus.Text = Config.LocalLanguage.Messages.AutomaticSwitchingOn;
                AutoSwitchProxyStatus.Image = Resources.astart;
            }
            else
            {
                AutoSwitchProxyStatus.Text = Config.LocalLanguage.Messages.AutomaticSwitchingOff;
                AutoSwitchProxyStatus.Image = Resources.astop;
                tsslCountdown.Text = DateTime.Now.ToShortDateString();
            }
            AutoSwitchProxyStatus.ToolTipText = AutoSwitchProxyStatus.Text;
            if (Config.ProxyApplicatioin == "IE")
            {
                SetToolTipText("IE" + AutoSwitchProxyStatus.Text);
            }
            else
            {
                SetBuiltinBrowserProxy("");
                SetToolTipText(Config.LocalLanguage.Messages.BuiltinBrowser + AutoSwitchProxyStatus.Text);
            }
        }


        public void SetAutoSwitch()
        {
            if (ProxyData.AliveProxyList.Count > 0)
            {
                if (AutoSwitchProxyStatus.Text == Config.LocalLanguage.Messages.AutomaticSwitchingOff)
                {
                    Config.ProxyApplicatioin = DialogResult.Yes ==
                                               MsgBox.ShowQuestionMessage(Config.LocalLanguage.Messages.SwitchToIEOrBuiltinBrowser) ? "IE" : "LB";
                    StartAutoSwitch();
                    TimerAutoChangeIcon.Start();
                }
                else
                {
                    StopAutoSwitch();
                    TimerAutoChangeIcon.Stop();
                    notifyIconMain.Icon = Resources.Icon;
                }
            }
            else
            {
                MsgBox.ShowMessage(Config.LocalLanguage.Messages.AutoSwitchingProxyListIsEmpty);
            }
        }

        /// <summary>
        ///     启动自动代理
        /// </summary>
        public void StartAutoSwitch()
        {
            AutoSwitching.Image = Resources.timer2;
            AutoSwitching.Text = Config.LocalLanguage.Messages.AutomaticSwitchingOn;
            Config.TsCountDown = new TimeSpan(0, 0, Config.LocalSetting.AutoChangeProxyInterval);
            Config.MainForm.TimerAutoSwitchingProxy.Start();
            Config.MainForm.SetAutoSwitchingStatus(Config.MainForm.TimerAutoSwitchingProxy.Enabled);
        }

        /// <summary>
        ///     关闭自动代理
        /// </summary>
        public void StopAutoSwitch()
        {
            AutoSwitching.Image = Resources.timer1;
            AutoSwitching.Text = Config.LocalLanguage.Messages.AutomaticSwitchingOff;
            Config.MainForm.TimerAutoSwitchingProxy.Stop();
            Config.MainForm.SetAutoSwitchingStatus(Config.MainForm.TimerAutoSwitchingProxy.Enabled);
        }

        private void SaveViewSetting()
        {
            var setting = new ViewSetting
                {
                    EnableDebug = Debug.Checked,
                    InformationWindow = InfomationWindow.Checked,
                    ProxyWindow = ProxyWindow.Checked,
                    StatusBar = StatusBar.Checked,
                    ToolBar = ToolBar.Checked,
                    MenuBar = MenuBar.Checked
                };

            XmlHelper.XmlSerialize(Config.ViewSettingFileName, setting, typeof (ViewSetting));
        }

        private void LoadViewSetting()
        {
            if (System.IO.File.Exists(Config.ViewSettingFileName))
            {
                var setting = XmlHelper.XmlDeserialize(
                    Config.ViewSettingFileName,
                    typeof (ViewSetting)) as ViewSetting;

                if (setting != null)
                {
                    Config.LocalSetting.NeedDebug = Debug.Checked = setting.EnableDebug;

                    InfomationWindow.Checked = setting.InformationWindow;
                    ShowOutput();

                    ProxyWindow.Checked = setting.ProxyWindow;
                    if (ProxyWindow.Checked)
                        ProxyPage.Show();
                    else
                        ProxyPage.Hide();

                    MainStatusBar.Visible = RightStatusBar.Checked = StatusBar.Checked = setting.StatusBar;

                    MainToolbar.Visible = RightToolBar.Checked = ToolBar.Checked = setting.ToolBar;
                    MainMenu.Visible = RightMenuBar.Checked = MenuBar.Checked = setting.MenuBar;
                }
                else
                {
                    System.IO.File.Delete(Config.ViewSettingFileName);
                    SplashScreen.CloseSplashScreen();
                    _hasViewSettingException = true;
                    MsgBox.ShowErrorMessage("ViewSetting.xml not found!");
                    Close();
                }
            }
        }


        private void CloseAllTab()
        {
            if (MainDockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                for (int index = MainDockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (MainDockPanel.Contents[index] != null)
                    {
                        IDockContent content = MainDockPanel.Contents[index];
                        if (content.GetType().Name != "InfomationForm" &&
                            content.GetType().Name != "ProxyForm" &&
                            content.GetType().Name != "StartForm")
                        {
                            content.DockHandler.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     设置内置浏览器代理
        /// </summary>
        public void SetBuiltinBrowserProxy(string proxyServer)
        {
            List<IEBrowserForm> browserForms = GetBrowserForms();
            foreach (IEBrowserForm ieForm in browserForms)
            {
                ieForm.Browser.ProxyServer = proxyServer;
            }
        }

        /// <summary>
        ///     获取内置浏览器窗体
        /// </summary>
        /// <returns></returns>
        public List<IEBrowserForm> GetBrowserForms()
        {
            var browserForms = new List<IEBrowserForm>();
            if (MainDockPanel.DocumentStyle != DocumentStyle.SystemMdi)
            {
                for (int index = MainDockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (MainDockPanel.Contents[index] != null)
                    {
                        IDockContent content = MainDockPanel.Contents[index];
                        if (content.GetType().Name == "IEBrowserForm")
                        {
                            var ie = (IEBrowserForm) content;
                            browserForms.Add(ie);
                        }
                    }
                }
            }
            return browserForms;
        }

        /// <summary>
        ///     检查并更新
        /// </summary>
        private void CheckVersionAndDownLoad()
        {
            #region update

            if (!string.IsNullOrEmpty(Config.NeedUpdate))
            {
                SplashScreen.CloseSplashScreen();
                if (DialogResult.Yes == MsgBox.ShowQuestionMessage(Config.NeedUpdate))
                {
                    OpenIE(Config.ProxyHeroCloudSetting.UpdateUrl);
                }
            }

            if (!Config.ProxyHeroCloudSetting.UpdatedEnableUse.Equals("1") && !string.IsNullOrEmpty(Config.NeedUpdate))
            {
                SplashScreen.CloseSplashScreen();
                if (DialogResult.OK == MsgBox.ShowMessage("此版本已无法使用，请立即更新！"))
                {
                    OpenIE(Config.ProxyHeroCloudSetting.UpdateUrl);
                    Application.Exit();
                }
            }

            #endregion
        }

        #endregion

        #region UI

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ClosingControl == "ToolStripMenuItem")
                {
                    CloseAllTab();
                    var fi = new FileInfo(Config.DockSettingFileName);
                    if (fi.Directory != null && !fi.Directory.Exists)
                        fi.Directory.Create();
                    if (!_hasDockSettingExceptioin)
                        MainDockPanel.SaveAsXml(Config.DockSettingFileName);
                    if (!_hasViewSettingException)
                        SaveViewSetting();

                    var ph = new ProxyHelper();
                    ph.SetIEProxy("", 0);

                    try
                    {
                        XmlHelper.XmlSerialize(Config.LastProxyFileName, ProxyData.ProxyList, typeof (List<ProxyServer>));
                            //设置最后的列表
                    }
                    catch
                    {
                    }

                    notifyIconMain.Dispose();
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    WindowState = FormWindowState.Minimized;
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void NewTab_Click(object sender, EventArgs e)
        {
            OpenNewTab();
        }

        private void ToolBarVisible_Click(object sender, EventArgs e)
        {
            MainToolbar.Visible = RightToolBar.Checked = ToolBar.Checked = !ToolBar.Checked;
        }

        private void StatusBarVisible_Click(object sender, EventArgs e)
        {
            MainStatusBar.Visible = RightStatusBar.Checked = StatusBar.Checked = !StatusBar.Checked;
        }

        private void InfomationWindowVisible_Click(object sender, EventArgs e)
        {
            ShowOutput();
        }


        private void ProxyWindowVisible_Click(object sender, EventArgs e)
        {
            if (ProxyWindow.Checked)
            {
                ProxyPage.Hide();
            }
            else
            {
                ProxyPage.Show(MainDockPanel, DockState.Document);
            }
            ProxyWindow.Checked = !ProxyWindow.Checked;
        }

        private void Homepage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenIE("http://www.loamen.com");
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Help_Click(object sender, EventArgs e)
        {
            try
            {
#if DEBUG
                if (Config.IsChineseLanguage)
#else
                if (Config.IsChineseOs)
#endif
                    OpenIE("http://www.cnblogs.com/mops/articles/1905521.html");
                else
                    OpenIE("https://github.com/loamen/ProxyHero/issues");
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            var of = new OptionForm();
            of.ShowDialog();
        }


        private void DirectConnection_Click(object sender, EventArgs e)
        {
            try
            {
                var proxyHelper = new ProxyHelper();
                proxyHelper.SetIEProxy("", 0);
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        public void Exit_Click(object sender, EventArgs e)
        {
            ClosingControl = sender.GetType().Name;
            Application.Exit();
        }

        private void ShowLPH_Click(object sender, EventArgs e)
        {
            try
            {
                Show();
                WindowState = FormWindowState.Normal;
                //this.TopMost = true;
                //this.notifyIconMain.Visible = false;
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void SwitchToDirectConection_Click(object sender, EventArgs e)
        {
            try
            {
                var proxyHelper = new ProxyHelper();
                proxyHelper.SetIEProxy("", 0);
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    Hide();
                    DisposeGc();
                    //this.notifyIconMain.Visible = true;
                }
                else
                {
                    notifyIconMain.Text = Text;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Debug_Click(object sender, EventArgs e)
        {
            Debug.Checked = !Debug.Checked;
            Config.LocalSetting.NeedDebug = Debug.Checked;
        }

        private void timerAutoChangeIcon_Elapsed(object sender, EventArgs e)
        {
            try
            {
                notifyIconMain.Icon = _intIcon%2 == 0 ? Resources.Icon : Resources.incon2;

                _intIcon++;
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void timerAutoSwitchingProxy_Elapsed(object sender, EventArgs e)
        {
            try
            {
                if (TimerAutoSwitchingProxy.Enabled && Config.TsCountDown.TotalMilliseconds > 0)
                {
                    var sb = new StringBuilder("Next Time:");
                    if (Config.TsCountDown.Days > 0)
                        sb.Append(Config.TsCountDown.Days + "d");
                    if (Config.TsCountDown.Hours > 0)
                        sb.Append(Config.TsCountDown.Hours + "h");
                    if (Config.TsCountDown.Minutes > 0)
                        sb.Append(Config.TsCountDown.Minutes + "m");
                    sb.Append(Config.TsCountDown.Seconds + "s");

                    tsslCountdown.Text = sb.ToString();
                    Config.TsCountDown = Config.TsCountDown.Add(new TimeSpan(0, 0, -1));
                }
                else if (TimerAutoSwitchingProxy.Enabled && Config.TsCountDown.TotalMilliseconds == 0)
                {
                    TimerAutoSwitchingProxy.Stop();
                    var aHelper = new AutoSwitchingHelper();
                    aHelper.SwitchingProxy();
                }
                else
                {
                    tsslCountdown.Text = DateTime.Now.ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void AutoSwitching_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProxyPage.Enabled)
                {
                    SetAutoSwitch();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void MenuBar_Click(object sender, EventArgs e)
        {
            MainMenu.Visible = RightMenuBar.Checked = MenuBar.Checked = !MenuBar.Checked;
        }


        private void CheckUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var updateForm = new CheckForUpdateForm();
                updateForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void ResetView_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MsgBox.ShowQuestionMessage(Config.LocalLanguage.Messages.AreYouSureDoThis))
                {
                    if (System.IO.File.Exists(Config.DockSettingFileName))
                    {
                        System.IO.File.Delete(Config.DockSettingFileName);
                    }

                    if (System.IO.File.Exists(Config.ViewSettingFileName))
                    {
                        System.IO.File.Delete(Config.ViewSettingFileName);
                    }
                    _hasDockSettingExceptioin = true;
                    _hasViewSettingException = true;

                    MsgBox.ShowMessage(Config.IsChineseOs ? "重置成功，请重新启动程序！" : "Reset Ok,Program Restart Required!");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }


        private void PluginManage_Click(object sender, EventArgs e)
        {
            try
            {
                var pmf = new PluginManageForm();
                pmf.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Status_TextChanged(object sender, EventArgs e)
        {
            tsslLoading.Visible = !ProxyPage.IsNotDownloadingOrTesting;
            StatusLabel.Spring = true;
        }

        private void CloseTab_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    if (ActiveMdiChild != null) ActiveMdiChild.Close();
                }
                else
                {
                    if (MainDockPanel.ActiveDocument != null &&
                        (MainDockPanel.ActiveDocument.GetType().Name != "InfomationForm" &&
                         MainDockPanel.ActiveDocument.GetType().Name != "ProxyForm" &&
                         MainDockPanel.ActiveDocument.GetType().Name != "StartForm"))
                        MainDockPanel.ActiveDocument.DockHandler.Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }


        private void CloseAllTabs_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllTab();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog();
        }

        private void notifyIconMain_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!Visible)
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Forum_Click(object sender, EventArgs e)
        {
            try
            {
                //OpenNewTab(Config.ProxyHeroCloudSetting.BbsDomain);
                OpenIE(Config.ProxyHeroCloudSetting.BbsDomain);
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Languages_Click(object sender, EventArgs e)
        {
            var of = new OptionForm();
            of.Tag = "Language";
            of.ShowDialog();
        }

        #endregion

        #region

        /// 方法名：DelegateSetToolTipText
        /// <summary>
        ///     显示状态栏提示信息委托
        /// </summary>
        /// <remarks>
        ///     显示状态栏提示信息委托
        /// </remarks>
        /// <param name="text"></param>
        private void DelegateSetToolTipText(string text)
        {
            notifyIconMain.ShowBalloonTip(2000, Config.LocalLanguage.Messages.Information, text, ToolTipIcon.Info);
        }

        #endregion

        #region

        /// <summary>
        ///     显示状态栏文字委托
        /// </summary>
        /// <param name="text"></param>
        private void DelegateSetStatusText(string text)
        {
            StatusLabel.Text = text;
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIconMain.Text = text;
            }
        }

        public void ShowOutput()
        {
            if (!InfomationWindow.Checked)
            {
                OutputPage.Show(MainDockPanel, DockState.DockBottomAutoHide);
            }
            else
            {
                OutputPage.Hide();
            }
            InfomationWindow.Checked = !InfomationWindow.Checked;
        }
        #endregion
    }
}