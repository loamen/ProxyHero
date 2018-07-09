using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Loamen.PluginFramework;
using Loamen.RefreshPlugin.Entity;
using System.Threading;

namespace Loamen.PH.Plugin.Refresh
{
    public partial class RefreshForm : DockPage
    {
        private IApp application = null;
        private Refresher refresher = null;
        delegate void UpdateDataGridCallback();
        Refresh refresh = null;

        public RefreshForm(IApp app, Refresh refresh)
        {
            InitializeComponent();
            this.refresh = refresh;
            application = app;
            this.dataGridView1.AutoGenerateColumns = false;
            this.btnStop.Enabled = false;

            this.cbUseProxy.SelectedIndex = 0;
        }

        private void CheckCompleted(object sender, EventArgs e)
        {
            try
            {
                this.btnOK.Enabled = true;
                this.btnStop.Enabled = false;
            }
            catch (Exception ex)
            {
                application.WriteExceptionLog(ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (application.IsDownloadingOrTesting)
                {
                    MessageBox.Show("代理公布器正在下载或验证代理，请先停止下载或验证以免占用更多资源！");
                }
                else
                {
                    if (application.AliveProxyList.Count > 0)
                    {
                        this.btnOK.Enabled = false;
                        this.btnStop.Enabled = true;
                        List<Proxy> proxyList = new List<Proxy>();
                        foreach (var item in application.AliveProxyList)//从龙门代理公布器的可用代理列表里获取代理数据
                        {
                            proxyList.Add(new Proxy(item.proxy, item.port));
                        }

                        List<string> urlList = new List<string>();
                        foreach (string line in this.rtbPageList.Lines)
                        {
                            urlList.Add(line);
                        }

                        refresher = new Refresher(urlList, proxyList, this.cbUseProxy.Text == "是", this.rbQuickly.Checked ? "Quickly" : "WebBrowser");
                        refresher.SleepTime = Convert.ToInt32(this.nudSleepInterval.Value);
                        refresher.TimeOut = Convert.ToInt32(this.nudTimeout.Value);
                        refresher.Completed += new Refresher.CompletedEventHandler(this.CheckCompleted);
                        refresher.Start();
                        BindData();
                    }
                    else
                    {
                        MessageBox.Show("可用代理列表为空！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindData()
        {
            if (refresher != null)
            {
                this.dataGridView1.DataSource = typeof(List<RefreshThread>);
                this.dataGridView1.DataSource = refresher.Threads;
                this.dataGridView1.Refresh();
            }
        }

        /// <summary>
        /// 更新线程测试数据
        /// </summary>
        public void UpdateDataGrid()
        {
            if (null == this.dataGridView1 ||
                null == refresher ||
                null == refresher.Threads) return;

            try
            {
                if (this.dataGridView1.InvokeRequired)
                {
                    UpdateDataGridCallback callback = new UpdateDataGridCallback(UpdateDataGrid);
                    this.Invoke(callback, new object[] { });
                }
                else
                {
                    this.dataGridView1.DataSource = typeof(List<RefreshThread>);
                    this.dataGridView1.DataSource = refresher.Threads;
                    this.dataGridView1.Refresh();
                }

                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                application.WriteExceptionLog(ex);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnStop.Enabled = false;
            if (this.refresher != null)
                this.refresher.Stop();
            this.btnOK.Enabled = true;
        }

        private void lbInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            application.OpenIE("https://github.com/loamen/ProxyHero/tree/master/src/Plugins");
        }

        private void RefreshForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ToolStripItem[] tsmis = application.PluginMenu.DropDownItems.Find("RefreshPluginMenu", true);
            if (tsmis.Length == 1)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmis[0];
                tsmi.Checked = false;
                this.refresh.refreshForm = null;
            }
        }

        private void rbQuickly_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbQuickly.Checked)
                this.rbWebBorwser.Checked = false;
        }

        private void rbWebBorwser_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbWebBorwser.Checked)
                this.rbQuickly.Checked = false;
        }
    }
}
