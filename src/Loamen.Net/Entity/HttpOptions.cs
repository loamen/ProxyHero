using System;
using System.Text;

namespace Loamen.Net.Entity
{
    [Serializable]
    public class HttpOptions
    {
        private string _accept =
            "application/x-shockwave-flash, image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-silverlight, */*";

        private string _contentType = "application/x-www-form-urlencoded";

        private Encoding _encoding = Encoding.Default;

        private int _postpone = 30;
        private string _referer;

        //private int _retry = 5;
        private int _timeout = 20*1000;
        private string _userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";

        /// <summary>
        ///     请求编码
        /// </summary>
        public Encoding Encoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        /// <summary>
        ///     每次请求的间隔时间
        /// </summary>
        public int Postpone
        {
            get { return _postpone; }
            set { _postpone = value; }
        }

        ///// <summary>
        ///// 请求失败后的重新尝试次数
        ///// </summary>
        //public int Retry
        //{
        //    get { return _retry; }
        //    set { _retry = value; }
        //}

        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public string Referer
        {
            get { return _referer; }
            set { _referer = value; }
        }

        public string Accept
        {
            get { return _accept; }
            set { _accept = value; }
        }

        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        /// <summary>
        /// 超时时间，单位：秒
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
    }
}