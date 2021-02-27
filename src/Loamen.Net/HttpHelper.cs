using Loamen.Common;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace Loamen.Net
{
    public class HttpHelper : HttpBase
    {
        #region Methods

        /// <summary>
        ///     得到一张网络图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Image GetImage(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                Stream stream = DoGet(webRequest).GetResponseStream();
                if (stream != null)
                {
                    Image img = Image.FromStream(stream);
                    return img;
                }
                return null;
            }
            finally
            {
                if(webRequest != null)
                {
                    webRequest.Abort();
                    webRequest = null;
                }
            }
        }

        /// <summary>
        ///     根据返回流获取返回的html代码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string GetHtml(string url, Encoding encode = null)
        {
            string html = "";
            WebResponse response = null;

            // GC.Collect();
            ServicePointManager.DefaultConnectionLimit = 200;

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                response = DoGet(webRequest);
                if (response != null)
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), encode ?? HttpOption.Encoding))
                    {
                        html = stream.ReadToEnd();
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex, Proxy.IpAndPort);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if(webRequest != null)
                {
                    webRequest.Abort();
                    webRequest = null;
                }
            }
            return html;
        }

        /// <summary>
        ///     根据返回流获取返回的html代码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string GetHtml(string url, string postData, Encoding encode = null)
        {
            string html = "";
            WebResponse response = null;

            GC.Collect();
            ServicePointManager.DefaultConnectionLimit = 200;

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                response = DoPost(webRequest, postData);
                using (var stream = new StreamReader(response.GetResponseStream(), encode ?? HttpOption.Encoding))
                {
                    html = stream.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (webRequest != null)
                {
                    webRequest.Abort();
                    webRequest = null;
                }
            }
            return html;
        }

        #endregion
    }
}