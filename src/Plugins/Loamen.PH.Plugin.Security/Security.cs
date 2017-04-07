using System;
using System.Reflection;
using System.Windows.Forms;
using Loamen.PH.Plugin.Security.Properties;
using Loamen.PluginFramework;
using WeifenLuo.WinFormsUI.Docking;

namespace Loamen.PH.Plugin.Security
{
    public class Security : IPlugin
    {
        #region 变量

        private IApp app;
        private string author = "龙门信息网";
        private string description = "加密解密插件用于对龙门代理公布器插件脚本的c#源代码加密，以达到保护你的知识产权的目录！";
        private ToolStripMenuItem dsmi;
        private string lPHVersion = "1.6.0+";

        private string name = "加密解密";
        public SecurityForm securityForm = null;
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

        #endregion

        #region 方法

        /// <summary>
        ///     判断主窗体上是否有本插件
        /// </summary>
        public bool Exist
        {
            get
            {
                bool IsExist = false;
                for (int index = app.DockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (app.DockPanel.Contents[index] is IDockContent)
                    {
                        IDockContent content = app.DockPanel.Contents[index];
                        if (content.GetType().Name == "RefreshForm")
                        {
                            IsExist = true;
                            break;
                        }
                    }
                }

                return IsExist;
            }
        }

        /// <summary>
        ///     初始化宿主
        /// </summary>
        public void InitApp()
        {
            #region

            // 定义一个下拉菜单
            dsmi = new ToolStripMenuItem();
            dsmi.Text = "加密解密";
            dsmi.Name = "EncodePluginMenu";
            dsmi.Visible = true;
            dsmi.Click += item_Click; //为下拉单添加时间
            App.PluginMenu.DropDownItems.Add(dsmi); //在宿主程序中添加菜单

            //定义一个工具按钮
            var tsb = new ToolStripButton();
            tsb.Name = "EncodePlugin";
            tsb.Text = "加密解密";
            tsb.ToolTipText = tsb.Text;
            tsb.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsb.Image = Resources._lock;
            tsb.Click += item_Click;
            App.Toolbar.Items.Add(tsb);

            #endregion
        }

        /// <summary>
        ///     显示插件
        /// </summary>
        public void ShowPlugin()
        {
            try
            {
                if (null == securityForm)
                {
                    securityForm = new SecurityForm(App); //初始化插件窗体
                }

                securityForm.Show(app.DockPanel, DockState.Document); //显示在停靠栏中间
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
                if (null != securityForm)
                {
                    if (Exist)
                        securityForm.Hide();
                    else
                        securityForm = null;
                }
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex); //记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            try
            {
                dsmi.Checked = !dsmi.Checked;
                if (dsmi.Checked)
                    ShowPlugin();
                else
                    HidePlugin();
            }
            catch (Exception ex)
            {
                //app.WriteDebug(ex); //代理公布器调试窗体信息显示
                app.WriteExceptionLog(ex); //记录错误日志
                //app.WriteLog(ex.Message);
            }
        }

        #endregion
    }
}