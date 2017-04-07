using System;
using System.Text.RegularExpressions;

namespace Loamen.Common
{
    public class StringHelper
    {
        /// <summary>
        ///     截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="startStr">开始字符串</param>
        /// <param name="endStr">结束字符串</param>
        /// <returns>介于开始和结束字符串之间的字符串</returns>
        public static string GetMidString(string str, string startStr, string endStr)
        {
            try
            {
                if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(startStr) || string.IsNullOrEmpty(endStr))
                {
                    return string.Empty;
                }

                int startIndex = str.IndexOf(startStr, StringComparison.CurrentCultureIgnoreCase);

                if (startIndex == -1)
                {
                    return string.Empty;
                }

                startIndex += startStr.Length;

                int endIndex = str.IndexOf(endStr, startIndex, StringComparison.CurrentCultureIgnoreCase);

                return str.Substring(startIndex, endIndex - startIndex);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///     删除所有的html标记
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelHtmlString(string str)
        {
            string[] Regexs =
                {
                    @"<script[^>]*?>.*?</script>",
                    @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                    @"([\r\n])[\s]+",
                    @"&(quot|#34);",
                    @"&(amp|#38);",
                    @"&(lt|#60);",
                    @"&(gt|#62);",
                    @"&(nbsp|#160);",
                    @"&(iexcl|#161);",
                    @"&(cent|#162);",
                    @"&(pound|#163);",
                    @"&(copy|#169);",
                    @"&#(\d+);",
                    @"-->",
                    @"<!--.*\n"
                };

            string[] Replaces =
                {
                    "",
                    "",
                    "",
                    "\"",
                    "&",
                    "<",
                    ">",
                    " ",
                    "\xa1", //chr(161),
                    "\xa2", //chr(162),
                    "\xa3", //chr(163),
                    "\xa9", //chr(169),
                    "",
                    "\r\n",
                    ""
                };

            string s = str;
            for (int i = 0; i < Regexs.Length; i++)
            {
                s = new Regex(Regexs[i], RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(s, Replaces[i]);
            }
            s.Replace("<", "");
            s.Replace(">", "");
            s.Replace("\r\n", "");
            return s;
        }

        /// <summary>
        ///     删除字符串中的特定标记
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tag"></param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns></returns>
        public static string DelTag(string str, string tag, bool isContent)
        {
            if (tag == null || tag == " ")
            {
                return str;
            }

            if (isContent) //要求清除内容 
            {
                return Regex.Replace(str, string.Format("<({0})[^>]*>([\\s\\S]*?)<\\/\\1>", tag), "",
                                     RegexOptions.IgnoreCase);
            }

            return Regex.Replace(str, string.Format(@"(<{0}[^>]*(>)?)|(</{0}[^>] *>)|", tag), "",
                                 RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     删除字符串中的一组标记
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tagA"></param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns></returns>
        public static string DelTagArray(string str, string tagA, bool isContent)
        {
            string[] tagAa = tagA.Split(',');

            foreach (string sr1 in tagAa) //遍历所有标记，删除 
            {
                str = DelTag(str, sr1, isContent);
            }
            return str;
        }
    }
}