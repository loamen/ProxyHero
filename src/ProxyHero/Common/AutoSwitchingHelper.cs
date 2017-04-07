using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Loamen.Net;
using Loamen.Net.Entity;
using Loamen.PluginFramework;
using ProxyHero.Model;

namespace ProxyHero.Common
{
    public class AutoSwitchingHelper
    {
        private int _counts;
        private DataTable _dataProxy = new DataTable();
        readonly TestProxyHelper _testProxyHelper;

        /// <summary>
        ///     自动切换代理
        /// </summary>
        public AutoSwitchingHelper()
        {
            try
            {
                SetQueue();
                _testProxyHelper = new TestProxyHelper(Config.LocalSetting.DefaultTestOption,
                                                      Config.LocalSetting.TestTimeOut,
                                                      Config.LocalSetting.UserAgent);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                Config.ConsoleEx.Debug(ex);
                throw ex;
            }
        }

        /// <summary>
        ///     设置自动切换队列
        /// </summary>
        private void SetQueue()
        {
            if (Config.AutoProxyQueue.Count == 0)
            {
                List<ProxyServer> list = (from row in ProxyData.AliveProxyList
                                          orderby row.response
                                          select row).ToList<ProxyServer>();

                foreach (ProxyServer dr in list)
                {
                    var pe = new HttpProxy {Ip = dr.proxy, Port = dr.port, Response = dr.response};
                    Config.AutoProxyQueue.Enqueue(pe);
                }
            }
        }

        /// <summary>
        ///     自动切换代理
        /// </summary>
        public void SwitchingProxy()
        {
            if (Config.MainForm.AutoSwitchingStatus.Text == Config.LocalLanguage.Messages.AutomaticSwitchingOff) return;
            try
            {
                if (Config.AutoProxyQueue.Count > 0)
                {
                    Config.MainForm.tsslCountdown.Text = Config.LocalLanguage.Messages.Swithing;

                    #region 如果有代理队列

                    HttpProxy httpProxy = Config.AutoProxyQueue.Dequeue();
           

                    var result = _testProxyHelper.TestProxy(httpProxy);
                    var canUse = result.IsAlive;
                    var speed = result.Response;

                    if (canUse)
                    {
                        #region 更换代理

                        if (speed < Config.LocalSetting.AutoProxySpeed*1000)
                        {
                            if (Config.ProxyApplicatioin == "IE")
                            {
                                var ph = new ProxyHelper();
                                ph.SetIEProxy(httpProxy.IpAndPort, 1);
                                Config.MainForm.SetProxyStatusLabel();
                            }
                            else
                            {
                                SetBrowserProxy(httpProxy.IpAndPort);
                            }
                            Config.TsCountDown = new TimeSpan(0, 0, Config.LocalSetting.AutoChangeProxyInterval);
                            Config.MainForm.TimerAutoSwitchingProxy.Start();
                        }
                        else
                        {
                            Config.MainForm.tsslCountdown.Text =
                                Config.LocalLanguage.Messages.SpeedDoestConformToTheRequirement + "," +
                                Config.LocalLanguage.Messages.Swithing + "...";
                            Config.MainForm.SetToolTipText(httpProxy.IpAndPort +
                                                           Config.LocalLanguage.Messages
                                                                 .SpeedDoestConformToTheRequirement + "," +
                                                           Config.LocalLanguage.Messages.Swithing + "...");
                            Thread.Sleep(1000);
                            SwitchingProxy();
                        }

                        #endregion
                    }
                    else
                    {
                        #region 经验证代理已失效

                        Config.MainForm.tsslCountdown.Text = Config.LocalLanguage.Messages.ProxyIsDead + "," +
                                                             Config.LocalLanguage.Messages.Swithing + "...";
                        ProxyData.Remove(httpProxy.Ip, httpProxy.Port);

                        Config.MainForm.SetToolTipText(
                            string.Format(Config.LocalLanguage.Messages.ProxyIsDeadAutoSwithing, httpProxy.IpAndPort));
                        Thread.Sleep(1000);
                        SwitchingProxy();

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    if (_counts < 20) //如果队列为空，自动设置队列20次停止
                    {
                        SetQueue();
                        _counts++;
                    }
                    else
                    {
                        //Config.ProxyViewForm.StopAutoChange();
                        Config.MainForm.tsslCountdown.Text = string.Empty;
                        Config.ConsoleEx.Debug(Config.LocalLanguage.Messages.AutoSwitchingProxyListIsEmpty);
                        Config.MainForm.SetToolTipText(Config.LocalLanguage.Messages.AutoSwitchingProxyListIsEmpty);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                Thread.Sleep(3000);
                SwitchingProxy();
            }
        }

        private void SetBrowserProxy(string proxyServer)
        {
            Config.BuiltinBrowserProxyServer = proxyServer;
            Config.MainForm.SetBuiltinBrowserProxy(proxyServer);
        }

        /// <summary>
        ///     应用于内置浏览器，如果没内容浏览器则打开一个窗口
        /// </summary>
        /// <param name="proxyServer"></param>
        public static void StartBrowserProxy(string proxyServer)
        {
            if (!string.IsNullOrEmpty(proxyServer))
            {
                if (Config.MainForm.GetBrowserForms().Count == 0)
                {
                    Config.MainForm.OpenNewTab();
                }
            }
            Config.BuiltinBrowserProxyServer = proxyServer;
            Config.MainForm.SetBuiltinBrowserProxy(proxyServer);
        }
    }
}