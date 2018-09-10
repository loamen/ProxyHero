using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.Net;
using Loamen.Net.Entity;
using Loamen.PluginFramework;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.LanguageInformation;
using ProxyHero.Model;
using ProxyHero.Net;
using ProxyHero.Option;
using ProxyHero.Properties;
using ProxyHero.Proxy;
using WeifenLuo.WinFormsUI.Docking;
using Timer = System.Timers.Timer;

namespace ProxyHero.TabPages
{
    public partial class ProxyForm : DockContent
    {
        #region Variable

        private readonly LanguageLoader _languageLoader = new LanguageLoader();
        private readonly ProxyHelper _proxyHelper = new ProxyHelper();
        private readonly Timer _timerCheckAllTested = new Timer();

        /// <summary>
        ///     获取代理类
        /// </summary>
        private Downloader _downloader;

        /// <summary>
        ///     是否更新过
        /// </summary>
        private bool _isUpdated = true;

        private string _strIp;
        private string _strPort;

        public Tester Tester;
        private int _testingNumber; //验证中的代理数量

        private delegate void DelegateVoid();

        #endregion

        #region init

        public ProxyForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            LoadLanguage();
            DockHandler.CloseButton = false;
            DockHandler.CloseButtonVisible = false;
            CountryLabel.Text = CountryLabel.Text + @":";
            PortLabel.Text = PortLabel.Text + @":";
            AnonymityLabel.Text = AnonymityLabel.Text + @":";

            _timerCheckAllTested.Interval = 1000;
            _timerCheckAllTested.Enabled = false;
            _timerCheckAllTested.Elapsed += timerCheckAllTested_Elapsed;
            CheckForIllegalCrossThreadCalls = false;
            dgvProxyList.AutoGenerateColumns = false;

            var dataGridViewColumn = dgvProxyList.Columns["LocationColumn"];
            if (dataGridViewColumn != null)
            {
                var gridViewColumn = dgvProxyList.Columns["AnonymityCol"];
                if (gridViewColumn != null)
                    gridViewColumn.Visible = dataGridViewColumn.Visible = true;
            }
            var viewColumn = dgvProxyList.Columns["AnonymityEn"];
            if (viewColumn != null)
            {
                var column = dgvProxyList.Columns["CountryEn"];
                if (column != null)
                    viewColumn.Visible = column.Visible = false;
            }
        }

        #endregion

        #region UI

        /// <summary>
        ///     加载窗体语言
        /// </summary>
        private void LoadLanguage()
        {
            Language language = Config.LocalLanguage;
            //if (System.Globalization.CultureInfo.InstalledUICulture.Name.ToLower().Contains("zh-"))
            Text = Config.IsChineseLanguage ? "代理公布器" : "Proxy Window";
            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.ProxyPage;
                _languageLoader.Load(model, typeof (ProxyForm), this);
            }
        }

        private void DownloadProxy_Click(object sender, EventArgs e)
        {
            try
            {
                if (Config.ProxySiteUrlList.Count > 0)
                {
                    //如果当前显示为“下载数据”并且“停止验证”按钮不可用，“菜单上验证按钮”不可用
                    if (DownloadProxy.Text == Config.LocalLanguage.ProxyPage.DownloadProxy &&
                        !StopTest.Enabled)
                    {
                        DownloadProxy.Text = Config.LocalLanguage.Messages.StopDownload;
                        DownloadProxy.ToolTipText = DownloadProxy.Text;
                        DownloadProxy.Image = Resources.stopread;
                        DownloadingEnable(false);
                        DownloadProxyList();
                    }
                    else if (DownloadProxy.Text == Config.LocalLanguage.Messages.StopDownload)
                    {
                        Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.Stopping);
                        Config.ConsoleEx.Debug(Config.LocalLanguage.Messages.Stopping);
                        DownloadProxy.Enabled = false;
                        _downloader.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void TestAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProxyData.ProxyList.Count > 0 && DownloadProxy.Text == Config.LocalLanguage.ProxyPage.DownloadProxy)
                {
                    Config.MainForm.WindowState = FormWindowState.Minimized;
                    Config.MainForm.SetToolTipText(Config.LocalLanguage.Messages.MinimizedToSaveResources);
                    TestAllProxies(ProxyData.ProxyList);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void StopTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (Config.MainForm.StatusContains(Config.LocalLanguage.Messages.Testing))
                {
                    StopTest.Enabled = false;
                    Tester.Stop();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void ImportProxy_Click(object sender, EventArgs e)
        {
            try
            {
                Import();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void ExportProxy_Click(object sender, EventArgs e)
        {
            try
            {
                Export();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void DeleteInvalidProxy_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Config.MainForm.StatusContains(Config.LocalLanguage.Messages.Testing))
                {
                    List<ProxyServer> list = (from row in ProxyData.ProxyList
                                              where row.status != 0
                                              select row).ToList<ProxyServer>();

                    ProxyData.ProxyList = list;

                    BindDgv(ProxyData.ProxyList);

                    Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.Total + ":" + ProxyData.ProxyList.Count);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void DeleteSeleted_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Config.MainForm.StatusContains(Config.LocalLanguage.Messages.Testing))
                {
                    DeleteProxy();
                    Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.Total + ":" + ProxyData.ProxyList.Count);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            var of = new OptionForm();
            of.ShowDialog();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (DownloadProxy.Text == Config.LocalLanguage.ProxyPage.DownloadProxy && !StopTest.Enabled)
                {
                    if (tstxtArea.Text.Trim() != "" || tstxtPort.Text.Trim() != "" || tstxtAnonymity.Text.Trim() != "")
                    {
                        var model = new ProxyServer();
                        if (tstxtArea.Text.Trim() != "")
                            model.country = tstxtArea.Text.Trim();
                        if (tstxtPort.Text.Trim() != "")
                            model.port = int.Parse(tstxtPort.Text.Trim());
                        if (tstxtAnonymity.Text.Trim() != "")
                            model.anonymity = tstxtAnonymity.Text.Trim();

                        IList<ProxyServer> list = ProxyData.Search(model);

                        BindDgv(list);
                    }
                    else
                    {
                        BindDgv(ProxyData.ProxyList);
                    }
                    Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.Total + @":" + dgvProxyList.RowCount);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        #endregion

        #region Operation

        private void tsmiTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProxyList.SelectedRows.Count == 1)
                {
                    string ipAndPort = GetProxyInfo(false);
                    if (!string.IsNullOrEmpty(ipAndPort))
                    {
                        _strIp = ipAndPort.Split(':')[0];
                        _strPort = ipAndPort.Split(':')[1];
                    }

                    if (string.IsNullOrEmpty(_strIp) ||
                        string.IsNullOrEmpty(_strPort))
                    {
                        Config.MainForm.SetStatusText("请先设置代理服务器，再验证！");
                        return;
                    }

                    DelegateVoid delegateTest = TestSingleProxy;
                    var testingThread = new Thread(new ThreadStart(delegateTest));
                    testingThread.Start();
                }
                else if (dgvProxyList.SelectedRows.Count > 1)
                {
                    IList<ProxyServer> list = (from DataGridViewRow dgvr in dgvProxyList.SelectedRows select (dgvr.DataBoundItem as ProxyServer)).ToList();
                    if (list.Count > 0)
                    {
                        TestAllProxies(list);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }


        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            try
            {
                string strProxyServer = GetProxyInfo(true);
                Clipboard.SetDataObject(strProxyServer, true, 5, 3000);
                Config.ConsoleEx.WriteLine(Config.LocalLanguage.Messages.CopySucess + ":" + strProxyServer);
            }
            catch (ExternalException eex)
            {
                LogHelper.WriteException(eex);
                MsgBox.ShowErrorMessage(Config.LocalLanguage.Messages.CopyFailed);
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        ///     检查是否完成
        /// </summary>
        private void tester_Completed(object obj, EventArgs arg)
        {
            #region 验证完成

            var sb = new StringBuilder();
            if (Tester.Watch.Elapsed.Days > 0)
                sb.Append(Tester.Watch.Elapsed.Days + "d");
            if (Tester.Watch.Elapsed.Hours > 0)
                sb.Append(Tester.Watch.Elapsed.Hours + "h");
            if (Tester.Watch.Elapsed.Minutes > 0)
                sb.Append(Tester.Watch.Elapsed.Minutes + "m");
            sb.Append(Tester.Watch.Elapsed.Seconds + "s");

            Test.Enabled = DeleteThis.Enabled = Clear.Enabled = true;
            var sbInfo = new StringBuilder();
            if (StopTest.Enabled)
            {
                sbInfo.Append(string.Format(Config.LocalLanguage.Messages.NumOfProxiesTested, _testingNumber));
                var message = Config.LocalLanguage.Messages.AllTestingCompleted + "," + sb;
                Config.MainForm.SetToolTipText(message);
                Config.ConsoleEx.Debug(message);
            }
            else
            {
                sbInfo.Append(Config.LocalLanguage.Messages.TestingHaveBeenTerminated);
                var message = Config.LocalLanguage.Messages.TestingHaveBeenTerminated + "," + sb;
                Config.MainForm.SetToolTipText(message);
                Config.ConsoleEx.Debug(message);
            }
            sbInfo.Append("," + sb);
            UpdateLabelInfo();

            try
            {
                var thread = new Thread(UpdateCloudProxyList);
                thread.Start();
            }
            catch (Exception)
            {
            }

            TestingAllEnable(true);
            _timerCheckAllTested.Stop();
            _timerCheckAllTested.Enabled = false;
            Config.MainForm.SetStatusText(sbInfo.ToString());

            #endregion
        }

        /// <summary>
        ///     显示验证信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCheckAllTested_Elapsed(object sender, EventArgs e)
        {
            var TestedThreadsCount = 0;
            try
            {
                if (null != Tester && null != Tester.Threads)
                {
                    var aliveThreadsCount = 0; //活动线程数
                    var testedProxyCount = 0; //验证完成代理数量
                    foreach (TestThread t in Tester.Threads)
                    {
                        if (t.IsAlive)
                        {
                            aliveThreadsCount++;
                        }
                        if (t.Status == "Completed")
                        {
                            TestedThreadsCount++;
                        }
                        testedProxyCount += t.TestedCount;
                    }

                    var strInfo = new StringBuilder();

                    strInfo.Append(string.Format(Config.LocalLanguage.Messages.NumOfThreadsAreTesting, aliveThreadsCount));
                    strInfo.Append(testedProxyCount);
                    strInfo.Append("/");
                    strInfo.Append(_testingNumber);
                    strInfo.Append("...");

                    if (Config.LocalSetting.NeedDebug && null != Config.MainForm)
                        Config.MainForm.InfoPage.UpdateDataGrid();

                    if (Config.MainForm != null) Config.MainForm.SetStatusText(strInfo.ToString());
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                _timerCheckAllTested.Stop();
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void tsmiCancelProxy_Click(object sender, EventArgs e)
        {
            try
            {
                _proxyHelper.SetIEProxy("", 0);
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DeleteSeleted_Click(sender, e);
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MsgBox.ShowQuestionMessage(Config.LocalLanguage.Messages.AreYouSureClearThis))
            {
                ProxyData.ProxyList.Clear();
                if (File.Exists(Config.LastProxyFileName)) File.Delete(Config.LastProxyFileName);
                BindDgv(ProxyData.ProxyList);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     更新云服务器信息
        /// </summary>
        private void UpdateCloudProxyList()
        {
            TimeSpan ts = DateTime.Now - Config.LateUpdateProxyListTime;
            if (_isUpdated && ts.TotalSeconds > 30)
            {
                _isUpdated = false;

                var cloudHelper = new CloudHelper();

                var list = (from p in ProxyData.ProxyList
                             where p.status == 0
                             select p).ToList();

                var updateList = new List<ProxyServer>();
                foreach (var item in from item in list let cloudModel = CloudProxyData.Get(item.proxy, item.port) where null != cloudModel where updateList.FirstOrDefault(p => p.proxy == item.proxy && p.port == item.port) == null select item)
                {
                    updateList.Add(item); //云端存在且无法连接的数据
                }

                updateList = (from p in ProxyData.ProxyList
                        where p.status == 1
                        select p).Union(updateList).Distinct().ToList();

                cloudHelper.UploadProxyList(updateList);
                Config.MainForm.ConnectCloud();
                _isUpdated = true;
            }
        }

        private void EnableThis()
        {
            dgvProxyList.Enabled = Toolbar.Enabled = !dgvProxyList.Enabled;
        }

        /// <summary>
        ///     下载代理列表
        /// </summary>
        private void DownloadProxyList()
        {
            Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.Downloading);
            _downloader = new Downloader(Config.ProxySiteUrlList);
            _downloader.Completed += downloader_Completed;
            _downloader.Start();
        }

        public void BindDgv(IList<ProxyServer> list)
        {
            dgvProxyList.DataSource = typeof (ListEx<ProxyServer>);
            if (null != list && list.Count > 0)
            {
                var listEx = new ListEx<ProxyServer>();
                foreach (ProxyServer item in list)
                {
                    listEx.Add(item);
                }
                dgvProxyList.DataSource = listEx;
            }
            dgvProxyList.Refresh();
        }

        public void BindData()
        {
            dgvProxyList.DataSource = typeof (ListEx<ProxyServer>);
            if (null != ProxyData.ProxyList && ProxyData.ProxyList.Count > 0)
            {
                var list = new ListEx<ProxyServer>();
                foreach (ProxyServer item in ProxyData.ProxyList)
                {
                    list.Add(item);
                }
                dgvProxyList.DataSource = list;
            }
            dgvProxyList.Refresh();
            DelegateUpdateLabelInfo();
        }

        /// <summary>
        ///     获取代理信息
        /// </summary>
        /// <returns></returns>
        private string GetProxyInfo(bool isAll)
        {
            if (dgvProxyList.Rows.Count > 0)
            {
                if (!isAll)
                {
                    if (dgvProxyList.CurrentRow != null)
                    {
                        int rowIndex = dgvProxyList.CurrentRow.Index;
                        return dgvProxyList.Rows[rowIndex].Cells[1].Value + ":" + dgvProxyList.Rows[rowIndex].Cells[2].Value;
                    }
                }
                var sb = new StringBuilder();
                foreach (DataGridViewRow dgvr in dgvProxyList.SelectedRows)
                {
                    sb.AppendFormat("{0}:{1}\n", dgvr.Cells[1].Value, dgvr.Cells[2].Value);
                }

                return sb.ToString();
            }
            return "";
        }


        /// <summary>
        ///     验证代理
        /// </summary>
        private void TestSingleProxy()
        {
            DelegateVoid delegateVoid = EnableThis;
            Invoke(delegateVoid);

            Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.Testing);
            var sw = new Stopwatch();
            sw.Start();
            HttpProxy httpProxy= GetProxySetting();
            var model = new ProxyServer {port = httpProxy.Port, proxy = httpProxy.Ip};

            try
            {
                model.description = Config.LocalLanguage.Messages.Testing;
                model.status = -1;

                var testOption = new TestOption();

                #region 读取验证配置

                if (Config.LocalSetting != null && Config.LocalSetting.DefaultTestOption != null)
                {
                    testOption = Config.LocalSetting.DefaultTestOption;
                }

                #endregion

                if (Config.LocalSetting != null && new TestProxyHelper(testOption, Config.LocalSetting.TestTimeOut, Config.LocalSetting.UserAgent).TestProxy(httpProxy).IsAlive)
                {
                    sw.Stop();
                    Config.MainForm.SetStatusText(
                        string.Format(
                            Config.LocalLanguage.Messages.ProxyIsAlive + "！" +
                            Config.LocalLanguage.Messages.TimeConsuming, sw.ElapsedMilliseconds.ToString("F0")));

                    model.description = sw.ElapsedMilliseconds + "ms";
                    model.response = int.Parse(sw.ElapsedMilliseconds + "");
                    model.status = 1;
                    ProxyData.Set(model);
                }
                else
                {
                    Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.ProxyIsDead);
                    model.description = Config.LocalLanguage.Messages.ProxyIsDead;
                    model.response = 99999;
                    model.status = 0;
                    ProxyData.Set(model);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    model.status = 0;
                    model.response = 99999;
                    model.description = ex.Message;
                    Config.MainForm.SetStatusText(ex.Message);
                }
                catch
                {
                }
            }
            finally
            {
                Invoke(delegateVoid);
            }
        }

        /// <summary>
        ///     获取代理设置
        /// </summary>
        /// <returns></returns>
        public HttpProxy GetProxySetting()
        {
            var setting = new HttpProxy();

            if (_strIp != "")
            {
                setting.Ip = _strIp;
            }
            if (_strPort != "")
            {
                setting.Port = int.Parse(_strPort);
            }

            setting.Type = 2;
            setting.UserName = string.Empty;
            setting.Password = string.Empty;
            return setting;
        }


        /// <summary>
        ///     验证全部代理
        /// </summary>
        public void TestAllProxies(IList<ProxyServer> list)
        {
            try
            {
                if (list.Count > 0)
                {
                    _testingNumber = list.Count;
                    TestingAllEnable(false);
                    var message = Config.LocalLanguage.Messages.Testing + "...";
                    Config.MainForm.SetStatusText(message);
                    Config.ConsoleEx.Debug(message);
                    Test.Enabled = Cut.Enabled = Clear.Enabled = DeleteThis.Enabled = false;
                    _timerCheckAllTested.Enabled = true;
                    _timerCheckAllTested.Start();

                    Tester = new Tester(list);
                    Tester.TesterCompleted += tester_Completed;
                    Tester.Start();
                }
            }
            catch (Exception)
            {
                if (Tester != null)
                    Tester.Stop();
                throw;
            }
        }

        /// <summary>
        ///     删除代理
        /// </summary>
        public void DeleteProxy()
        {
            foreach (DataGridViewRow dgvr in dgvProxyList.SelectedRows)
            {
                var model = (ProxyServer) dgvr.DataBoundItem;
                ProxyData.Remove(model);
            }
            BindData();
            _strIp = "";
            _strPort = "";
        }

        #endregion

        /// <summary>
        ///     是否没有下载或验证
        /// </summary>
        public bool IsNotDownloadingOrTesting
        {
            get
            {
                return DownloadProxy.Text == Config.LocalLanguage.ProxyPage.DownloadProxy &&
                       !StopTest.Enabled;
            }
        }

        /// <summary>
        ///     列表添加
        /// </summary>
        /// <param name="values"></param>
        public void DataGridViewAddRow(object[] values)
        {
            try
            {
                var dr = new DataGridViewRow();
                dr.SetValues(values);

                var hasIt = dgvProxyList.Rows.Cast<DataGridViewRow>().Any(dgvr => dgvr.Cells[0].Value.ToString() == values[0].ToString() && dgvr.Cells[1].Value.ToString() == values[1].ToString());
                if (hasIt) return;
                lock (dgvProxyList)
                {
                    dgvProxyList.Rows.Add(values);
                }
                dgvProxyList.Refresh();
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
            }
        }

        /// <summary>
        ///     下载完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void downloader_Completed(object sender, EventArgs arg)
        {
            try
            {
                if (CloudProxyData.CloudProxyList.Count > 0)
                {
                    lock (ProxyData.ProxyList)
                    {
                        List<ProxyServer> list =
                            ProxyData.ProxyList.Union(CloudProxyData.CloudProxyList).Distinct().ToList();
                        List<ProxyServer> listUni = (from row in list
                                                     select new ProxyServer {proxy = row.proxy, port = row.port})
                            .Distinct().ToList();

                        ProxyData.ProxyList.Clear();
                        foreach (ProxyServer proxy in listUni)
                        {
                            ProxyServer model = list.FirstOrDefault(p => p.proxy == proxy.proxy && p.port == proxy.port);
                            ProxyData.Set(model);
                        }
                    }
                }
                if (ProxyData.TotalNum == 0)
                {
                    Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.NetworkIsbusyPleaseTryAgainLater);
                }
                else
                {
                    DelegateVoid dv = BindData;
                    Invoke(dv);
                    DelegateUpdateLabelInfo();
                    Config.MainForm.SetToolTipText(string.Format(Config.LocalLanguage.Messages.NumOfProxiesDownloaded,
                                                                 ProxyData.TotalNum));
                    dv = ReadDataOk;
                    Invoke(dv);
                    Config.MainForm.SetStatusText(string.Format(Config.LocalLanguage.Messages.NumOfProxiesDownloaded,
                                                                ProxyData.TotalNum));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
            }
            finally
            {
                DelegateVoid dv = ReadDataOk;
                Invoke(dv);
            }
        }

        /// <summary>
        ///     更新显示板数据信息
        /// </summary>
        public void DelegateUpdateLabelInfo()
        {
            DelegateVoid dv = UpdateLabelInfo;
            Invoke(dv);
        }

        /// <summary>
        ///     更新显示板信息
        /// </summary>
        private void UpdateLabelInfo()
        {
            try
            {
                var sb = new StringBuilder();
                var list = (ListEx<ProxyServer>) dgvProxyList.DataSource;

                int aliveCount = list.Count(p => p.status == 1);
                int deadCount = list.Count(p => p.status == 0);
                int notTestCount = list.Count(p => p.status != 1 && p.status != 0);

                sb.Append(Config.LocalLanguage.Messages.Alive + ":" + aliveCount + "/" + dgvProxyList.Rows.Count);
                sb.Append(Config.LocalLanguage.Messages.Dead + ":" + deadCount);
                sb.Append(Config.LocalLanguage.Messages.NotTest + ":" + notTestCount);
                tsslCountInfo.Text = sb.ToString();
                Config.MainForm.SetStatusText(Config.LocalLanguage.Messages.Total + ":" + list.Count);
                if (dgvProxyList.Rows.Count > 0)
                {
                    if (DownloadProxy.Text == Config.LocalLanguage.ProxyPage.DownloadProxy &&
                        !StopTest.Enabled)
                    {
                        Delete.Enabled = true;
                    }
                }
                else
                {
                    Delete.Enabled = false;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        ///     设置IE代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiIE_Click(object sender, EventArgs e)
        {
            try
            {
                _proxyHelper.SetIEProxy(GetProxyInfo(false), 1);
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        ///     设置内置浏览器代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiInnerBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                AutoSwitchingHelper.StartBrowserProxy(GetProxyInfo(false));
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        ///     正在下载
        /// </summary>
        /// <param name="enable"></param>
        public void DownloadingEnable(bool enable)
        {
            TestAll.Enabled =
                ImportProxy.Enabled =
                ExportProxy.Enabled =
                Delete.Enabled =
                Test.Enabled = Paste.Enabled = Cut.Enabled = DeleteThis.Enabled = Clear.Enabled = enable;
        }

        /// <summary>
        ///     验证所有代理
        /// </summary>
        /// <param name="enable"></param>
        public void TestingAllEnable(bool enable)
        {
            DownloadProxy.Enabled = enable;
            DownloadingEnable(enable);
            StopTest.Enabled = !enable;
        }

        /// <summary>
        ///     数据读取完毕
        /// </summary>
        public void ReadDataOk()
        {
            DownloadProxy.Text = Config.LocalLanguage.ProxyPage.DownloadProxy;
            DownloadProxy.ToolTipText = DownloadProxy.Text;
            DownloadProxy.Image = Resources.read;
            DownloadingEnable(true);
            DownloadProxy.Enabled = true;
        }

        /// <summary>
        ///     导出TXT
        /// </summary>
        private void Export()
        {
            if (dgvProxyList.Rows.Count > 0)
            {
                var list = (IList<ProxyServer>) dgvProxyList.DataSource;

                if (list.Count > 0)
                {
                    var objSave = new SaveFileDialog
                        {
                            Filter = @"(*.txt)|*.txt|" + @"(*.*)|*.*",
                            FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt"
                        };

                    if (objSave.ShowDialog() == DialogResult.OK)
                    {
                        string[] result = TxtHelper.WriteProxyTxt(list, objSave.FileName, Encoding.UTF8);
                        if (result[1] != "")
                        {
                            MsgBox.ShowErrorMessage(result[1]);
                        }
                    }
                }
                else
                {
                    MsgBox.ShowMessage("No Data!");
                }
            }
        }

        /// <summary>
        ///     导入TXT
        /// </summary>
        private void Import()
        {
            if (Config.MainForm.StatusContains(Config.LocalLanguage.Messages.Testing)) return;
            var objOpen = new OpenFileDialog {Filter = @"(*.txt)|*.txt|" + @"(*.*)|*.*"};

            if (objOpen.ShowDialog() == DialogResult.OK)
            {
                string[] result = TxtHelper.ReadProxyTxt(objOpen.FileName, Encoding.UTF8);

                if (result[1] != "")
                {
                    MsgBox.ShowErrorMessage(result[1]);
                }
                else
                {
                    BindDgv(ProxyData.ProxyList);
                }
            }
        }

        /// <summary>
        ///     粘贴
        /// </summary>
        public void PasteThis()
        {
            var iData = Clipboard.GetDataObject();
            if (iData == null || !iData.GetDataPresent(DataFormats.Text)) return;
            var text = (String) iData.GetData(DataFormats.Text);

            //text = TxtHelper.Format(text);
            TxtHelper.ReadProxyTxt(text);
            BindData();
        }

        /// <summary>
        ///     点击粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Paste_Click(object sender, EventArgs e)
        {
            PasteThis();
        }

        /// <summary>
        ///     剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cut_Click(object sender, EventArgs e)
        {
            try
            {
                tsmiCopy_Click(sender, e);
                tsmiDelete_Click(sender, e);
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        ///     排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProxyList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (e.RowIndex == -1 && ProxyData.ProxyList.Count > 0)
                //{
                //    var columName = this.dgvProxyList.Columns[e.ColumnIndex].DataPropertyName;
                //    var list = (List<ProxyServer>)this.dgvProxyList.DataSource;

                //    if (columName == "response")
                //    {
                //        if (responseSorted)
                //        {
                //            list = list.OrderByDescending(p => p.response).ToList<ProxyServer>();
                //        }
                //        else
                //        {
                //            list = list.OrderBy(p => p.response).ToList<ProxyServer>();
                //        }
                //        BindDgv(list);
                //    }

                //    if (columName == "anonymity")
                //    {
                //        if (responseSorted)
                //        {
                //            list = list.OrderByDescending(p => p.anonymity).ToList<ProxyServer>();
                //        }
                //        else
                //        {
                //            list = list.OrderBy(p => p.anonymity).ToList<ProxyServer>();
                //        }
                //        BindDgv(list);
                //    }

                //    if (columName == "country")
                //    {
                //        if (responseSorted)
                //        {
                //            list = list.OrderByDescending(p => p.country).ToList<ProxyServer>();
                //        }
                //        else
                //        {
                //            list = list.OrderBy(p => p.country).ToList<ProxyServer>();
                //        }
                //        BindDgv(list);
                //    }

                //    if (columName == "proxy")
                //    {
                //        if (responseSorted)
                //        {
                //            list = list.OrderByDescending(p => p.proxy).ToList<ProxyServer>();
                //        }
                //        else
                //        {
                //            list = list.OrderBy(p => p.proxy).ToList<ProxyServer>();
                //        }
                //        BindDgv(list);
                //    }

                //    if (columName == "port")
                //    {
                //        if (responseSorted)
                //        {
                //            list = list.OrderByDescending(p => p.port).ToList<ProxyServer>();
                //        }
                //        else
                //        {
                //            list = list.OrderBy(p => p.port).ToList<ProxyServer>();
                //        }
                //        BindDgv(list);
                //    }

                //    if (columName == "status")
                //    {
                //        if (responseSorted)
                //        {
                //            list = list.OrderByDescending(p => p.status).ToList<ProxyServer>();
                //        }
                //        else
                //        {
                //            list = list.OrderBy(p => p.status).ToList<ProxyServer>();
                //        }
                //        BindDgv(list);
                //    }
                //    responseSorted = !responseSorted;
                //}
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        ///     启用或禁用界面
        /// </summary>
        /// <param name="enabled"></param>
        public void EnabledProxyPageUi(bool enabled)
        {
            Toolbar.Enabled = contextMenuStrip1.Enabled = enabled;
        }
    }
}