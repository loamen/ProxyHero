using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Loamen.Net
{
    /// <summary>
    ///     NetHelper 。
    /// </summary>
    public static class NetHelper
    {
        #region IsPublicIPAddress

        /// <summary>
        ///     是否是公网IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsPublicIPAddress(string ip)
        {
            if (ip.StartsWith("10.")) //A类 10.0.0.0到10.255.255.255.255 
            {
                return false;
            }

            if (ip.StartsWith("172.")) //B类 172.16.0.0到172.31.255.255 
            {
                if (ip.Substring(6, 1) == ".")
                {
                    int secPart = int.Parse(ip.Substring(4, 2));
                    if ((16 <= secPart) && (secPart <= 31))
                    {
                        return false;
                    }
                }
            }

            if (ip.StartsWith("192.168.")) //C类 192.168.0.0到192.168.255.255 
            {
                return false;
            }

            return true;
        }

        #endregion

        #region ReceiveData

        /// <summary>
        ///     ReceiveData 从网络读取指定长度的数据
        /// </summary>
        public static byte[] ReceiveData(NetworkStream stream, int size)
        {
            var result = new byte[size];

            ReceiveData(stream, result, 0, size);

            return result;
        }

        /// <summary>
        ///     ReceiveData 从网络读取指定长度的数据 ，存放在buff中offset处
        /// </summary>
        public static void ReceiveData(NetworkStream stream, byte[] buff, int offset, int size)
        {
            int readCount = 0;
            int totalCount = 0;
            int curOffset = offset;

            while (totalCount < size)
            {
                int exceptSize = size - totalCount;
                readCount = stream.Read(buff, curOffset, exceptSize);
                if (readCount == 0)
                {
                    throw new IOException("NetworkStream Interruptted !");
                }
                curOffset += readCount;
                totalCount += readCount;
            }
        }

        #endregion

        #region GetRemotingHanler

        //前提是已经注册了remoting通道
        public static object GetRemotingHanler(string channelTypeStr, string ip, int port, string remotingServiceName,
                                               Type destInterfaceType)
        {
            try
            {
                string remoteObjUri = string.Format("{0}://{1}:{2}/{3}", channelTypeStr, ip, port, remotingServiceName);
                return Activator.GetObject(destInterfaceType, remoteObjUri);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region GetLocalIp

        /// <summary>
        ///     获取本机首个IP地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress FirstLocalIp
        {
            get
            {
                string hostName = Dns.GetHostName();
                IPHostEntry hEntry = Dns.Resolve(hostName);

                return hEntry.AddressList[0];
            }
        }

        /// <summary>
        ///     获取本机的公网IP地址
        /// </summary>
        public static string LocalPublicIp
        {
            get
            {
                string ipAddress = GetPublicIP();
                if (IsPublicIPAddress(ipAddress))
                {
                    return ipAddress;
                }

                return null;
            }
        }

        /// <summary>
        ///     GetLocalIp 获取本机的IP地址组
        /// </summary>
        public static IPAddress[] GetLocalIp()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry hEntry = Dns.Resolve(hostName);

            return hEntry.AddressList;
        }

        /// <summary>
        ///     获取本机外部IP
        /// </summary>
        /// <returns></returns>
        private static string GetPublicIP()
        {
            try
            {
                var client = new WebClient();
                client.Proxy = null;
                client.Credentials = CredentialCache.DefaultCredentials;
                client.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)");
                client.Headers.Add("Host", "ip.cn");

                string str = client.DownloadString("https://ip.cn"); //下载网页数据 
                string r =
                    @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])";
                string ip = Regex.Match(str, r).ToString(); //提取信息                 
                return ip;
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region IsConnectedToInternet

        /// <summary>
        ///     IsConnectedToInternet 机器是否联网
        /// </summary>
        public static bool IsConnectedToInternet
        {
            get
            {
                int Desc = 0;
                return InternetGetConnectedState(ref Desc, 0);
            }
        }

        /// <summary>
        ///     获取网络状态
        /// </summary>
        public static string InternetConnectedState
        {
            get
            {
                int INTERNET_CONNECTION_MODEM = 1;
                int INTERNET_CONNECTION_LAN = 2;
                int INTERNET_CONNECTION_PROXY = 4;
                int INTERNET_CONNECTION_MODEM_BUSY = 8;

                string outPut = null;
                var flags = new int(); //上网方式 
                bool m_bOnline = true; //是否在线 

                m_bOnline = InternetGetConnectedState(ref flags, 0);
                if (m_bOnline) //在线   
                {
                    if ((flags & INTERNET_CONNECTION_MODEM) == INTERNET_CONNECTION_MODEM)
                    {
                        outPut = "拨号上网";
                    }
                    if ((flags & INTERNET_CONNECTION_LAN) == INTERNET_CONNECTION_LAN)
                    {
                        outPut = "通过局域网";
                    }
                    if ((flags & INTERNET_CONNECTION_PROXY) == INTERNET_CONNECTION_PROXY)
                    {
                        outPut = "代理上网";
                    }
                    if ((flags & INTERNET_CONNECTION_MODEM_BUSY) == INTERNET_CONNECTION_MODEM_BUSY)
                    {
                        outPut = "MODEM被其他非Internet连接占用";
                    }
                }
                else
                {
                    outPut = "未连接Internet";
                }

                return outPut;
            }
        }

        public static string IsNetworkAlive()
        {
            int NETWORK_ALIVE_LAN = 0;
            int NETWORK_ALIVE_WAN = 2;
            int NETWORK_ALIVE_AOL = 4;

            string outPut = null;
            int flags; //上网方式 
            bool m_bOnline = true; //是否在线 

            m_bOnline = IsNetworkAlive(out flags);
            if (m_bOnline) //在线   
            {
                if ((flags & NETWORK_ALIVE_LAN) == NETWORK_ALIVE_LAN)
                {
                    outPut = "在线：NETWORK_ALIVE_LAN\n";
                }
                if ((flags & NETWORK_ALIVE_WAN) == NETWORK_ALIVE_WAN)
                {
                    outPut = "在线：NETWORK_ALIVE_WAN\n";
                }
                if ((flags & NETWORK_ALIVE_AOL) == NETWORK_ALIVE_AOL)
                {
                    outPut = "在线：NETWORK_ALIVE_AOL\n";
                }
            }
            else
            {
                outPut = "不在线\n";
            }

            return outPut;
        }


        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(ref int Description, int ReservedValue);

        [DllImport("sensapi.dll")]
        private static extern bool IsNetworkAlive(out int connectionDescription);

        #endregion

        #region DownLoadFileFromUrl

        /// <summary>
        ///     DownLoadFileFromUrl 将url处的文件下载到本地
        /// </summary>
        public static void DownLoadFileFromUrl(string url, string saveFilePath)
        {
            var fstream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write);
            WebRequest wRequest = WebRequest.Create(url);

            try
            {
                WebResponse wResponse = wRequest.GetResponse();
                var contentLength = (int) wResponse.ContentLength;

                var buffer = new byte[1024];
                int read_count = 0;
                int total_read_count = 0;
                bool complete = false;

                while (!complete)
                {
                    read_count = wResponse.GetResponseStream().Read(buffer, 0, buffer.Length);

                    if (read_count > 0)
                    {
                        fstream.Write(buffer, 0, read_count);
                        total_read_count += read_count;
                    }
                    else
                    {
                        complete = true;
                    }
                }

                fstream.Flush();
            }
            finally
            {
                fstream.Close();
                wRequest = null;
            }
        }

        #endregion
    }

    #region ADSL

    public struct RASCONN
    {
        public int dwSize;
        public IntPtr hrasconn;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)] public string szDeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)] public string szDeviceType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)] public string szEntryName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct RasStats
    {
        public int dwSize;
        public int dwBytesXmited;
        public int dwBytesRcved;
        public int dwFramesXmited;
        public int dwFramesRcved;
        public int dwCrcErr;
        public int dwTimeoutErr;
        public int dwAlignmentErr;
        public int dwHardwareOverrunErr;
        public int dwFramingErr;
        public int dwBufferOverrunErr;
        public int dwCompressionRatioIn;
        public int dwCompressionRatioOut;
        public int dwBps;
        public int dwConnectionDuration;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct RasEntryName
    {
        public int dwSize;
        //[MarshalAs(UnmanagedType.ByValTStr,SizeConst=(int)RasFieldSizeConstants.RAS_MaxEntryName + 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256 + 1)] public string szEntryName;
#if WINVER5
          public int dwFlags;
          [MarshalAs(UnmanagedType.ByValTStr,SizeConst=260+1)]
          public string szPhonebookPath;
#endif
    }

    public enum DEL_CACHE_TYPE //要删除的类型。
    {
        File, //表示internet临时文件
        Cookie //表示Cookie
    }

    public class RAS
    {
        [DllImport("Rasapi32.dll", EntryPoint = "RasEnumConnectionsA",
            SetLastError = true)]
        internal static extern int RasEnumConnections
            (
            ref RASCONN lprasconn, // buffer to receive connections data
            ref int lpcb, // size in bytes of buffer
            ref int lpcConnections // number of connections written to buffer
            );


        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        internal static extern uint RasGetConnectionStatistics(
            IntPtr hRasConn, // handle to the connection
            [In, Out] RasStats lpStatistics // buffer to receive statistics
            );

        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        public static extern uint RasHangUp(
            IntPtr hrasconn // handle to the RAS connection to hang up
            );

        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        public static extern uint RasEnumEntries(
            string reserved, // reserved, must be NULL
            string lpszPhonebook, // pointer to full path and
            //  file name of phone-book file
            [In, Out] RasEntryName[] lprasentryname, // buffer to receive
            //  phone-book entries
            ref int lpcb, // size in bytes of buffer
            out int lpcEntries // number of entries written
            //  to buffer
            );

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public static extern int InternetDial(
            IntPtr hwnd,
            [In] string lpszConnectoid,
            uint dwFlags,
            ref int lpdwConnection,
            uint dwReserved
            );
    }

    public class RASDisplay
    {
        private readonly IntPtr m_ConnectedRasHandle;
        private readonly string m_ConnectionName;
        private readonly string[] m_ConnectionNames;
        private readonly double m_RX;
        private readonly double m_TX;
        private readonly bool m_connected;
        private readonly string m_duration;

        private RasStats status = new RasStats();

        public RASDisplay()
        {
            m_connected = true;

            var lpras = new RAS();
            var lprasConn = new RASCONN();

            lprasConn.dwSize = Marshal.SizeOf(typeof (RASCONN));
            lprasConn.hrasconn = IntPtr.Zero;

            int lpcb = 0;
            int lpcConnections = 0;
            int nRet = 0;
            lpcb = Marshal.SizeOf(typeof (RASCONN));

            nRet = RAS.RasEnumConnections(ref lprasConn, ref lpcb, ref
                                                                       lpcConnections);

            if (nRet != 0)
            {
                m_connected = false;
                return;
            }

            if (lpcConnections > 0)
            {
                //for (int i = 0; i < lpcConnections; i++)

                //{
                var stats = new RasStats();

                m_ConnectedRasHandle = lprasConn.hrasconn;
                RAS.RasGetConnectionStatistics(lprasConn.hrasconn, stats);


                m_ConnectionName = lprasConn.szEntryName;

                int Hours = 0;
                int Minutes = 0;
                int Seconds = 0;

                Hours = ((stats.dwConnectionDuration/1000)/3600);
                Minutes = ((stats.dwConnectionDuration/1000)/60) - (Hours*60);
                Seconds = ((stats.dwConnectionDuration/1000)) - (Minutes*60) - (Hours*3600);


                m_duration = Hours + " hours " + Minutes + " minutes " + Seconds + " secs";
                m_TX = stats.dwBytesXmited;
                m_RX = stats.dwBytesRcved;
                //}
            }
            else
            {
                m_connected = false;
            }


            int lpNames = 1;
            int entryNameSize = 0;
            int lpSize = 0;
            RasEntryName[] names = null;
            entryNameSize = Marshal.SizeOf(typeof (RasEntryName));
            lpSize = lpNames*entryNameSize;
            names = new RasEntryName[lpNames];
            names[0].dwSize = entryNameSize;
            uint retval = RAS.RasEnumEntries(null, null, names, ref lpSize, out lpNames);

            //if we have more than one connection, we need to do it again
            if (lpNames > 1)
            {
                names = new RasEntryName[lpNames];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i].dwSize = entryNameSize;
                }

                retval = RAS.RasEnumEntries(null, null, names, ref lpSize, out lpNames);
            }
            m_ConnectionNames = new string[names.Length];


            if (lpNames > 0)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    m_ConnectionNames[i] = names[i].szEntryName;
                }
            }
        }

        public string Duration
        {
            get { return m_connected ? m_duration : ""; }
        }

        /// <summary>
        ///     所有连接
        /// </summary>
        [Description("获取所有的ADSL连接")]
        public string[] Connections
        {
            get { return m_ConnectionNames; }
        }

        public double BytesTransmitted
        {
            get { return m_connected ? m_TX : 0; }
        }

        public double BytesReceived
        {
            get { return m_connected ? m_RX : 0; }
        }

        /// <summary>
        ///     连接名称
        /// </summary>
        [Description("连接名称")]
        public string ConnectionName
        {
            get { return m_connected ? m_ConnectionName : ""; }
        }

        /// <summary>
        ///     是否已连接
        /// </summary>
        [Description("是否已连接")]
        public bool IsConnected
        {
            get { return m_connected; }
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public static extern bool DeleteUrlCacheEntry(
            DEL_CACHE_TYPE type
            );

        /// <summary>
        ///     连接到某个ADSL连接
        /// </summary>
        /// <param name="Connection"></param>
        /// <returns></returns>
        [Description("ADSL")]
        public int Connect(string Connection)
        {
            int temp = 0;
            uint INTERNET_AUTO_DIAL_UNATTENDED = 2;
            int retVal = RAS.InternetDial(IntPtr.Zero, Connection, INTERNET_AUTO_DIAL_UNATTENDED, ref temp, 0);
            return retVal;
        }

        /// <summary>
        ///     断开连接
        /// </summary>
        [Description("断开连接")]
        public void Disconnect()
        {
            RAS.RasHangUp(m_ConnectedRasHandle);
        }
    }

    #endregion
}