using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.Net.Entity;
using Loamen.WinControls.UI;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.LanguageInformation;

namespace ProxyHero.Option.Panels
{
    public partial class TestPanel : OptionsPanel
    {
        private readonly LanguageLoader _languageLoader = new LanguageLoader();

        public TestPanel()
        {
            InitializeComponent();

            LoadLanguage(Config.LocalLanguage);
        }

        private void TestOptionPanel_Load(object sender, EventArgs e)
        {
            try
            {
                InitSetting();
                InitAccessibleDescription();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        ///     加载窗体语言
        /// </summary>
        private void LoadLanguage(Language language)
        {
            Text = CultureInfo.InstalledUICulture.Name.ToLower().Contains("zh-") ? "选项" : "Options";

            //lblForLPHVersion.Text = ProductVersion;

            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.OptionPage;
                _languageLoader.Load(model, typeof(TestPanel), this);
            }
        }

        /// <summary>
        ///     加载登录信息
        /// </summary>
        private void InitSetting()
        {
            cbbChoose.DisplayMember = "TestUrl";
            cbbChoose.ValueMember = "TestUrl";
            var localSetting = Config.LocalSetting ?? new Setting();

            //如果存在登录信息文件则读取信息

            foreach (var testOption in localSetting.TestOptionsList)
            {
                if (!cbbChoose.Items.Contains(testOption))
                {
                    cbbChoose.Items.Add(testOption);
                }
            }

           
            nudTestOutTime.Value = localSetting.TestTimeOut;
            nudTestOutTime.DataBindings.Add("Value", localSetting, "TestTimeOut",
                                                                  true, DataSourceUpdateMode.OnPropertyChanged);

            nudThreadsCount.Value = localSetting.TestThreadsCount;
            nudThreadsCount.DataBindings.Add("Value", localSetting, "TestThreadsCount",
                                                                  true, DataSourceUpdateMode.OnPropertyChanged);

            Country.Checked = localSetting.CheckArea;
            Country.DataBindings.Add("Checked", localSetting, "CheckArea",
                                                                  true, DataSourceUpdateMode.OnPropertyChanged);

            txtUrl.Text = localSetting.DefaultTestOption.TestUrl;
            txtUrl.DataBindings.Add("Text", localSetting.DefaultTestOption, "TestUrl",
                                                                  true, DataSourceUpdateMode.OnPropertyChanged);

            txtTitle.Text = localSetting.DefaultTestOption.TestWebTitle;
            txtTitle.DataBindings.Add("Text", localSetting.DefaultTestOption, "TestWebTitle",
                                                                  true, DataSourceUpdateMode.OnPropertyChanged);

            txtEncoding.Text = localSetting.DefaultTestOption.TestWebEncoding;
            txtEncoding.DataBindings.Add("Text", localSetting.DefaultTestOption, "TestWebEncoding",
                                                                  true, DataSourceUpdateMode.OnPropertyChanged);

            txtCzIpDbFileName.Text = localSetting.CzIpDbFileName;
            txtCzIpDbFileName.DataBindings.Add("Text", localSetting, "CzIpDbFileName",
                                                                  true, DataSourceUpdateMode.OnPropertyChanged);
           

            if (cbbChoose.Items.Count > 0)
            {
                if (localSetting != null && localSetting.DefaultTestOption != null)
                {
                    for (int i = 0; i < cbbChoose.Items.Count; i++)
                    {
                        var testOption = cbbChoose.Items[i] as TestOption;
                        if (testOption == null ||
                            testOption.TestUrl.ToLower() != localSetting.DefaultTestOption.TestUrl.ToLower()) continue;
                        cbbChoose.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void InitAccessibleDescription()
        {
            cbbChoose.AccessibleDescription = Config.LocalLanguage.Messages.SelectDefaultTestWebSiteToolTip;
            txtUrl.AccessibleDescription = Config.LocalLanguage.Messages.TestWebsiteUrlToolTip;
            txtTitle.AccessibleDescription = Config.LocalLanguage.Messages.TestWebsiteTitleToolTip;
            txtEncoding.AccessibleDescription = Config.LocalLanguage.Messages.TestWebsiteEncodeToolTip;
            Country.AccessibleDescription = Config.LocalLanguage.Messages.TestAreaToolTip;
        }

        private void cbbChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //在选择帐号时并配置帐号相关信息显示到控件

                if (cbbChoose.SelectedItem == null)
                {
                    return;
                }

                var testSetting = cbbChoose.SelectedItem as TestOption;
                if (testSetting == null)
                {
                    return;
                }

                txtUrl.Text = testSetting.TestUrl;
                txtTitle.Text = testSetting.TestWebTitle;
                txtEncoding.Text = testSetting.TestWebEncoding;
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void btnBrowseCzIpDb_Click(object sender, EventArgs e)
        {
            try
            {
                var objOpen = new OpenFileDialog
                {
                    Filter = @"纯真IP数据库|qqwry.dat|所有文件|*.*",
                    InitialDirectory = Config.LanguagePath
                };

                if (objOpen.ShowDialog() == DialogResult.OK)
                {
                    txtCzIpDbFileName.Text = objOpen.FileName;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }
    }
}
