using System;
using System.Windows.Forms;
using Loamen.WinControls.UI;
using ProxyHero.Common;
using WeifenLuo.WinFormsUI.Docking;

namespace ProxyHero.TabPages
{
    public partial class StartForm : DockContent
    {
        #region Init

        public StartForm()
        {
            try
            {
                InitializeComponent();
                DockHandler.CloseButton = false;
                DockHandler.CloseButtonVisible = false;
                wbStatistics.Visible = false;
                Loading.Visible = true;
                Loading.Left = Width/2;
                Loading.Top = Height/2;
                wbStatistics.ScriptErrorsSuppressed = true;
                wbStatistics.ProxyServer = "";
                wbStatistics.IsWebBrowserContextMenuEnabled = false;

                if (Config.IsChineseLanguage)
                    Text = "起始页";
                else
                    Text = "Home";

                if (Config.IsChineseLanguage)
                {
                    wbStatistics.Navigate(Config.ProxyHeroCloudSetting.StatisticsUrl);
                }
                else
                {
                    wbStatistics.Navigate(Config.ProxyHeroCloudSetting.EnglishStatisticsUrl);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        #endregion

        #region 界面操作

        private void wbStatistics_NavigateError(object sender, WebBrowserNavigateErrorEventArgs e)
        {
            int code = e.StatusCode;
            wbStatistics.DocumentText = Config.GetErrorHtml(code);
        }

        #endregion

        private void wbStatistics_BeforeNewWindow(object sender, EventArgs e)
        {
            var eventArgs = e as WebBrowserExtendedNavigatingEventArgs;
            Config.MainForm.OpenNewTab(eventArgs.Url);
            eventArgs.Cancel = true;
        }

        private void wbStatistics_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wbStatistics.Visible = true;
            Loading.Visible = false;
        }

        private void StartForm_Resize(object sender, EventArgs e)
        {
            Loading.Left = (Width - Loading.Width)/2;
            Loading.Top = (Height - Loading.Height)/2;
        }
    }
}