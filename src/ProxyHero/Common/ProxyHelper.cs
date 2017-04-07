using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Loamen.Common;
using Loamen.Net;
using Microsoft.Win32;

namespace ProxyHero.Common
{
    public class ProxyHelper
    {
        #region Variable

        private const int INTERNET_OPTION_REFRESH = 0x000025;
        private const int INTERNET_OPTION_SETTINGS_CHANGED = 0x000027;

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lPBuffer,
                                                     int lpdwBufferLength);

        #endregion

        #region Constructors

        #endregion

        #region Public Method

        /// <summary>
        ///     注册表所有权限
        /// </summary>
        /// <returns></returns>
        public static RegistrySecurity RegistryFullRight
        {
            get
            {
                var rsy = new RegistrySecurity();
                var rar = new RegistryAccessRule(Environment.UserDomainName +
                                                 "\\" + Environment.UserName,
                                                 RegistryRights.FullControl | RegistryRights.ReadKey |
                                                 RegistryRights.WriteKey |
                                                 RegistryRights.Delete, InheritanceFlags.ContainerInherit,
                                                 PropagationFlags.None,
                                                 AccessControlType.Allow);
                rsy.AddAccessRule(rar);

                return rsy;
            }
        }

        /// <summary>
        ///     注册表只读权限
        /// </summary>
        /// <returns></returns>
        public static RegistrySecurity RegistryReadOnlyRight
        {
            get
            {
                var rsy = new RegistrySecurity();
                var rar = new RegistryAccessRule(Environment.UserDomainName +
                                                 "\\" + Environment.UserName, RegistryRights.ReadKey,
                                                 InheritanceFlags.ContainerInherit, PropagationFlags.None,
                                                 AccessControlType.Allow);
                rsy.AddAccessRule(rar);

                return rsy;
            }
        }

        /// <summary>
        ///     获取默认的拨号连接
        /// </summary>
        public static string DefaultADSLName
        {
            get
            {
                string result = "";

                var ras = new RASDisplay();
                result = ras.ConnectionName;

                if (result == "")
                {
                    RegistryKey pregkey;
                    pregkey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\RAS AutoDial\\Default", true);
                    if (pregkey == null)
                    {
                        result = "";
                    }
                    else
                    {
                        result = pregkey.GetValue("DefaultInternet") + "";
                    }
                }

                return result;
            }
        }

        /// <summary>
        ///     让IE支持WAP
        /// </summary>
        public void SetIESupportWap()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(
                @"Software\Classes\MIME\Database\Content Type\text/vnd.wap.wml",
                true);

            if (rk == null)
            {
                rk = Registry.CurrentUser.CreateSubKey(
                    @"Software\Classes\MIME\Database\Content Type\text/vnd.wap.wml");
            }

            if (rk.GetValue("CLSID") == null ||
                rk.GetValue("CLSID").ToString() != "{25336920-03F9-11cf-8FD0-00AA00686F13}")
            {
                rk.SetValue("CLSID", "{25336920-03F9-11cf-8FD0-00AA00686F13}");
            }

            rk = Registry.CurrentUser.OpenSubKey(
                @"Software\Classes\MIME\Database\Content Type\application/xhtml+xml",
                true);

            if (rk == null)
            {
                rk = Registry.CurrentUser.CreateSubKey(
                    @"Software\Classes\MIME\Database\Content Type\application/xhtml+xml");
            }

            if (rk.GetValue("CLSID") == null ||
                rk.GetValue("CLSID").ToString() != "{25336920-03F9-11cf-8FD0-00AA00686F13}")
            {
                //设置代理可用 
                rk.SetValue("CLSID", "{25336920-03F9-11cf-8FD0-00AA00686F13}");
            }

            if (rk.GetValue("Extension") == null || rk.GetValue("Extension").ToString() != ".xhtm")
            {
                //设置代理可用 
                rk.SetValue("Extension", ".xhtm");
            }

            if (rk.GetValue("Encoding") == null)
            {
                //设置代理可用 
                rk.SetValue("Encoding", new byte[] {08, 00, 00, 00}, RegistryValueKind.Binary);
            }


            rk.Close();
            //Reflush();
        }

        /// <summary>
        ///     设置代理
        /// </summary>
        /// <param name="ProxyServer"></param>
        /// <param name="EnableProxy"></param>
        /// <returns></returns>
        public void SetIEProxy(string ProxyServer, int EnableProxy)
        {
            if (NetHelper.IsPublicIPAddress(NetHelper.FirstLocalIp.ToString()))
            {
                string adslName = DefaultADSLName;
                if (string.IsNullOrEmpty(adslName))
                    return;
                SetADSLProxy(ProxyServer, EnableProxy, adslName);
            }
            else
            {
                SetLanProxy(ProxyServer, EnableProxy);
            }
            Config.MainForm.SetProxyStatusLabel();
        }

        /// <summary>
        ///     设置局域网
        /// </summary>
        /// <param name="ProxyServer"></param>
        /// <param name="EnableProxy"></param>
        public void SetLanProxy(string ProxyServer, int EnableProxy)
        {
            if (IsWindows7)
            {
                if (EnableProxy == 1)
                {
                    Proxies.SetProxy(ProxyServer);
                }
                else
                {
                    Proxies.UnsetProxy();
                }
            }
            else
            {
                //打开注册表键 
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                    true);

                //rk.SetAccessControl(RegistryFullRight);

                //设置代理是否可用 
                rk.SetValue("ProxyEnable", EnableProxy, RegistryValueKind.DWord);


                if (!ProxyServer.Equals("") && EnableProxy == 1)
                {
                    rk.SetValue("ProxyServer", ProxyServer, RegistryValueKind.String);
                }

                if (EnableProxy == 0)
                {
                    rk.SetValue("ProxyServer", "", RegistryValueKind.String);
                }

                rk.Close();
                Reflush();
            }

            if (GetIEProxy()[0] != EnableProxy.ToString())
            {
                //打开注册表键 
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                    true);

                //rk.SetAccessControl(RegistryFullRight);

                //设置代理是否可用 
                rk.SetValue("ProxyEnable", EnableProxy, RegistryValueKind.DWord);
                if (!ProxyServer.Equals("") && EnableProxy == 1)
                {
                    rk.SetValue("ProxyServer", ProxyServer, RegistryValueKind.String);
                }

                if (EnableProxy == 0)
                {
                    rk.SetValue("ProxyServer", "", RegistryValueKind.String);
                }
            }
        }

        /// <summary>
        ///     设置ADSL
        /// </summary>
        /// <param name="ProxyServer"></param>
        /// <param name="EnableProxy"></param>
        /// <param name="ADSLName"></param>
        public void SetADSLProxy(string ProxyServer, int EnableProxy, string ADSLName)
        {
            int i = (ProxyServer.Trim()).Length;
            var key = new byte[50];
            char[] source = (ProxyServer.Trim()).ToCharArray();
            key[0] = 60;
            key[4] = 3;
            key[8] = 3;
            key[12] = (byte) i;
            for (int ii = 0; ii < source.Length; ii++)
            {
                key[16 + ii] = ChangeTobyte(source[ii]);
            }
            string sDirectX = "";
            for (int k = 0; k < key.Length; k++)
            {
                if (key[k] != 0)
                {
                    sDirectX += key[k] + " ";
                }
            }
            //MessageBox.Show(sDirectX);
            RegistryKey pregkey =
                Registry.CurrentUser.OpenSubKey(
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections", true);
            if (pregkey != null)
            {
                if (EnableProxy == 1)
                {
                    pregkey.SetValue(ADSLName, key, RegistryValueKind.Binary);
                    //激活代理设置
                    Reflush();
                }
                else
                {
                    pregkey.DeleteValue(ADSLName, false);
                }
            }
        }

        //将值转换为注册表中的二进制值
        public byte ChangeTobyte(char i)
        {
            byte key = 0;
            switch (i)
            {
                case '0':
                    key = 48;
                    break;
                case '1':
                    key = 49;
                    break;
                case '2':
                    key = 50;
                    break;
                case '3':
                    key = 51;
                    break;
                case '4':
                    key = 52;
                    break;
                case '5':
                    key = 53;
                    break;
                case '6':
                    key = 54;
                    break;
                case '7':
                    key = 55;
                    break;
                case '8':
                    key = 56;
                    break;
                case '9':
                    key = 57;
                    break;
                case '.':
                    key = 46;
                    break;
                case ':':
                    key = 58;
                    break;
            }
            return key;
        }

        /// <summary>
        ///     获取当前使用的代理
        /// </summary>
        /// <returns></returns>
        public static string[] GetIEProxy()
        {
            string[] result = {"0", ""};
            //打开注册表键 
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                true);

            result[0] = rk.GetValue("ProxyEnable") == null ? "" : rk.GetValue("ProxyEnable").ToString();
            result[1] = rk.GetValue("ProxyServer") == null ? "" : rk.GetValue("ProxyServer").ToString();
            rk.Close();

            return result;
        }

        /// <summary>
        ///     设置主页
        /// </summary>
        public static string[] SetStartPage(string startPage, bool enable)
        {
            var result = new[] {"0", ""};
            try
            {
                string key = @"Software\Microsoft\Internet Explorer\Main";
                //打开注册表键 
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(
                    key,
                    true);
                RegistryKey rkUser = Registry.CurrentUser.OpenSubKey(
                    key,
                    true);

                // rk.SetAccessControl(RegistryFullRight);
                // rkUser.SetAccessControl(RegistryFullRight);

                if (enable)
                {
                    rk.SetValue("Start Page", startPage);
                    rk.SetValue("Default_Page_URL", startPage);
                    rkUser.SetValue("Start Page", startPage);
                    rkUser.SetValue("Default_Page_URL", startPage);
                }
                else
                {
                    rk.SetValue("Start Page", "about:blank");
                    rk.SetValue("Default-Page-URL", "about:blank");
                    rkUser.SetValue("Start Page", "about:blank");
                    rkUser.SetValue("Default-Page-URL", "about:blank");
                }

                // rk.SetAccessControl(RegistryReadOnlyRight);
                // rkUser.SetAccessControl(RegistryReadOnlyRight);

                rk.Close();
                rkUser.Close();
            }
            catch (Exception ex)
            {
                result[0] = "1";
                result[1] = ex.Message;
            }
            return result;
        }

        public static bool GetStartPage(string startPage)
        {
            bool result = false;
            try
            {
                string key = @"Software\Microsoft\Internet Explorer\Main";
                //打开注册表键 
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(
                    key,
                    true);
                RegistryKey rkUser = Registry.CurrentUser.OpenSubKey(
                    key,
                    true);

                rk.SetAccessControl(RegistryFullRight);
                rkUser.SetAccessControl(RegistryFullRight);


                if (rk.GetValue("Start Page").ToString() == startPage) return true;
                //if (rk.GetValue("Default_Page_URL").ToString() == startPage) return true;
                if (rkUser.GetValue("Start Page").ToString() == startPage) return true;
                //if (rkUser.GetValue("Default_Page_URL").ToString() == startPage) return true;

                rk.SetAccessControl(RegistryReadOnlyRight);
                rkUser.SetAccessControl(RegistryReadOnlyRight);

                rk.Close();
                rkUser.Close();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        ///     当键值名为“HomePage”的键值修改为“1”，则禁止修改IE主页
        /// </summary>
        /// <param name="enable">enable为true则禁止修改IE主页</param>
        public static void EnableModifyHomePage(bool enable)
        {
            //自定义RegistryKey对象实例
            RegistryKey reg =
                Registry.CurrentUser.CreateSubKey(@"SoftWare\Policies\Microsoft\Internet Explorer\Control Panel");

            reg.SetValue("HomePage", enable ? 1 : 0, RegistryValueKind.DWord);
        }

        #endregion

        #region PrivateMethods

        private void Reflush()
        {
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        #endregion

        public static bool IsWindows7
        {
            get
            {
                return
                    (Environment.OSVersion.Platform == PlatformID.Win32NT) &&
                    (Environment.OSVersion.Version.Major == 6) &&
                    (Environment.OSVersion.Version.Minor == 1);
            }
        }
    }
}