using System;
using System.Reflection;
using System.Text;
using Loamen.Common;

namespace ProxyHero.Common
{
    public class ConsoleEx
    {
        private void WriteText(string value)
        {
            Config.MainForm.InfoPage.RichBox.AppendText(value);
        }

        /// <summary>
        ///     输出
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            if (Config.MainForm != null)
            {
                DoSomething doSth = WriteText;
                Config.MainForm.InfoPage.Invoke(doSth, new object[] {DateTime.Now.ToString() + ":" + value});
            }
        }

        /// <summary>
        ///     信息窗口显示信息
        /// </summary>
        /// <param name="value"></param>
        public void WriteLine(string value)
        {
            if (Config.MainForm != null)
            {
                DoSomething doSth = WriteText;
                Config.MainForm.InfoPage.Invoke(doSth, new object[] {DateTime.Now.ToString() + ":" + value + "\n"});
            }
        }

        public void Debug(string value)
        {
            if (null != Config.MainForm && Config.LocalSetting.NeedDebug)
            {
                DoSomething doSth = WriteText;
                Config.MainForm.InfoPage.Invoke(doSth, new object[] {DateTime.Now.ToString() + ":" + value + "\n"});
            }
        }

        /// <summary>
        ///     调试异常
        /// </summary>
        /// <param name="ex"></param>
        public void Debug(Exception ex)
        {
            if (null != Config.MainForm && Config.LocalSetting != null && Config.LocalSetting.NeedDebug)
            {
                var sb = new StringBuilder("ErrorMessage:" + ex.Message);
                sb.Append("\nType:" + ex.GetType());
                sb.Append("\nSource:" + ex.Source);
                sb.Append("\nTargetSite:" + ex.TargetSite);
                sb.Append("\nStack Trace:" + ex.StackTrace);

                sb.Append("\n【OS Version】: ");
                sb.Append(OSVersion.VersionString);
                sb.Append("\n【PH Version】: ");
                sb.Append(Assembly.GetExecutingAssembly().GetName().Version);
                sb.Append("\n【IEV ersion】: ");
                sb.Append(OSVersion.InternetExplorerVersion);

                DoSomething doSth = WriteText;
                Config.MainForm.InfoPage.Invoke(doSth, new object[] {DateTime.Now.ToString() + ":" + sb + "\n"});
            }
        }

        private delegate void DoSomething(string value);
    }
}