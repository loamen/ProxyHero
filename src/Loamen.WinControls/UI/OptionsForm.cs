using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using Loamen.WinControls.UI.Collections;

namespace Loamen.WinControls.UI
{
    [ToolboxItem(true)]
    public partial class OptionsForm : Form
    {

        #region Fields

        readonly OptionsPanelList _panels = new OptionsPanelList();

        //改动过的配置集合
        private readonly PropertyDictionary<string, object> _changedSettings;

        private Color _boxBackgroundColor = SystemColors.ControlLight;

        private bool _firstLoad = true;
        private int _categoryTreeWidth;

        private bool _saving;

        private string _optionsNoDescription = string.Empty;
        private bool _selectFirstPanel;
        private bool _automaticSaveSettings = true;
        private bool _applyAlwaysEnabled;
        private bool _okClicked;

        [Browsable(true)]
        [Category("Action")]
        [Description("保存表单选项（点击应用或确定表单按钮）触发事件")]
        public event EventHandler OptionsSaving;

        [Browsable(true)]
        [Category("Action")]
        [Description("已保存表单选项触发事件")]
        public event EventHandler OptionsSaved;

        [Browsable(true)]
        [Category("Action")]
        [Description("表单项目必须重置（点击取消表单按钮）触发事件")]
        public event EventHandler ResetForm;

        #endregion

        #region Properties

        public override Size MinimumSize
        {
            get
            {
                Size msz = base.MinimumSize;
                int w = OptionsFormSplit.Panel2MinSize + OptionsFormSplit.SplitterWidth + OptionsFormSplit.SplitterDistance + 3;

                if (base.MinimumSize.Width < w)
                {
                    msz.Width = w;
                }

                return msz;
            }
            set
            {
                base.MinimumSize = value;
            }
        }

        [Browsable(false)]
        [Category("Options Form")]
        [Description("在OptionsForm中加载的Panels")]
        public OptionsPanelList Panels
        {
            get
            {
                return this._panels;
            }
        }

        /// <summary>
        /// 同步设置的选项配置
        /// </summary>
        [Browsable(false), Category("Options Form"), Description("同步设置的选项配置")]
        public PropertyDictionary<string, object> AppSettings { get; set; }

        [Category("Options Form Flags")]
        [Description("如果应用程序必须重新启动")]
        public bool ApplicationMustRestart
        {
            get
            {
                return !OptDescrSplit.Panel1Collapsed;
            }
            set
            {
                OptDescrSplit.Panel2Collapsed = value;
                OptDescrSplit.Panel1Collapsed = !value;
            }
        }


        /// <summary>
        /// 传入的原始配置
        /// </summary>
        [Browsable(false), Category("Options Form"), Description("传入的原始配置。")]
        public PropertyDictionary<string, object> OldSettings { get; private set; }

        [Category("Options Form")]
        [Description("类别树中使用的图像")]
        public ImageList CategoryImages
        {
            get
            {
                return CatTree.ImageList;
            }
            set
            {
                CatTree.ImageList = value;
            }
        }


        [Category("Options Form")]
        [Description("类别树的宽度")]
        public int CategoryTreeWidth
        {
            get
            {
                if (_firstLoad && _categoryTreeWidth > 0)
                {
                    OptionsFormSplit.SplitterDistance = _categoryTreeWidth;
                    return _categoryTreeWidth;
                }

                return OptionsFormSplit.SplitterDistance;
            }
            set
            {
                OptionsFormSplit.SplitterDistance = value;
                _categoryTreeWidth = value;
            }
        }

        [Category("Options Form")]
        [Description("类别标题文字")]
        public string CategoryHeaderText
        {
            get
            {
                return CatHeader.Text;
            }
            set
            {
                CatHeader.Text = value;
            }
        }

        [Category("Options Form")]
        [Description("确定按钮文字")]
        public string OkButtonText
        {
            get
            {
                return OKBtn.Text;
            }
            set
            {
                OKBtn.Text = value;
            }
        }

        [Category("Options Form")]
        [Description("应用按钮文字")]
        public string ApplyButtonText
        {
            get
            {
                return ApplyBtn.Text;
            }
            set
            {
                ApplyBtn.Text = value;
            }
        }

        [Category("Options Form")]
        [Description("取消按钮文字")]
        public string CancelButtonText
        {
            get
            {
                return CancelBtn.Text;
            }
            set
            {
                CancelBtn.Text = value;
            }
        }

        [Category("Options Form")]
        [Description("必须重新启动应用程序的文本标签框")]
        public string AppRestartText
        {
            get
            {
                return AppRestartLabel.Text;
            }
            set
            {
                AppRestartLabel.Text = value;
            }
        }

        [Category("Options Form Colors")]
        [Description("显示的每个框的背景颜色")]
        public Color BoxBackColor
        {
            get
            {
                return _boxBackgroundColor;
            }
            set
            {
                if (value != null && _boxBackgroundColor != value)
                {
                    _boxBackgroundColor = value;

                    CatHeader.BackColor = _boxBackgroundColor;
                    CatDescr.BackColor = _boxBackgroundColor;
                    OptionsPanelPath.BackColor = _boxBackgroundColor;
                    OptionDescrLabel.BackColor = _boxBackgroundColor;

                }
            }
        }

        [Category("Options Form Colors")]
        [Description("选项面板路径“框中前景色")]
        public Color OptionsPathForeColor
        {
            get
            {
                return OptionsPanelPath.ForeColor;
            }
            set
            {
                OptionsPanelPath.ForeColor = value;
            }
        }

        [Category("Options Form Colors")]
        [Description("必须重新启动应用程序的前景色标签框")]
        public Color AppRestartForeColor
        {
            get
            {
                return AppRestartLabel.ForeColor;
            }
            set
            {
                AppRestartLabel.ForeColor = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("当没有可用的描述，这是默认的文字说明框")]
        public string OptionsNoDescription
        {
            get
            {
                return _optionsNoDescription;
            }
            set
            {
                _optionsNoDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("这是描述的类别标题框，当鼠标悬停")]
        public string CategoryHeaderDescription
        {
            get
            {
                return CatHeader.AccessibleDescription;
            }
            set
            {
                CatHeader.AccessibleDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("描述的选项面板路径“框中，当鼠标悬停")]
        public string OptionsPanelPathDescription
        {
            get
            {
                return OptionsPanelPath.AccessibleDescription;
            }
            set
            {
                OptionsPanelPath.AccessibleDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("描述类别树中，当鼠标悬停")]
        public string CategoryTreeDescription
        {
            get
            {
                return CatTree.AccessibleDescription;
            }
            set
            {
                CatTree.AccessibleDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("描述的分类描述“框中，当鼠标悬停")]
        public string CategoryDescrDescription
        {
            get
            {
                return CatDescr.AccessibleDescription;
            }
            set
            {
                CatDescr.AccessibleDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("描述的选项说明“框中，当鼠标悬停")]
        public string OptionDescrDescription
        {
            get
            {
                return OptionDescrLabel.AccessibleDescription;
            }
            set
            {
                OptionDescrLabel.AccessibleDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("描述的应用程序必须重新启动框，当鼠标悬停")]
        public string ApplicationRestartDescription
        {
            get
            {
                return AppRestartLabel.AccessibleDescription;
            }
            set
            {
                AppRestartLabel.AccessibleDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("这是描述确定表按钮，当鼠标悬停")]
        public string OkButtonDescription
        {
            get
            {
                return OKBtn.AccessibleDescription;
            }
            set
            {
                OKBtn.AccessibleDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("描述应用表单按钮，当鼠标悬停")]
        public string ApplyButtonDescription
        {
            get
            {
                return ApplyBtn.AccessibleDescription;
            }
            set
            {
                ApplyBtn.AccessibleDescription = value;
            }
        }

        [Category("Options Form Descriptions")]
        [Description("描述的取消表格“按钮，当鼠标悬停")]
        public string CancelButtonDescription
        {
            get
            {
                return CancelBtn.AccessibleDescription;
            }
            set
            {
                CancelBtn.AccessibleDescription = value;
            }
        }


        [Category("Options Form Flags")]
        [Description("如果“类别标题”框中必须是可见的")]
        public bool ShowCategoryHeader
        {
            get
            {
                return CatHeaderPanel.Visible;
            }
            set
            {
                CatHeaderPanel.Visible = value;
            }
        }

        [Category("Options Form Flags")]
        [Description("如果“类别说明”框中必须是可见的")]
        public bool ShowCategoryDescription
        {
            get
            {
                return CatDescrPanel.Visible;
            }
            set
            {
                CatDescrPanel.Visible = value;
            }
        }

        [Category("Options Form Flags")]
        [Description("选项说明中表示，如果必须是可见的")]
        public bool ShowOptionsDescription
        {
            get
            {
                return !OptionsSplitContainer.Panel2Collapsed;
            }
            set
            {
                OptionsSplitContainer.Panel2Collapsed = !value;
            }
        }

        [Category("Options Form Flags")]
        [Description("如果第一小组时，必须选择选择父类别。")]
        public bool SelectFirstPanel
        {
            get
            {
                return _selectFirstPanel;
            }
            set
            {
                _selectFirstPanel = value;
            }
        }


        [Category("Options Form Flags")]
        [Description("表示必须是可见的，如果在选项面板路径“框中")]
        public bool ShowOptionsPanelPath
        {
            get
            {
                return OptionsPanelPath.Visible;
            }
            set
            {
                OptionsPanelPath.Visible = value;
            }
        }

        [Category("Options Form Flags")]
        [Description("表示如果分配器容量已启用“选项表")]
        public bool EnableFormSplitter
        {
            get
            {
                return !OptionsFormSplit.IsSplitterFixed;
            }
            set
            {
                OptionsFormSplit.IsSplitterFixed = !value;
            }
        }

        [Category("Options Form Flags")]
        [Description("表示如果选项表格会自动保存应用程序设置")]
        public bool AutomaticSaveSettings
        {
            get
            {
                return _automaticSaveSettings;
            }
            set
            {
                _automaticSaveSettings = value;
            }
        }

        [Category("Options Form Flags")]
        [Description("表示如果必须始终启用“应用”按钮（而不是只当选项改变）")]
        public bool ApplyAlwaysEnabled
        {
            get
            {
                return _applyAlwaysEnabled;
            }
            set
            {
                _applyAlwaysEnabled = value;

                if (_applyAlwaysEnabled)
                {
                    ApplyBtn.Enabled = true;
                }
            }
        }

        [Category("Options Form Flags")]
        [Description("表示如果必须始终启用“应用”按钮（而不是只当选项改变）")]
        public bool SaveAndCloseOnReturn
        {
            get
            {
                return AcceptButton != null;
            }
            set
            {
                if (value)
                {
                    AcceptButton = OKBtn;
                }
                else
                {
                    AcceptButton = null;
                }
            }
        }

        [Browsable(false)]
        [Category("Options Form Flags")]
        [Description("如果点击“确定”按钮形式接近。")]
        public bool OkClicked
        {
            get
            {
                return _okClicked;
            }
            set
            {
                _okClicked = value;
            }
        }

        #endregion

        #region Construction

        public OptionsForm()
            : this(new PropertyDictionary<string, object>())
        {
        }

        public OptionsForm(PropertyDictionary<string, object> settings)
        {
            InitializeComponent();

            BoxBackColor = SystemColors.ControlLight;
            OptionsNoDescription = "";

            _panels.PanelAdded += new OptionsPanelEventHandler(_Panels_PanelAdded);

            ApplyBtn.Enabled = _applyAlwaysEnabled;

            AppSettings = settings;
            OldSettings = settings;
            _changedSettings = new PropertyDictionary<string, object>();

            // AppSettings.PropertyChanged += new PropertyChangedEventHandler(this._AppSettings_SettingChanging);
        }

        #endregion

        #region Methods


        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (_categoryTreeWidth > 0 && _firstLoad)
            {
                OptionsFormSplit.Dock = DockStyle.None;
                OptionsFormSplit.Dock = DockStyle.Fill;
                OptionsFormSplit.SplitterDistance = _categoryTreeWidth;
                _firstLoad = false;
            }
        }

        void _Panels_PanelAdded(object sender, OptionsPanelEventArgs e)
        {
            AddPanel(e.Panel);
        }

        private void AddPanel(OptionsPanel panel)
        {
            string optionCategory = panel.CategoryPath;
            string displayName = panel.DisplayName;

            string[][] lpaths = GetPaths(optionCategory);
            string[] paths = lpaths[0];
            string[] labs = lpaths[1];

            TreeNode pnode = null;
            TreeNode nnode = null;

            if (paths.Length > 1)
            {
                TreeNode[] search = CatTree.Nodes.Find(paths[0], false);

                if (search != null && search.Length > 0)
                {
                    pnode = search[0];
                }
                else
                {
                    CatTree.Nodes.Add(paths[0], labs[0], paths[0], paths[0]);
                    pnode = CatTree.Nodes[CatTree.Nodes.Count - 1];
                }

                int i = 1;
                int sub = paths.Length - 1;
                for (; i < sub; i++)
                {
                    search = pnode.Nodes.Find(paths[i], false);

                    if (search != null && search.Length > 0)
                    {
                        pnode = search[0];
                    }
                    else
                    {
                        pnode.Nodes.Add(paths[i], labs[i], string.Join(@"\", paths, 0, i + 1), string.Join(@"\", paths, 0, i + 1));
                        pnode = pnode.Nodes[pnode.Nodes.Count - 1];
                    }
                }

                if (i < sub)
                {
                    pnode = null;
                }
            }

            if (pnode != null)
            {
                nnode = new TreeNode(displayName);
                nnode.Name = optionCategory;
                nnode.ImageKey = string.Join(@"\", paths);
                nnode.SelectedImageKey = string.Join(@"\", paths);
                pnode.Nodes.Add(nnode);

                panel.OptionsChanged += new EventHandler(panel_OptionsChanged);
                panel.Dock = DockStyle.Fill;
                panel.PanelAdded(this);

                EnableDescrControl(panel);
            }
            else if (paths.Length == 1)
            {
                nnode = new TreeNode(displayName);
                nnode.Name = optionCategory;
                CatTree.Nodes.Add(nnode);

                panel.OptionsChanged += new EventHandler(panel_OptionsChanged);
                panel.Dock = DockStyle.Fill;
                panel.PanelAdded(this);

                EnableDescrControl(panel);
            }
        }

        private string[][] GetPaths(string path)
        {
            string[][] ret;

            string[] p = path.Split(new string[] { CatTree.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);

            ret = new string[][] { new string[p.Length], new string[p.Length] };

            int i1, i2;
            for (int i = 0; i < p.Length; i++)
            {
                string sp = p[i];

                if ((i1 = sp.IndexOf("{\"")) > -1
                    && (i2 = sp.IndexOf("\"}")) > -1)
                {
                    ret[0][i] = sp.Substring(0, i1);
                    ret[1][i] = sp.Substring(i1 + 2, i2 - (i1 + 2));
                }
                else
                {
                    ret[0][i] = sp;
                    ret[1][i] = sp;
                }
            }

            return ret;
        }

        void panel_OptionsChanged(object sender, EventArgs e)
        {
            if (!_applyAlwaysEnabled)
            {
                ApplyBtn.Enabled = true;
            }
        }

        private void EnableDescrControl(Control ctrl)
        {
            ctrl.MouseEnter += new EventHandler(MouseEnterDescr);
            ctrl.MouseLeave += new EventHandler(MouseLeaveDescr);

            foreach (Control ctrln in ctrl.Controls)
            {
                EnableDescrControl(ctrln);
            }
        }

        public OptionsPanel SwitchPanel(string optionCategory)
        {
            try
            {
                OptionsPanelPath.Text = "";

                if (_panels.Count > 0)
                {
                    OptionsPanel pn = null;

                    for (int i = 0; i < _panels.Count; i++)
                    {
                        if (_panels[i].CategoryPath == optionCategory)
                        {
                            pn = _panels[i];
                            break;
                        }
                    }

                    if (pn != null)
                    {
                        var displayName = pn.DisplayName;

                        var lpaths = GetPaths(optionCategory);
                        var paths = lpaths[0];

                        if (paths.Length > 1)
                        {
                            var search = CatTree.Nodes.Find(paths[0], false);

                            if (search.Length > 0)
                            {
                                OptionsPanelPath.Text += search[0].Text + @" >";

                                int i = 1;
                                int sub = paths.Length - 1;
                                for (; i < sub; i++)
                                {
                                    search = search[0].Nodes.Find(paths[i], false);

                                    if (search.Length > 0)
                                    {
                                        OptionsPanelPath.Text += search[0].Text + @" >";
                                    }
                                }

                            }
                        }

                        OptionsPanelPath.Text += displayName;

                        if (this.OptionPanelContainer.Controls.Count == 0 || !OptionPanelContainer.Controls[0].Equals(pn))
                        {
                            this.OptionPanelContainer.Controls.Clear();
                            this.OptionPanelContainer.Controls.Add(pn);

                            pn.Visible = true;
                        }

                        return pn;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private void CatTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GoToFirstPanel(e.Node);
        }

        private void GoToFirstPanel(TreeNode selectedNode)
        {
            if (selectedNode.Nodes.Count > 0)
            {
                selectedNode.Expand();

                if (_selectFirstPanel)
                {
                    selectedNode.TreeView.SelectedNode = selectedNode.Nodes[0];
                }
                else
                {
                    GoToFirstPanel(selectedNode.Nodes[0]);
                }
            }
            else
            {
                OptionsPanel pn = SwitchPanel(selectedNode.Name);

                if (pn != null && !string.IsNullOrEmpty(pn.AccessibleDescription))
                {
                    CatDescr.Text = pn.AccessibleDescription;
                }
                else
                {
                    CatDescr.Text = OptionsNoDescription;
                }
            }
        }

        public void GoToPanel(string optionCategory)
        {
            var search = CatTree.Nodes.Find(optionCategory, true);

            if (search.Length > 0)
            {
                TreeNode node = search[0];
                CatTree.SelectedNode = node;
            }
        }

        public void _AppSettings_SettingChanging(object sender, PropertyChangedEventArgs e)
        {
            if (!_saving)
            {
                try
                {
                    var newSetting = PropertyDictionary<string, object>.Convert(sender);
                    object newVal = newSetting[e.PropertyName];

                    if (newVal != null)
                    {
                        if (!newVal.Equals(OldSettings[e.PropertyName]))
                        {
                            try
                            {
                                if (!_changedSettings.ContainsKey(e.PropertyName))
                                {
                                    AppSettings[e.PropertyName] = newVal;
                                    _changedSettings.Add(e.PropertyName, newVal);
                                }
                            }
                            catch
                            {
                                AppSettings[e.PropertyName] = newVal;
                                _changedSettings.Add(e.PropertyName, newVal);
                            }
                        }
                        else
                        {
                            try
                            {
                                _changedSettings.Remove(e.PropertyName);
                            }
                            catch
                            { }
                        }

                        if (!_applyAlwaysEnabled)
                        {
                            ApplyBtn.Enabled = _changedSettings.Count > 0;
                        }
                    }
                }
                catch
                { }
            }
        }

        public virtual void OnSaveOptions()
        {
            _saving = true;

            if (OptionsSaving != null)
            {
                OptionsSaving(this, EventArgs.Empty);
            }

            foreach (var sett in _changedSettings)
            {
                try
                {
                    AppSettings[sett.Key] = sett.Value;
                }
                catch
                { }
            }

            if (_automaticSaveSettings)
            {
                //_appSettings.Save();
                //_appSettings.Reload();

                if (OptionsSaved != null)
                {
                    OptionsSaved(this, EventArgs.Empty);
                }
            }

            _saving = false;
        }

        public virtual void OnResetOptions()
        {
            if (ResetForm != null)
            {
                ResetForm(this, EventArgs.Empty);
            }

            if (!_applyAlwaysEnabled)
            {
                ApplyBtn.Enabled = false;
            }
        }

        public void MouseEnterDescr(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if (ctrl != null && !string.IsNullOrEmpty(ctrl.AccessibleDescription))
            {
                OptionDescrLabel.Text = ctrl.AccessibleDescription;
            }
            else
            {
                OptionDescrLabel.Text = OptionsNoDescription;
            }
        }

        public void MouseLeaveDescr(object sender, EventArgs e)
        {
            OptionDescrLabel.Text = _optionsNoDescription;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            _okClicked = true;
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            OnSaveOptions();

            if (!_applyAlwaysEnabled)
            {
                ApplyBtn.Enabled = false;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            _okClicked = false;
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Visible = false;

            if (_okClicked)
            {
                OnSaveOptions();
            }
            else
            {
                OnResetOptions();
            }
        }

        #endregion
    }
}