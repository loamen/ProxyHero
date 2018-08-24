using System;
using System.Collections.Generic;

namespace Loamen.PluginFramework
{
    public interface IPlugin
    {
        /// <summary>
        ///     LPH主程序
        /// </summary>
        IApp App { get; set; }

        /// <summary>
        ///     插件名称
        /// </summary>
        String Name { get; set; }

        /// <summary>
        ///     插件作者
        /// </summary>
        String Author { get; set; }

        /// <summary>
        ///     插件版本
        /// </summary>
        String Version { get; set; }

        /// <summary>
        ///     适用于LPH的版本
        /// </summary>
        String LPHVersion { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        String Description { get; set; }

        /// <summary>
        /// 插件添加的菜单
        /// </summary>
        List<string> MenuItems { get; set; }
        /// <summary>
        /// 插件添加的工具按钮
        /// </summary>
        List<string> ToolButtons { get; set; }

        /// <summary>
        ///     加载主窗体控件
        /// </summary>
        void InitApp();

        /// <summary>
        ///     显示插件
        /// </summary>
        void ShowPlugin();

        /// <summary>
        ///     隐藏插件
        /// </summary>
        void HidePlugin();
    }
}