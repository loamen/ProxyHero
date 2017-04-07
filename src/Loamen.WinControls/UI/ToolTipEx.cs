using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Loamen.WinControls.UI
{
    public partial class ToolTipEx : Component
    {
        #region Constructor

        public ToolTipEx()
        {
            InitializeComponent();
        }

        public ToolTipEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Constants

        private const int DEFAULT_GOLDEN_RATIO_SAMPLE_RATE = 3;
        private const int DEFAULT_LEFT_POPUP_MARGIN = 1;
        private const int DEFAULT_MAX_POPUP_WIDTH = 500;
        private const int DEFAULT_RIGHT_POPUP_MARGIN = 4;
        private const int DEFAULT_SHADOW_DEPTH = 5;
        private const int MIN_MAX_POPUP_WIDTH = 100;
        private const int MAX_GOLDEN_RATIO_SAMPLE_RATE = 10;
        private const string CATEGORY = "Appearance";
        private const string DEFAULT_F1HELP = "F1获取帮助";
        private const string DEFAULT_MESSAGE = "正在加载...";
        private const string DEFAULT_TITLE = "正在加载...";

        #endregion

        #region Variables

        private ToolTipExForm form;

        #region Text

        private string f1HelpText = DEFAULT_F1HELP;
        private string messageText = DEFAULT_MESSAGE;
        private string titleText = DEFAULT_TITLE;

        #endregion

        #region Fonts

        private Font f1HelpFont = new Font("Segoe UI", 8.25f, FontStyle.Bold);
        private Font messageFont = new Font("Segoe UI", 8.25f);
        private Font titleFont = new Font("Segoe UI", 9.75f, FontStyle.Bold);

        #endregion

        #region Maximum Width and Shadow

        private int goldenRatioSampleRate = DEFAULT_GOLDEN_RATIO_SAMPLE_RATE;
        private int maximumPopupWidth = DEFAULT_MAX_POPUP_WIDTH;
        private int shadowDepth = DEFAULT_SHADOW_DEPTH;
        private bool useGoldenRatio = true;

        #endregion

        #region Popup Margins

        private int leftPopupMargin = DEFAULT_LEFT_POPUP_MARGIN;
        private int rightPopupMargin = DEFAULT_RIGHT_POPUP_MARGIN;

        #endregion

        #region Padding

        private Padding f1HelpPadding = new Padding(6, 4, 6, 4);
        private Padding messagePadding = new Padding(12, 6, 12, 2);
        private Padding titlePadding = new Padding(6, 8, 6, 0);

        #endregion

        #region Fore Colors

        private Color f1HelpForeColor = Color.FromArgb(64, 64, 64);
        private Color messageForeColor = Color.FromArgb(64, 64, 64);
        private Color titleForeColor = Color.FromArgb(64, 64, 64);

        #endregion

        #region Background Colors

        private Color borderColor = Color.FromArgb(118, 118, 118);
        private Color darkBackgroundColor = Color.FromArgb(201, 217, 239);
        private Color darkLineColor = Color.FromArgb(158, 187, 221);
        private Color lightBackgroundColor = Color.White;
        private Color lightLineColor = Color.White;

        #endregion

        #endregion

        #region Properties

        #region ParentControl

        private Control parentControl;

        [Category(CATEGORY),
         DefaultValue(null)]
        public Control ParentControl
        {
            get { return parentControl; }
            set
            {
                parentControl = value;
                parentControl.MouseEnter += parentControl_MouseEnter;
                parentControl.MouseLeave += parentControl_MouseLeave;
                parentToolStripItem = null;
            }
        }

        #endregion

        #region ToolStripItem

        private ToolStripDropDownButton parentToolStripItem;

        [Category(CATEGORY),
         DefaultValue(null)]
        public ToolStripDropDownButton ParentToolStripItem
        {
            get { return parentToolStripItem; }
            set
            {
                parentToolStripItem = value;
                parentToolStripItem.MouseEnter += parentControl_MouseEnter;
                parentToolStripItem.MouseLeave += parentControl_MouseLeave;
                parentControl = null;
            }
        }

        #endregion

        #region Text

        [Category(CATEGORY),
         DefaultValue(DEFAULT_F1HELP)]
        public string F1HelpText
        {
            set { f1HelpText = value; }
            get { return f1HelpText; }
        }

        [DefaultValue(DEFAULT_MESSAGE),
         Browsable(true)]
        public string MessageText
        {
            set { messageText = value; }
            get { return messageText; }
        }

        [Category(CATEGORY),
         DefaultValue(DEFAULT_TITLE)]
        public string TitleText
        {
            set { titleText = value; }
            get { return titleText; }
        }

        #endregion

        #region Fonts

        [Category(CATEGORY),
         DefaultValue(typeof (Font), "Segoe UI, 8.25pt, style=Bold")]
        public Font F1HelpFont
        {
            get { return f1HelpFont; }
            set
            {
                if (f1HelpFont != null)
                    f1HelpFont.Dispose();
                f1HelpFont = value;
            }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Font), "Segoe UI, 8.25pt")]
        public Font MessageFont
        {
            get { return messageFont; }
            set
            {
                if (messageFont != null)
                    messageFont.Dispose();
                messageFont = value;
            }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Font), "Segoe UI, 9.75pt, style=Bold")]
        public Font TitleFont
        {
            get { return titleFont; }
            set
            {
                if (titleFont != null)
                    titleFont.Dispose();
                titleFont = value;
            }
        }

        #endregion

        #region Maximum Width and Shadow

        [Category(CATEGORY),
         DefaultValue(true)]
        public bool UseGoldenRatio
        {
            set { useGoldenRatio = value; }
            get { return useGoldenRatio; }
        }

        [Category(CATEGORY),
         DefaultValue(DEFAULT_GOLDEN_RATIO_SAMPLE_RATE)]
        public int GoldenRatioSampleRate
        {
            set
            {
                if (value <= 0)
                    value = 1;
                if (value > MAX_GOLDEN_RATIO_SAMPLE_RATE)
                    value = MAX_GOLDEN_RATIO_SAMPLE_RATE;
                goldenRatioSampleRate = value;
            }
            get { return goldenRatioSampleRate; }
        }

        [Category(CATEGORY),
         DefaultValue(DEFAULT_MAX_POPUP_WIDTH)]
        public int MaximumPopupWidth
        {
            set
            {
                if (value < MIN_MAX_POPUP_WIDTH)
                    value = MIN_MAX_POPUP_WIDTH;
                maximumPopupWidth = value;
            }
            get { return maximumPopupWidth; }
        }

        [Category(CATEGORY),
         DefaultValue(DEFAULT_SHADOW_DEPTH)]
        public int ShadowDepth
        {
            set
            {
                if (value < 0)
                    value = 0;
                shadowDepth = value;
            }
            get { return shadowDepth; }
        }

        #endregion

        #region Popup Margins

        [Category(CATEGORY),
         DefaultValue(DEFAULT_LEFT_POPUP_MARGIN)]
        public int LeftPopupMargin
        {
            set
            {
                if (value < 0)
                    value = 0;
                leftPopupMargin = value;
            }
            get { return leftPopupMargin; }
        }

        [Category(CATEGORY),
         DefaultValue(DEFAULT_RIGHT_POPUP_MARGIN)]
        public int RightPopupMargin
        {
            set
            {
                if (value < 0)
                    value = 0;
                rightPopupMargin = value;
            }
            get { return rightPopupMargin; }
        }

        #endregion

        #region Padding

        [Category(CATEGORY),
         DefaultValue(typeof (Padding), "6, 4, 6, 4")]
        public Padding F1HelpPadding
        {
            set { f1HelpPadding = value; }
            get { return f1HelpPadding; }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Padding), "12, 6, 12, 2")]
        public Padding MessagePadding
        {
            set { messagePadding = value; }
            get { return messagePadding; }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Padding), "6, 8, 6, 0")]
        public Padding TitlePadding
        {
            set { titlePadding = value; }
            get { return titlePadding; }
        }

        #endregion

        #region Fore Colors

        [Category(CATEGORY),
         DefaultValue(typeof (Color), "64, 64, 64")]
        public Color F1HelpForeColor
        {
            set { f1HelpForeColor = value; }
            get { return f1HelpForeColor; }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Color), "64, 64, 64")]
        public Color MessageForeColor
        {
            set { messageForeColor = value; }
            get { return messageForeColor; }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Color), "64, 64, 64")]
        public Color TitleForeColor
        {
            set { titleForeColor = value; }
            get { return titleForeColor; }
        }

        #endregion

        #region Background Colors

        [Category(CATEGORY),
         DefaultValue(typeof (Color), "118, 118, 118")]
        public Color BorderColor
        {
            set { borderColor = value; }
            get { return borderColor; }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Color), "201, 217, 239")]
        public Color DarkBackgroundColor
        {
            set { darkBackgroundColor = value; }
            get { return darkBackgroundColor; }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Color), "158, 187, 221")]
        public Color DarkLineColor
        {
            set { darkLineColor = value; }
            get { return darkLineColor; }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Color), "255, 255, 255")]
        public Color LightBackgroundColor
        {
            set { lightBackgroundColor = value; }
            get { return lightBackgroundColor; }
        }

        [Category(CATEGORY),
         DefaultValue(typeof (Color), "255, 255, 255")]
        public Color LightLineColor
        {
            set { lightLineColor = value; }
            get { return lightLineColor; }
        }

        #endregion

        private bool enable;

        [Category(CATEGORY),
         DefaultValue(false), Description("是否可用")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        #endregion

        #region Functions

        public void ShowPopup()
        {
            if (form == null && enable)
                form = new ToolTipExForm(this);
        }

        public void HidePopup()
        {
            if (form != null && enable)
            {
                form.Close();
                form.Dispose();
                form = null;
            }
        }

        #endregion

        #region Event Handlers

        private void parentControl_MouseEnter(object sender, EventArgs e)
        {
            ShowPopup();
        }

        private void parentControl_MouseLeave(object sender, EventArgs e)
        {
            HidePopup();
        }

        #endregion
    }
}