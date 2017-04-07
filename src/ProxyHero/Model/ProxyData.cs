using System.Collections.Generic;
using System.Linq;
using Loamen.PluginFramework;

namespace ProxyHero.Model
{
    public class ProxyData
    {
        #region 属性

        private static IList<ProxyServer> proxyList = new List<ProxyServer>();

        /// <summary>
        ///     代理列表
        /// </summary>
        public static IList<ProxyServer> ProxyList
        {
            get
            {
                if (null == proxyList)
                {
                    proxyList = new List<ProxyServer>();
                }
                return proxyList;
            }
            set { proxyList = value; }
        }

        /// <summary>
        ///     可用代理数量
        /// </summary>
        public static int AliveNum
        {
            get
            {
                int count = ProxyList.Count(p => p.status == 1);
                return count;
            }
        }

        /// <summary>
        ///     不可用代理数量
        /// </summary>
        public static int DeadNum
        {
            get
            {
                int count = ProxyList.Count(p => p.status == 0);
                return count;
            }
        }

        /// <summary>
        ///     代理总数
        /// </summary>
        public static int TotalNum
        {
            get { return ProxyList.Count; }
        }

        /// <summary>
        ///     不可用代理数量
        /// </summary>
        public static int UnkownNum
        {
            get
            {
                int count = ProxyList.Count(p => p.status != 0 && p.status != 1);
                return count;
            }
        }

        /// <summary>
        ///     可用代理列表
        /// </summary>
        public static IList<ProxyServer> AliveProxyList
        {
            get
            {
                return (from row in ProxyList
                        where row.status == 1
                        select row).ToList<ProxyServer>();
            }
        }

        #endregion

        #region 方法

        /// <summary>
        ///     添加或代理
        /// </summary>
        /// <param name="model"></param>
        public static void Set(ProxyServer model)
        {
            try
            {
                ProxyServer proxy = Get(model.proxy, model.port);
                if (null == proxy)
                {
                    try
                    {
                        model.id = ProxyList.Max(p => p.id) + 1;
                    }
                    catch
                    {
                        model.id = 1;
                    }
                    lock (ProxyList)
                    {
                        ProxyList.Add(model);
                    }
                }
                else
                {
                    lock (ProxyList)
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
            catch
            {
            }
        }

        /// <summary>
        ///     删除代理
        /// </summary>
        /// <param name="model"></param>
        public static void Remove(ProxyServer model)
        {
            try
            {
                ProxyServer proxy = Get(model.proxy, model.port);
                if (null != proxy)
                {
                    lock (ProxyList)
                    {
                        ProxyList.Remove(proxy);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        ///     删除代理
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="port"></param>
        public static void Remove(string proxy, int port)
        {
            try
            {
                ProxyServer model = Get(proxy, port);
                if (null != model)
                {
                    lock (ProxyList)
                    {
                        ProxyList.Remove(model);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        ///     获取代理
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static ProxyServer Get(string proxy, int port)
        {
            try
            {
                return ProxyList.FirstOrDefault(p => p.proxy == proxy && p.port == port);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     获取代理拷贝
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static ProxyServer GetCopy(string proxy, int port)
        {
            try
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
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     查询代理列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<ProxyServer> Search(ProxyServer model)
        {
            try
            {
                List<ProxyServer> result = (from row in ProxyList
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
            catch
            {
                return new List<ProxyServer>();
            }
        }

        #endregion
    }
}