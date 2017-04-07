using System;
using System.Windows.Forms;

namespace ProxyHero.LanguageInformation
{
    public class Language
    {
        private IEBrowserPage _ieBrowserPage = new IEBrowserPage();
        private InfomationPage _informationPage = new InfomationPage();
        private string _languageFileAuthor = "龙门信息网（www.loamen.com）";
        private string _languageFileOForLphVersion = "V " + Application.ProductVersion;
        private string _languageFileVersion = "V 1.0";
        private string _languageName = "简体中文";
        private MainPage _mainPage = new MainPage();
        private Messages _messages = new Messages();
        private OptionPage _optionPage = new OptionPage();
        private PluginManagePage _pluginManagePage = new PluginManagePage();
        private ProxyPage _proxyPage = new ProxyPage();

        public string LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }

        /// <summary>
        ///     语言文件作者
        /// </summary>
        public string LanguageFileAuthor
        {
            get { return _languageFileAuthor; }
            set { _languageFileAuthor = value; }
        }

        /// <summary>
        ///     语言文件版本
        /// </summary>
        public string LanguageFileVersion
        {
            get { return _languageFileVersion; }
            set { _languageFileVersion = value; }
        }

        /// <summary>
        ///     对本软件以下有效
        /// </summary>
        public string LanguageFileForLPHVersion
        {
            get { return _languageFileOForLphVersion; }
            set { _languageFileOForLphVersion = value; }
        }

        public MainPage MainPage
        {
            get { return _mainPage; }
            set { _mainPage = value; }
        }

        public ProxyPage ProxyPage
        {
            get { return _proxyPage; }
            set { _proxyPage = value; }
        }

        public OptionPage OptionPage
        {
            get { return _optionPage; }
            set { _optionPage = value; }
        }

        public InfomationPage InformationPage
        {
            get { return _informationPage; }
            set { _informationPage = value; }
        }

        public IEBrowserPage IeBrowserPage
        {
            get { return _ieBrowserPage; }
            set { _ieBrowserPage = value; }
        }

        public PluginManagePage PluginManagePage
        {
            get { return _pluginManagePage; }
            set { _pluginManagePage = value; }
        }

        public Messages Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }
    }

    /// <summary>
    ///     主界面
    /// </summary>
    [Serializable]
    public class MainPage
    {
        private string _about = "关于";
        private string _automaticSwitching = "开启自动切换代理";
        private string _checkUpdate = "检测更新";
        private string _closeAllTabs = "关闭所有标签";
        private string _closeTab = "关闭标签";
        private string _debug = "允许调试信息";
        private string _directConnection = "禁用代理";
        private string _donate = "捐赠";
        private string _exit = "退出";
        private string _file = "文件";
        private string _forum = "论坛";
        private string _help = "帮助";
        private string _homepage = "官网主页";
        private string _infomationWindow = "信息窗口";
        private string _menuBar = "菜单栏";
        private string _newTab = "新建标签";
        private string _onlineHelp = "帮助文档";
        private string _options = "选项";
        private string _plugin = "插件";
        private string _pluginManage = "插件管理";
        private string _proxyWindow = "代理窗口";
        private string _resetView = "重置视图";
        private string _showLph = "显示主界面";
        private string _statusBar = "状态栏";
        private string _tool = "工具";
        private string _toolBar = "工具栏";
        private string _view = "视图";

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public string Exit
        {
            get { return _exit; }
            set { _exit = value; }
        }

        public string RightExit
        {
            get { return Exit; }
        }

        public string View
        {
            get { return _view; }
            set { _view = value; }
        }

        public string MenuBar
        {
            get { return _menuBar; }
            set { _menuBar = value; }
        }

        public string RightMenuBar
        {
            get { return MenuBar; }
        }

        public string ToolBar
        {
            get { return _toolBar; }
            set { _toolBar = value; }
        }

        public string RightToolBar
        {
            get { return ToolBar; }
        }

        public string StatusBar
        {
            get { return _statusBar; }
            set { _statusBar = value; }
        }

        public string RightStatusBar
        {
            get { return StatusBar; }
        }

        public string InfomationWindow
        {
            get { return _infomationWindow; }
            set { _infomationWindow = value; }
        }

        public string ProxyWindow
        {
            get { return _proxyWindow; }
            set { _proxyWindow = value; }
        }

        public string ResetView
        {
            get { return _resetView; }
            set { _resetView = value; }
        }

        public string Plugin
        {
            get { return _plugin; }
            set { _plugin = value; }
        }

        public string PluginManage
        {
            get { return _pluginManage; }
            set { _pluginManage = value; }
        }

        public string Tool
        {
            get { return _tool; }
            set { _tool = value; }
        }

        public string DirectConnection
        {
            get { return _directConnection; }
            set { _directConnection = value; }
        }

        public string MenuHelp
        {
            get { return Help; }
        }

        public string OnlineHelp
        {
            get { return _onlineHelp; }
            set { _onlineHelp = value; }
        }

        public string Forum
        {
            get { return _forum; }
            set { _forum = value; }
        }

        /// <summary>
        ///     检测更新
        /// </summary>
        public string CheckUpdate
        {
            get { return _checkUpdate; }
            set { _checkUpdate = value; }
        }

        public string Donate
        {
            get { return _donate; }
            set { _donate = value; }
        }

        public string tsbDonate
        {
            get { return Donate; }
        }

        public string About
        {
            get { return _about; }
            set { _about = value; }
        }

        public string NewTab
        {
            get { return _newTab; }
            set { _newTab = value; }
        }

        public string CloseTab
        {
            get { return _closeTab; }
            set { _closeTab = value; }
        }

        public string CloseAllTabs
        {
            get { return _closeAllTabs; }
            set { _closeAllTabs = value; }
        }

        public string DockNewTab
        {
            get { return NewTab; }
        }

        public string MenuNewTab
        {
            get { return NewTab; }
        }

        public string Options
        {
            get { return _options; }
            set { _options = value; }
        }

        public string Option
        {
            get { return Options; }
        }

        public string Homepage
        {
            get { return _homepage; }
            set { _homepage = value; }
        }

        public string MenuHomepage
        {
            get { return Homepage; }
        }

        public string Help
        {
            get { return _help; }
            set { _help = value; }
        }

        public string BarExit
        {
            get { return Exit; }
        }

        /// <summary>
        ///     显示主界面
        /// </summary>
        public string ShowLPH
        {
            get { return _showLph; }
            set { _showLph = value; }
        }

        public string SwitchToDirectConection
        {
            get { return DirectConnection; }
        }

        public string AutomaticSwitching
        {
            get { return _automaticSwitching; }
            set { _automaticSwitching = value; }
        }

        public string AutoSwitching
        {
            get { return AutomaticSwitching; }
        }

        public string TSExit
        {
            get { return Exit; }
        }

        public string Debug
        {
            get { return _debug; }
            set { _debug = value; }
        }
    }

    /// <summary>
    ///     代理页
    /// </summary>
    [Serializable]
    public class ProxyPage
    {
        private string _anonymity = "匿名度";
        private string _builtinBrowser = "内置浏览器";
        private string _clear = "清空";
        private string _copy = "复制";
        private string _country = "地区";
        private string _cut = "剪切";
        private string _delete = "删除";
        private string _deleteDeadProxy = "删除无效数据";
        private string _deleteSeleted = "删除选定";
        private string _doNotUseProxy = "禁止使用代理";
        private string _downloadProxy = "下载代理数据";
        private string _exportProxy = "导出代理";
        private string _importProxy = "导入代理";
        private string _option = "选项";
        private string _paste = "粘贴";
        private string _port = "端口";
        private string _resetAllSetting = "初始化所有设置";
        private string _search = "查找";
        private string _stopTest = "停止验证";
        private string _switchTo = "使用该代理";
        private string _test = "验证";
        private string _testAll = "验证全部";

        public string ResetAllSetting
        {
            get { return _resetAllSetting; }
            set { _resetAllSetting = value; }
        }

        public string Option
        {
            get { return _option; }
            set { _option = value; }
        }

        public string DownloadProxy
        {
            get { return _downloadProxy; }
            set { _downloadProxy = value; }
        }

        public string TestAll
        {
            get { return _testAll; }
            set { _testAll = value; }
        }

        public string StopTest
        {
            get { return _stopTest; }
            set { _stopTest = value; }
        }

        public string ImportProxy
        {
            get { return _importProxy; }
            set { _importProxy = value; }
        }

        public string ExportProxy
        {
            get { return _exportProxy; }
            set { _exportProxy = value; }
        }

        public string Delete
        {
            get { return _delete; }
            set { _delete = value; }
        }

        public string Cut
        {
            get { return _cut; }
            set { _cut = value; }
        }

        public string DeleteDeadProxy
        {
            get { return _deleteDeadProxy; }
            set { _deleteDeadProxy = value; }
        }

        public string DeleteSeleted
        {
            get { return _deleteSeleted; }
            set { _deleteSeleted = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string CountryLabel
        {
            get { return Country; }
        }

        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string PortLabel
        {
            get { return Port; }
        }

        public string Anonymity
        {
            get { return _anonymity; }
            set { _anonymity = value; }
        }

        public string AnonymityLabel
        {
            get { return Anonymity; }
        }

        public string Search
        {
            get { return _search; }
            set { _search = value; }
        }

        public string Copy
        {
            get { return _copy; }
            set { _copy = value; }
        }

        public string Paste
        {
            get { return _paste; }
            set { _paste = value; }
        }

        public string Test
        {
            get { return _test; }
            set { _test = value; }
        }

        public string SwitchTo
        {
            get { return _switchTo; }
            set { _switchTo = value; }
        }

        /// <summary>
        ///     禁止使用代理
        /// </summary>
        public string DoNotUseProxy
        {
            get { return _doNotUseProxy; }
            set { _doNotUseProxy = value; }
        }

        /// <summary>
        ///     内置浏览器
        /// </summary>
        public string BuiltinBrowser
        {
            get { return _builtinBrowser; }
            set { _builtinBrowser = value; }
        }

        public string DeleteThis
        {
            get { return Delete; }
        }

        public string Clear
        {
            get { return _clear; }
            set { _clear = value; }
        }
    }

    [Serializable]
    public class OptionPage
    {
        private string _anonymity = "验证匿名度";
        private string _apply = "应用";
        private string _autoSwitchingInterval = "自动切换代理时间间隔";
        private string _autoSwitchingProxyMaxDelay = "自动代理最大延迟小于";
        private string _basicOptions = "基本设置";
        private string _builtinBrowserScriptErrorsSuppressed = "禁止显示内置浏览器脚本错误";
        private string _cancel = "取消";
        private string _clickForSystemTesting = "点击进行系统检测";
        private string _cloudServer = "云服务器";
        private string _country = "验证地理位置";
        private string _defaultTestWebsite = "默认网址";
        private string _exportTxtFormat = "导出TXT文件格式";
        private string _languageFile = "语言文件路径";
        private string _languageFileAuthor = "语言文件作者";
        private string _languageFileOriginallyIntendedForLphVersion = "对本软件以下版本有效";
        private string _languageFileVersion = "语言文件版本";
        private string _languageName = "语言名称";
        private string _languageOptions = "Language";
        private string _ok = "确定";
        private string _pleaseSelectOrFill = "请选择默认验证网址或者在下面填写";
        private string _programRestartRequired = "需要重启才能完全生效";
        private string _systemCheck = "系统检测";

        private string _testOptions = "验证配置";
        private string _testThreadsCount = "验证线程数";
        private string _testTimeout = "验证超时时间";
        private string _testWebsite = "验证网址";
        private string _testWebsiteEncode = "网页编码";
        private string _testWebsiteTitle = "网站标题";
        private string _useSystemProxySetting = "使用系统代理设置访问网络";
        private string _czIpDbFileName = "纯真IP数据库地址";

        /// <summary>
        /// 纯真IP数据库地址
        /// </summary>
        public string CzIpDbFileName
        {
            get { return _czIpDbFileName; }
            set { _czIpDbFileName = value; }
        }

        /// <summary>
        ///     基本设置
        /// </summary>
        public string BasicOptions
        {
            get { return _basicOptions; }
            set { _basicOptions = value; }
        }

        /// <summary>
        ///     验证配置
        /// </summary>
        public string TestOptions
        {
            get { return _testOptions; }
            set { _testOptions = value; }
        }

        /// <summary>
        ///     语言配置
        /// </summary>
        public string LanguageOptions
        {
            get { return _languageOptions; }
            set { _languageOptions = value; }
        }

        /// <summary>
        ///     系统检测
        /// </summary>
        public string SystemCheck
        {
            get { return _systemCheck; }
            set { _systemCheck = value; }
        }

        /// <summary>
        ///     云服务器
        /// </summary>
        public string CloudServer
        {
            get { return _cloudServer; }
            set { _cloudServer = value; }
        }

        /// <summary>
        ///     自动切换代理时间间隔
        /// </summary>
        public string AutoSwitchingInterval
        {
            get { return _autoSwitchingInterval; }
            set { _autoSwitchingInterval = value; }
        }

        /// <summary>
        ///     自动代理最大延迟小于
        /// </summary>
        public string AutoSwitchingProxyMaxDelay
        {
            get { return _autoSwitchingProxyMaxDelay; }
            set { _autoSwitchingProxyMaxDelay = value; }
        }

        /// <summary>
        ///     导出TXT文件格式
        /// </summary>
        public string ExportTxtFormat
        {
            get { return _exportTxtFormat; }
            set { _exportTxtFormat = value; }
        }

        /// <summary>
        ///     确定按钮
        /// </summary>
        public string OK
        {
            get { return _ok; }
            set { _ok = value; }
        }

        /// <summary>
        ///     取消按钮
        /// </summary>
        public string Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        /// <summary>
        ///     应用按钮
        /// </summary>
        public string Apply
        {
            get { return _apply; }
            set { _apply = value; }
        }

        /// <summary>
        ///     禁止显示内置浏览器脚本错误
        /// </summary>
        public string BuiltinBrowserScriptErrorsSuppressed
        {
            get { return _builtinBrowserScriptErrorsSuppressed; }
            set { _builtinBrowserScriptErrorsSuppressed = value; }
        }

        /// <summary>
        ///     语言文件路径
        /// </summary>
        public string LanguageFile
        {
            get { return _languageFile; }
            set { _languageFile = value; }
        }

        /// <summary>
        ///     语言名称
        /// </summary>
        public string LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }

        /// <summary>
        ///     语言文件作者
        /// </summary>
        public string LanguageFileAuthor
        {
            get { return _languageFileAuthor; }
            set { _languageFileAuthor = value; }
        }

        /// <summary>
        ///     语言文件版本
        /// </summary>
        public string LanguageFileVersion
        {
            get { return _languageFileVersion; }
            set { _languageFileVersion = value; }
        }

        /// <summary>
        ///     对本软件以下有效
        /// </summary>
        public string LanguageFileOriginallyIntendedForLPHVersion
        {
            get { return _languageFileOriginallyIntendedForLphVersion; }
            set { _languageFileOriginallyIntendedForLphVersion = value; }
        }

        /// <summary>
        ///     需要重启才能完全生效
        /// </summary>
        public string ProgramRestartRequired
        {
            get { return _programRestartRequired; }
            set { _programRestartRequired = value; }
        }

        /// <summary>
        ///     使用系统代理设置访问网络
        /// </summary>
        public string UseSystemProxySetting
        {
            get { return _useSystemProxySetting; }
            set { _useSystemProxySetting = value; }
        }

        /// <summary>
        ///     默认侧室网址
        /// </summary>
        public string DefaultTestWebsite
        {
            get { return _defaultTestWebsite; }
            set { _defaultTestWebsite = value; }
        }

        /// <summary>
        ///     验证网址
        /// </summary>
        public string TestWebsite
        {
            get { return _testWebsite; }
            set { _testWebsite = value; }
        }

        /// <summary>
        ///     验证网站标题
        /// </summary>
        public string TestWebsiteTitle
        {
            get { return _testWebsiteTitle; }
            set { _testWebsiteTitle = value; }
        }

        /// <summary>
        ///     验证网站编码
        /// </summary>
        public string TestWebsiteEncode
        {
            get { return _testWebsiteEncode; }
            set { _testWebsiteEncode = value; }
        }

        /// <summary>
        ///     验证超时时间
        /// </summary>
        public string TestTimeout
        {
            get { return _testTimeout; }
            set { _testTimeout = value; }
        }

        /// <summary>
        ///     验证线程数
        /// </summary>
        public string TestThreadsCount
        {
            get { return _testThreadsCount; }
            set { _testThreadsCount = value; }
        }

        /// <summary>
        ///     验证地理位置
        /// </summary>
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        /// <summary>
        ///     验证匿名度
        /// </summary>
        public string Anonymity
        {
            get { return _anonymity; }
            set { _anonymity = value; }
        }


        /// <summary>
        ///     请选择默认验证网址或者在下面填写
        /// </summary>
        public string PleaseSelectOrFill
        {
            get { return _pleaseSelectOrFill; }
            set { _pleaseSelectOrFill = value; }
        }

        /// <summary>
        ///     点击进行系统检测
        /// </summary>
        public string ClickForSystemTesting
        {
            get { return _clickForSystemTesting; }
            set { _clickForSystemTesting = value; }
        }
    }

    [Serializable]
    public class InfomationPage
    {
        private string clear = "清空";
        private string copy = "复制";
        private string cut = "剪切";
        private string delete = "删除";
        private string paste = "粘贴";
        private string switchTo = "使用该代理";
        private string testing = "验证";
        private string threadsInfo = "验证线程信息";

        public string Delete
        {
            get { return delete; }
            set { delete = value; }
        }

        public string Copy
        {
            get { return copy; }
            set { copy = value; }
        }

        public string Cut
        {
            get { return cut; }
            set { cut = value; }
        }

        public string SwitchTo
        {
            get { return switchTo; }
            set { switchTo = value; }
        }

        /// <summary>
        ///     粘贴
        /// </summary>
        public string Paste
        {
            get { return paste; }
            set { paste = value; }
        }

        /// <summary>
        ///     清空
        /// </summary>
        public string Clear
        {
            get { return clear; }
            set { clear = value; }
        }

        /// <summary>
        ///     验证
        /// </summary>
        public string Testing
        {
            get { return testing; }
            set { testing = value; }
        }

        public string txtCopy
        {
            get { return Copy; }
        }

        public string txtClear
        {
            get { return Clear; }
        }

        public string ThreadsInfo
        {
            get { return threadsInfo; }
            set { threadsInfo = value; }
        }
    }

    [Serializable]
    public class IEBrowserPage
    {
        private string _addToTestingList = "添加当前页代理到验证窗口(Ctrl+T)";
        private string _back = "后退";
        private string _forward = "前进";
        private string _fresh = "刷新";
        private string _go = "转到";
        private string _homePage = "主页";
        private string _lockAndRefresh = "锁定当前页，当代理切换时自动刷新";
        private string _setting = "设置";

        /// <summary>
        ///     主页
        /// </summary>
        public string HomePage
        {
            get { return _homePage; }
            set { _homePage = value; }
        }

        /// <summary>
        ///     前进
        /// </summary>
        public string Forward
        {
            get { return _forward; }
            set { _forward = value; }
        }

        /// <summary>
        ///     后退
        /// </summary>
        public string Back
        {
            get { return _back; }
            set { _back = value; }
        }

        /// <summary>
        ///     刷新
        /// </summary>
        public string Fresh
        {
            get { return _fresh; }
            set { _fresh = value; }
        }

        /// <summary>
        ///     锁定当前页，当代理切换时自动刷新
        /// </summary>
        public string LockAndRefresh
        {
            get { return _lockAndRefresh; }
            set { _lockAndRefresh = value; }
        }

        /// <summary>
        ///     转到
        /// </summary>
        public string Go
        {
            get { return _go; }
            set { _go = value; }
        }

        /// <summary>
        ///     添加当前页代理到验证窗口
        /// </summary>
        public string AddToTestingList
        {
            get { return _addToTestingList; }
            set { _addToTestingList = value; }
        }

        /// <summary>
        ///     设置
        /// </summary>
        public string Setting
        {
            get { return _setting; }
            set { _setting = value; }
        }
    }

    [Serializable]
    public class PluginManagePage
    {
        private string _add = "添加";
        private string _cancel = "取消";
        private string _compile = "编译(.cs->.dll)";
        private string _downloadPlugins = "下载插件";
        private string _oK = "确定";

        private string _remove = "移除";

        public string Add
        {
            get { return _add; }
            set { _add = value; }
        }

        public string Remove
        {
            get { return _remove; }
            set { _remove = value; }
        }

        public string OK
        {
            get { return _oK; }
            set { _oK = value; }
        }

        public string Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        public string DownloadPlugins
        {
            get { return _downloadPlugins; }
            set { _downloadPlugins = value; }
        }

        public string Compile
        {
            get { return _compile; }
            set { _compile = value; }
        }
    }

    [Serializable]
    public class Messages
    {
        private string _abortingThread = "正在终止读取线程";
        private string _alive = "可用";
        private string _allTestingCompleted = "全部验证完毕";
        private string _anonymous = "普通匿名代理";
        private string _areYouSureClearThis = "确定要清空列表中数据吗？";
        private string _areYouSureDoThis = "确定要继续操作此项吗？";
        private string _autoSwitchProxyMaxDelayToolTip = "自动切换代理时代理速度如果比此值慢则会弃用";
        private string _autoSwitchingProxyListIsEmpty = "可用代理列表为空，不能自动切换代理！";
        private string _automaticSwitchingOff = "自动切换代理已关闭";
        private string _automaticSwitchingOn = "自动切换代理已开启";
        private string _builtinBrowser = "内置浏览器";
        private string _checkUpdate = "检测更新";
        private string _connectCloudEngineFailed = "云引擎连接失败";
        private string _connectCloudEngineSuccess = "云引擎连接成功";
        private string _connectingCloudEngine = "正在连接云引擎";
        private string _copyFailed = "复制失败，当前系统可能运行了其他程序（例如：Microsoft Office软件）大量占用了粘贴板资源！";
        private string _copySucess = "已成功复制";
        private string _currentProxy = "当前代理";
        private string _dead = "失效";
        private string _downloading = "正在下载";
        private string _error = "错误";
        private string _exception = "异常";
        private string _googleCloudServer = "谷歌云服务器";
        private string _highAnonymous = "高匿名代理";
        private string _information = "提示";
        private string _informationWindow = "信息窗口";
        private string _initializeComponent = "初始化组件";
        private string _initializeDatabase = "初始化数据库";
        private string _initializeDockPanel = "初始化停靠窗体";
        private string _initializeFailed = "初始化失败";
        private string _loadingLanguages = "加载语言";
        private string _loadingPlugins = "加载插件";
        private string _loamenFormat = "公布器格式";
        private string _loamenServer = "官方服务器";
        private string _minimizedToSaveResources = "最小化以节省资源";
        private string _networkIsbusyPleaseTryAgainLater = "网络繁忙，请稍后再试！";
        private string _notTest = "未验证";
        private string _notUseProxy = "当前未使用代理";
        private string _numOfProxiesDownloaded = "共{0}条获取完毕";
        private string _numOfProxiesDownloadedByThreads = "共{0}条线程正在读取数据{1}条";
        private string _numOfProxiesTested = "{0}条验证完毕";
        private string _numOfThreadsAreTesting = "共{0}条线程正在验证";
        private string _pleaseCheckNetworkSettingsAreCorrect = "请检查网络设置是否正确";
        private string _pleaseEnterTheUrlYouWantToRefresh = "请输入要刷新的网址！";
        private string _pleaseTurnOnTheAutomaticSwitching = "请先开启自动切换代理！";
        private string _proxyIsAlive = "此代理有效";
        private string _proxyIsDead = "此代理无效";
        private string _proxyIsDeadAutoSwithing = "代理{0}无效，自动切换下一代理。";
        private string _selectDefaultTestWebSiteToolTip = "选择用来验证的默认网址";
        private string _sinaCloudServer = "新浪云服务器";
        private string _speedDoestConformToTheRequirement = "速度不符合要求";
        private string _standardFormat = "标准格式";
        private string _stopDownload = "停止下载";
        private string _stopping = "正在停止";
        private string _switchToIeOrBuiltinBrowser = "请确认代理应用于IE还是内置浏览器？\n点击“是”应用于Internet Explorer。\n点击“否”应用于内置浏览器。";
        private string _swithing = "自动切换中";
        private string _testWebsiteEncodeToolTip = "网页的编码格式，如：GB2312或者UTF-8";
        private string _testWebsiteTitleToolTip = "验证网址中的任意一段文字\n如百度的为：百度一下，你就知道\n你输入“百度”或“百度一下”都行";
        private string _testWebsiteUrlToolTip = "用来验证的网址，如：http://www.baidu.com。\r\n必须加http://";
        private string _testAreaToolTip = "验证代理地理位置";
        private string _testing = "正在验证";
        private string _testingHaveBeenTerminated = "已经终止验证";
        private string _timeConsuming = "耗时：{0}毫秒";
        private string _total = "总共";
        private string _transparent = "透明代理";
        private string _warning = "警告";

        /// <summary>
        ///     请检查网络设置是否正确
        /// </summary>
        public string PleaseCheckNetworkSettingsAreCorrect
        {
            get { return _pleaseCheckNetworkSettingsAreCorrect; }
            set { _pleaseCheckNetworkSettingsAreCorrect = value; }
        }

        /// <summary>
        ///     初始化组件
        /// </summary>
        public string InitializeComponent
        {
            get { return _initializeComponent; }
            set { _initializeComponent = value; }
        }

        /// <summary>
        ///     初始化数据库
        /// </summary>
        public string InitializeDatabase
        {
            get { return _initializeDatabase; }
            set { _initializeDatabase = value; }
        }

        /// <summary>
        ///     加载语言
        /// </summary>
        public string LoadingLanguages
        {
            get { return _loadingLanguages; }
            set { _loadingLanguages = value; }
        }

        /// <summary>
        ///     初始化停靠窗体
        /// </summary>
        public string InitializeDockPanel
        {
            get { return _initializeDockPanel; }
            set { _initializeDockPanel = value; }
        }

        /// <summary>
        ///     初始化失败
        /// </summary>
        public string InitializeFailed
        {
            get { return _initializeFailed; }
            set { _initializeFailed = value; }
        }

        /// <summary>
        ///     检测更新
        /// </summary>
        public string CheckUpdate
        {
            get { return _checkUpdate; }
            set { _checkUpdate = value; }
        }

        /// <summary>
        ///     加载插件
        /// </summary>
        public string LoadingPlugins
        {
            get { return _loadingPlugins; }
            set { _loadingPlugins = value; }
        }

        /// <summary>
        ///     正在连接云引擎
        /// </summary>
        public string ConnectingCloudEngine
        {
            get { return _connectingCloudEngine; }
            set { _connectingCloudEngine = value; }
        }

        /// <summary>
        ///     云引擎连接成功
        /// </summary>
        public string ConnectCloudEngineSuccess
        {
            get { return _connectCloudEngineSuccess; }
            set { _connectCloudEngineSuccess = value; }
        }

        /// <summary>
        ///     "云引擎连接失败
        /// </summary>
        public string ConnectCloudEngineFailed
        {
            get { return _connectCloudEngineFailed; }
            set { _connectCloudEngineFailed = value; }
        }

        /// <summary>
        ///     当前代理
        /// </summary>
        public string CurrentProxy
        {
            get { return _currentProxy; }
            set { _currentProxy = value; }
        }

        /// <summary>
        ///     当前未使用代理
        /// </summary>
        public string NotUseProxy
        {
            get { return _notUseProxy; }
            set { _notUseProxy = value; }
        }

        /// <summary>
        ///     自动切换代理已开启
        /// </summary>
        public string AutomaticSwitchingOn
        {
            get { return _automaticSwitchingOn; }
            set { _automaticSwitchingOn = value; }
        }

        /// <summary>
        ///     自动切换代理已关闭
        /// </summary>
        public string AutomaticSwitchingOff
        {
            get { return _automaticSwitchingOff; }
            set { _automaticSwitchingOff = value; }
        }

        /// <summary>
        ///     内置浏览器
        /// </summary>
        public string BuiltinBrowser
        {
            get { return _builtinBrowser; }
            set { _builtinBrowser = value; }
        }

        /// <summary>
        ///     提示信息
        /// </summary>
        public string Information
        {
            get { return _information; }
            set { _information = value; }
        }

        /// <summary>
        ///     警告
        /// </summary>
        public string Warning
        {
            get { return _warning; }
            set { _warning = value; }
        }

        /// <summary>
        ///     异常
        /// </summary>
        public string Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }

        /// <summary>
        ///     错误
        /// </summary>
        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }

        /// <summary>
        ///     停止下载
        /// </summary>
        public string StopDownload
        {
            get { return _stopDownload; }
            set { _stopDownload = value; }
        }

        /// <summary>
        ///     正在下载
        /// </summary>
        public string Downloading
        {
            get { return _downloading; }
            set { _downloading = value; }
        }

        /// <summary>
        ///     正在停止
        /// </summary>
        public string Stopping
        {
            get { return _stopping; }
            set { _stopping = value; }
        }

        /// <summary>
        ///     网络繁忙，请稍后再试！
        /// </summary>
        public string NetworkIsbusyPleaseTryAgainLater
        {
            get { return _networkIsbusyPleaseTryAgainLater; }
            set { _networkIsbusyPleaseTryAgainLater = value; }
        }

        /// <summary>
        ///     "共{0}条获取完毕"
        /// </summary>
        public string NumOfProxiesDownloaded
        {
            get { return _numOfProxiesDownloaded; }
            set { _numOfProxiesDownloaded = value; }
        }

        /// <summary>
        ///     共{0}条线程正在读取数据{1}条
        /// </summary>
        public string NumOfProxiesDownloadedByThreads
        {
            get { return _numOfProxiesDownloadedByThreads; }
            set { _numOfProxiesDownloadedByThreads = value; }
        }

        /// <summary>
        ///     正在验证
        /// </summary>
        public string Testing
        {
            get { return _testing; }
            set { _testing = value; }
        }

        /// <summary>
        ///     自动切换中
        /// </summary>
        public string Swithing
        {
            get { return _swithing; }
            set { _swithing = value; }
        }

        /// <summary>
        ///     {0}条验证完毕
        /// </summary>
        public string NumOfProxiesTested
        {
            get { return _numOfProxiesTested; }
            set { _numOfProxiesTested = value; }
        }

        /// <summary>
        ///     全部验证完毕
        /// </summary>
        public string AllTestingCompleted
        {
            get { return _allTestingCompleted; }
            set { _allTestingCompleted = value; }
        }

        /// <summary>
        ///     已经终止验证
        /// </summary>
        public string TestingHaveBeenTerminated
        {
            get { return _testingHaveBeenTerminated; }
            set { _testingHaveBeenTerminated = value; }
        }

        /// <summary>
        ///     此代理有效
        /// </summary>
        public string ProxyIsAlive
        {
            get { return _proxyIsAlive; }
            set { _proxyIsAlive = value; }
        }

        /// <summary>
        ///     此代理无效
        /// </summary>
        public string ProxyIsDead
        {
            get { return _proxyIsDead; }
            set { _proxyIsDead = value; }
        }

        /// <summary>
        ///     耗时：{0}毫秒
        /// </summary>
        public string TimeConsuming
        {
            get { return _timeConsuming; }
            set { _timeConsuming = value; }
        }

        /// <summary>
        ///     正在终止读取线程
        /// </summary>
        public string AbortingThread
        {
            get { return _abortingThread; }
            set { _abortingThread = value; }
        }

        /// <summary>
        ///     官方服务器
        /// </summary>
        public string LoamenServer
        {
            get { return _loamenServer; }
            set { _loamenServer = value; }
        }

        /// <summary>
        ///     谷歌云服务器
        /// </summary>
        public string GoogleCloudServer
        {
            get { return _googleCloudServer; }
            set { _googleCloudServer = value; }
        }

        /// <summary>
        ///     新浪云服务器
        /// </summary>
        public string SinaCloudServer
        {
            get { return _sinaCloudServer; }
            set { _sinaCloudServer = value; }
        }

        /// <summary>
        ///     选择用来验证的默认网址
        /// </summary>
        public string SelectDefaultTestWebSiteToolTip
        {
            get { return _selectDefaultTestWebSiteToolTip; }
            set { _selectDefaultTestWebSiteToolTip = value; }
        }

        /// <summary>
        ///     用来验证的网址，如：http://www.baidu.com。\r\n必须加http://
        /// </summary>
        public string TestWebsiteUrlToolTip
        {
            get { return _testWebsiteUrlToolTip; }
            set { _testWebsiteUrlToolTip = value; }
        }

        /// <summary>
        ///     验证网址中的任意一段文字\n如百度的为：百度一下，你就知道\n你输入“百度”或“百度一下”都行
        /// </summary>
        public string TestWebsiteTitleToolTip
        {
            get { return _testWebsiteTitleToolTip; }
            set { _testWebsiteTitleToolTip = value; }
        }

        /// <summary>
        ///     网页的编码格式，如：GB2312或者UTF-8
        /// </summary>
        public string TestWebsiteEncodeToolTip
        {
            get { return _testWebsiteEncodeToolTip; }
            set { _testWebsiteEncodeToolTip = value; }
        }

        /// <summary>
        ///     验证代理地理位置
        /// </summary>
        public string TestAreaToolTip
        {
            get { return _testAreaToolTip; }
            set { _testAreaToolTip = value; }
        }


        /// <summary>
        ///     自动切换代理时代理速度如果比此值慢则会弃用
        /// </summary>
        public string AutoSwitchProxyMaxDelayToolTip
        {
            get { return _autoSwitchProxyMaxDelayToolTip; }
            set { _autoSwitchProxyMaxDelayToolTip = value; }
        }

        /// <summary>
        ///     公布器格式
        /// </summary>
        public string LoamenFormat
        {
            get { return _loamenFormat; }
            set { _loamenFormat = value; }
        }

        /// <summary>
        ///     标准格式
        /// </summary>
        public string StandardFormat
        {
            get { return _standardFormat; }
            set { _standardFormat = value; }
        }

        /// <summary>
        ///     可用代理列表为空，不能自动切换代理！
        /// </summary>
        public string AutoSwitchingProxyListIsEmpty
        {
            get { return _autoSwitchingProxyListIsEmpty; }
            set { _autoSwitchingProxyListIsEmpty = value; }
        }

        /// <summary>
        ///     请确认代理应用于IE还是内置浏览器？\n点击“是”应用于Internet Explorer。\n点击“否”应用于内置浏览器。
        /// </summary>
        public string SwitchToIEOrBuiltinBrowser
        {
            get { return _switchToIeOrBuiltinBrowser; }
            set { _switchToIeOrBuiltinBrowser = value; }
        }

        /// <summary>
        ///     确定要清空列表中数据吗？
        /// </summary>
        public string AreYouSureClearThis
        {
            get { return _areYouSureClearThis; }
            set { _areYouSureClearThis = value; }
        }

        /// <summary>
        ///     信息窗口
        /// </summary>
        public string InformationWindow
        {
            get { return _informationWindow; }
            set { _informationWindow = value; }
        }

        /// <summary>
        ///     高匿名代理
        /// </summary>
        public string HighAnonymous
        {
            get { return _highAnonymous; }
            set { _highAnonymous = value; }
        }

        /// <summary>
        ///     普通匿名代理
        /// </summary>
        public string Anonymous
        {
            get { return _anonymous; }
            set { _anonymous = value; }
        }

        /// <summary>
        ///     透明代理
        /// </summary>
        public string Transparent
        {
            get { return _transparent; }
            set { _transparent = value; }
        }

        /// <summary>
        ///     已成功复制
        /// </summary>
        public string CopySucess
        {
            get { return _copySucess; }
            set { _copySucess = value; }
        }

        /// <summary>
        ///     复制失败，当前系统可能运行了其他程序（例如：Microsoft Office软件）大量占用了粘贴板资源！
        /// </summary>
        public string CopyFailed
        {
            get { return _copyFailed; }
            set { _copyFailed = value; }
        }

        /// <summary>
        ///     可用
        /// </summary>
        public string Alive
        {
            get { return _alive; }
            set { _alive = value; }
        }

        /// <summary>
        ///     失效
        /// </summary>
        public string Dead
        {
            get { return _dead; }
            set { _dead = value; }
        }

        /// <summary>
        ///     未验证
        /// </summary>
        public string NotTest
        {
            get { return _notTest; }
            set { _notTest = value; }
        }

        /// <summary>
        ///     总共
        /// </summary>
        public string Total
        {
            get { return _total; }
            set { _total = value; }
        }

        /// <summary>
        ///     共{0}条线程正在验证
        /// </summary>
        public string NumOfThreadsAreTesting
        {
            get { return _numOfThreadsAreTesting; }
            set { _numOfThreadsAreTesting = value; }
        }

        /// <summary>
        ///     请输入要刷新的网址！
        /// </summary>
        public string PleaseEnterTheUrlYouWantToRefresh
        {
            get { return _pleaseEnterTheUrlYouWantToRefresh; }
            set { _pleaseEnterTheUrlYouWantToRefresh = value; }
        }

        /// <summary>
        ///     请先开启自动切换代理！
        /// </summary>
        public string PleaseTurnOnTheAutomaticSwitching
        {
            get { return _pleaseTurnOnTheAutomaticSwitching; }
            set { _pleaseTurnOnTheAutomaticSwitching = value; }
        }

        /// <summary>
        ///     确定要继续操作此项吗？
        /// </summary>
        public string AreYouSureDoThis
        {
            get { return _areYouSureDoThis; }
            set { _areYouSureDoThis = value; }
        }

        /// <summary>
        ///     代理{0}无效，自动切换下一代理。
        /// </summary>
        public string ProxyIsDeadAutoSwithing
        {
            get { return _proxyIsDeadAutoSwithing; }
            set { _proxyIsDeadAutoSwithing = value; }
        }

        /// <summary>
        ///     速度不符合要求
        /// </summary>
        public string SpeedDoestConformToTheRequirement
        {
            get { return _speedDoestConformToTheRequirement; }
            set { _speedDoestConformToTheRequirement = value; }
        }

        /// <summary>
        ///     最小化以节省资源
        /// </summary>
        public string MinimizedToSaveResources
        {
            get { return _minimizedToSaveResources; }
            set { _minimizedToSaveResources = value; }
        }
    }
}