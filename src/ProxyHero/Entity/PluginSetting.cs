using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProxyHero.Entity
{
    [Serializable]
    public class Plugin
    {
        public int Id { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string LphVersion { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// 插件添加的菜单
        /// </summary>
        public List<string> MenuItems { get; set; } = new List<string>();
        /// <summary>
        /// 插件添加的工具按钮
        /// </summary>
        public List<string> ToolButtons { get; set; } = new List<string>();
    }
}