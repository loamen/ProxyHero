using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Loamen.WinControls.UI
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [ToolboxItem(true)]
    public partial class OptionsPanel : UserControl
    {
        private OptionsForm _optionsForm;
        private string _path;
        private string _displayName;

        private bool _optionsUpdated;

        [Browsable(true)]
        [Description("面板选项改变时，该事件被触发")]
        public event EventHandler OptionsChanged;

        #region Properties
        /// <summary>
        /// Gets the options form.
        /// </summary>
        /// <value>The options form.</value>
        [Browsable(false)]
        public OptionsForm OptionsForm
        {
            get
            {
                return _optionsForm;
            }
        }

        [Category("Options Form")]
        [Description("路径的OptionsPanel（类别的TreeView在父OptionsForm形式确定的位置）")]
        public string CategoryPath
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        [Category("Options Form")]
        [Description("此面板中显示的名称，类别的TreeView在父OptionsForm形式")]
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether application must restart.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if application must restart to apply options otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool ApplicationMustRestart
        {
            get
            {
                if (OptionsForm != null)
                {
                    return OptionsForm.ApplicationMustRestart;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (OptionsForm != null)
                {
                    OptionsForm.ApplicationMustRestart = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [options changed].
        /// </summary>
        /// <value><c>true</c> if [options changed]; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool OptionsUpdated
        {
            get
            {
                return _optionsUpdated;
            }
            set
            {
                _optionsUpdated = value;

                if (_optionsUpdated)
                {
                    OnOptionsChanged();
                }
            }
        }

        #endregion

        public OptionsPanel()
        {
            InitializeComponent();
        }

        virtual public void PanelAdded(OptionsForm optf)
        {
            _optionsForm = optf;

            InitPanelForControl(this);

            _optionsForm.OptionsSaving += new EventHandler(OptionsSaving);
            _optionsForm.OptionsSaved += new EventHandler(OptionsSaved);
            _optionsForm.ResetForm += new EventHandler(ResetForm);
        }

        protected void SetOption(string optionName, object value)
        {
            _optionsForm.AppSettings[optionName]=value;
            //_optionsForm.AppSettings.Current[OptionName] = value;
            OptionsUpdated = true;
        }

        protected void OnOptionsChanged()
        {
            if (OptionsChanged != null)
            {
                OptionsChanged(this, EventArgs.Empty);
            }
        }

        protected void OptionsSaving(object sender, EventArgs e)
        {
        }

        protected void OptionsSaved(object sender, EventArgs e)
        {
            ReloadValues(this);
        }

        protected virtual void ResetForm(object sender, EventArgs e)
        {
            ReloadValues(this);
        }
        
        private void ReloadValues(Control ctrl)
        {
            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                Control ctrl2 = ctrl.Controls[i];

                for (int l = 0; l < ctrl2.DataBindings.Count; l++)
                {
                    Binding bind = ctrl2.DataBindings[l];
                    bind.ReadValue();
                }

                ReloadValues(ctrl2);
            }
        }

        private void InitPanelForControl(Control ctrl)
        {
            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                Control ctrl2 = ctrl.Controls[i];

                for (int l = 0; l < ctrl2.DataBindings.Count; l++)
                {
                    Binding bind = ctrl2.DataBindings[l];
                    string prop = bind.BindingMemberInfo.BindingMember;

                    try
                    {
                        object value = _optionsForm.AppSettings[prop];

                        if (value != null)
                        {
                            bind.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                            bind.ControlUpdateMode = ControlUpdateMode.Never;
                            _optionsForm.OldSettings.Add(prop,value);
                        }
                    }
                    catch
                    { }
                }

                InitPanelForControl(ctrl2);
            }
        }
    }
}
