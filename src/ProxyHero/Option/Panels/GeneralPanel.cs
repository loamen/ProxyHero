using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loamen.WinControls.UI;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.LanguageInformation;

namespace ProxyHero.Option.Panels
{
    public partial class GeneralPanel : OptionsPanel
    {
        private readonly LanguageLoader _languageLoader = new LanguageLoader();

        public GeneralPanel()
        {
            InitializeComponent();

            LoadLanguage(Config.LocalLanguage);
            cbbExportMode.Items.Clear();
            cbbExportMode.Items.AddRange(new object[]
                {Config.LocalLanguage.Messages.LoamenFormat, Config.LocalLanguage.Messages.StandardFormat});

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
                _languageLoader.Load(model, typeof(GeneralPanel), this);
                this.CategoryPath = Config.LocalLanguage.OptionPage.GeneralPanelCategoryPath;
                this.DisplayName = Config.LocalLanguage.OptionPage.GeneralPanelDisplayName;
            }
        }

        private void GeneralPanel_Load(object sender, EventArgs e)
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
            var localSetting = Config.LocalSetting ?? new Setting();

            BuiltinBrowserScriptErrorsSuppressed.Checked =localSetting.ScriptErrorsSuppressed;
            BuiltinBrowserScriptErrorsSuppressed.DataBindings.Add("Checked", localSetting, "ScriptErrorsSuppressed",
                                                                  true, DataSourceUpdateMode.OnPropertyChanged);

            nudChangeProxyInterval.Minimum = 1;
            nudChangeProxyInterval.Value = localSetting.AutoChangeProxyInterval < nudChangeProxyInterval.Minimum
                                               ? nudChangeProxyInterval.Minimum
                                               : localSetting.AutoChangeProxyInterval;
            nudChangeProxyInterval.DataBindings.Add("Value", localSetting, "AutoChangeProxyInterval", true,
                                                    DataSourceUpdateMode.OnPropertyChanged);

            nupAutoProxySpeed.Value = localSetting.AutoProxySpeed;
            nupAutoProxySpeed.AccessibleDescription = Config.LocalLanguage.Messages.AutoSwitchProxyMaxDelayToolTip;
            nupAutoProxySpeed.DataBindings.Add("Value", localSetting, "AutoProxySpeed", true,
                                                    DataSourceUpdateMode.OnPropertyChanged);

            cbbExportMode.Text = localSetting.ExportMode != Config.LocalLanguage.Messages.StandardFormat
                                     ? Config.LocalLanguage.Messages.LoamenFormat
                                     : localSetting.ExportMode;
            cbbExportMode.DataBindings.Add("Text", localSetting, "ExportMode", true,
                                                   DataSourceUpdateMode.OnPropertyChanged);

            UseSystemProxySetting.Checked = localSetting.IsUseSystemProxy;
            UseSystemProxySetting.DataBindings.Add("Checked", localSetting, "IsUseSystemProxy", true,
                                                   DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
