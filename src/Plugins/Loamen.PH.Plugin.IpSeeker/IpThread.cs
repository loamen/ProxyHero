using Loamen.Net;
using Loamen.PH.Plugin.IpSeeker.Entity;
using Loamen.PluginFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Loamen.PH.Plugin.IpSeeker
{
    public class IpThread
    {
        #region 变量

        public delegate void CompletedEventHandler(object sender, EventArgs e);

        private readonly string DbPath;

        private readonly IApp _application;
        private readonly Thread _thread;
        public event CompletedEventHandler Completed;

        #endregion

        #region 属性

        /// <summary>
        ///     线程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     线程状态
        /// </summary>
        public ThreadState ThreadState
        {
            get
            {
                if (_thread == null) return ThreadState.Unstarted;
                return _thread.ThreadState;
            }
        }

        /// <summary>
        ///     当前线程执行状态
        /// </summary>
        public bool IsAlive
        {
            get
            {
                if (_thread == null) return false;
                return _thread.IsAlive;
            }
        }

        /// <summary>
        ///     总刷新数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        ///     失败次数
        /// </summary>
        public int FailedCount { get; set; }

        public string Description { get; set; }

        #endregion

        #region 构造函数

        public IpThread(string dbPath, IApp app)
        {
            DbPath = dbPath;
            _application = app;
            _thread = new Thread(DoWork);
            Name = _thread.ManagedThreadId.ToString(CultureInfo.InvariantCulture);
            Status = _thread.ThreadState.ToString();
        }

        #endregion

        #region 方法

        public void Start()
        {
            _thread.Start(this);
        }

        public void Abort()
        {
            Description = "";
            _thread.Abort();
            OnCompleted();
        }

        public void OnCompleted()
        {
            if (null != Completed)
            {
                Completed(this, new EventArgs()); //发出警报
            }
        }

        private static void DoWork(object data)
        {
            var ipThread = (IpThread) data;

            try
            {
                while (IpSeekerForm.ProxyQueue.Count > 0)
                {
                    Monitor.Enter(IpSeekerForm.ProxyQueue);
                    Proxy proxy = IpSeekerForm.ProxyQueue.Dequeue();
                    Monitor.Exit(IpSeekerForm.ProxyQueue);
                    ipThread.Description = proxy.IpAndPort + "开始刷新...";
                    try
                    {
                        string location = SearchIp(proxy.Ip, ipThread.DbPath);
                        lock (ipThread._application.ProxyList)
                        {
                            List<ProxyServer> drs = (from p in ipThread._application.ProxyList
                                                     where p.proxy == proxy.Ip &&
                                                           p.port == proxy.Port
                                                     select p).ToList<ProxyServer>();
                            foreach (ProxyServer dr in drs)
                            {
                                dr.country = location;
                            }
                        }
                    }
                    catch (ThreadAbortException)
                    {
                    }
                    catch
                    {
                    }
                    finally
                    {
                        ipThread.TotalCount++;
                        var fm = (IpSeekerForm) Application.OpenForms["IpSeekerForm"];
                        if (null != fm && (ipThread.TotalCount%1000 == 0))
                        {
                            fm.Refresh(ipThread.TotalCount + "/" + ipThread._application.ProxyList.Count);
                        }
                    }
                    ipThread.Status = ipThread._thread.ThreadState.ToString();
                }

                ipThread.Status = "Completed";
            }
            catch (Exception ex)
            {
                // 线程被放弃
                ipThread.Status = "Completed";
                Console.WriteLine(ex.Message);
                //WriteException(ex);
            }
            finally
            {
                ipThread.OnCompleted();
            }
        }

        public static string SearchIp(string strIp, string path)
        {
            var pz = new CzIpHelper(path);
            if (pz.SetDbFilePath(path))
            {
                if (pz.IpAddressCheck(strIp))
                {
                    return pz.GetAddressWithIp(strIp).ToLower().Replace("cz88.net", "Loamen.Com");
                }
                else
                {
                    return strIp + "格式不正确";
                }
            }
            else
            {
                return "文件" + path + "不存在";
            }
        }

        #endregion
    }
}