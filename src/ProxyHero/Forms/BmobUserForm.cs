using cn.bmob.io;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyHero
{
    public partial class BmobUserForm : BmobBaseForm
    {
        public BmobUserForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            //登录用户
            var future = Bmob.LoginTaskAsync<BmobUser>(username.Text.Trim(), password.Text.Trim());
            try
            {
                FinishedCallback(future.Result, result);
                if(BmobUser.CurrentUser != null)
                {
                    this.Close();
                }
            }
            catch
            {
                result.Text = "登录失败，原因：" + future.Exception.InnerException.ToString();
            }
        }

        private void regBtn_Click(object sender, EventArgs e)
        {
            var form = new BmobUserRegForm();
            form.Show();
            this.Close();
        }

        private void BmobUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(BmobUser.CurrentUser == null)
            {
               // e.Cancel = true;
            }
        }
    }
}
