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
using ProxyHero.Common;
using Loamen.Common;
using System.Reflection;
using Loamen.Net;
using System.Net;
using System.IO;
using ProxyHero.Entity;

namespace ProxyHero.Option.Panels
{
    public partial class SystemTestPanel : OptionsPanel
    {
        private readonly LanguageLoader _languageLoader = new LanguageLoader();

        public SystemTestPanel()
        {
            InitializeComponent();

            LoadLanguage(Config.LocalLanguage);
        }

        /// <summary>
        ///     加载窗体语言
        /// </summary>
        private void LoadLanguage(Language language)
        {
            Text = Config.IsChineseLanguage ? "选项" : "Options";

            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.OptionPage;
                _languageLoader.Load(model, typeof(SystemTestPanel), this);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                var sb = new StringBuilder("  【OS   Version】: ");
                sb.Append(OSVersion.VersionString);
                sb.Append("\n  【LPH  Version】: ");
                sb.Append(Assembly.GetExecutingAssembly().GetName().Version);
                sb.Append("\n  【IE   Version】: ");
                sb.Append(OSVersion.InternetExplorerVersion);
                sb.Append("\n  【Install Path】: ");
                sb.Append(Application.StartupPath);
                sb.Append("\n---------------------------------");
                string ips = "";
                foreach (IPAddress ip in NetHelper.GetLocalIp())
                {
                    ips += ip + "\n";
                }
                sb.Append("\n        【本地IP】: " + ips);
                sb.Append("        【外部IP】: " + NetHelper.LocalPublicIp);
                sb.Append("\n      【连接方式】: " + NetHelper.InternetConnectedState);
                sb.Append("\n  【是否是局域网】: " + (NetHelper.IsPublicIPAddress(NetHelper.FirstLocalIp + "") ? "否" : "是"));
                sb.Append("\n【是否连接因特网】: " + (NetHelper.IsConnectedToInternet ? "是" : "否"));
                if (NetHelper.IsPublicIPAddress(NetHelper.FirstLocalIp + ""))
                    sb.Append("\n  【宽带连接名称】: " + ProxyHelper.DefaultADSLName);
                rbSystemInfo.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void ResetAllSetting_Click(object sender, EventArgs e)
        {
            if (MsgBox.ShowQuestionMessage(Config.LocalLanguage.Messages.AreYouSureDoThis) == DialogResult.Yes)
            {
                var di = new DirectoryInfo(Config.ProxyHeroPath);
                if (di.Exists)
                {
                    di.Delete(true);
                    MsgBox.ShowMessage(Config.LocalLanguage.OptionPage.ProgramRestartRequired);
                    Config.MainForm.Exit_Click(new ToolStripMenuItem(), e);
                }
            }
        }

        private void SystemTestPanel_Load(object sender, EventArgs e)
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
            //Setting localSetting = Config.LocalSetting ?? new Setting();
        }
    }
}
