using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.Net;
using ProxyHero.Common;

namespace ProxyHero
{
    public partial class ExceptionForm : Form
    {
        private readonly StringBuilder _stringBuilder;
        private HttpHelper _httpHelper;

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
#if DEBUG
                LogHelper.WriteException(ex);
#endif

                _stringBuilder.Append("\n【操作系统版本】: ");
                _stringBuilder.Append(OSVersion.VersionString + " Language:" + CultureInfo.InstalledUICulture.Name);
                _stringBuilder.Append("\n【PH主程序版本】: ");
                _stringBuilder.Append(Assembly.GetExecutingAssembly().GetName().Version);
                _stringBuilder.Append("\n【IE浏览器版本】: ");
                _stringBuilder.Append(OSVersion.InternetExplorerVersion);
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
                var name = ih.Location + "网友";

                if (PostGuestBook(_stringBuilder.ToString(), name, txtQQ.Text.Trim()))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MsgBox.ShowMessage("发送反馈失败，请检查网络设置！\nSend Failed!");
                }
            }
            catch (Exception exx)
            {
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
            //DelegateVoid needVerifyCode = new DelegateVoid(NeedVerifyCode);
            //this.BeginInvoke(needVerifyCode);
        }

        private delegate void DelegateVoid();

        public bool PostGuestBook(string verifyCode, string message)
        {
            try
            {
                var title = "异常" + DateTime.Now.ToString("yyyyMMddhhmmss");
                var ih = new IpHelper(NetHelper.LocalPublicIp);
                //拼凑登陆参数
                string postData = string.Format(
                    "title={0}&validate={1}&vdcode2={2}&uname={3}&img=01&action=save&msg={4}",
                    title,
                    verifyCode,
                    verifyCode,
                    ih.Location + "网友",
                    Microsoft.JScript.GlobalObject.escape(message));

                string result = _httpHelper.GetHtml("http://www.loamen.com/plus/guestbook.php", postData, Encoding.GetEncoding("UTF-8"));

                if (result.Contains(title))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool PostGuestBook(string message, string name, string email)
        {
            try
            {
                string title = "异常" + DateTime.Now.ToString("yyyyMMddhhmmss");
                IpHelper ih = new IpHelper(NetHelper.LocalPublicIp, true);


                #region values
                //System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
                //VarPost.Add("action", "save");
                //VarPost.Add("title", title);
                //VarPost.Add("validate", "SendException");
                //byte[] strUtf8 = System.Text.Encoding.GetEncoding("GB2312").GetBytes(ih.Location + "网友");
                //VarPost.Add("uname", Encoding.GetEncoding("GB2312").GetString(strUtf8));
                //VarPost.Add("qq", "");
                //VarPost.Add("email", "");
                //VarPost.Add("homepage", "http://www.loamen.com");
                //VarPost.Add("msg", message);
                //VarPost.Add("img", "01");
                #endregion

                Random ran = new Random();
                int imgIndex = ran.Next(1, 20);
                string img = imgIndex.ToString().Length == 1 ? "0" + imgIndex : imgIndex.ToString();

                //拼凑登陆参数
                string postData = string.Format(
                    "title={0}&validate={1}&homepage={2}&uname={3}&img={4}&action=save&msg={5}&email={6}",
                    title,
                    "sendexception",
                    "http://www.loamen.com",
                    string.IsNullOrEmpty(name) ? ih.Location + "网友" : name,
                    img,
                    message,
                    email);

                var sRemoteInfo = _httpHelper.GetHtml("http://www.loamen.com/plus/guestbook.php", postData, Encoding.UTF8);
               

                if (sRemoteInfo.Contains("成功"))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}