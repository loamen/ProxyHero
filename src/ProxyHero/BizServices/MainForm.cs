using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.Net;
using Loamen.Net.Entity;
using Loamen.PluginFramework;
using ProxyHero.Common;
using ProxyHero.Model;
using ProxyHero.Net;
using ProxyHero.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace ProxyHero
{
    public partial class MainForm :  IApp
    {
        #region Interface

        #region 属性

        /// <summary>
        ///     代理列表
        /// </summary>
        public IList<ProxyServer> ProxyList
        {
            get { return ProxyData.ProxyList; }
            set { ProxyData.ProxyList = value; }
        }

        /// <summary>
        ///     可用代理列表
        /// </summary>
        public IList<ProxyServer> AliveProxyList
        {
            get { return ProxyData.AliveProxyList; }
        }

        /// <summary>
        ///     左边工具栏容器
        /// </summary>
        public ToolStripPanel LeftPanel
        {
            get { return leftPanel; }
        }

        /// <summary>
        ///     上面工具栏容器
        /// </summary>
        public ToolStripPanel TopPanel
        {
            get { return topPanel; }
        }

        /// <summary>
        ///     下面工具栏容器
        /// </summary>
        public ToolStripPanel BottomPanel
        {
            get { return bottomPanel; }
        }

        /// <summary>
        ///     右边工具栏容器
        /// </summary>
        public ToolStripPanel RightPanel
        {
            get { return rightPanel; }
        }

        /// <summary>
        ///     插件菜单
        /// </summary>
        public ToolStripMenuItem PluginMenu
        {
            get { return Plugin; }
        }

        /// <summary>
        ///     停靠窗体
        /// </summary>
        public DockPanel DockPanel
        {
            get { return MainDockPanel; }
        }

        /// <summary>
        ///     工具栏
        /// </summary>
        public ToolStrip Toolbar
        {
            get { return MainToolbar; }
        }

        /// <summary>
        ///     龙门代理公布器路径
        /// </summary>
        public string ProxyHeroSettingPath
        {
            get { return Config.ProxyHeroPath; }
        }

        #endregion

        #region 方法

        /// <summary>
        ///     设置状态栏显示
        /// </summary>
        /// <param name="text"></param>
        public void SetToolTipText(string text)
        {
            DelegateSet set = DelegateSetToolTipText;
            Invoke(set, new object[] { text });
        }

        /// <summary>
        ///     设置状态
        /// </summary>
        /// <param name="text"></param>
        public void SetStatusText(string text)
        {
            DelegateSet set = DelegateSetStatusText;
            Invoke(set, new object[] { text });
        }

        /// <summary>
        ///     写日志
        /// </summary>
        /// <param name="text"></param>
        public void WriteLog(object text)
        {
            LogHelper.WriteLog(text + "");
        }

        /// <summary>
        ///     写异常日志
        /// </summary>
        /// <param name="ex"></param>
        public void WriteExceptionLog(Exception ex)
        {
            LogHelper.WriteException(ex);
        }

        /// <summary>
        ///     写调试信息
        /// </summary>
        /// <param name="text"></param>
        public void WriteDebug(string text)
        {
            Config.ConsoleEx.Debug(text);
        }

        /// <summary>
        ///     写异常调试信息
        /// </summary>
        /// <param name="ex"></param>
        public void WriteDebug(Exception ex)
        {
            Config.ConsoleEx.Debug(ex);
        }

        /// <summary>
        ///     是否正在下载或者验证
        /// </summary>
        public bool IsDownloadingOrTesting
        {
            get { return !ProxyPage.IsNotDownloadingOrTesting; }
        }

        /// <summary>
        ///     XML序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool XmlSerialize(string path, object obj, Type type)
        {
            return XmlHelper.XmlSerialize(path, obj, type);
        }

        /// <summary>
        ///     XML反序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object XmlDeserialize(string path, Type type)
        {
            return XmlHelper.XmlDeserialize(path, type);
        }

        /// <summary>
        ///     获取网站的HTML内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public string GetHtml(string url, string proxy)
        {
            var httpHelper = new HttpHelper();
            if (!string.IsNullOrEmpty(proxy))
            {
                HttpProxy pe = Config.GetProxyEntity(proxy);
                if (!string.IsNullOrEmpty(pe.Ip))
                    httpHelper.Proxy = pe;
            }
            return httpHelper.GetHtml(url);
        }

        /// <summary>
        ///     获取网站的HTML内容
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="proxy"></param>
        /// <param name="encode">编码格式</param>
        /// <returns></returns>
        public string GetHtml(string url, string proxy, string encode)
        {
            var httpHelper = new HttpHelper();
            if (!string.IsNullOrEmpty(proxy))
            {
                HttpProxy pe = Config.GetProxyEntity(proxy);
                if (!string.IsNullOrEmpty(pe.Ip))
                    httpHelper.Proxy = pe;
            }
            return httpHelper.GetHtml(url, Encoding.GetEncoding(encode));
        }

        /// <summary>
        ///     获取网站的HTML内容
        /// </summary>
        /// <param name="url">网站地址</param>
        /// <param name="proxy">代理服务器</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="encode">编码格式</param>
        /// <returns></returns>
        public string GetHtml(string url, string proxy, int timeOut, string encode)
        {
            var httpHelper = new HttpHelper();
            if (timeOut > 0)
            {
                httpHelper.HttpOption.Timeout = timeOut;
            }
            if (!string.IsNullOrEmpty(proxy))
            {
                HttpProxy pe = Config.GetProxyEntity(proxy);
                if (!string.IsNullOrEmpty(pe.Ip))
                    httpHelper.Proxy = pe;
            }
            return httpHelper.GetHtml(url, Encoding.GetEncoding(encode));
        }

        /// <summary>
        ///     提交数据并返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="proxy"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string GetHtml(string url, string postData, string proxy, string encode)
        {
            var helper = new HttpHelper();
            if (!string.IsNullOrEmpty(proxy))
            {
                HttpProxy pe = Config.GetProxyEntity(proxy);
                if (!string.IsNullOrEmpty(pe.Ip))
                    helper.Proxy = pe;
            }
            return helper.GetHtml(url, postData, Encoding.GetEncoding(encode));
        }

        /// <summary>
        ///     添加主菜单
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public ToolStripMenuItem AddMenuItem(string name, string text)
        {
            var dsmi = new ToolStripMenuItem { Text = text, Name = name, Visible = true };
            PluginMenu.DropDownItems.Add(dsmi); //在宿主程序中添加菜单

            return dsmi;
        }

        /// <summary>
        /// 移出主菜单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void RemoveMenuItem(string name)
        {
            foreach (ToolStripMenuItem item in PluginMenu.DropDownItems)
            {
                if(item.Name == name)
                {
                    PluginMenu.DropDownItems.Remove(item);
                    break;
                }
            }
        }

        /// <summary>
        ///     添加工具栏按钮
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public ToolStripButton AddToolButton(string name, string text)
        {
            var tsb = new ToolStripButton { Name = name, Text = text };
            tsb.ToolTipText = tsb.Text;
            tsb.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsb.Image = Resources.app;
            Toolbar.Items.Add(tsb);

            return tsb;
        }

        /// <summary>
        /// 移出工具栏按钮
        /// </summary>
        /// <param name="name"></param>
        public void RemoveToolButton(string name)
        {
            foreach (ToolStripButton item in Toolbar.Items)
            {
                if (item.Name == name)
                {
                    Toolbar.Items.Remove(item);
                    break;
                }
            }
        }

        /// <summary>
        ///     截取字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string GetMidString(string text, string start, string end)
        {
            return StringHelper.GetMidString(text, start, end);
        }

        /// <summary>
        ///     更新云服务器信息
        /// </summary>
        public void RefreshCloud()
        {
            if (ProxyData.ProxyList != null && ProxyData.ProxyList.Count > 0)
            {
                var cloudHelper = new CloudHelper();
                cloudHelper.UploadProxyList(ProxyData.ProxyList);
                ConnectCloud();
            }
        }

        public void DataBind()
        {
            if (ProxyData.ProxyList != null && ProxyData.ProxyList.Count > 0)
            {
                ProxyPage.BindData();
            }
        }

        /// <summary>
        ///     启用或禁用界面
        /// </summary>
        /// <param name="enabled"></param>
        public void EnabledProxyPageUI(bool enabled)
        {
            ProxyPage.EnabledProxyPageUi(enabled);
        }

        #endregion

        #endregion

        #region IServiceContainer Members

        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            _serviceContainer.AddService(serviceType, callback, promote);
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            _serviceContainer.AddService(serviceType, callback);
        }

        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            _serviceContainer.AddService(serviceType, serviceInstance, promote);
        }

        public void AddService(Type serviceType, object serviceInstance)
        {
            _serviceContainer.AddService(serviceType, serviceInstance);
        }

        public void RemoveService(Type serviceType, bool promote)
        {
            _serviceContainer.RemoveService(serviceType, promote);
        }

        public void RemoveService(Type serviceType)
        {
            _serviceContainer.RemoveService(serviceType);
        }

        #endregion

        #region IServiceProvider Members

        //由于Form类型本身间接的继承了IServiceProvider接口，所以我们要覆盖掉Form本身的实现
        //所以我们使用了new关键字
        public new object GetService(Type serviceType)
        {
            return _serviceContainer.GetService(serviceType);
        }

        #endregion
    }
}
