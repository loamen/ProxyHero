using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using Loamen.Net.Entity;

namespace Loamen.Net
{
    /// <summary>
    ///     测试代理类
    /// </summary>
    public class TestProxyHelper
    {
        /// <summary>
        ///     HTTP
        /// </summary>
        private readonly HttpHelper _httpHelper;

        public TestProxyHelper(TestOption testOption, int timeOut, string userAgent = null)
        {
            TestOption = testOption;
            TimeOut = timeOut;
            UserAgent = userAgent;


            var option = new HttpOptions();
            if (!string.IsNullOrEmpty(TestOption.TestWebEncoding))
                option.Encoding = Encoding.GetEncoding(TestOption.TestWebEncoding);
            if (TimeOut > 0)
                option.Timeout = TimeOut;
            if (!string.IsNullOrEmpty(UserAgent))
            {
                option.UserAgent = UserAgent;
            }

            _httpHelper = new HttpHelper {HttpOption = option, IsUseDefaultProxy = false};
            //禁止默认代理
        }

        /// <summary>
        ///     待测试的代理
        /// </summary>
        public HttpProxy TestingProxy { get; set; }

        /// <summary>
        ///     测试选项
        /// </summary>
        public TestOption TestOption { get; set; }

        /// <summary>
        ///     测试超时时间
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        ///     User-Agent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        ///     验证代理配置
        /// </summary>
        /// <param name="httpProxy">代理信息</param>
        public TestResult TestProxy(HttpProxy httpProxy)
        {
            var result = new TestResult();

            if (httpProxy == null)
                return result;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                _httpHelper.Proxy = httpProxy;
                string html = _httpHelper.GetHtml(TestOption.TestUrl);

                if (html.ToLower().Contains(TestOption.TestWebTitle.ToLower()))
                {
                    result.IsAlive = true;
                    result.Message = html;
                }
                else
                {
                    result.IsAlive = false;
                }
            }
            catch (WebException webEx)
            {
                var sb = new StringBuilder(" 错误信息:" + webEx.Message);
#if DEBUG
                sb.Append("\nType:" + webEx.GetType());
                sb.Append("\nSource:" + webEx.Source);
                sb.Append("\nTargetSite:" + webEx.TargetSite);
                sb.Append("\nStack Trace:" + webEx.StackTrace);
#endif

                result.IsAlive = false;
                result.Message = sb + ":" + stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                result.IsAlive = false;
                result.Message = ex.Message + stopwatch.ElapsedMilliseconds;
            }
            finally
            {
                stopwatch.Stop();
                result.Response = stopwatch.ElapsedMilliseconds;
            }
            return result;
        }
    }
}