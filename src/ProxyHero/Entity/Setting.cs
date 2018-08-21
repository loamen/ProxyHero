using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Loamen.Net.Entity;

namespace ProxyHero.Entity
{
    [Serializable]
    public class Setting
    {
        private int _autoChangeProxyInterval = 10;
        private bool _autoLogin;
        private int _autoProxySpeed = 10;
        private bool _checkArea;
        private string _czIpDbFileName = Application.StartupPath + @"\ip\qqwry.dat";
        private TestOption _defaultTestOption = new TestOption();
        private string _exportMode = Config.LocalLanguage.Messages.LoamenFormat;
        private HttpOptions _httpOption;
        private bool _isUseSystemProxy = true;
        private string _languageFileName = Config.LanguageFileName;
        private bool _needDebug;
        private string _password;
        private int _readRssInterval;
        private bool _rememberPassword;
        private bool _scriptErrorsSuppressed = true;
        private List<TestOption> _testOptions = new List<TestOption>();
        private int _testThreadsCount = 50;
        private int _testTimeOut = 10;
        private string _userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322)";
        private string _userName = string.Empty;
        public int Id { get; set;}

        /// <summary>
        ///     读取RSS间隔
        /// </summary>
        public int ReadRssInterval
        {
            get { return _readRssInterval; }
            set
            {
                SendPropertyChanging();
                _readRssInterval = value;
                SendPropertyChanged("ReadRssInterval");
            }
        }

        /// <summary>
        ///     默认验证地址
        /// </summary>
        public TestOption DefaultTestOption
        {
            get { return _defaultTestOption; }
            set
            {
                SendPropertyChanging();
                _defaultTestOption = value;
                SendPropertyChanged("DefaultTestOption");
            }
        }

        /// <summary>
        ///     验证设置列表
        /// </summary>
        public List<TestOption> TestOptionsList
        {
            get { return _testOptions; }
            set
            {
                SendPropertyChanging();
                _testOptions = value;
                SendPropertyChanged("TestOptionsList");
            }
        }

        /// <summary>
        ///     http设置
        /// </summary>
        public HttpOptions HttpOption
        {
            get { return _httpOption; }
            set
            {
                SendPropertyChanging();
                _httpOption = value;
                SendPropertyChanged("HttpOption");
            }
        }

        /// <summary>
        ///     禁止显示脚本错误
        /// </summary>
        public bool ScriptErrorsSuppressed
        {
            get { return _scriptErrorsSuppressed; }
            set
            {
                SendPropertyChanging();
                _scriptErrorsSuppressed = value;
                SendPropertyChanged("ScriptErrorsSuppressed");
            }
        }

        /// <summary>
        ///     验证超时，单位毫秒
        /// </summary>
        public int TestTimeOut
        {
            get { return _testTimeOut; }
            set
            {
                SendPropertyChanging();
                _testTimeOut = value;
                SendPropertyChanged("TestTimeOut");
            }
        }

        /// <summary>
        ///     自动切换代理时间间隔，单位秒
        /// </summary>
        public int AutoChangeProxyInterval
        {
            get { return _autoChangeProxyInterval; }
            set
            {
                SendPropertyChanging();
                _autoChangeProxyInterval = value;
                SendPropertyChanged("AutoChangeProxyInterval");
            }
        }

        /// <summary>
        ///     自动代理速度
        /// </summary>
        public int AutoProxySpeed
        {
            get { return _autoProxySpeed; }
            set
            {
                SendPropertyChanging();
                _autoProxySpeed = value;
                SendPropertyChanged("AutoProxySpeed");
            }
        }

        /// <summary>
        ///     验证线程数
        /// </summary>
        public int TestThreadsCount
        {
            get { return _testThreadsCount; }
            set
            {
                SendPropertyChanging();
                _testThreadsCount = value;
                SendPropertyChanged("TestThreadsCount");
            }
        }

        /// <summary>
        ///     验证地理位置
        /// </summary>
        public bool CheckArea
        {
            get { return _checkArea; }
            set
            {
                SendPropertyChanging();
                _checkArea = value;
                SendPropertyChanged("CheckArea");
            }
        }

        public bool RememberPassword
        {
            get { return _rememberPassword; }
            set
            {
                SendPropertyChanging();
                _rememberPassword = value;
                SendPropertyChanged("RememberPassword");
            }
        }

        public bool AutoLogin
        {
            get { return _autoLogin; }
            set
            {
                SendPropertyChanging();
                _autoLogin = value;
                SendPropertyChanged("AutoLogin");
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                SendPropertyChanging();
                _userName = value;
                SendPropertyChanged("UserName");
            }
        }

        /// <summary>
        ///     密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                SendPropertyChanging();
                _password = value;
                SendPropertyChanged("Password");
            }
        }

        /// <summary>
        ///     导出格式
        /// </summary>
        public string ExportMode
        {
            get { return _exportMode; }
            set
            {
                SendPropertyChanging();
                _exportMode = value;
                SendPropertyChanged("ExportMode");
            }
        }

        /// <summary>
        ///     使用系统默认代理访问网络
        /// </summary>
        public bool IsUseSystemProxy
        {
            get { return _isUseSystemProxy; }
            set
            {
                SendPropertyChanging();
                _isUseSystemProxy = value;
                SendPropertyChanged("IsUseSystemProxy");
            }
        }


        /// <summary>
        ///     是否需要显示DEBUG信息
        /// </summary>
        public bool NeedDebug
        {
            get { return _needDebug; }
            set
            {
                SendPropertyChanging();
                _needDebug = value;
                SendPropertyChanged("NeedDebug");
            }
        }

        /// <summary>
        ///     语言文件路径
        /// </summary>
        public string LanguageFileName
        {
            get { return _languageFileName; }
            set
            {
                SendPropertyChanging();
                _languageFileName = value;
                SendPropertyChanged("LanguageFileName");
            }
        }

        public string UserAgent
        {
            get
            {
                if (string.IsNullOrEmpty(_userAgent))
                {
                    _userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322)";
                }
                return _userAgent;
            }
            set
            {
                SendPropertyChanging();
                _userAgent = value;
                SendPropertyChanged("UserAgent");
            }
        }

        /// <summary>
        ///     纯真IP数据库全文件地址
        /// </summary>
        public string CzIpDbFileName
        {
            get { return _czIpDbFileName; }
            set
            {
                SendPropertyChanging();
                _czIpDbFileName = value;
                SendPropertyChanged("CzIpDbFileName");
            }
        }

        #region 属性事件

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly PropertyChangingEventArgs emptyChangingEventArgs =
            new PropertyChangingEventArgs(String.Empty);

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}