using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Loamen.PluginFramework;

namespace Loamen.PH.Plugin.Security
{
    public partial class SecurityForm : DockPage
    {
        private IApp app;

        public SecurityForm(IApp _app)
        {
            app = _app;
            InitializeComponent();
        }

        private string Code
        {
            get
            {
                string code = tstbCode.Text.Trim();
                code = code == "" ? "Loamen.Com" : code;
                return code;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            rtbResult.Text = SecurityHelper.EncryptDES(rtbSource.Text, Code);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            rtbResult.Text = SecurityHelper.DecryptDES(rtbSource.Text, Code);
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbResult.Text.Trim() != "")
                {
                    var objSave = new SaveFileDialog();
                    objSave.Filter = "龙门加密脚本(*.lm)|*.lm|c#文件(*.cs)|*.cs|所有文件(*.*)|*.*";

                    objSave.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".lm";
                    if (objSave.ShowDialog() == DialogResult.OK)
                    {
                        using (var objWriter = new StreamWriter(objSave.FileName, false, Encoding.UTF8))
                        {
                            objWriter.Write(rtbResult.Text);
                        }
                    }
                }
                else
                {
                    ShowMsg(0, "没有要保存的数据！");
                }
            }
            catch (Exception ex)
            {
                ShowMsg(1, ex.Message);
            }
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            try
            {
                var objOpen = new OpenFileDialog();
                objOpen.Filter = "c#文件(*.cs)|*.cs|龙门加密脚本(*.lm)|*.lm|所有文件(*.*)|*.*";

                if (objOpen.ShowDialog() == DialogResult.OK)
                {
                    using (var objReader = new StreamReader(objOpen.FileName, Encoding.UTF8, false))
                    {
                        rtbSource.Text = objReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(1, ex.Message);
            }
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            rtbSource.Clear();
            rtbResult.Clear();
        }
    }
}