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
    public partial class OutputForm : DockContent
    {
        private readonly LanguageLoader languageLoader = new LanguageLoader();
        private delegate void AppendTextCallback(string text);
        private delegate void UpdateDataGridCallback();

        public OutputForm()
        {
            InitializeComponent();
            CloseButtonVisible = CloseButton = false;

            LoadLanguage();

            ShowDebugPanel(false);
            dataGridThreads.AutoGenerateColumns = false;
            splitContainer1.Panel2Collapsed = true;
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
                Text = "输出";
            else
                Text = "Output";
            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.InformationPage;
                languageLoader.Load(model, typeof (OutputForm), this);
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


        public void AppendText(string text)
        {
            if (this.InfoBox.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (string.IsNullOrEmpty(text)) return;
                InfoBox.AppendText(text);
            }
        }

        /// <summary>
        /// 向输出窗口追加一行文本
        /// </summary>
        /// <param name="s"></param>
        public void AppendLine(string text)
        {
            AppendText(text + Environment.NewLine);
        }
    }
}