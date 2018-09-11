using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.Net;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.Net;

namespace ProxyHero
{
    public partial class ExceptionForm : Form
    {
        private readonly StringBuilder _stringBuilder;
        private HttpHelper _httpHelper;
        private delegate bool DelegateVoid(string message, string userIp, string email);

        public ExceptionForm(Exception ex)
        {
            try
            {
                InitializeComponent();
                _stringBuilder = new StringBuilder("错误信息:" + ex.Message);
                _stringBuilder.Append("\nType:" + ex.GetType());
                _stringBuilder.Append("\nSource:" + ex.Source);
                _stringBuilder.Append("\nTargetSite:" + ex.TargetSite);
                _stringBuilder.Append("\nStack Trace:" + ex.StackTrace);

                txtMessage.Text = _stringBuilder.ToString();

                _stringBuilder.Append("\n【操作系统版本】: ");
                _stringBuilder.Append(OSVersion.VersionString + " Language:" + CultureInfo.InstalledUICulture.Name);
                _stringBuilder.Append("\n【PH主程序版本】: ");
                _stringBuilder.Append(Assembly.GetExecutingAssembly().GetName().Version);
                _stringBuilder.Append("\n【IE浏览器版本】: ");
                _stringBuilder.Append(OSVersion.InternetExplorerVersion);

                Config.ConsoleEx.Debug(ex);
            }
            catch (Exception exx)
            {
                MsgBox.ShowMessage(exx.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                var ih = new IpHelper(NetHelper.LocalPublicIp, true);
                var userIp = ih.IpAddress;

                var d = new DelegateVoid(this.PostGuestBook);
                var result = this.Invoke(d, new[] { _stringBuilder.ToString(), userIp, txtQQ.Text.Trim() });

                if ((bool)result)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    DialogResult = DialogResult.No;
                    MsgBox.ShowMessage("发送反馈失败，请检查网络设置！\nSend Failed!");
                }
            }
            catch (Exception exx)
            {
                DialogResult = DialogResult.No;
                MsgBox.ShowMessage(exx.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Config.MainForm.ClosingControl = "ToolStripMenuItem";
            Application.Exit();
        }

        private void frmException_Load(object sender, EventArgs e)
        {
            _httpHelper = new HttpHelper();
        }




        public bool PostGuestBook(string message, string userIp, string email)
        {
            try
            {
                string title = "异常" + DateTime.Now.ToString("yyyyMMddhhmmss");

                var log = new OperatorLog();
                log.contact = email;
                log.message = message;
                log.title = title;
                log.type = "Exception";
                log.userip = userIp;

                var apiHelper = new ApiHelper();

                var result = apiHelper.AddExceptionLog(log);

                if (!string.IsNullOrEmpty(result))
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                Config.ConsoleEx.Debug(ex);
                return false;
            }
        }
    }
}