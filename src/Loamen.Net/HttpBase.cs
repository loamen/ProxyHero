using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Loamen.Common;
using Loamen.Net.Entity;

namespace Loamen.Net
{
    public class HttpBase
    {
        #region Property

        /// <summary>
        /// 与请求相关的cookie（用于保持session）
        /// </summary>
        private CookieContainer _cookies;

        private HttpOptions _httpOption;

        /// <summary>
        /// 关联Cookie，用于保持session会话
        /// </summary>
        public CookieContainer Cookies
        {
            get { return _cookies ?? (_cookies = new CookieContainer()); }
            set { _cookies = value; }
        }

        /// <summary>
        /// 请求信息配置
        /// </summary>
        public HttpOptions HttpOption
        {
            get { return _httpOption ?? (_httpOption = new HttpOptions()); }
            set { _httpOption = value; }
        }

        /// <summary>
        /// 代理信息
        /// </summary>
        public HttpProxy Proxy { get; set; }

        private bool _isUseDefaultProxy = true;

        /// <summary>
        /// 是否使用系统默认代理
        /// </summary>
        public bool IsUseDefaultProxy
        {
            get { return _isUseDefaultProxy; }
            set { _isUseDefaultProxy = value; }
        }
        #endregion

        #region

        /// <summary>
        /// 代理设置
        /// </summary>
        /// <param name="request"></param>
        public void ProxySetting(HttpWebRequest request)
        {
            WebProxy webProxy = null;

            if (IsUseDefaultProxy)
#pragma warning disable 612,618
                webProxy = WebProxy.GetDefaultProxy();
#pragma warning restore 612,618

            if (Proxy != null && !string.IsNullOrEmpty(Proxy.Ip) && Proxy.Port != 0)
            {
#pragma warning disable 612,618
                webProxy = WebProxy.GetDefaultProxy();
#pragma warning restore 612,618
                webProxy.Address = new Uri("http://" + Proxy.Ip + ":" + Proxy.Port + "/");

                if (!string.IsNullOrEmpty(Proxy.UserName) &&
                    !string.IsNullOrEmpty(Proxy.Password))
                {
                    webProxy.Credentials = new NetworkCredential(Proxy.UserName, Proxy.Password);
                }
            }
            request.Proxy = webProxy;
        }

        #endregion

        #region HTTP DOGET

        /// <summary>
        ///     发送Get类型请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public WebResponse DoGet(HttpWebRequest webRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //设置代理
            ProxySetting(webRequest);

            webRequest.KeepAlive = false;
            webRequest.CookieContainer = Cookies;
            webRequest.Method = "GET";

            webRequest.ContentType = !string.IsNullOrEmpty(HttpOption.ContentType)
                                         ? HttpOption.ContentType
                                         : "application/x-www-form-urlencoded";
            if (!string.IsNullOrEmpty(HttpOption.Referer))
            {
                webRequest.Referer = HttpOption.Referer;
            }

            webRequest.Accept = !string.IsNullOrEmpty(HttpOption.Accept)
                                    ? HttpOption.Accept
                                    : "application/x-shockwave-flash, image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-silverlight, */*";
            webRequest.UserAgent = !string.IsNullOrEmpty(HttpOption.UserAgent)
                                       ? HttpOption.UserAgent
                                       : "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";

            webRequest.CookieContainer = Cookies;

            webRequest.Timeout = HttpOption.Timeout > 0
                                     ? webRequest.Timeout = HttpOption.Timeout * 1000
                                     : webRequest.Timeout = 30 * 1000;
            webRequest.ReadWriteTimeout = HttpOption.Timeout > 0
                                     ? webRequest.Timeout = HttpOption.Timeout * 1000
                                     : webRequest.Timeout = 30 * 1000;
            webRequest.ServicePoint.ConnectionLimit = 100;

            var response = webRequest.GetResponse();
            stopwatch.Stop();

            return response;
        }

        #endregion

        #region HTTP DOPOST
        /// <summary>
        /// 发送Post类型请求
        /// </summary>
        /// <param name="webRequest"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public WebResponse DoPost(HttpWebRequest webRequest, string postData)
        {
            var paramByte = HttpOption.Encoding.GetBytes(postData); // 转化
            //设置代理
            ProxySetting(webRequest);

            webRequest.Method = "POST";
            webRequest.ContentType = !string.IsNullOrEmpty(HttpOption.ContentType)
                                         ? HttpOption.ContentType
                                         : "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(HttpOption.Referer))
            {
                webRequest.Referer = HttpOption.Referer;
            }

            webRequest.Accept = !string.IsNullOrEmpty(HttpOption.Accept)
                                    ? HttpOption.Accept
                                    : "application/x-shockwave-flash, image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-silverlight, */*";
            
            webRequest.UserAgent = !string.IsNullOrEmpty(HttpOption.UserAgent)
                                       ? HttpOption.UserAgent
                                       : "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";

            webRequest.Timeout = HttpOption.Timeout > 0
                                     ? webRequest.Timeout = HttpOption.Timeout * 1000
                                     : webRequest.Timeout = 30*1000;
            webRequest.ReadWriteTimeout = HttpOption.Timeout > 0
                                     ? webRequest.Timeout = HttpOption.Timeout * 1000
                                     : webRequest.Timeout = 30 * 1000;
            webRequest.ServicePoint.ConnectionLimit = 100;

            webRequest.ContentLength = paramByte.Length;
            webRequest.CookieContainer = Cookies;

            var newStream = webRequest.GetRequestStream();
            newStream.Write(paramByte, 0, paramByte.Length); //写入参数
            newStream.Close();
            return webRequest.GetResponse();
        }
        #endregion
    }
}
