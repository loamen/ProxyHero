using System;
using System.Globalization;

namespace Loamen.Net.Entity
{
    /// <summary>
    /// 代理设置
    /// </summary>
    [Serializable]
    public class HttpProxy
    {
        private string _added = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        private int _id;

        private string _ip;
        private string _password;

        private int _port;
        private double _response;
        private int _type;

        private string _userName;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        ///     代理服务器IP
        /// </summary>
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        /// <summary>
        ///     代理服务器端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        ///     代理用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        ///     代理密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public double Response
        {
            get { return _response; }
            set { _response = value; }
        }

        public string Added
        {
            get { return _added; }
            set { _added = value; }
        }

        public string Anonymity { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Status { get; set; } //状态0:dead,1:alive,2:not test
        public string TestDate { get; set; }

        public string IpAndPort
        {
            get { return _ip + ":" + _port; }
        }
    }
}