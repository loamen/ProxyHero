using System;
using System.IO;
using System.Windows.Forms;
using Loamen.Common;
using Loamen.PluginFramework;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.LanguageInformation;

namespace ProxyHero.Plugin
{
    public partial class PluginManageForm : Form
    {
        private readonly LanguageLoader languageLoader = new LanguageLoader();

        public PluginManageForm()
        {
            InitializeComponent();
            LoadLanguage();
        }

        /// <summary>
        ///     加载窗体语言
        /// </summary>
        private void LoadLanguage()
        {
            Language language = Config.LocalLanguage;
            //if (System.Globalization.CultureInfo.InstalledUICulture.Name.ToLower().Contains("zh-"))
            if (Config.IsChineseLanguage)
                Text = "插件管理";
            else
                Text = "Plugin Manage";

            object model = language.PluginManagePage;

            languageLoader.Load(model, typeof (PluginManageForm), this);
        }

        private void PluginManageForm_Load(object sender, EventArgs e)
        {
            var ps = Config.PluginSetting;
            if (null != ps && ps.Plugins.Count > 0)
            {
                foreach (Entity.Plugin plugin in ps.Plugins)
                {
                    var lvi = new ListViewItem(plugin.Name);
                    lvi.Checked = plugin.Checked;
                    lvi.SubItems.Add(plugin.Author);
                    lvi.SubItems.Add(plugin.Version);
                    lvi.SubItems.Add(plugin.LphVersion);
                    lvi.SubItems.Add(plugin.Description);
                    lvi.SubItems.Add(plugin.FileName.ToLower());
                    lvPlugin.Items.Add(lvi);
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            p_OpenFileDialog.FileName = String.Empty;
            //p_OpenFileDialog.InitialDirectory = Config.PluginPath;
            p_OpenFileDialog.Filter = @"dll(*.dll)|*.dll|c#(*.cs)|*.cs|龙门加密脚本(*.lm)|*.lm|所有文件(*.*)|*.*";
            p_OpenFileDialog.Title = @"Compile and run plug-in";

            if (p_OpenFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (Exists(p_OpenFileDialog.FileName))
                {
                    MsgBox.ShowMessage("Plug-in has been added!");
                    return;
                }

                var pm = new PluginManager(p_OpenFileDialog.FileName, Config.MainForm);
                pm.Run();
                if (pm.Engine.Errors.Count == 0)
                {
                    var lvi = new ListViewItem(pm.Engine.Name);
                    lvi.Checked = true;
                    lvi.SubItems.Add(pm.Engine.Author);
                    lvi.SubItems.Add(pm.Engine.Version);
                    lvi.SubItems.Add(pm.Engine.LPHVersion);
                    lvi.SubItems.Add(pm.Engine.Description);
                    lvi.SubItems.Add(pm.Engine.FileName.ToLower());
                    lvPlugin.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        ///     插件是否已经添加
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool Exists(string fileName)
        {
            bool result = false;
            foreach (ListViewItem li in lvPlugin.Items)
            {
                if (li.SubItems[5].Text.ToLower() == fileName.ToLower())
                    result = true;
                break;
            }
            return result;
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvPlugin.SelectedItems.Count == 0)
                {
                    MsgBox.ShowMessage("请选择要删除的插件！");
                    return;
                }
                foreach (ListViewItem li in lvPlugin.SelectedItems)
                {
                    PluginManager.Remove(li.SubItems[5].Text);
                    lvPlugin.Items.Remove(li);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void Compile_Click(object sender, EventArgs e)
        {
            try
            {
                p_OpenFileDialog.FileName = String.Empty;
                //p_OpenFileDialog.InitialDirectory = Config.PluginPath;
                p_OpenFileDialog.Filter = "C# class files (*.cs)|*.cs";
                p_OpenFileDialog.Title = "Compile and save plug-in (.cs->.dll)";

                if (p_OpenFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var p_PluginEngine = new PluginEngine(p_OpenFileDialog.FileName, Config.MainForm);

                    if (
                        !p_PluginEngine.Compile(Path.GetDirectoryName(p_OpenFileDialog.FileName) + "\\" +
                                                Path.GetFileNameWithoutExtension(p_OpenFileDialog.FileName) + ".dll"))
                    {
                        foreach (string error in p_PluginEngine.Errors)
                        {
                            MessageBox.Show("Error compiling plug-in (" + p_PluginEngine.FileName + ")" +
                                            Environment.NewLine + Environment.NewLine + error);
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                Save:
                var ps = new PluginSetting();
                foreach (ListViewItem li in lvPlugin.Items)
                {
                    if (!File.Exists(li.SubItems[5].Text))
                    {
                        lvPlugin.Items.Remove(li);
                        goto Save;
                    }
                    var plugin = new Entity.Plugin();

                    plugin.Checked = li.Checked;

                    plugin.Name = li.SubItems[0].Text;
                    plugin.Author = li.SubItems[1].Text;
                    plugin.Version = li.SubItems[2].Text;
                    plugin.LphVersion = li.SubItems[3].Text;
                    plugin.Description = li.SubItems[4].Text;
                    plugin.FileName = li.SubItems[5].Text;

                    ps.Plugins.Add(plugin);
                    if (li.Checked)
                    {
                        if (!PluginManager.Exists(plugin.FileName))
                        {
                            var pe = new PluginManager(plugin.FileName, Config.MainForm);
                            pe.Run();
                        }
                    }
                }

                if (ps.Plugins.Count > 0)
                    Config.PluginSetting = ps;

                Close();
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
            }
        }

        private void DownloadPlugins_Click(object sender, EventArgs e)
        {
            Config.MainForm.OpenIE("https://github.com/loamen/ProxyHero/releases");
        }
    }
}