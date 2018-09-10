using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loamen.WinControls.UI;
using System.Globalization;
using ProxyHero.LanguageInformation;
using Loamen.Common;
using System.IO;
using ProxyHero.Common;
using ProxyHero.Entity;

namespace ProxyHero.Option.Panels
{
    public partial class LanguagePanel : OptionsPanel
    {
        private readonly LanguageLoader _languageLoader = new LanguageLoader();

        public LanguagePanel()
        {
            InitializeComponent();

            LoadLanguage(Config.LocalLanguage);

        }

        /// <summary>
        ///     加载窗体语言
        /// </summary>
        private void LoadLanguage(Language language)
        {
            Text = Config.IsChineseLanguage ? "选项" : "Settings";

            lblForLPHVersion.Text = ProductVersion;

            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.OptionPage;
                _languageLoader.Load(model, typeof(LanguagePanel), this);
                this.CategoryPath = Config.LocalLanguage.OptionPage.LanguagePanelCategoryPath;
                this.DisplayName = Config.LocalLanguage.OptionPage.LanguagePanelDisplayName;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                var path = Config.LanguagePath;
                if (!Config.IsChineseOs) //如果不是中文操作系统
                {
                    if (!File.Exists(Path.Combine(path, "English.xml")))
                    {
                        path = Path.Combine(Application.StartupPath, "Languages");
                    }
                }

                var objOpen = new OpenFileDialog
                {
                    Filter = @"(*.xml)|*.xml|(*.*)|*.*",
                    InitialDirectory = path
                };

                if (objOpen.ShowDialog() == DialogResult.OK)
                {
                    txtLanguageFile.Text = objOpen.FileName;
                    var language = XmlHelper.XmlDeserialize(
                        objOpen.FileName,
                        typeof(Language)) as Language;

                    if (language != null)
                    {
                        lblForLPHVersion.Text = language.LanguageFileForLPHVersion;
                        lblLanguageFileAuthor.Text = language.LanguageFileAuthor;
                        lblLanguageFileVersion.Text = language.LanguageFileVersion;
                        lblLanguageName.Text = language.LanguageName;

                        LoadLanguage(language);
                    }
                    var dal = new SettingDAL();
                    var model = dal.FindAll().FirstOrDefault();
                    if(model != null)
                    {
                        model.LanguageFileName = objOpen.FileName;
                        dal.Update(model);
                    }

                    ApplicationMustRestart = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void LanguagePanel_Load(object sender, EventArgs e)
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
            txtLanguageFile.Text = localSetting.LanguageFileName;
            txtLanguageFile.DataBindings.Add("Text", localSetting, "LanguageFileName",
                                                                 true, DataSourceUpdateMode.OnPropertyChanged);

            lblForLPHVersion.Text = Config.LocalLanguage.LanguageFileForLPHVersion;
            lblLanguageFileAuthor.Text = Config.LocalLanguage.LanguageFileAuthor;
            lblLanguageFileVersion.Text = Config.LocalLanguage.LanguageFileVersion;
            lblLanguageName.Text = Config.LocalLanguage.LanguageName;
        }

        private void Download_Click(object sender, EventArgs e)
        {
            Config.MainForm.OpenIE("https://github.com/loamen/ProxyHero/tree/master/documents/languages");
        }
    }
}
