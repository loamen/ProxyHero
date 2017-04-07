using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Loamen.WinControls.UI;
using ProxyHero.Common;
using ProxyHero.LanguageInformation;
using ProxyHero.Option;
using ProxyHero.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace ProxyHero.TabPages
{
    public partial class IEBrowserForm : DockContent
    {
        private readonly LanguageLoader languageLoader = new LanguageLoader();
        private bool IsPasting;
        private TreeNode rootNode;

        #region Properties

        /// <summary>
        ///     浏览器
        /// </summary>
        public WebBrowserEx Browser
        {
            get { return wbMain; }
        }

        #endregion

        #region Constructors

        public IEBrowserForm(string url)
        {
            try
            {
                InitializeComponent();
                LoadLanguage();

                Text = Config.LocalLanguage.MainPage.NewTab;

                wbMain.CanGoBackChanged += wbMain_CanGoBackChanged;
                wbMain.CanGoForwardChanged += wbMain_CanGoForwardChanged;
                wbMain.StatusTextChanged += wbMain_StatusTextChanged;
                Config.MainForm.WindowState = FormWindowState.Maximized;
                txtUrl.Text = url;
                wbMain.UserAgent = Config.LocalSetting.UserAgent;

                wbMain.Navigate(url);
                if (string.IsNullOrEmpty(url))
                    txtUrl.Text = "about:blank";

                Back.Enabled = wbMain.CanGoBack;
                Forward.Enabled = wbMain.CanGoForward;
                splitContainer1.Panel1Collapsed = true;

                wbMain.ScriptErrorsSuppressed = Config.LocalSetting.ScriptErrorsSuppressed;
            }
            catch (Exception ex)
            {
                Config.ConsoleEx.Debug(ex);
            }
        }

        #endregion

        #region Operation

        private void txtUrl_Click(object sender, EventArgs e)
        {
            //txtUrl.SelectAll();
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(txtUrl.Text);
            }
        }

        private void btn_Go_Click(object sender, EventArgs e)
        {
            Navigate(txtUrl.Text);
        }

        private void backFileToolStripButton_Click(object sender, EventArgs e)
        {
            wbMain.GoBack();
        }

        private void wbMain_CanGoBackChanged(object sender, EventArgs e)
        {
            Back.Enabled = wbMain.CanGoBack;
        }

        private void forwardFileToolStripButton_Click(object sender, EventArgs e)
        {
            wbMain.GoForward();
        }

        private void wbMain_CanGoForwardChanged(object sender, EventArgs e)
        {
            Forward.Enabled = wbMain.CanGoForward;
        }

        private void wbMain_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (wbMain.ProxyServer != "" && !wbMain.StatusLabel.Visible)
            {
                wbMain.StatusLabel.Visible = true;
            }
            if (!wbMain.DocumentText.Contains("<HTML></HTML>"))
            {
                if (txtUrl.Enabled)
                    txtUrl.Text = wbMain.Url.ToString();
            }
            Text = FormatString(wbMain.Document.Title);
        }

        private void wbMain_StatusTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (wbMain.ReadyState != WebBrowserReadyState.Complete)
                {
                    wbMain.StatusLabel.Visible = true;
                    wbMain.StatusLabel.Text = wbMain.StatusText;
                }
                else
                {
                    wbMain.StatusLabel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Config.ConsoleEx.Debug(ex);
            }
        }

        private void wbMain_BeforeNewWindow(object sender, EventArgs e)
        {
            var eventArgs = e as WebBrowserExtendedNavigatingEventArgs;
            Config.MainForm.OpenNewTab(eventArgs.Url);
            eventArgs.Cancel = true;
        }

        private void wbMain_NavigateError(object sender, WebBrowserNavigateErrorEventArgs e)
        {
            int code = e.StatusCode;
            // 发生错误时，转向本地页面
            if (code == 404)
            {
                wbMain.DocumentText = Config.GetErrorHtml(code);
            }
        }


        private void tsbFresh_Click(object sender, EventArgs e)
        {
            try
            {
                wbMain.Refresh();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void tsbStop_Click(object sender, EventArgs e)
        {
            try
            {
                wbMain.Stop();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void tsbSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUrl.Enabled)
                {
                    var formSetting = new ProxySettingForm();
                    formSetting.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void tsbHome_Click(object sender, EventArgs e)
        {
            try
            {
                wbMain.Navigate("http://www.loamen.com");
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void tsbFreshPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Config.MainForm.AutoSwitchingStatus.Text == Config.LocalLanguage.Messages.AutomaticSwitchingOn)
                {
                    if (txtUrl.Text != "" && txtUrl.Text.ToLower() != "about:blank")
                    {
                        txtUrl.Enabled = !txtUrl.Enabled;
                        if (txtUrl.Enabled)
                        {
                            LockAndRefresh.Image = Resources.lock1;
                            LockAndRefresh.Tag = "lock1";
                        }
                        else
                        {
                            LockAndRefresh.Image = Resources.lock2;
                            LockAndRefresh.Tag = "lock2";
                        }
                    }
                    else
                    {
                        MsgBox.ShowMessage(Config.LocalLanguage.Messages.PleaseEnterTheUrlYouWantToRefresh);
                    }
                }
                else
                {
                    if (LockAndRefresh.Tag + "" == "lock2")
                    {
                        txtUrl.Enabled = !txtUrl.Enabled;
                        LockAndRefresh.Image = Resources.lock1;
                    }
                    else
                        MsgBox.ShowMessage(Config.LocalLanguage.Messages.PleaseTurnOnTheAutomaticSwitching);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void wbMain_AfterProxyChanged(object sender, EventArgs e)
        {
            try
            {
                if (!txtUrl.Enabled)
                {
                    if (txtUrl.Text.Trim().ToLower() != wbMain.Url.ToString().ToLower())
                        wbMain.Navigate(txtUrl.Text.Trim());
                    else
                        wbMain.Refresh(WebBrowserRefreshOption.Completely);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void tsbAddToTest_Click(object sender, EventArgs e)
        {
            try
            {
                AddProxiesOfBrowserToTestingList();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        ///     将浏览器中的代理内容添加到验证代理列表
        /// </summary>
        public void AddProxiesOfBrowserToTestingList()
        {
            if (Config.MainForm.ProxyPage.Enabled)
            {
                if (Config.MainForm.ProxyPage.IsNotDownloadingOrTesting)
                {
                    if (!IsPasting) //如没有操作粘贴,上次操作没有完成
                    {
                        Config.ConsoleEx.Debug("Adding " + wbMain.Url + " proxies to testing list,please wait!");
                        wbMain.Document.ExecCommand("SelectAll", false, null);
                        wbMain.Document.ExecCommand("Copy", false, null);
                        wbMain.Document.ExecCommand("Unselect", false, null);
                        PasteThis();
                    }
                }
                else
                {
                    Config.ConsoleEx.Debug("Can't do this,you are downloading or testing!");
                }
            }
        }

        public void PasteThis()
        {
            IsPasting = true;
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                var text = (String) iData.GetData(DataFormats.Text);

                //text = TxtHelper.Format(text);
                string result = TxtHelper.ReadProxyTxt(text)[1];
                Config.ConsoleEx.Debug(result);

                Config.MainForm.ProxyPage.BindData();
                Config.MainForm.ProxyPage.Activate();
            }
            IsPasting = false;
        }

        #endregion

        #region Methods

        private void LoadLanguage()
        {
            Language language = Config.LocalLanguage;
            //if (System.Globalization.CultureInfo.InstalledUICulture.Name.ToLower().Contains("zh-"))

            if (!Config.LanguageFileName.Contains("Simplified Chinese.xml"))
            {
                object model = language.IeBrowserPage;
                languageLoader.Load(model, typeof (IEBrowserForm), this);
            }
        }

        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") &&
                !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                wbMain.Navigate(new Uri(address));
            }
            catch (UriFormatException)
            {
                return;
            }
        }

        protected string FormatString(string str)
        {
            if (str.Length > 20)
            {
                str = str.Substring(0, 19) + "...";
            }
            else if (str.Length == 0)
            {
                str = Config.LocalLanguage.MainPage.NewTab;
            }
            return str;
        }

        #endregion

        private void txtUrl_DoubleClick(object sender, EventArgs e)
        {
            txtUrl.SelectAll();
        }

        public void FavorTreeView(TreeView treeView) //动态生成收藏夹菜单
        {
            try
            {
                string favorfolder = Environment.GetFolderPath(Environment.SpecialFolder.Favorites); //获取系统收藏夹路径
                tvFavirate.Nodes.Clear();
                rootNode = new TreeNode("收藏夹");
                rootNode.ImageIndex = 0;
                treeView.Nodes.Add(rootNode);

                ListTreeNode(rootNode, new DirectoryInfo(favorfolder));
                    //引用生成收藏夹菜单的函数，                                                                        这个收藏夹ToolStripMenuItem参数是指向你要加入菜单的父菜单
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListTreeNode(TreeNode node, FileSystemInfo info) //生成收藏夹菜单的函数，递归使用
        {
            if (!info.Exists) return;
            var dir = info as DirectoryInfo;
            //不是目录 
            if (dir == null) return;

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            var nodes = new TreeNode[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i] as FileInfo;

                nodes[i] = new TreeNode();
                if (file != null)
                {
                    if (file.Extension == ".url")
                    {
                        string str = file.Name; //获取收藏夹的文件名（都是URL文件）
                        str = str.Remove(str.Length - 4); //去掉.url后缀名
                        string url = GetLinkFileUrl(file.FullName);
                        nodes[i].Text = str; //然后赋值给菜单文本
                        nodes[i].ToolTipText = url;
                        nodes[i].Tag = url;
                        nodes[i].ImageIndex = 1;
                        nodes[i].SelectedImageIndex = 2;
                        node.Nodes.Add(nodes[i]); //生成的子菜单添加到上一级菜单
                    }
                }
                    //对于子目录，进行递归调用 
                else
                {
                    var Direct = files[i] as DirectoryInfo;
                    nodes[i].SelectedImageIndex = 0;
                    nodes[i].Text = files[i].Name;
                    nodes[i].ImageIndex = 0;
                    node.Nodes.Add(nodes[i]); //生成的子菜单添加到上一级菜单
                    ListTreeNode(nodes[i], new DirectoryInfo(Direct.FullName)); //递归使用，生成子菜单
                }
            }
        }

        /// <summary>
        ///     根据收藏夹URL文件获取对应的链接网址
        /// </summary>
        /// <param name="linkFilePath"></param>
        /// <returns></returns>
        private string GetLinkFileUrl(string linkFilePath)
        {
            string strReturn = "";
            //异常检测开始
            try
            {
                var fs = new FileStream(linkFilePath, FileMode.Open, FileAccess.Read); //读取文件设定
                var myStreamReader = new StreamReader(fs, Encoding.Default); //设定读写的编码
                //使用StreamReader类来读取文件
                myStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                string strLine = myStreamReader.ReadLine();
                while (strLine != null)
                {
                    if (strLine.IndexOf("URL=") == 0)
                    {
                        strReturn = strLine.Replace("URL=", "");
                        break;
                    }
                    strLine = myStreamReader.ReadLine();
                }
                //关闭此StreamReader对象
                myStreamReader.Close();
            }
            catch
            {
                strReturn = "";
            }
            return strReturn;
        }

        private void tsbFavorites_Click(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
                if (rootNode == null)
                {
                    FavorTreeView(tvFavirate);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void tvFavirate_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                string url = e.Node.Tag + "";
                if (!string.IsNullOrEmpty(url))
                {
                    wbMain.Navigate(url);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }
    }
}