using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Loamen.PluginFramework
{
    public interface IApp : IServiceContainer
    {
        #region Property

        /// <summary>
        ///     代理列表
        /// </summary>
        IList<ProxyServer> ProxyList { get; }

        /// <summary>
        ///     可用代理列表
        /// </summary>
        IList<ProxyServer> AliveProxyList { get; }

        /// <summary>
        ///     主窗体左边ToolStripPanel
        /// </summary>
        ToolStripPanel LeftPanel { get; }

        ToolStripPanel TopPanel { get; }
        ToolStripPanel BottomPanel { get; }
        ToolStripPanel RightPanel { get; }

        /// <summary>
        ///     主停靠栏
        /// </summary>
        DockPanel DockPanel { get; }

        /// <summary>
        ///     插件菜单
        /// </summary>
        ToolStripMenuItem PluginMenu { get; }

        /// <summary>
        ///     主工具栏
        /// </summary>
        ToolStrip Toolbar { get; }

        /// <summary>
        ///     正在下载或验证代理
        /// </summary>
        bool IsDownloadingOrTesting { get; }

        /// <summary>
        ///     龙门代理公布器配置路径
        /// </summary>
        string ProxyHeroSettingPath { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     状态栏提示
        /// </summary>
        /// <param name="text"></param>
        void SetToolTipText(string text);

        /// <summary>
        ///     状态标签信息
        /// </summary>
        /// <param name="text"></param>
        void SetStatusText(string text);

        /// <summary>
        ///     写日志
        /// </summary>
        /// <param name="text"></param>
        void WriteLog(object text);

        /// <summary>
        ///     写异常日志
        /// </summary>
        /// <param name="ex"></param>
        void WriteExceptionLog(Exception ex);

        /// <summary>
        ///     主程序信息窗口显示
        /// </summary>
        /// <param name="text"></param>
        void WriteDebug(string text);

        /// <summary>
        ///     主程序信息窗口显示
        /// </summary>
        /// <param name="ex"></param>
        void WriteDebug(Exception ex);

        /// <summary>
        ///     内置浏览器打开一个新网页
        /// </summary>
        /// <param name="url"></param>
        void OpenNewTab(string url);

        /// <summary>
        ///     用IE打开一个网页
        /// </summary>
        /// <param name="url"></param>
        void OpenIE(string url);

        /// <summary>
        ///     查找内容页
        /// </summary>
        /// <param name="tabText">标签名称</param>
        /// <returns></returns>
        IDockContent FindDockContentByTabText(string tabText);

        /// <summary>
        ///     查找内容页
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns></returns>
        IDockContent FindDockContentByClassName(string className);

        /// <summary>
        ///     Xml序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool XmlSerialize(string path, object obj, Type type);

        /// <summary>
        ///     Xml反序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object XmlDeserialize(string path, Type type);

        /// <summary>
        ///     获取网站HTML内容
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="proxy">代理服务器</param>
        /// <returns></returns>
        string GetHtml(string url, string proxy);

        /// <summary>
        ///     获取网站HTML内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="proxy"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        string GetHtml(string url, string proxy, string encode);

        /// <summary>
        ///     获取网站HTML内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="proxy"></param>
        /// <param name="timeOut"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        string GetHtml(string url, string proxy, int timeOut, string encode);

        /// <summary>
        ///     获取网站HTML内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="proxy"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        string GetHtml(string url, string postData, string proxy, string encode);

        /// <summary>
        ///     添加主菜单
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        ToolStripMenuItem AddMenuItem(string name, string text);

        /// <summary>
        ///     添加工具栏按钮
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        ToolStripButton AddToolButton(string name, string text);

        /// <summary>
        ///     截取字符串
        /// </summary>
        /// <param name="text">原字符串</param>
        /// <param name="start">开始字符串</param>
        /// <param name="end">结束字符串</param>
        /// <returns></returns>
        string GetMidString(string text, string start, string end);

        /// <summary>
        ///     更新云服务器信息
        /// </summary>
        void RefreshCloud();

        /// <summary>
        ///     启用或禁用界面
        /// </summary>
        /// <param name="enabled"></param>
        void EnabledProxyPageUI(bool enabled);

        #endregion
    }
}