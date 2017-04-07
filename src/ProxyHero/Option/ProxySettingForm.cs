using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ProxyHero.Common;

namespace ProxyHero.Option
{
    public partial class ProxySettingForm : Form
    {
        public ProxySettingForm()
        {
            InitializeComponent();

            if (Config.IsChineseLanguage)
            {
                Text = "内置浏览器代理设置";
                chbEnable.Text = "为内置浏览器使用代理设置";
            }
            else
            {
                Text = "BuiltinBrowser Proxy Setting";
                chbEnable.Text = "Set Proxy Server";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chbEnable.Checked)
            {
                string proxy = txtProxy.Text.Trim();
                var regex =
                    new Regex(
                        @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\:(?<Port>\d{1,5})");
                MatchCollection matchs = regex.Matches(proxy);
                if (matchs.Count == 1)
                {
                    AutoSwitchingHelper.StartBrowserProxy(proxy);
                }
            }
            else
            {
                AutoSwitchingHelper.StartBrowserProxy("");
            }
            Close();
        }

        private void chbEnable_CheckedChanged(object sender, EventArgs e)
        {
            txtProxy.Enabled = chbEnable.Checked;
        }

        private void frmProxySetting_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.BuiltinBrowserProxyServer))
            {
                txtProxy.Text = Config.BuiltinBrowserProxyServer;
                chbEnable.Checked = true;
            }

            if (Config.IsChineseLanguage)
            {
                labelInfo.Text = "HTTP代理格式：127.0.0.1:80\nSOCKS代理格式：socks=127.0.0.1:80";
            }
            else
            {
                labelInfo.Text = "HTTP Like:127.0.0.1:80\nSOCKS Like:socks=127.0.0.1:80";
            }
        }
    }
}