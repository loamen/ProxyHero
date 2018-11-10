using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Loamen.Common;

namespace ProxyHero
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        [DllImport("user32.dll")]
        private static extern bool FlashWindowEx(int pfwi);

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool runone;
            var run = new Mutex(true, "ProxyHero", out runone);
            if (runone)
            {
                run.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                LogHelper.Init(Path.Combine(Application.StartupPath, "Configs", "log4net.config"));

                var formMain = new MainForm();
                int hdc = formMain.Handle.ToInt32();
                Application.Run(formMain);
                var a = new IntPtr(hdc);
            }
            else
            {
                ProxyHero.Common.MsgBox.ShowMessage("已经运行了一个实例！");
            }
        }
    }
}