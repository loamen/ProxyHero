using System;
using System.IO;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.PH.Plugin.DownloadProxy.Entity;

namespace Loamen.PH.Plugin.DownloadProxy
{
    public partial class OptionDialog : Form
    {
        public OptionDialog()
        {
            InitializeComponent();
            txtWebsite.Text = "https://raw.githubusercontent.com/clarketm/proxy-list/master/proxy-list.txt";
            txtWebsite.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWebsite.Text.Trim()))
                return;

            Add(txtWebsite.Text.Trim());
        }

        private void Add(string url)
        {
            bool exist = false;
            foreach (ListViewItem li in listView1.Items)
            {
                if (li.Text.ToLower() == url.Trim())
                {
                    exist = true;
                    break;
                }
            }

            if (!exist)
            {
                var lvi = new ListViewItem(url);
                listView1.Items.Add(lvi);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("请选择要删除的网址！");
                    return;
                }

                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    listView1.Items.Remove(listView1.SelectedItems[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var setting = new DownloadSetting();
                foreach (ListViewItem li in listView1.Items)
                {
                    var web = new Website();
                    web.Url = li.SubItems[0].Text;
                    setting.Websites.Add(web);
                }

                if (setting.Websites.Count > 0)
                    XmlHelper.XmlSerialize(Download.DownloadSettingFileName, setting, typeof (DownloadSetting));
                else if (File.Exists(Download.DownloadSettingFileName))
                    File.Delete(Download.DownloadSettingFileName);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OptionDialog_Load(object sender, EventArgs e)
        {
            if (File.Exists(Download.DownloadSettingFileName))
            {
                var setting =
                    XmlHelper.XmlDeserialize(Download.DownloadSettingFileName, typeof (DownloadSetting)) as
                    DownloadSetting;
                if (null != setting && setting.Websites.Count > 0)
                {
                    foreach (Website website in setting.Websites)
                    {
                        var lvi = new ListViewItem(website.Url);
                        listView1.Items.Add(lvi);
                    }
                }
            }
        }
    }
}