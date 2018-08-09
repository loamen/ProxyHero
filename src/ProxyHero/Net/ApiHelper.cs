using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using cn.bmob.api;
using cn.bmob.io;
using Loamen.Common;
using Loamen.Net;
using Loamen.PluginFramework;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.Model;
using ProxyHero.Threading;

namespace ProxyHero.Net
{
    public class ApiHelper : BmobBaseForm
    {
    
        //接下来要操作的数据的数据
        private ProxyHero.Entity.ProxyServers gameObject = new ProxyHero.Entity.ProxyServers();

        private ProxyItems _proxyItems;
        private Worker _worker;

        public ApiHelper()
        {
        }

        /// <summary>
        ///     下载代理数据
        /// </summary>
        /// <returns></returns>
        public ProxyItems DownloadProxyList()
        {
            var sw = new Stopwatch();
            var result = new ProxyItems {items = new List<ProxyServer>()};
            _proxyItems = new ProxyItems {items = new List<ProxyServer>()};

            try
            {
                sw.Start();
                var totalCount = GetTotalCount();
                if (totalCount > 0)
                {
                    const int pageSize = 1000; //每次取的数量 
                    var page = totalCount/pageSize;
                    page = totalCount%pageSize > 0 ? page + 1 : page; //总页数

                    #region 单线程

                    //for (var i = 0; i < page; i++)
                    //{
                    //    proxyItems = DownloadProxyList(i * pageSize, pageSize);
                    //    if (null != proxyItems && proxyItems.items != null)
                    //    {
                    //        result.items = result.items.AsEnumerable().Union(proxyItems.items.AsEnumerable()).Distinct().ToList<ProxyServer>();
                    //    }
                    //}

                    #endregion

                    #region 多线程

                    _worker = new Worker(new object[] {page, pageSize});
                    _worker.Worker_Started += WorkStart; //工作开始
                    _worker.Worker_Completed += WorkCompleted; //工作结束
                    _worker.Start();
                    while (_worker.ReturnValue == null)
                    {
                        if (sw.Elapsed.Minutes > 2)
                        {
                            sw.Stop();
                            return result;
                        }
                    }
                    result = (ProxyItems) _worker.ReturnValue;

                    #endregion
                }
                sw.Stop();
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        ///     下载代理数据
        /// </summary>
        /// <returns></returns>
        private ProxyItems DownloadProxyList(int sinceId, int count)
        {
            //创建一个BmobQuery查询对象
            BmobQuery query = new BmobQuery();
            //查询playerName的值为bmob的记录
            query.WhereEqualTo("isvip", false);
            query.Limit(count);
            query.Skip(sinceId);

            ProxyItems items = new ProxyItems();

            var future = Bmob.FindTaskAsync<ProxyServers>(ProxyServers.TABLE_NAME, query);

            //对返回结果进行处理
            var list = future.Result.results;
            if (list != null && list.Count > 0)
            {
                items.items = new List<ProxyServer>();
                foreach (var model in list)
                {
                    var proxy = model.Get();
                    items.items.Add(proxy);
                }
            }
            else
            {
                items = null;
            }


            return items;
        }

        /// <summary>
        ///     获取代理总数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            //创建一个BmobQuery查询对象
            BmobQuery query = new BmobQuery();
            //查询playerName的值为bmob的记录
            query.WhereEqualTo("status", 1);

            BmobInt count = 0;

            try
            {
                query.Count();
                var future = Bmob.FindTaskAsync<ProxyServers>(ProxyServers.TABLE_NAME, query);
                count = future.Result.count;
            }
            catch
            {
            }
            return count.Get();
        }

        /// <summary>
        ///     新增或插入数据
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public void AddOrUpdate(ProxyServer proxy)
        {
            var result = string.Empty;

            //创建一个BmobQuery查询对象
            BmobQuery query = new BmobQuery();
            //查询playerName的值为bmob的记录
            query.WhereEqualTo("proxy", proxy.proxy).And().WhereEqualTo("port", proxy.port);

            var futrue = Bmob.FindTaskAsync<ProxyServers>(ProxyServers.TABLE_NAME, query);
            ProxyServers proxyServer;
            if (futrue.Result == null || futrue.Result.results == null || futrue.Result.results.Count == 0)
            {
                proxyServer = new ProxyServers();
                proxyServer.Set(proxy);
                if (BmobUser.CurrentUser != null)
                {
                    proxyServer.user = BmobUser.CurrentUser;
                }
                var createResult = Bmob.CreateTaskAsync(proxyServer);
            }
            else
            {
                proxyServer = futrue.Result.results.FirstOrDefault();
                proxyServer.Set(proxy);
                if(proxyServer.status.Get() == 0)
                {
                    proxyServer.failedcount = proxyServer.failedcount.Get() + 1;
                }
                if (BmobUser.CurrentUser != null)
                {
                    proxyServer.user = BmobUser.CurrentUser;
                }

                if (proxy.failedcount > 5) //失败次数大于5次删除云端
                {
                    Bmob.DeleteTaskAsync(ProxyServers.TABLE_NAME, proxyServer.objectId);
                }
                else
                {
                    var updateResult = Bmob.UpdateTaskAsync(ProxyServers.TABLE_NAME, proxyServer.objectId, proxyServer);
                }
            }
           
        }


        #region 工作

        /// <summary>
        ///     开始工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="parameters"></param>
        private void WorkStart(object sender, EventArgs e, object[] parameters)
        {
            var page = (int) parameters[0]; //总页数
            var pageSize = (int) parameters[1]; //每页大小
            for (int i = 0; i < page; i++)
            {
                var thread = new WorkThread(new object[] {i*pageSize, pageSize});
                thread.WorkThread_Completed += WorkThread_Completed;
                thread.WorkThread_DoWork += WorkThread_DoWork; //设定工作内容
                thread.Start(); //启动工作线程
                _worker.Threads.Add(thread);
            }
        }

        /// <summary>
        ///     结束工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkCompleted(object sender, EventArgs e)
        {
            var worker = (Worker) sender;
            List<ProxyServer> proxies = _proxyItems.items.Where(p => p.status == 0).ToList();
            foreach (ProxyServer proxy in proxies)
            {
                proxy.status = -1;
            }
            worker.ReturnValue = _proxyItems;
        }

        #endregion

        #region 工作线程

        /// <summary>
        ///     线程工作内容
        /// </summary>
        /// <param name="WorkThread"></param>
        /// <param name="arg"></param>
        private void WorkThread_DoWork(object WorkThread, EventArgs arg)
        {
            var workThread = (WorkThread) WorkThread;
            var sinceId = (int) workThread.Parameters[0];
            var count = (int) workThread.Parameters[1];
            ProxyItems items = DownloadProxyList(sinceId, count);
            if (null != items && items.items != null)
            {
                _proxyItems.items = items.items.AsEnumerable().Union(_proxyItems.items.AsEnumerable()).Distinct().ToList();
            }
        }

        /// <summary>
        ///     线程结束时发出警告
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="arg"></param>
        private void WorkThread_Completed(object obj, EventArgs arg)
        {
            var thread = obj as WorkThread;
            if (thread != null && thread.Status == "Completed")
            {
                _worker.WorkThreadCompletedCount++;
            }

            if (_worker.WorkThreadCompletedCount == _worker.Threads.Count)
            {
                _worker.OnWorkerCompleted(); //全部工作完成，提醒上一级结束
            }
        }

        #endregion
    }
}