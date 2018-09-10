using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.Net.Entity;
using Loamen.WinControls.UI;
using Loamen.WinControls.UI.Collections;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.LanguageInformation;
using ProxyHero.Option.Panels;

namespace ProxyHero.Option
{
    public partial class OptionForm : OptionsForm
    {
        private readonly LanguageLoader _languageLoader = new LanguageLoader();
        private LanguagePanel languagePanel;

        public OptionForm()
            : base(PropertyDictionary<string, object>.Convert(Config.LocalSetting))
        {
            InitializeComponent();
            languagePanel = new LanguagePanel();

            Panels.Add(new GeneralPanel());
            Panels.Add(new TestPanel());
            Panels.Add(languagePanel);
            Panels.Add(new UserAgentPanel());
            Panels.Add(new SystemTestPanel());

            OkButtonText = Config.LocalLanguage.OptionPage.OK;
            ApplyButtonText = Config.LocalLanguage.OptionPage.Apply;
            CancelButtonText = Config.LocalLanguage.OptionPage.Cancel;
            OptionsNoDescription = Config.LocalLanguage.OptionPage.OptionsNoDescription;

            AppRestartText = Config.LocalLanguage.OptionPage.ProgramRestartRequired;
            Config.LocalSetting.PropertyChanged += LocalSetting_PropertyChanged;
        }

        private void LocalSetting_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _AppSettings_SettingChanging(sender, e);
        }

        public override void OnSaveOptions()
        {
            base.OnSaveOptions();
            try
            {
                Config.LocalSetting.PropertyChanged -= LocalSetting_PropertyChanged;
                Save();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Save()
        {
            try
            {
                SaveLoginSetting();
            }
            catch{}
        }

        /// <summary>
        ///     保存登录信息
        /// </summary>
        private void SaveLoginSetting()
        {
            var localSetting = Config.LocalSetting;
            var testSetting = (TestOption)AppSettings["DefaultTestOption"];

            localSetting.DefaultTestOption = testSetting;
            localSetting.ScriptErrorsSuppressed = (bool)AppSettings["ScriptErrorsSuppressed"];
            localSetting.AutoChangeProxyInterval = (int)AppSettings["AutoChangeProxyInterval"];
            localSetting.TestTimeOut = (int)AppSettings["TestTimeOut"];
            localSetting.TestThreadsCount = (int)AppSettings["TestThreadsCount"];
            localSetting.AutoProxySpeed = (int)AppSettings["AutoProxySpeed"];
            localSetting.ExportMode = (string)AppSettings["ExportMode"];
            localSetting.IsUseSystemProxy = (bool)AppSettings["IsUseSystemProxy"];
            localSetting.LanguageFileName = (string)AppSettings["LanguageFileName"];
            localSetting.UserAgent = string.IsNullOrEmpty((string)AppSettings["UserAgent"])
                                                 ? "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322)"
                                                 : (string)AppSettings["UserAgent"];
            var dal = new SettingDAL();
            var model = dal.FindAll().FirstOrDefault();
            var res = 0;
            if (model == null)
            {
                res = dal.Insert(localSetting);
            }
            else
            {
                TestOption searchSetting = model.TestOptionsList.FirstOrDefault(temp => temp.TestUrl == testSetting.TestUrl);

                if (searchSetting != null && searchSetting.TestUrl != "")
                {
                    if (searchSetting.TestUrl == testSetting.TestUrl) //如果已经设置，则移除换成新的
                    {
                        model.TestOptionsList.Remove(searchSetting);
                        model.DefaultTestOption = testSetting;
                        localSetting.DefaultTestOption = testSetting;
                    }
                }

                if (!model.TestOptionsList.Any(te => te.TestUrl.ToLower() == testSetting.TestUrl.ToLower()))
                {
                    model.TestOptionsList.Add(testSetting);
                }

                localSetting.TestOptionsList = model.TestOptionsList;


                localSetting.Id = model.Id;
                res = dal.Update(localSetting) ? 1 : 0;
            }

            if (res == 0)
            {
                MsgBox.ShowErrorMessage("保存失败！");
            }
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            Text = Config.IsChineseLanguage ? "选项" : "Settings";
        }

        /// <summary>
        ///     加载窗体语言
        /// </summary>
        private void LoadLanguage(Language language)
        {
            Text = Config.IsChineseLanguage ? "选项" : "Settings";

            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.OptionPage;
                _languageLoader.Load(model, typeof(OptionForm), this);
            }
        }

        private void OptionForm_Shown(object sender, EventArgs e)
        {
            if (this.Tag != null && this.Tag.ToString() == "Language")
            {
                GoToPanel(languagePanel.CategoryPath);
            }
        }
    }
}
