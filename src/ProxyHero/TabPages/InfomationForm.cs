using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ProxyHero.Common;
using ProxyHero.LanguageInformation;
using ProxyHero.Proxy;
using WeifenLuo.WinFormsUI.Docking;

namespace ProxyHero.TabPages
{
    public partial class InfomationForm : DockContent
    {
        private readonly LanguageLoader languageLoader = new LanguageLoader();

        public InfomationForm()
        {
            InitializeComponent();
            CloseButtonVisible = CloseButton = false;

            LoadLanguage();

            ShowDebugPanel(false);
            dataGridThreads.AutoGenerateColumns = false;
            splitContainer1.Panel2Collapsed = true;
        }

        /// <summary>
        ///     信息框
        /// </summary>
        public RichTextBox RichBox
        {
            get { return InfoBox; }
        }

        public void ShowDebugPanel(bool isShow)
        {
            spInfo.Panel2Collapsed = !isShow;
        }

        private void LoadLanguage()
        {
            Language language = Config.LocalLanguage;
            //if (System.Globalization.CultureInfo.InstalledUICulture.Name.ToLower().Contains("zh-"))
            if (Config.IsChineseLanguage)
                Text = "信息窗口";
            else
                Text = "Information";
            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.InformationPage;
                languageLoader.Load(model, typeof (InfomationForm), this);
            }
        }

        private void InfomationForm_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(InfoBox, Config.LocalLanguage.Messages.InformationWindow);
        }

        private void txtCopy_Click(object sender, EventArgs e)
        {
            InfoBox.Copy();
        }

        private void txtClear_Click(object sender, EventArgs e)
        {
            InfoBox.Clear();
        }

        /// <summary>
        ///     更新线程验证数据
        /// </summary>
        public void UpdateDataGrid()
        {
            if (null == dataGridThreads ||
                null == Config.MainForm ||
                null == Config.MainForm.ProxyPage ||
                null == Config.MainForm.ProxyPage.Tester ||
                Config.MainForm.ProxyPage.Tester.Threads.Count == 0) return;

            try
            {
                if (dataGridThreads.InvokeRequired)
                {
                    UpdateDataGridCallback callback = UpdateDataGrid;
                    Invoke(callback, new object[] {});
                }
                else
                {
                    dataGridThreads.DataSource = typeof (List<TestThread>);
                    dataGridThreads.DataSource = Config.MainForm.ProxyPage.Tester.Threads;
                }
            }
            catch (ObjectDisposedException odEx)
            {
                LogHelper.WriteException(odEx);
            }
        }

        private void ThreadsInfo_Click(object sender, EventArgs e)
        {
            ThreadsInfo.Checked = !ThreadsInfo.Checked;
            ShowDebugPanel(ThreadsInfo.Checked);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //this.contextMenuStrip1.Enabled = (this.dataGridThreads.Rows.Count > 0);
        }

        private delegate void UpdateDataGridCallback();
    }
}