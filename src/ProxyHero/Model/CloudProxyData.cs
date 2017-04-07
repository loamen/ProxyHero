using System.Collections.Generic;
using System.Linq;
using Loamen.PluginFramework;

namespace ProxyHero.Model
{
    /// <summary>
    ///     云服务器资源
    /// </summary>
    public class CloudProxyData
    {
        private static IList<ProxyServer> cloudProxyList = new List<ProxyServer>();

        /// <summary>
        ///     服务器代理列表
        /// </summary>
        public static IList<ProxyServer> CloudProxyList
        {
            get { return cloudProxyList; }
            set { cloudProxyList = value; }
        }

        #region 云端方法

        /// <summary>
        ///     添加或云端代理
        /// </summary>
        /// <param name="model"></param>
        public static void Set(ProxyServer model)
        {
            ProxyServer proxy = Get(model.proxy, model.port);
            if (null == proxy)
            {
                try
                {
                    model.id = CloudProxyList.Max(p => p.id) + 1;
                }
                catch
                {
                    model.id = 1;
                }
                lock (CloudProxyList)
                {
                    CloudProxyList.Add(model);
                }
            }
            else
            {
                lock (CloudProxyList)
                {
                    proxy.anonymity = model.anonymity;
                    proxy.anonymityen = model.anonymityen;
                    proxy.country = model.country;
                    proxy.countryen = model.countryen;
                    proxy.description = model.description;
                    proxy.isvip = model.isvip;
                    proxy.port = model.port;
                    proxy.proxy = model.proxy;
                    proxy.proxypassword = model.proxypassword;
                    proxy.proxyusername = model.proxyusername;
                    proxy.response = model.response;
                    proxy.status = model.status;
                    proxy.testdate = model.testdate;
                    proxy.type = model.type;
                    proxy.userid = model.userid;
                    proxy.userip = model.userip;
                    proxy.username = model.username;
                }
            }
        }

        /// <summary>
        ///     删除云代理
        /// </summary>
        /// <param name="model"></param>
        public static void Remove(ProxyServer model)
        {
            ProxyServer proxy = Get(model.proxy, model.port);
            if (null != proxy)
            {
                lock (CloudProxyList)
                {
                    CloudProxyList.Remove(proxy);
                }
            }
        }

        /// <summary>
        ///     删除云代理
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="port"></param>
        public static void Remove(string proxy, int port)
        {
            ProxyServer model = Get(proxy, port);
            if (null != model)
            {
                lock (CloudProxyList)
                {
                    CloudProxyList.Remove(model);
                }
            }
        }

        /// <summary>
        ///     获取验证列表
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static ProxyServer Get(string proxy, int port)
        {
            return CloudProxyList.FirstOrDefault(p => p.proxy == proxy && p.port == port);
        }

        /// <summary>
        ///     获取代理拷贝
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static ProxyServer GetCopy(string proxy, int port)
        {
            ProxyServer model = Get(proxy, port);
            var modelNew = new ProxyServer();
            if (null != model)
            {
                modelNew.id = model.id;
                modelNew.anonymity = model.anonymity;
                modelNew.anonymityen = model.anonymityen;
                modelNew.country = model.country;
                modelNew.countryen = model.countryen;
                modelNew.description = model.description;
                modelNew.isvip = model.isvip;
                modelNew.port = model.port;
                modelNew.proxy = model.proxy;
                modelNew.proxypassword = model.proxypassword;
                modelNew.proxyusername = model.proxyusername;
                modelNew.response = model.response;
                modelNew.status = model.status;
                modelNew.testdate = model.testdate;
                modelNew.type = model.type;
                modelNew.userid = model.userid;
                modelNew.userip = model.userip;
                modelNew.username = model.username;
            }
            return modelNew;
        }

        /// <summary>
        ///     查询代理列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<ProxyServer> Search(ProxyServer model)
        {
            List<ProxyServer> result = (from row in CloudProxyList
                                        where (string.IsNullOrEmpty(model.proxy) ? true : row.proxy == model.proxy)
                                              && (model.port == 0 ? true : row.port == model.port)
                                              &&
                                              ((model.status != 0 && model.status != 1)
                                                   ? true
                                                   : row.status == model.status)
                                              &&
                                              (string.IsNullOrEmpty(model.country)
                                                   ? true
                                                   : (row.country != null && row.country.Contains(model.country)))
                                              &&
                                              (string.IsNullOrEmpty(model.anonymity)
                                                   ? true
                                                   : (row.anonymity != null && row.anonymity == model.anonymity))
                                        select row).ToList<ProxyServer>();

            return result;
        }

        #endregion
    }
}