using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProxyHero.Entity
{
    [Serializable]
    public class PluginSetting
    {
        private List<Plugin> _plugins = new List<Plugin>();

        public List<Plugin> Plugins
        {
            get { return _plugins; }
            set { _plugins = value; }
        }
    }


    [Serializable]
    public class Plugin
    {
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string LphVersion { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
    }
}