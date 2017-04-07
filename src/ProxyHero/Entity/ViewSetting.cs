using System;

namespace ProxyHero.Entity
{
    [Serializable]
    public class ViewSetting
    {
        private bool enableDebug;
        private bool informationWindow;
        private bool menuBar = true;
        private bool proxyWindow = true;
        private bool statusBar = true;

        private bool toolBar;

        /// <summary>
        ///     显示主菜单
        /// </summary>
        public bool MenuBar
        {
            get { return menuBar; }
            set { menuBar = value; }
        }

        /// <summary>
        ///     显示工具栏
        /// </summary>
        public bool ToolBar
        {
            get { return toolBar; }
            set { toolBar = value; }
        }

        /// <summary>
        ///     显示状态栏
        /// </summary>
        public bool StatusBar
        {
            get { return statusBar; }
            set { statusBar = value; }
        }

        /// <summary>
        ///     显示信息窗口
        /// </summary>
        public bool InformationWindow
        {
            get { return informationWindow; }
            set { informationWindow = value; }
        }

        /// <summary>
        ///     显示代理窗口
        /// </summary>
        public bool ProxyWindow
        {
            get { return proxyWindow; }
            set { proxyWindow = value; }
        }

        /// <summary>
        ///     调试信息
        /// </summary>
        public bool EnableDebug
        {
            get { return enableDebug; }
            set { enableDebug = value; }
        }
    }
}