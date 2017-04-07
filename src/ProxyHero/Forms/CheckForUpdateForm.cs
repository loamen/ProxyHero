using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProxyHero
{
    public partial class CheckForUpdateForm : Form
    {
        public CheckForUpdateForm()
        {
            InitializeComponent();
            label1.Text = "Current version: v" + Application.ProductVersion;
            label2.Text = "The latest version: v" + Config.ProxyHeroCloudSetting.Version;
        }

        private void Download_Click(object sender, EventArgs e)
        {
            Process.Start(Config.ProxyHeroCloudSetting.UpdateUrl);
        }
    }
}