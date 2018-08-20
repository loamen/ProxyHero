using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loamen.WinControls.UI;
using ProxyHero.LanguageInformation;
using System.Globalization;
using ProxyHero.Model;
using ProxyHero.Common;
using ProxyHero.Entity;

namespace ProxyHero.Option.Panels
{
    public partial class UserAgentPanel : OptionsPanel
    {
        private readonly LanguageLoader _languageLoader = new LanguageLoader();

        public UserAgentPanel()
        {
            InitializeComponent();

            LoadLanguage(Config.LocalLanguage);

            //将数据源的属性与ComboBox的属性对应
            cbbUserAgent.DisplayMember = "Text"; //显示
            cbbUserAgent.ValueMember = "Value"; //值
            cbbUserAgent.DataSource = UserAgentList;
            cbbUserAgent.Refresh();
            cbbUserAgent.SelectedIndex = 0;
        }

        /// <summary>
        ///     加载窗体语言
        /// </summary>
        private void LoadLanguage(Language language)
        {
            Text = Config.IsChineseLanguage ? "选项" : "Settings";

            //lblForLPHVersion.Text = ProductVersion;

            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.OptionPage;
                _languageLoader.Load(model, typeof(UserAgentPanel), this);
                this.CategoryPath = Config.LocalLanguage.OptionPage.UserAgentPanelCategoryPath;
                this.DisplayName = Config.LocalLanguage.OptionPage.UserAgentPanelDisplayName;
            }
        }

        private List<ListItem> UserAgentList
        {
            get
            {
                var list = new List<ListItem>();
                var browser = new WebBrowserEx();
                list.Add(new ListItem("默认", browser.UserAgent));
                list.Add(new ListItem("Internet Explorer 6(Windows XP)",
                                      "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 2.0.50727;)"));
                list.Add(new ListItem("Internet Explorer 7(Windows XP)",
                                      "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)"));
                list.Add(new ListItem("Internet Explorer 8(Windows 7)",
                                      "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)"));
                list.Add(new ListItem("Chrome/15.0.874.54(Windows XP)",
                                      "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2"));
                list.Add(new ListItem("Firefox/8.0(Windows XP)",
                                      "Mozilla/5.0 (Windows NT 5.1; rv:8.0) Gecko/20100101 Firefox/8.0"));
                list.Add(new ListItem("HTC(Android 2.3.5)",
                                      "Mozilla/5.0 (Linux; U; Android 2.3.5; zh-cn; HTC Vision Build/GRI40) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1"));
                list.Add(new ListItem("HTC_Touch_3G(Windows Mobile)",
                                      "HTC_Touch_3G Mozilla/4.0 (compatible; MSIE 6.0; Windows CE; IEMobile 7.11)"));
                list.Add(new ListItem("IE Mobile 9.0(Window Phone 7)",
                                      "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)"));
                list.Add(new ListItem("Nokia N97",
                                      "Mozilla/5.0 (SymbianOS/9.4; Series60/5.0 NokiaN97-1/20.0.019; Profile/MIDP-2.1 Configuration/CLDC-1.1) AppleWebKit/525 (KHTML, like Gecko) BrowserNG/7.1.18124"));
                list.Add(new ListItem("iPad",
                                      "Mozilla/5.0 (iPad; U; CPU OS 3_2 like Mac OS X; zh-cn) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B334b Safari/531.21.10"));
                list.Add(new ListItem("iPhone4",
                                      "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_0 like Mac OS X; zh-cn) AppleWebKit/532.9 (KHTML, like Gecko) Version/4.0.5 Mobile/8A293 Safari/6531.22.7"));
                return list;
            }
        }

        private void cbbUserAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbUserAgent.Text = cbbUserAgent.SelectedValue + "";
        }

        private void UserAgentPanel_Load(object sender, EventArgs e)
        {
            try
            {
                InitSetting();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        ///     加载登录信息
        /// </summary>
        private void InitSetting()
        {
            Setting localSetting = Config.LocalSetting ?? new Setting();

          
            rtbUserAgent.Text = localSetting.UserAgent;
            cbbUserAgent.SelectedValue = localSetting.UserAgent;
            cbbUserAgent.DataBindings.Add("SelectedValue", localSetting, "UserAgent",
                                                                 true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
