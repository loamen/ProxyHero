using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Loamen.PH.Plugin.IpSeeker.Entity;
using Loamen.PluginFramework;

namespace Loamen.PH.Plugin.IpSeeker
{
    public partial class IpSeekerForm : DockPage
    {
        public static Queue<Proxy> ProxyQueue;
        private readonly IApp _application;
        private readonly string _settingFilename;

        private int _compltedCount;
        private IpSeeker _ipSeeker;
        private Stopwatch _sw = new Stopwatch();
        private List<IpThread> _threads;

        public IpSeekerForm(IApp app, IpSeeker ipSeeker)
        {
            try
            {
                InitializeComponent();
                _application = app;
                _ipSeeker = ipSeeker;
                richTextBox1.Clear();
                richTextBox1.AppendText("由于龙门代理公布器使用的都是网络验证，所以验证地理位置数据相当缓慢。本插件采用纯真本地IP数据库。验证地理位置那是超级快啊！");
                richTextBox1.AppendText("\n请大家点击起始页上的广告，支持龙门的发展。每天点一次就好，谢谢！");
                richTextBox1.AppendText("\n使用方法：");
                richTextBox1.AppendText(
                    "\n1.设置纯真IP数据库文件（\"qqwry.dat\"）路径！如果没有，请到纯真下载：http://www.cz88.net/down/76250/");
                richTextBox1.AppendText("\n2.设置线程数！");
                richTextBox1.AppendText("\n3.开始验证！");
                richTextBox1.AppendText("\n4.龙门代理公布器->选项->验证配置，去掉“验证地理位置”选项的钩，去掉“验证匿名度”的钩钩！");
                richTextBox1.AppendText("\n5.验证全部代理，搞定！");
                _settingFilename = app.ProxyHeroSettingPath + @"\IpSeekerSetting.xml";

                if (File.Exists(_settingFilename))
                {
                    var setting = app.XmlDeserialize(_settingFilename, typeof (IpSeekerSetting)) as IpSeekerSetting;
                    if (null != setting)
                    {
                        txtDbFile.Text = setting.DbFileName;
                        numericUpDown1.Value = setting.ThreadCount;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                var objOpen = new OpenFileDialog {FileName = "QQWry.Dat", Filter = @"纯真IP数据库(QQWry.Dat)|*.dat"};

                if (objOpen.ShowDialog() == DialogResult.OK)
                {
                    txtDbFile.Text = objOpen.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Refresh(string text)
        {
            DelegateUpdate d = UpdateStatus;
            Invoke(d, new object[] {text});
        }

        private void UpdateStatus(string text)
        {
            richTextBox1.AppendText(text + "\n");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDbFile.Text.Trim() == "")
                {
                    MessageBox.Show(@"请先设置纯真IP数据库文件！");
                    btnBrowser_Click(sender, e);
                    return;
                }

                if (_application.ProxyList.Count == 0)
                {
                    MessageBox.Show(@"代理列表数据为空！");
                    return;
                }
                if (_application.IsDownloadingOrTesting)
                {
                    MessageBox.Show(@"代理公布器正在下载数据或验证代理，请先关闭以免占用更多资源！");
                    return;
                }

                try
                {
                    _application.EnabledProxyPageUI(false);
                    var setting = new IpSeekerSetting
                        {
                            DbFileName = txtDbFile.Text,
                            ThreadCount = Convert.ToInt32(numericUpDown1.Value)
                        };
                    _application.XmlSerialize(_settingFilename, setting, typeof (IpSeekerSetting));
                }
                catch (Exception)
                {
                }

                richTextBox1.Clear();
                _threads = new List<IpThread>();
                ProxyQueue = new Queue<Proxy>();
                _sw = new Stopwatch();
                _sw.Start();

                foreach (ProxyServer dr in _application.ProxyList)
                {
                    var pe = new Proxy {Ip = dr.proxy, Port = dr.port};
                    ProxyQueue.Enqueue(pe);
                }
                var threadCount = (int) numericUpDown1.Value;
                threadCount = _application.ProxyList.Count >= threadCount ? threadCount : _application.ProxyList.Count;
                for (int i = 0; i < threadCount; i++)
                {
                    var thread = new IpThread(txtDbFile.Text, _application);
                    thread.Completed += CheckCompelted;
                    thread.Start();
                }
                Refresh("正在验证地理位置，请稍后...");
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckCompelted(object sender, EventArgs args)
        {
            var thread = (IpThread) sender;
            if (thread.Status == "Complted")
            {
                _compltedCount++;
                Refresh("线程" + thread.Name + "完成！");
            }

            if (_threads.Count == _compltedCount)
            {
                _application.EnabledProxyPageUI(true);
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                _sw.Stop();
                var sb = new StringBuilder();
                if (_sw.Elapsed.Days > 0)
                    sb.Append(_sw.Elapsed.Days + "天");
                if (_sw.Elapsed.Hours > 0)
                    sb.Append(_sw.Elapsed.Hours + "小时");
                if (_sw.Elapsed.Minutes > 0)
                    sb.Append(_sw.Elapsed.Minutes + "分");
                sb.Append(_sw.Elapsed.Seconds + "秒");
                sb.Append(_sw.Elapsed.Milliseconds + "毫秒");
                Refresh("全部操作完成！耗时：" + sb);
                _application.RefreshCloud();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != _threads)
                {
                    foreach (IpThread thread in _threads)
                    {
                        thread.Abort();
                    }
                    Refresh("正在终止验证，请稍后...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDbFile.Text.Trim() == "")
                {
                    MessageBox.Show(@"请先设置纯真IP数据库文件！");
                    btnBrowser_Click(sender, e);
                    return;
                }

                if (txtIPAdress.Text.Trim() == "")
                {
                    MessageBox.Show(@"请填写IP地址");
                    txtIPAdress.Focus();
                    return;
                }
                richTextBox1.Text = IpThread.SearchIp(this.txtIPAdress.Text.Trim(), this.txtDbFile.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private delegate void DelegateUpdate(string text);
    }
}