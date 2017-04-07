using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Loamen.Common;
using Loamen.Net.Entity;

namespace Loamen.Net
{
    public class IpHelper
    {
        private readonly bool _mIsChinese = true;
        private readonly string _mProxyAddress = "";
        private string _mIpAddress = "";
        private string _mLocation = "";
        private string _mProxyType = "";
        private string _mResponse = "";

        #region

        ///// <summary>
        ///// 代理匿名类型
        ///// </summary>
        //public string ProxyType
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (string.IsNullOrEmpty(m_ProxyType))
        //            {
        //                #region get proxy type
        //                Regex regex = new Regex(@"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\:(?<Port>\d{1,5})");
        //                MatchCollection matchs = regex.Matches(m_proxyAddress);
        //                if (matchs.Count == 1)
        //                {
        //                    ProxyEntity pe = new ProxyEntity();
        //                    pe.Ip = matchs[0].Groups["Proxy"].Value;
        //                    pe.Port = int.Parse(matchs[0].Groups["Port"].Value);

        //                    TestEntity te = new TestEntity();
        //                    te.TestUrl = "http://proxygo.com.ru/proxyjudge.php";
        //                    te.TestWebEncoding = "UTF-8";
        //                    te.TestWebTitle = "REMOTE_ADDR";

        //                    string localIp = NetHelper.LocalPublicIp;

        //                    string REMOTE_ADDR = "", HTTP_VIA = "", HTTP_X_FORWARDED_FOR = "";

        //                    string html = (string)HttpHelper.TestProxy(pe, te, 30)[2];
        //                    if (string.IsNullOrEmpty(html))
        //                    {
        //                        te.TestUrl = "http://www.helllabs.com.ua/cgi-bin/textenv.pl";
        //                        html = (string)HttpHelper.TestProxy(pe, te, 30)[2];
        //                    }
        //                    //html = StringHelper.GetMidString(html, "<PRE>", "</PRE>");

        //                    if (html.ToLower().Contains(te.TestWebTitle.ToLower()))
        //                    {
        //                        REMOTE_ADDR = StringHelper.GetMidString(html, "REMOTE_ADDR=", "\n");
        //                        HTTP_VIA = StringHelper.GetMidString(html, "HTTP_VIA=", "\n");
        //                        HTTP_X_FORWARDED_FOR = StringHelper.GetMidString(html, "HTTP_X_FORWARDED_FOR=", "\n");

        //                        string[] peIps = pe.Ip.Split('.');
        //                        string peIp2 = peIps[0] + "." + peIps[1];//代理前两位

        //                        peIps = localIp.Split('.');
        //                        string localip2 = peIps[0] + "." + peIps[1];//本地外部IP前两位

        //                        if (REMOTE_ADDR.Contains(localIp) &&
        //                            string.IsNullOrEmpty(HTTP_VIA) &&
        //                            string.IsNullOrEmpty(HTTP_X_FORWARDED_FOR))
        //                        {
        //                            m_ProxyType = "未使用代理";
        //                        }
        //                        else if ((REMOTE_ADDR.Contains(pe.Ip) || REMOTE_ADDR.Contains(peIp2) || !REMOTE_ADDR.Contains(localip2)) &&
        //                            (HTTP_X_FORWARDED_FOR.Contains(localIp) || HTTP_X_FORWARDED_FOR.Contains(localip2)))
        //                        {
        //                            m_ProxyType = "透明代理";
        //                        }
        //                        else if ((REMOTE_ADDR.Contains(pe.Ip) || REMOTE_ADDR.Contains(peIp2) || !REMOTE_ADDR.Contains(localip2)) &&
        //                            (HTTP_VIA.Contains(pe.Ip) || HTTP_VIA.Contains(peIp2) || HTTP_VIA.ToLower().Contains("proxy") || HTTP_VIA.ToLower().Contains("localhost") || HTTP_VIA.Contains(pe.Port.ToString()) || HTTP_VIA.Contains("1.1 ") || string.IsNullOrEmpty(HTTP_VIA)) &&
        //                            (HTTP_X_FORWARDED_FOR.Contains(pe.Ip) || HTTP_X_FORWARDED_FOR.Contains(peIp2) || string.IsNullOrEmpty(HTTP_X_FORWARDED_FOR) || HTTP_X_FORWARDED_FOR.Contains("unknown") || HTTP_X_FORWARDED_FOR.Contains("proxygo.com.ru")))
        //                        {
        //                            m_ProxyType = "匿名代理";
        //                        }
        //                        else if ((REMOTE_ADDR.Contains(pe.Ip) || REMOTE_ADDR.Contains(peIp2) || !REMOTE_ADDR.Contains(localip2)) &&
        //                            (HTTP_VIA.Contains(pe.Ip) || HTTP_VIA.Contains(peIp2)) &&
        //                            (!HTTP_X_FORWARDED_FOR.Contains(localIp) && !HTTP_X_FORWARDED_FOR.Contains(pe.Ip) && !HTTP_X_FORWARDED_FOR.Contains(peIp2) && !string.IsNullOrEmpty(HTTP_X_FORWARDED_FOR)))
        //                        {
        //                            m_ProxyType = "欺骗性匿名代理";
        //                        }
        //                        else if ((REMOTE_ADDR.Contains(pe.Ip) || REMOTE_ADDR.Contains(peIp2) || !REMOTE_ADDR.Contains(localip2)) &&
        //                            string.IsNullOrEmpty(HTTP_VIA) &&
        //                             string.IsNullOrEmpty(HTTP_X_FORWARDED_FOR))
        //                        {
        //                            m_ProxyType = "高匿名代理";
        //                        }
        //                        else
        //                        {
        //                            if (REMOTE_ADDR != "")
        //                            {
        //                                StringBuilder sb = new StringBuilder("透明代理；IP：" + localIp + "；");
        //                                sb.Append("REMOTE_ADDR：" + REMOTE_ADDR + "；");
        //                                sb.Append("HTTP_VIA：" + HTTP_VIA + "；");
        //                                sb.Append("HTTP_X_FORWARDED_FOR：" + HTTP_X_FORWARDED_FOR);
        //                                m_ProxyType = sb.ToString();
        //                            }
        //                            else
        //                            {
        //                                m_ProxyType = "";
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        m_ProxyType = html;
        //                    }
        //                }
        //                #endregion
        //            }
        //        }
        //        catch(Exception ex)
        //        {
        //            m_ProxyType = ex.Message;
        //        }

        //        return m_ProxyType;
        //    }
        //    set { m_ProxyType = value; }
        //}

        #endregion

        public IpHelper(string localPublicIp)
        {
        }

        /// <summary>
        ///     新建IpLocation实例以获得IP地理位置
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="isChinese">是否是中文版</param>
        public IpHelper(string ipAddress, bool isChinese)
        {
            _mProxyAddress = ipAddress.Trim();
            _mIpAddress = ipAddress.Trim();
            _mIsChinese = isChinese;
        }

        /// <summary>
        ///     获取myip.cn地理位置名称和匿名度
        /// </summary>
        public string[] LocationAndType
        {
            get
            {
                string[] result = {"未知地区", ""};

                if (_mIsChinese)
                {
                    #region

                    try
                    {
                        if (string.IsNullOrEmpty(_mProxyType))
                        {
                            #region 中文查询

                            var regex =
                                new Regex(
                                    @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\:(?<Port>\d{1,5})");
                            var matchs = regex.Matches(_mProxyAddress);
                            if (matchs.Count == 1)
                            {
                                var pe = new HttpProxy
                                    {
                                        Ip = matchs[0].Groups["Proxy"].Value,
                                        Port = int.Parse(matchs[0].Groups["Port"].Value)
                                    };

                                #region

                                var tstOption = new TestOption
                                    {
                                        TestUrl = "http://www.myip.cn/",
                                        TestWebEncoding = "UTF-8",
                                        TestWebTitle = "我的IP地址查询"
                                    };

                                string localIp = NetHelper.LocalPublicIp;

                                string html = new TestProxyHelper(tstOption, 30).TestProxy(pe).Message;
                                html = StringHelper.GetMidString(html,
                                                                 "<div class='div_main' style=\"text-align:center;\">",
                                                                 "<!-- weather -->");
                                string ip = StringHelper.GetMidString(html, "您的IP地址:", "</b>").Trim();
                                string location = StringHelper.GetMidString(html, "来自: ", "&nbsp;&nbsp;").Trim();
                                if (string.IsNullOrEmpty(location))
                                    location = StringHelper.GetMidString(html, "来自: ", ".").Trim();
                                if (string.IsNullOrEmpty(location))


                                    if (location.Contains(""))
                                        location = StringHelper.GetMidString(html, "来自: ", "<br>").Trim();

                                string proxyType = StringHelper.GetMidString(html, "<a href=/judge.php>", "</a>").Trim();

                                if (!string.IsNullOrEmpty(proxyType))
                                    _mProxyType = proxyType;
                                if (!string.IsNullOrEmpty(location))
                                    _mLocation = location;

                                if (!string.IsNullOrEmpty(ip))
                                {
                                    if (ip != localIp && proxyType.Contains("无代理"))
                                    {
                                        _mProxyType = "高匿名代理";
                                    }
                                }

                                #endregion
                            }

                            #endregion
                        }

                        if (string.IsNullOrEmpty(_mLocation))
                        {
                            _mLocation = Location;
                        }
                    }
                    catch (Exception ex)
                    {
                        _mProxyType = ex.Message;
                    }

                    #endregion
                }
                else
                {
                    #region

                    try
                    {
                        var regex =
                            new Regex(
                                @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))");
                        MatchCollection matchs = regex.Matches(_mIpAddress);
                        if (matchs.Count == 1)
                        {
                            _mIpAddress = matchs[0].Groups["Proxy"].Value;

                            var client = new WebClient {Encoding = Encoding.UTF8};

                            const string url = "http://api.wipmania.com/";
                            var post = _mIpAddress + "?loamen.com";
                            client.Headers.Set("Content-Type", "application/x-www-form-urlencoded");
                            client.Proxy = null;
                            var response = client.UploadString(url, post);
                            _mResponse = response;

                            _mLocation = _mResponse;
                        }
                        if (string.IsNullOrEmpty(_mLocation))
                            _mLocation = "Unkown Loamen.Com";
                    }
                    catch
                    {
                        _mLocation = "Unkown Loamen.Com";
                    }

                    #endregion
                }

                result[0] = _mLocation;
                result[1] = _mProxyType;

                return result;
            }
        }

        /// <summary>
        ///     返回IP138Ip地址的地理位置名称
        /// </summary>
        public string Location
        {
            get
            {
                if (_mIsChinese)
                {
                    #region

                    try
                    {
                        var regex =
                            new Regex(
                                @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))");
                        MatchCollection matchs = regex.Matches(_mIpAddress);
                        if (matchs.Count == 1)
                        {
                            _mIpAddress = matchs[0].Groups["Proxy"].Value;

                            var client = new WebClient {Encoding = Encoding.GetEncoding("GB2312")};

                            const string url = "http://www.ip138.com/ips.asp";
                            var post = "ip=" + _mIpAddress + "&action=2";
                            client.Headers.Set("Content-Type", "application/x-www-form-urlencoded");
                            client.Proxy = null;
                            var response = client.UploadString(url, post);
                            _mResponse = response;

                            var p = @"<li>本站主数据：(?<location>[^<>]+?)</li>";

                            var match = Regex.Match(response, p);
                            _mLocation = match.Groups["location"].Value.Trim();
                            if (string.IsNullOrEmpty(_mLocation))
                            {
                                p = @"<li>参考数据一：(?<location>[^<>]+?)</li>";
                                match = Regex.Match(response, p);
                                _mLocation = match.Groups["location"].Value.Trim();
                            }
                            if (string.IsNullOrEmpty(_mLocation))
                            {
                                p = @"<li>参考数据二：(?<location>[^<>]+?)</li>";
                                match = Regex.Match(response, p);
                                _mLocation = match.Groups["location"].Value.Trim();
                            }
                        }
                        if (string.IsNullOrEmpty(_mLocation))
                            _mLocation = "未知地区";
                    }
                    catch (Exception)
                    {
                        _mLocation = "未知地区Loamen.Com";
                        //TxtHelper.WriteLog(ex.Message);
                    }

                    #endregion
                }
                else
                {
                    #region

                    try
                    {
                        var regex =
                            new Regex(
                                @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))");
                        var matchs = regex.Matches(_mIpAddress);
                        if (matchs.Count == 1)
                        {
                            _mIpAddress = matchs[0].Groups["Proxy"].Value;

                            var client = new WebClient {Encoding = Encoding.UTF8};

                            const string url = "http://api.wipmania.com/";
                            var post = _mIpAddress + "?loamen.com";
                            client.Headers.Set("Content-Type", "application/x-www-form-urlencoded");
                            client.Proxy = null;
                            var response = client.UploadString(url, post);
                            _mResponse = response;
                            if (_mResponse.ToLower().Contains("<br>"))
                                _mLocation = _mResponse.Substring(_mResponse.IndexOf('>') + 1,
                                                                  _mResponse.Length - _mResponse.IndexOf('>') - 1);
                            else
                                _mLocation = _mResponse;
                        }
                        if (string.IsNullOrEmpty(_mLocation))
                            _mLocation = "Unkown Loamen.Com";
                    }
                    catch
                    {
                        _mLocation = "Unkown Loamen.Com";
                    }

                    #endregion
                }

                return _mLocation;
            }
        }

        public string IpAddress
        {
            get { return _mIpAddress; }
        }

        /// <summary>
        ///     返回网络反馈原始数据HTML
        /// </summary>
        public string Response
        {
            get { return _mResponse; }
        }
    }
}