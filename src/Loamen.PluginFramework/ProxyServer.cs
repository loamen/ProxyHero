using System;

namespace Loamen.PluginFramework
{
    /// <summary>
    ///     代理实体
    /// </summary>
    public class ProxyServer
    {
        private int _status = -1;
        private string _testDate = DateTime.UtcNow.ToString();
        private string _type = "HTTP";

        /// <summary>
        ///     编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        ///     代理IP
        /// </summary>
        public string proxy { get; set; }

        /// <summary>
        ///     代理端口
        /// </summary>
        public int port { get; set; }

        /// <summary>
        ///     代理类型
        /// </summary>
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        ///     响应速度
        /// </summary>
        public int response { get; set; }

        /// <summary>
        ///     代理账户
        /// </summary>
        public string proxyusername { get; set; }

        /// <summary>
        ///     代理密码
        /// </summary>
        public string proxypassword { get; set; }

        /// <summary>
        ///     匿名度
        /// </summary>
        public string anonymity { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        ///     匿名度
        /// </summary>
        public string anonymityen { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string countryen { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        ///     测试时间
        /// </summary>
        public string testdate
        {
            get { return _testDate; }
            set { _testDate = value; }
        }

        /// <summary>
        ///     //状态0:dead,1:alive,2:not test
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        ///     提供用户
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        ///     提供用户
        /// </summary>
        public string username { get; set; }

        /// <summary>
        ///     提供用户
        /// </summary>
        public string userip { get; set; }

        public int isvip { get; set; }
        public int failedcount { get; set; }
    }
}