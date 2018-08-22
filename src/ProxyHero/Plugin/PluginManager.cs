using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Loamen.Common;
using Loamen.PluginFramework;
using ProxyHero.Entity;
using System.Linq;

namespace ProxyHero
{
    public class PluginManager
    {
        private static List<PluginEngine> engines;
        private PluginEngine engine;
        private FileInfo pluginFileInfo;

        public PluginManager(string fileName, IApp app)
        {
            pluginFileInfo = new FileInfo(fileName);
            if (Find(pluginFileInfo.FullName) != null)
                throw new Exception("Plug-in has been added!");
            engine = new PluginEngine(fileName, app);
        }

        [Description("插件列表")]
        public static List<PluginEngine> Engines
        {
            get { return engines; }
            set { engines = value; }
        }

        public PluginEngine Engine
        {
            get { return engine; }
            set { engine = value; }
        }

        /// <summary>
        ///     插件文件信息
        /// </summary>
        public FileInfo PluginFileInfo
        {
            get { return pluginFileInfo; }
            set { pluginFileInfo = value; }
        }

        [Description("查找插件引擎")]
        public static PluginEngine Find(string fileName)
        {
            PluginEngine engine = null;
            if (null == engines)
                return null;

            foreach (PluginEngine pe in engines)
            {
                if (pe.FileName.ToLower() == fileName.ToLower())
                {
                    engine = pe;
                    break;
                }
            }

            return engine;
        }

        /// <summary>
        ///     是否已经存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool Exists(string fileName)
        {
            bool result = false;
            PluginEngine pe = Find(fileName);
            if (pe != null)
                result = true;
            return result;
        }

        [Description("移除插件")]
        public static void Remove(string fileName)
        {
            PluginEngine engine = Find(fileName);
            if (null != engine)
                engines.Remove(engine);
        }

        public void Run()
        {
            if (null != engine)
            {
                if (pluginFileInfo.Extension == ".cs" ||
                    pluginFileInfo.Extension == ".lm" ||
                    pluginFileInfo.Extension == ".dll")
                {
                    if (pluginFileInfo.Extension == ".cs" || pluginFileInfo.Extension == ".lm")
                    {
                        if (!engine.Compile(String.Empty))
                        {
                            foreach (string error in engine.Errors)
                            {
                                Config.ConsoleEx.WriteLine("Error compiling plug-in (" + engine.FileName + ")" +
                                                           Environment.NewLine + Environment.NewLine + error);
                            }
                            return;
                        }
                    }

                    if (!engine.Run())
                    {
                        foreach (string error in engine.Errors)
                        {
                            Config.ConsoleEx.WriteLine("Error running plug-in (" + engine.FileName + ")" +
                                                       Environment.NewLine + Environment.NewLine + error);
                        }
                    }
                    else
                    {
                        if (null == engines)
                            engines = new List<PluginEngine>();
                        Engines.Add(engine);
                    }
                }
            }
        }

        /// <summary>
        ///     加载所有插件
        /// </summary>
        public static void LoadAllPlugins()
        {
            var dal = new PluginDAL();
            var ps = dal.FindAll().ToList();
            if (null != ps && ps.Count > 0)
            {
                foreach (Entity.Plugin plugin in ps)
                {
                    if (File.Exists(plugin.FileName) && plugin.Checked && !Exists(plugin.FileName))
                    {
                        var pm = new PluginManager(plugin.FileName, Config.MainForm);
                        pm.Run();
                    }
                }
            }
        }
    }
}