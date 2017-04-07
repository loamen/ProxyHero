using System;

namespace Loamen.Net.Entity
{
    [Serializable]
    public class TestOption
    {
        private string _testUrl = "http://www.baidu.com";
        private string _testWebEncoding = "UTF-8";

        private string _testWebTitle = "百度";

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