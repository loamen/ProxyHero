using System;
using System.Reflection;
using System.Text;
using Loamen.Common;

namespace ProxyHero.Common
{
    public class ConsoleEx
    {
        /// <summary>
        ///     信息窗口显示信息
        /// </summary>
        /// <param name="value"></param>
        public void WriteLine(string value)
        {
            try
            {
                Config.MainForm.OutputPage.AppendLine(value);
                LogHelper.Debug(value);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public void Debug(string value)
        {
            try
            {
                if (null != Config.MainForm && Config.LocalSetting.NeedDebug)
                {
                    Config.MainForm.OutputPage.AppendLine(DateTime.Now.ToString() + ":" + value);
                    LogHelper.Debug(value);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        ///     调试异常
        /// </summary>
        /// <param name="ex"></param>
        public void Debug(Exception ex)
        {
            try
            {
                if (null != Config.MainForm && Config.LocalSetting.NeedDebug)
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


                    Config.MainForm.OutputPage.AppendLine(DateTime.Now.ToString() + ":" + sb.ToString());
                    LogHelper.Error(ex);
                }
            }catch(Exception e)
            {
                LogHelper.Error(e);
            }
        }
    }
}