using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using ProxyHero.Net;

namespace ProxyHero.Common
{
    public class CookiesHelper
    {
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookie(string url, string cookieName, StringBuilder cookieData,
                                                    ref int size);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetSetCookie(string url, string cookieName, string cookieData);

        /// <summary>
        ///     删除COOKIE
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="text">包含字符串</param>
        public static void DeleteCookie(string url, string text)
        {
            try
            {
                //获取旧的   
                var cookie = new StringBuilder(new String(' ', 2048));
                int datasize = cookie.Length;

                bool b = InternetGetCookie(url, null, cookie, ref datasize);

                //删除旧的   
                foreach (
                    string fileName in
                        Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Cookies),
                                           "*" + text + "*", SearchOption.AllDirectories))
                {
                    if (fileName.ToLower().IndexOf(text) > 0)
                    {
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static void SetIECookie(SnsHelper snsHelper, string url)
        //{
        //    var str = new StringBuilder();

        //    string cookieexpires = string.Format(";expires={0}", DateTime.Now.AddDays(1).ToString("r"));
        //        //";expires=Sun,22-Feb-2099 00:00:00 GMT";
        //    CookieCollection loamenCookies = snsHelper.Cookies.GetCookies(new Uri(url));
        //    foreach (Cookie cookie in loamenCookies)
        //    {
        //        InternetSetCookie(url, cookie.Name, cookie.Value + cookieexpires);
        //    }
        //}
    }
}