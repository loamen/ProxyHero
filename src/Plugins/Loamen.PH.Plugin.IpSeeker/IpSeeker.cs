using System;
using System.Collections.Generic;
using System.Text;
using Loamen.PluginFramework;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection;

namespace Loamen.PH.Plugin.IpSeeker
{
    public class IpSeeker : IPlugin
    {
        #region 变量
        private IApp app = null;
        public IpSeekerForm ipForm = null;
        ToolStripMenuItem dsmi = null;

        private string name = "验证IP地理位置";
        private string author = "龙门信息网";
        private string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private string lPHVersion = "1.6.0+";
        private string description = "使用纯真本地IP数据库验证服务器地理位置！";
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
        /// 插件名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 作者名称
        /// </summary>
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        /// <summary>
        /// 插件版本
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        /// 适用于代理公布器的代理
        /// </summary>
        public string LPHVersion
        {
            get { return lPHVersion; }
            set { lPHVersion = value; }
        }

        /// <summary>
        /// 插件描述
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
        /// 初始化宿主
        /// </summary>
        public void InitApp()
        {
            #region
            // 定义一个下拉菜单
            dsmi = new ToolStripMenuItem();
            dsmi.Text = "验证IP地理位置";
            dsmi.Name = "IPSeekerPluginMenu";
            dsmi.Visible = true;
            dsmi.Click += new EventHandler(this.item_Click); //为下拉单添加时间
            App.PluginMenu.DropDownItems.Add(dsmi); //在宿主程序中添加菜单

            //定义一个工具按钮
            ToolStripButton tsb = new ToolStripButton();
            tsb.Name = "IPSeekerPlugin";
            tsb.Text = "验证IP地理位置";
            tsb.ToolTipText = tsb.Text;
            tsb.Image = Properties.Resources.world;
            tsb.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsb.Click += new EventHandler(this.item_Click);
            App.Toolbar.Items.Add(tsb);

            #endregion
        }
        /// <summary>
        /// 显示插件
        /// </summary>
        public void ShowPlugin()
        {
            try
            {
                if (null == ipForm)
                {
                    ipForm = new IpSeekerForm(app, this); //初始化插件窗体
                }

                ipForm.Show(app.DockPanel, DockState.Document); //显示在停靠栏中间
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex);//记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 判断主窗体上是否有本插件
        /// </summary>
        public bool Exist
        {
            get
            {
                return null != app.FindDockContentByClassName("IpSeekerForm");
            }
        }

        /// <summary>
        /// 隐藏插件
        /// </summary>
        public void HidePlugin()
        {
            try
            {
                if (null != ipForm)
                {
                    if (this.Exist)
                        ipForm.Hide();
                    else
                        ipForm = null;
                }
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex);//记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            try
            {
                this.dsmi.Checked = !this.dsmi.Checked;
                if (this.dsmi.Checked)
                    this.ShowPlugin();
                else
                    this.HidePlugin();
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex);//记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        #endregion
    }
}
