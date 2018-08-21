using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.Net;
using Loamen.Net.Entity;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.LanguageInformation;
using ProxyHero.Model;
using ProxyHero.Net;
using System.Linq;

namespace ProxyHero
{
    public class Config
    {
        /// <summary>
        ///     我的文档
        /// </summary>
        public static string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        ///     吸附代理正则表达式
        /// </summary>
        public static string RegexProxy =
            @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))(\s|\:|\：|<(.*?)\n{0,1}(.*?)>)(?<Port>\d{1,5})";

        private static string _languageFileName;

        ///// <summary>
        ///// 数据库全路径
        ///// </summary>
        //public static string DbFileName = DataPath + @"\Proxy.db3";

        /// <summary>
        ///     插件配置文件全路径+文件名
        /// </summary>
        private  static readonly string PluginSettingFileName = ProxyHeroPath + @"\PluginSetting.xml";

        /// <summary>
        ///     停靠栏配置文件全路径+文件名
        /// </summary>
        public static string DockSettingFileName = ProxyHeroPath + @"\DockSetting.xml";

        /// <summary>
        ///     视图配置文件全路径+文件名
        /// </summary>
        public static readonly string ViewSettingFileName = ProxyHeroPath + @"\ViewSetting.xml";

        public static string DatabasePath
        {
            get
            {
                string path = ProxyHeroPath + @"\Data";
                var di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }
                return di.FullName;
            }
        }

        /// <summary>
        /// 配置数据文件
        /// </summary>
        public static string SettingDataFileName
        {
            get
            {
                return DatabasePath + @"\data.db";
            }
        }

        /// <summary>
        ///     可用代理文件全路径+文件名
        /// </summary>
        public static readonly string ProxyFileName = ProxyHeroPath + @"\Proxy\Proxy.txt";

        /// <summary>
        ///     上次验证完成代理全路径+文件名
        /// </summary>
        public static readonly string LastProxyFileName = ProxyHeroPath + @"\Proxy\LastedProxyList.xml";

        private static Language _language;

        private static ConsoleEx _consoleEx;

        private static Setting _localSetting;

        /// <summary>
        ///     是否需要升级
        /// </summary>
        public static string NeedUpdate = "";

        /// <summary>
        ///     下载INI和加载起始页时错误信息
        /// </summary>
        private static string _initErrorInfo = "";

        public static Queue<HttpProxy> AutoProxyQueue = new Queue<HttpProxy>();

        private static string _proxyApplicatioin = "IE";

        private static List<string> _proxySiteUrlList = new List<string>();

        public static DateTime LateUpdateProxyListTime = DateTime.Now.AddMinutes(-1);

        /// <summary>
        ///     论坛操作
        /// </summary>
        //public static SnsHelper BbsHelper = null;
        private static ProxyHeroEntity _proxyHeroCloudSetting = new ProxyHeroEntity();

        /// <summary>
        ///     内置浏览器代理
        /// </summary>
        public static string BuiltinBrowserProxyServer = "";

        private static string _userName = "";
        private static ApiHelper _myApiHelper;
        private static PluginSetting _pluginSetting;

        /// <summary>
        ///     代理公布器存储目录
        /// </summary>
        public static string ProxyHeroPath
        {
            get
            {
                string path = MyDocumentsPath + @"\Loamen\ProxyHero";
                var di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }
                return di.FullName;
            }
        }

        /// <summary>
        ///     本地语言文件路径
        /// </summary>
        public static string LanguagePath
        {
            get
            {
                string path = MyDocumentsPath + @"\Loamen\ProxyHero\Languages";
                var di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }
                return di.FullName;
            }
        }

        ///// <summary>
        ///// 数据库路径
        ///// </summary>
        //public static string DataPath
        //{
        //    get
        //    {
        //        string path = MyDocumentsPath + @"\Loamen\ProxyHero\Data";
        //        DirectoryInfo di = new DirectoryInfo(path);
        //        if (!di.Exists)
        //        {
        //            di.Create();
        //        }
        //        return di.FullName;
        //    }
        //}
        /// <summary>
        ///     插件路径
        /// </summary>
        public static string PluginPath
        {
            get
            {
                string path = Application.StartupPath + @"\Plugins\";
                var di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }
                return di.FullName;
            }
        }

        /// <summary>
        ///     语言文件
        /// </summary>
        public static string LanguageFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_languageFileName))
                    _languageFileName = LanguagePath + @"\Simplified Chinese.xml";
                return _languageFileName;
            }
            set { _languageFileName = value; }
        }

        /// <summary>
        ///     语言文件配置
        /// </summary>
        public static Language LocalLanguage
        {
            get
            {
                if (_language == null && !string.IsNullOrEmpty(LanguageFileName) &&
                    File.Exists(LanguageFileName))
                {
                    _language = XmlHelper.XmlDeserialize(
                        LanguageFileName,
                        typeof (Language)) as Language;
                }

                if (_language == null)
                {
                    _language = new Language();
                    string defaultFile = LanguagePath + @"\Simplified Chinese.xml";
                    if (!File.Exists(defaultFile))
                    {
                        XmlHelper.XmlSerialize(defaultFile, _language, typeof (Language));
                    }
                }
                return _language;
            }
            set { _language = value; }
        }

        public static ConsoleEx ConsoleEx
        {
            get { return _consoleEx ?? (_consoleEx = new ConsoleEx()); }
            set { _consoleEx = value; }
        }

        /// <summary>
        ///     读取或设置本地配置文件
        /// </summary>
        public static Setting LocalSetting
        {
            get
            {
                if (_localSetting == null)
                {
                    var dal = new SettingDAL();
                    var model = dal.FindAll().FirstOrDefault();
                    if (model == null)
                    {
                        model = new Setting();
                        model.DefaultTestOption = new TestOption();
                        model.DefaultTestOption.TestUrl = "https://www.baidu.com";
                        model.DefaultTestOption.TestWebEncoding = "UTF-8";
                        model.DefaultTestOption.TestWebTitle = "百度";

                        model.TestOptionsList.Add(model.DefaultTestOption);

                        dal.Insert(model);
                    }
                    _localSetting = model;
                }
                return _localSetting;
            }
            set
            {
                _localSetting = value;
            }
        }

        public static string InitErrorInfo
        {
            get { return _initErrorInfo; }
            set
            {
                if (_initErrorInfo == "")
                {
                    _initErrorInfo = value;
                }
            }
        }

        /// <summary>
        ///     主界面
        /// </summary>
        public static MainForm MainForm
        {
            get
            {
                var fm = (MainForm) Application.OpenForms["MainForm"];
                return fm;
            }
        }

        /// <summary>
        ///     自动切换代理倒计时时间
        /// </summary>
        public static TimeSpan TsCountDown { get; set; }

        /// <summary>
        ///     设置代理应用于IE还是内置浏览器
        /// </summary>
        public static string ProxyApplicatioin
        {
            get { return _proxyApplicatioin; }
            set { _proxyApplicatioin = value; }
        }

        /// <summary>
        ///     代理网站页面列表
        /// </summary>
        public static List<string> ProxySiteUrlList
        {
            get { return _proxySiteUrlList; }
            set { _proxySiteUrlList = value; }
        }

        /// <summary>
        ///     代理公布器云端配置
        /// </summary>
        public static ProxyHeroEntity ProxyHeroCloudSetting
        {
            get { return _proxyHeroCloudSetting; }
            set { _proxyHeroCloudSetting = value; }
        }

        /// <summary>
        ///     语言文件是否是中文
        /// </summary>
        public static bool IsChineseLanguage
        {
            get
            {
                return (LocalLanguage.LanguageName.Contains("中文") ||
                        LocalLanguage.LanguageName.ToLower().Contains("chinese"));
            }
        }

        /// <summary>
        ///     是否是中文操作系统
        /// </summary>
        public static bool IsChineseOs
        {
            get { return CultureInfo.InstalledUICulture.Name.ToLower().Contains("zh-"); }
        }

        public static string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_userName))
                {
                    _userName = NetHelper.FirstLocalIp + "";
                    string publicIp = NetHelper.LocalPublicIp;
                    if (!string.IsNullOrEmpty(publicIp))
                        _userName = publicIp;
                }

                return _userName;
            }
        }

        /// <summary>
        ///     云引擎连接器
        /// </summary>
        public static ApiHelper MyApiHelper
        {
            get
            {
                if (null == _myApiHelper)
                {
                    _myApiHelper = new ApiHelper();
                }
                return _myApiHelper;
            }
            set { _myApiHelper = value; }
        }

        /// <summary>
        ///     获取网页错误
        /// </summary>
        /// <param name="title"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static string GetErrorHtml(string title, string errorMessage)
        {
            title = title == "" ? "出错啦" : title;
            title = "Loamen.Com:" + title;
            errorMessage = errorMessage == "" ? "可能无法连接到网络，请检查网络设置是否正确。" : errorMessage;
            var html =
                new StringBuilder(
                    "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            html.Append("<html><head>\n");
            html.Append("<title>" + title + "</title>\n");
            html.Append("<meta name=\"Author\"content=\"\"/>\n");
            html.Append("<meta name=\"Keywords\"content=\"\"/>\n");
            html.Append("<meta name=\"Description\"content=\"\"/>\n");
            html.Append("<script language=\"javascript\">\n");
            html.Append("document.onkeydown = function(){\n");
            html.Append(" if(event.keyCode==116) {\n");
            html.Append("event.keyCode=0;\n");
            html.Append("event.returnValue = false;}}\n");
            html.Append("document.oncontextmenu = function() {event.returnValue = false;}\n");
            html.Append("</script>\n");
            html.Append(
                "<style type=\"text/css\">*{margin:0;padding:0;list-style:none}html{height:100%;overflow:hidden;background:#fff}body{height:100%;overflow:hidden;background:#fff;scrollbar-face-color:#ECF1F4;scrollbar-highlight-color:#ffffff;scrollbar-shadow-color:#ffffff;scrollbar-3dlight-color:#cccccc;scrollbar-arrow-color:#6EA8C6;scrollbar-track-color:#EFEFEF;scrollbar-darkshadow-color:#b2b2b2;scrollbar-base-color:#000000}a{color:#000;text-decoration:none}a:hover{color:#000;text-decoration:none}.tab_new{border-top-width:1px;border-right-width:1px;border-left-width:1px;border-bottom-width:1px;border-top-style:solid;border-right-style:solid;border-left-style:solid;border-bottom-style:solid;border-top-color:#99bbe8;border-right-color:#99bbe8;border-left-color:#99bbe8;border-bottom-color:#99bbe8}.tab_new th{background-color:#99bbe8;text-align:left;padding-left:2px;height:25px;font-family:\"宋体\";font-size:12px;font-weight:bold;color:#000000}.tab_new td{text-align:left;font-size:12px;padding-left:2px;height:30px;border-top-width:1px;border-top-style:solid;border-top-color:#99bbe8}</style>\n");
            html.Append("</head><body>\n");
            html.Append(
                "<table align=\"center\"class=\"tab_new\"style=\"width:60%;\"cellpadding=\"0\"cellspacing=\"0\">\n");
            html.Append("<tr><th>&nbsp;" + title + "</th></tr>\n");
            html.Append("<tr><td>&nbsp;" + errorMessage + "</td></tr>\n");
            html.Append("</table>\n");
            html.Append("</body></html>");
            return html.ToString();
        }

        public static string GetErrorHtml(int code)
        {
            string html;
            if (code < 0)
            {
                const string errorHtml =
                    @"<table border='0' cellspacing='0' cellpadding='0' width='100%' height='450px' style='margin-left:74px'>
	                        <tr>
		                        <td  align='center' valign='middle' style='text-align:left;'> 
                        		
		                        无法显示网页，可能的原因是：<br />
		                        1、未连接到Internet<br />
		                        2、网站遇到问题<br /><br />
		                        请您尝试以下操作：<br />
		                        1、检查您的Internet连接。尝试访问其他网站以确保已连接到Internet。<br />
		                        </td>
	                        </tr>
                        </table>";
                html = GetErrorHtml("" + code, errorHtml);

                return html;
            }

            switch (code)
            {
                case 404:
                    html = GetErrorHtml("错误" + code, "访问的页面不存在。");
                    break;
                case 403:
                    html = GetErrorHtml("错误" + code, "禁止访问。");
                    break;
                case 502:
                    html = GetErrorHtml("错误" + code, "网关错误。");
                    break;
                default:
                    html = GetErrorHtml("错误" + code, "错误" + code);
                    break;
            }

            return html;
        }

        public static string Anoumity(string text)
        {
            text = text.Trim();
            if (!IsChineseLanguage)
            {
                switch (text)
                {
                    case "高匿名代理":
                        return LocalLanguage.Messages.HighAnonymous;
                    case "透明代理":
                        return LocalLanguage.Messages.Transparent;
                    case "普通匿名代理":
                        return LocalLanguage.Messages.Anonymous;
                    case "匿名代理":
                        return LocalLanguage.Messages.Anonymous;
                }
            }

            return text;
        }

        /// <summary>
        ///     获取英文文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetEnglishText(string text)
        {
            string[] result = null;

            object obj = TranslateHelper.TranslatorString(new[] {text});
            if (obj != null)
                result = (string[]) obj;

            return (result != null && result.Length > 0) ? result[3] : "";
        }

        /// <summary>
        ///     获取代理
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static HttpProxy GetProxyEntity(string proxy)
        {
            var pe = new HttpProxy();
            var regex = new Regex(RegexProxy);
            MatchCollection matches = regex.Matches(proxy);
            if (matches.Count == 1)
            {
                pe.Ip = matches[0].Groups["Proxy"].Value;
                int port;
                int.TryParse(matches[0].Groups["Port"].Value, out port);
                pe.Port = port;
            }
            return pe;
        }

        public static PluginSetting PluginSetting
        {
            get
            {
                if (!File.Exists(PluginSettingFileName))
                {
                    _pluginSetting = new PluginSetting();
                    var plugins = _pluginSetting.Plugins;
                    if (plugins == null || plugins.Count == 0)
                    {
                        try
                        {

                            #region 采集代理插件

                            var downLoadPluginName = PluginPath + @"Loamen.PH.Plugin.DownloadProxy.dll";
                            var pm = new PluginManager(downLoadPluginName, Config.MainForm);
                            pm.Run();
                            if (pm.Engine.Errors.Count == 0)
                            {
                                if (plugins != null)
                                    plugins.Add(new ProxyHero.Entity.Plugin
                                        {
                                            Checked = true,
                                            Author = pm.Engine.Author,
                                            Version = pm.Engine.Version,
                                            LphVersion = pm.Engine.LPHVersion,
                                            Description = pm.Engine.Description,
                                            FileName = pm.Engine.FileName.ToLower(),
                                            Name = pm.Engine.Name
                                        });
                            }

                            #endregion

                            #region 验证匿名度插件

                            var anonymityPluginName = PluginPath + @"Loamen.PH.Plugin.Anonymity.dll";
                            pm = new PluginManager(anonymityPluginName, Config.MainForm);
                            pm.Run();
                            if (pm.Engine.Errors.Count == 0)
                            {
                                if (plugins != null)
                                    plugins.Add(new ProxyHero.Entity.Plugin
                                        {
                                            Checked = true,
                                            Author = pm.Engine.Author,
                                            Version = pm.Engine.Version,
                                            LphVersion = pm.Engine.LPHVersion,
                                            Description = pm.Engine.Description,
                                            FileName = pm.Engine.FileName.ToLower(),
                                            Name = pm.Engine.Name
                                        });
                            }

                            #endregion
                        }
                        catch
                        {
                        }

                    }

                    if (_pluginSetting.Plugins.Count > 0)
                        XmlHelper.XmlSerialize(Config.PluginSettingFileName, _pluginSetting, typeof(PluginSetting));
                }

                if (_pluginSetting == null)
                {
                    _pluginSetting = (PluginSetting)XmlHelper.XmlDeserialize(Config.PluginSettingFileName, typeof(PluginSetting));
                }

                return _pluginSetting;
            }
            set
            {
                if (_pluginSetting != value)
                {
                    if (File.Exists(Config.PluginSettingFileName))
                        File.Delete(Config.PluginSettingFileName);
                    XmlHelper.XmlSerialize(Config.PluginSettingFileName, value, typeof(PluginSetting));
                }
                _pluginSetting = value;
            }
        }
    }
}