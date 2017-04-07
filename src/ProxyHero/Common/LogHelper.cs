using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Loamen.Common;

namespace ProxyHero.Common
{
    public class LogHelper
    {
        /// <summary>
        ///     写错误日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteException(Exception ex)
        {
#if DEBUG
            WriteExp(ex);
#else
            WriteLog(ex.Message);
#endif
        }

        /// <summary>
        ///     记录日志
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLog(string text)
        {
            try
            {
                string fileName = Config.ProxyHeroPath + @"\Log\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                var fi = new FileInfo(fileName);
                if (!fi.Directory.Exists) fi.Directory.Create();

                using (var objWriter = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    objWriter.WriteLine(DateTime.Now + " " + text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        ///     写错误日志
        /// </summary>
        /// <param name="ex"></param>
        private static void WriteExp(Exception ex)
        {
            try
            {
                var sb = new StringBuilder("错误信息:" + ex.Message);
                sb.Append("\nType:" + ex.GetType());
                sb.Append("\nSource:" + ex.Source);
                sb.Append("\nTargetSite:" + ex.TargetSite);
                sb.Append("\nStack Trace:" + ex.StackTrace);
                sb.Append("【操作系统版本】: ");
                sb.Append(OSVersion.VersionString + " Language:" + CultureInfo.InstalledUICulture.Name);
                sb.Append("【PH主程序版本】: ");
                sb.Append(Assembly.GetExecutingAssembly().GetName().Version);
                sb.Append("【IE浏览器版本】: ");
                sb.Append(OSVersion.InternetExplorerVersion);
                sb.Append("\r\n\r\n");

                string fileName = Config.ProxyHeroPath + @"\Log\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                var fi = new FileInfo(fileName);
                if (!fi.Directory.Exists) fi.Directory.Create();

                using (var objWriter = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    objWriter.WriteLine(DateTime.Now + " " + sb);
                }
            }
            catch
            {
            }
        }
    }
}