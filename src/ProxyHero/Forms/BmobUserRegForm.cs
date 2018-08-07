using cn.bmob.api;
using cn.bmob.io;
using ProxyHero.Entity;
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
    public partial class BmobUserRegForm : BmobBaseForm
    {
        public BmobUserRegForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            var form = new BmobUserForm();
            form.Show();
            this.Close();
        }

        private void regBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNickName.Text.Trim()))
            {
                MessageBox.Show("请填写昵称！");
                return;
            }

            if (string.IsNullOrWhiteSpace(username.Text.Trim()))
            {
                MessageBox.Show("请填写帐号！");
                return;
            }

            if (string.IsNullOrWhiteSpace(username.Text.Trim()))
            {
                MessageBox.Show("请填写帐号！");
                return;
            }

            //注册用户
            var user = new UserEntity();
            user.username = username.Text.Trim();
            user.password = password.Text.Trim();
            user.NickName = txtNickName.Text.Trim();
            user.WechatOpenId = string.Empty;

            user.Sex = new BmobBoolean(false);
            var future = Bmob.CreateTaskAsync<UserEntity>(user);
            try
            {
                FinishedCallback(future.Result, result);
            }
            catch
            {
                MessageBox.Show("注册失败，原因：" + future.Exception.InnerException.ToString());
            }
        }
    }
}
