namespace Loamen.Net
{
    public class TranslateHelper
    {
        /// <summary>
        ///     获得中文<->英文双向翻译 String()
        ///     输入参数：wordKey = 单词； 返回数据：一维字符串数组 String[]。
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string[] TranslatorString(string[] args)
        {
            string webServiceUrl = "http://fy.webxml.com.cn/webservices/EnglishChinese.asmx";

            string[] result = null;
            object obj = WebServiceHelper.InvokeWebService(webServiceUrl, "TranslatorString", args);
            if (obj != null)
                result = (string[]) obj;
            return result;
        }
    }
}