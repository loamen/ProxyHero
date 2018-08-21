using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Loamen.Common
{
    [StructLayout(LayoutKind.Sequential)]
    public class OSVersionInfo
    {
        public int OSVersionInfoSize;
        public int MajorVersion;
        public int MinorVersion;
        public int BuildNumber;
        public int PlatformId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string versionString;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSVersionInfo2
    {
        public int OSVersionInfoSize;
        public int MajorVersion;
        public int MinorVersion;
        public int BuildNumber;
        public int PlatformId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string versionString;
    }

    public class LibWrap
    {
        [DllImport("kernel32")]
        public static extern bool GetVersionEx([In, Out] OSVersionInfo osvi);

        [DllImport("kernel32", EntryPoint = "GetVersionEx")]
        public static extern bool GetVersionEx2(ref OSVersionInfo2 osvi);
    }


    public class OSVersion
    {
        public static string VersionString
        {
            get
            {
                Console.WriteLine("\nPassing OSVersionInfo as class");

                var osvi = new OSVersionInfo();
                osvi.OSVersionInfoSize = Marshal.SizeOf(osvi);

                LibWrap.GetVersionEx(osvi);

                return string.Format("{0}(Builder {1}.{2}.{3})",
                                     OpSysName(osvi.MajorVersion, osvi.MinorVersion, osvi.PlatformId), osvi.MajorVersion,
                                     osvi.MinorVersion, osvi.BuildNumber);
            }
        }

        /// <summary>
        ///     获得IE版本
        /// </summary>
        public static string InternetExplorerVersion
        {
            get
            {
                string version = "";
                using (
                    RegistryKey versionKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer"))
                {
                    version = versionKey.GetValue("Version").ToString();
                }
                return version;
            }
        }


        public static string OpSysName(int MajorVersion, int MinorVersion, int PlatformId)
        {
            String str_opn = String.Format("{0}.{1}", MajorVersion, MinorVersion);

            switch (str_opn)
            {
                case "4.0":
                    return win95_nt40(PlatformId);
                case "4.10":
                    return "Windows 98";
                case "4.90":
                    return "Windows Me";
                case "3.51":
                    return "Windows NT 3.51";
                case "5.0":
                    return "Windows 2000";
                case "5.1":
                    return "Windows XP";
                case "5.2":
                    return "Windows Server 2003";
                case "6.0":
                    return "Windows Vista";
                case "6.1":
                    return "Windows 7";
                case "6.2":
                    return "Windows 10";
                default:
                    return "Unknown version";
            }
        }

        public static string win95_nt40(int PlatformId)
        {
            switch (PlatformId)
            {
                case 1:
                    return "Windows 95";
                case 2:
                    return "Windows NT 4.0";
                default:
                    return "This windows version is not distinguish!";
            }
        }
    }
}