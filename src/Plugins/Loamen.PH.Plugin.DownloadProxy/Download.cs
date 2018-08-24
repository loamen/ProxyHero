using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.PH.Plugin.DownloadProxy.Entity;
using Loamen.PH.Plugin.DownloadProxy.Properties;
using Loamen.PluginFramework;
using WeifenLuo.WinFormsUI.Docking;

namespace Loamen.PH.Plugin.DownloadProxy
{
    public class Download : IPlugin
    {
        #region 变量

        private IApp app;
        private string author = "龙门信息网";
        private string description = "代理公布器采集网页中的代理资源插件";
        private Downloader downloader;
        private string lPHVersion = "1.6.0+";

        private string name = "采集网页代理资源插件";
        private ToolStripButton tsbDownload;
        private ToolStripMenuItem tsmiDownloadPlugin;
        private ToolStripMenuItem tsmiDownloading;
        private ToolStripMenuItem tsmiOption;
        private string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        #endregion

        #region 继承属性 此处不用更改

        public IApp App
        {
            get { return app; }
            set
            {
                app = value;
                InitApp(); //此处不要更改
            }
        }

        /// <summary>
        ///     插件名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        ///     作者名称
        /// </summary>
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        /// <summary>
        ///     插件版本
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        ///     适用于代理公布器的代理
        /// </summary>
        public string LPHVersion
        {
            get { return lPHVersion; }
            set { lPHVersion = value; }
        }

        /// <summary>
        ///     插件描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 插件添加的菜单
        /// </summary>
        public List<string> MenuItems { get; set; }
        /// <summary>
        /// 插件添加的工具按钮
        /// </summary>
        public List<string> ToolButtons { get; set; }
        #endregion

        #region 方法

        /// <summary>
        ///     初始化宿主
        /// </summary>
        public void InitApp()
        {
            #region

            // 定义一个下拉菜单
            tsmiDownloadPlugin = new ToolStripMenuItem();
            tsmiDownloadPlugin.Text = "采集代理资源";
            tsmiDownloadPlugin.Name = "DownloadProxyPluginMenu";
            tsmiDownloadPlugin.Visible = true;
            App.PluginMenu.DropDownItems.Add(tsmiDownloadPlugin); //在宿主程序中添加菜单

            tsmiOption = new ToolStripMenuItem();
            tsmiOption.Name = "tsmiOption";
            tsmiOption.Size = new Size(152, 22);
            tsmiOption.Text = "选项";
            tsmiOption.Click += item_Click; //为下拉单添加事件

            tsmiDownloading = new ToolStripMenuItem();
            tsmiDownloading.Name = "tsmiDownload";
            tsmiDownloading.Size = new Size(152, 22);
            tsmiDownloading.Text = "开始采集";
            tsmiDownloading.Click += download_Click;

            tsmiDownloadPlugin.DropDownItems.AddRange(new ToolStripItem[]
                {
                    tsmiOption,
                    tsmiDownloading
                });


            //定义一个工具按钮
            tsbDownload = new ToolStripButton();
            tsbDownload.Name = "DownloadProxyPlugin";
            tsbDownload.Text = "开始采集";
            tsbDownload.ToolTipText = tsbDownload.Text;
            //tsb.Image = Properties.Resources.refresh;
            tsbDownload.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbDownload.Image = Resources.download;
            tsbDownload.Click += download_Click;
            App.Toolbar.Items.Add(tsbDownload);

            #endregion
        }

        /// <summary>
        ///     显示插件
        /// </summary>
        public void ShowPlugin()
        {
            try
            {
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex); //记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        /// <summary>
        ///     隐藏插件
        /// </summary>
        public void HidePlugin()
        {
            try
            {
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex); //记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        /// <summary>
        ///     判断主窗体上是否有本插件
        /// </summary>
        public void ActiveProxyForm()
        {
            for (int index = app.DockPanel.Contents.Count - 1; index >= 0; index--)
            {
                if (app.DockPanel.Contents[index] is IDockContent)
                {
                    IDockContent content = app.DockPanel.Contents[index];
                    if (content.GetType().Name == "ProxyForm")
                    {
                        content.DockHandler.Activate();
                        return;
                    }
                }
            }
        }

        private void download_Click(object sender, EventArgs e)
        {
            if (null != app)
            {
                if (File.Exists(DownloadSettingFileName))
                {
                    var setting =
                        XmlHelper.XmlDeserialize(DownloadSettingFileName, typeof (DownloadSetting)) as DownloadSetting;
                    if (null == setting || setting.Websites.Count == 0)
                    {
                        if (DialogResult.OK ==
                            MessageBox.Show("请添加要采集代理的网址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information))
                        {
                            var ofd = new OpenFileDialog();
                            ofd.ShowDialog();
                        }
                        return;
                    }

                    if (null != setting && tsbDownload.Text == "开始采集" && setting.Websites.Count > 0 &&
                        !app.IsDownloadingOrTesting)
                    {
                        app.ProxyList.Clear();
                        tsbDownload.Image = Resources.stop;
                        tsbDownload.Text = "停止采集";
                        app.WriteDebug("开始采集...");
                        downloader = new Downloader(setting.Websites, app);
                        downloader.Completed += CheckCompleted;
                        downloader.Start();
                    }
                }
            }
        }

        private void CheckCompleted(object obj, EventArgs arg)
        {
            tsbDownload.Image = Resources.download;
            tsbDownload.Text = "开始采集";
            app.WriteDebug("全部采集完成");
            app.DataBind();
            ActiveProxyForm();
        }

        private void item_Click(object sender, EventArgs e)
        {
            try
            {
                var od = new OptionDialog();
                od.ShowDialog();
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex); //记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        #endregion

        //public static string RegexProxy = @"(?<Proxy>([1-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(([0-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.){2}([1-9]|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))(\s+|\:|\：|<(.*?)>)(?<Port>\d{1,5})";
        public static string RegexProxy =
            @"(?<Proxy>(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]))(\s|\:|\：|<(.*?)\n{0,1}(.*?)>)(?<Port>\d{1,5})";

        /// <summary>
        ///     我的文档
        /// </summary>
        public static string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string DownloadSettingFileName
        {
            get { return ProxyHeroPath + @"\DownloadSetting.xml"; }
        }

        public static string ProxyHeroPath
        {
            get
            {
                string path = MyDocumentsPath + @"\Loamen\ProxyHero";
                var di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }
                return di.FullName;
            }
        }
    }
}