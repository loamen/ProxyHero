using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Loamen.PluginFramework;
using ProxyHero.Model;

namespace ProxyHero.Common
{
    public class TxtHelper
    {
        /// <summary>
        ///     导出代理TXT
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string[] WriteProxyTxt(IList<ProxyServer> list, string fileName, Encoding encode)
        {
            List<ProxyServer> proxyList = (from row in list
                                           orderby row.response descending
                                           select row).ToList<ProxyServer>();
            string[] result = {"0", ""};
            try
            {
                using (var objWriter = new StreamWriter(fileName, false, encode))
                {
                    int i = 0;
                    foreach (ProxyServer dr in proxyList)
                    {
                        var sb = new StringBuilder(dr.proxy);
                        sb.Append(":");
                        sb.Append(dr.port);
                        if (Config.LocalSetting.ExportMode != Config.LocalLanguage.Messages.StandardFormat)
                        {
                            sb.Append("@");
                            sb.Append(dr.type);
                            sb.Append("#");
                            sb.Append(dr.response);
                            sb.Append(" ");
                            if (Config.IsChineseLanguage)
                                sb.Append(string.IsNullOrEmpty(dr.country + "") ? "未知" : dr.country);
                            else
                                sb.Append(string.IsNullOrEmpty(dr.country + "") ? "Unkown" : dr.country);
                        }
                        if (i == proxyList.Count - 1)
                        {
                            objWriter.Write(sb.ToString());
                        }
                        else
                        {
                            objWriter.WriteLine(sb.ToString());
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                result[0] = "1";
                result[1] = ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     读取所有代理列表TXT
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string[] ReadProxyTxt(string fileName, Encoding encode)
        {
            string[] result = {"0", ""};
            try
            {
                using (var objReader = new StreamReader(fileName, encode, false))
                {
                    string line;
                    while ((line = objReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        var regex =
                            new Regex(
                                @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\:(?<Port>\d{1,5})\@(?<Type>(HTTP|SOCKS4|SOCK5))\#(?<Response>\d{1,5})\s(?<Country>\w+)");
                        MatchCollection matchs = regex.Matches(line);
                        if (matchs.Count == 1)
                        {
                            string ip = matchs[0].Groups["Proxy"].Value;
                            string port = matchs[0].Groups["Port"].Value;
                            string type = matchs[0].Groups["Type"].Value;
                            int delay = 0;
                            int.TryParse(matchs[0].Groups["Response"].Value, out delay);
                            string country = matchs[0].Groups["Country"].Value;
                            // IpHelper ih = new IpHelper(strs[0]);

                            var model = new ProxyServer();
                            model.proxy = ip;
                            model.port = int.Parse(port);
                            model.type = type;
                            model.response = delay;
                            model.country = country;
                            model.status = -1;

                            ProxyData.Set(model);
                        }
                        else
                        {
                            //regex = new Regex(@"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\:(?<Port>\d{1,5})");
                            //regex = new Regex(@"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))(\s|\:|\：|<(.*?)>)(?<Port>\d{1,5})");
                            regex =
                                new Regex(
                                    @"(?<Proxy>([1-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(([0-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.){2}([1-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))(\s+|\:|\：|<(.*?)>)(?<Port>\d{1,5})");

                            matchs = regex.Matches(line);
                            if (matchs.Count == 1)
                            {
                                string ip = matchs[0].Groups["Proxy"].Value;
                                string port = matchs[0].Groups["Port"].Value;

                                var model = new ProxyServer();
                                model.proxy = ip;
                                model.port = int.Parse(port);
                                model.type = "HTTP";
                                model.status = -1;

                                ProxyData.Set(model);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result[0] = "1";
                result[1] = ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     从文本中读取
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] ReadProxyTxt(string text)
        {
            string[] result = {"0", ""};
            try
            {
                string[] lines = text.Split('\n');

                int beginCount = ProxyData.ProxyList.Count;
                foreach (string line in lines)
                {
                    string proxy = line.Trim();
                    var regex =
                        new Regex(
                            @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\:(?<Port>\d{1,5})\@(?<Type>(HTTP|SOCKS4|SOCK5))\#(?<Response>\d{1,5})\s(?<Country>\w+)");
                    MatchCollection matchs = regex.Matches(proxy);
                    if (matchs.Count == 1)
                    {
                        string ip = matchs[0].Groups["Proxy"].Value;
                        string port = matchs[0].Groups["Port"].Value;
                        string type = matchs[0].Groups["Type"].Value;
                        int delay = 0;
                        int.TryParse(matchs[0].Groups["Response"].Value, out delay);
                        string country = matchs[0].Groups["Country"].Value;
                        // IpHelper ih = new IpHelper(strs[0]);

                        var model = new ProxyServer();
                        model.proxy = ip;
                        model.port = int.Parse(port);
                        model.type = type;
                        model.response = delay;
                        model.country = country;
                        model.status = -1;

                        ProxyData.Set(model);
                    }
                    else
                    {
                        //regex = new Regex(@"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\:(?<Port>\d{1,5})");
                        //regex = new Regex(@"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))(\s|\:|\：|<(.*?)>)(?<Port>\d{1,5})");
                        regex =
                            new Regex(
                                @"(?<Proxy>([1-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(([0-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.){2}([1-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))(\s+|\:|\：|<(.*?)>)(?<Port>\d{1,5})");
                        matchs = regex.Matches(proxy);
                        if (matchs.Count == 1)
                        {
                            string ip = matchs[0].Groups["Proxy"].Value;
                            string port = matchs[0].Groups["Port"].Value;

                            var model = new ProxyServer();
                            model.proxy = ip;
                            model.port = int.Parse(port);
                            model.type = "HTTP";
                            model.status = -1;

                            ProxyData.Set(model);
                        }
                        //else
                        //{
                        //    result[0] = "1";
                        //    result[1] = line + "数据格式错误！";
                        //    break;
                        //}
                    }
                }

                int endCount = ProxyData.ProxyList.Count;
                int insertCount = endCount - beginCount; //新增的记录条数
                result[1] = insertCount + " proxies added!";
            }
            catch (Exception ex)
            {
                result[0] = "1";
                result[1] = ex.Message;
            }
            return result;
        }

        public static string[] WriteTxt(string html, string fileName, Encoding encode)
        {
            string[] result = {"0", ""};
            try
            {
                using (var objWriter = new StreamWriter(fileName, false, encode))
                {
                    objWriter.Write(html);
                }
            }
            catch (Exception ex)
            {
                result[0] = "1";
                result[1] = ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     格式化代理
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Format(string text)
        {
            var sb = new StringBuilder();
            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                var regex =
                    new Regex(
                        @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))(\s|\:|\：|<(.*?)>)(?<Port>\d{1,5})");
                MatchCollection matchs = regex.Matches(line);
                if (matchs.Count == 1)
                {
                    string ip = matchs[0].Groups["Proxy"].Value;
                    string port = matchs[0].Groups["Port"].Value;
                    sb.Append(ip);
                    sb.Append(":");
                    sb.Append(port);
                    sb.Append("\n");
                }

                #region

                //else
                //{
                //    regex = new Regex(@"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\s(?<Port>\d{1,5})");
                //    matchs = regex.Matches(line);
                //    if (matchs.Count == 1)
                //    {
                //        string ip = matchs[0].Groups["Proxy"].Value;
                //        string port = matchs[0].Groups["Port"].Value;
                //        sb.Append(ip);
                //        sb.Append(":");
                //        sb.Append(port);
                //        sb.Append("\n");
                //    }
                //    else
                //    {
                //        regex = new Regex(@"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))\：(?<Port>\d{1,5})");
                //        matchs = regex.Matches(line);
                //        if (matchs.Count == 1)
                //        {
                //            string ip = matchs[0].Groups["Proxy"].Value;
                //            string port = matchs[0].Groups["Port"].Value;
                //            sb.Append(ip);
                //            sb.Append(":");
                //            sb.Append(port);
                //            sb.Append("\n");
                //        }
                //    }
                //}

                #endregion
            }

            if (sb.ToString().EndsWith("\n"))
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }
    }
}