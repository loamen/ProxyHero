using System;

namespace Loamen.Net.Entity
{
    [Serializable]
    public class TestOption
    {
        private string _testUrl = "http://180.97.33.107/robots.txt";
        private string _testWebEncoding = "UTF-8";

        private string _testWebTitle = "Baiduspider";

        /// <summary>
        ///     验证网站地址
        /// </summary>
        public string TestUrl
        {
            get { return _testUrl; }
            set { _testUrl = value; }
        }

        /// <summary>
        ///     验证网站Title
        /// </summary>
        public string TestWebTitle
        {
            get { return _testWebTitle; }
            set { _testWebTitle = value; }
        }

        /// <summary>
        ///     测试网页编码格式
        /// </summary>
        public string TestWebEncoding
        {
            get { return _testWebEncoding; }
            set { _testWebEncoding = value; }
        }
    }
}