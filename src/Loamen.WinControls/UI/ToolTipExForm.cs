using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Loamen.WinControls.Properties;

namespace Loamen.WinControls.UI
{
    public partial class ToolTipExForm : Form
    {
        #region Win32 API

        //Obtained from www.PInvoke.net

        private const int SW_NOACTIVATE = 4;

        #region UpdateLayeredWindow

        private const int AC_SRC_OVER = 0;
        private const int AC_SRC_ALPHA = 1;
        private const int ULW_ALPHA = 2;

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst,
                                                       ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc,
                                                       uint crKey,
                                                       [In] ref BLENDFUNCTION pblend, uint dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        #endregion

        #region GDI

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        #endregion

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        #region Constants

        private const int WS_EX_LAYERED = 0x80000;
        private const int LINE_HEIGHT = 2;
        private const int BORDER_THICKNESS = 1;
        private const int TOTAL_BORDER_THICKNESS = BORDER_THICKNESS + BORDER_THICKNESS;
        private const float GOLDEN_RATIO = 1.61803399f;

        #endregion

        #region Variables

        private readonly ToolTipEx ctrl;
        private Size f1HelpSize;
        private Size messageSize;
        private Size titleSize;

        #endregion

        #region Constructor

        public ToolTipExForm(ToolTipEx ctrl)
        {
            this.ctrl = ctrl;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;

            #region Popup Size

            Size = CalculateSize();

            #endregion

            #region Popup Location

            var screenPosition = new Point(0, 0);
            if (ctrl.ParentControl != null)
            {
                screenPosition =
                    ctrl.ParentControl.PointToScreen(
                        new Point(ctrl.RightPopupMargin + ctrl.ParentControl.Width + ctrl.RightPopupMargin,
                                  -ctrl.TitlePadding.Top));
            }
            else if (ctrl.ParentToolStripItem != null)
            {
                screenPosition =
                    ctrl.ParentToolStripItem.GetCurrentParent()
                        .PointToScreen(
                            new Point(ctrl.RightPopupMargin + ctrl.ParentToolStripItem.Width + ctrl.RightPopupMargin,
                                      -ctrl.TitlePadding.Top));
            }

            if (screenPosition.X + Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                //if the popup will not display entirely on the screen move it to the left of the control
                int x = ctrl.ParentControl.PointToScreen(new Point(0, 0)).X - ctrl.LeftPopupMargin - Size.Width;
                if (x >= 0) //if the left coordinate is left of the screen (negative) display what you can on the right
                    screenPosition.X = x;
            }
            Location = screenPosition;

            #endregion

            #region Draw Bitmap

            using (var bmp = new Bitmap(Size.Width, Size.Height))
            {
                DrawBitmap(bmp);
                SelectBitmap(bmp);
            }

            #endregion

            ShowWindow(Handle, SW_NOACTIVATE);
        }

        #endregion

        #region Properties

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED;
                return cp;
            }
        }

        #endregion

        #region Private Functions

        private Size CalculateSize()
        {
            int maximumPopupWidth = ctrl.UseGoldenRatio
                                        ? Math.Min(ctrl.MaximumPopupWidth, GetGoldenRatioWidth())
                                        : ctrl.MaximumPopupWidth;

            int maxTitleWidth = maximumPopupWidth - ctrl.TitlePadding.Left - ctrl.TitlePadding.Right;
            int maxMessageWidth = maximumPopupWidth - ctrl.MessagePadding.Left - ctrl.MessagePadding.Right;
            int maxF1HelpWidth = maximumPopupWidth - ctrl.F1HelpPadding.Left - Resources.unknown.Width -
                                 (Resources.unknown.Width/4) - ctrl.F1HelpPadding.Right;

            var retVal = new Size();
            using (Graphics g = CreateGraphics())
            {
                titleSize = MeasureString(g, ctrl.TitleText, ctrl.TitleFont, maxTitleWidth);
                messageSize = MeasureString(g, ctrl.MessageText, ctrl.MessageFont, maxMessageWidth);
                f1HelpSize = MeasureString(g, ctrl.F1HelpText, ctrl.F1HelpFont, maxF1HelpWidth);
                int titleWidth = ctrl.TitlePadding.Left + titleSize.Width + ctrl.TitlePadding.Right;
                int messageWidth = ctrl.MessagePadding.Left + messageSize.Width + ctrl.MessagePadding.Right;
                int f1HelpWidth = ctrl.F1HelpPadding.Left + Resources.unknown.Width + (Resources.unknown.Width/4) +
                                  f1HelpSize.Width + ctrl.F1HelpPadding.Right;
                int width = Math.Max(titleWidth, messageWidth);
                width = Math.Max(width, f1HelpWidth);
                retVal.Width = Math.Min(maximumPopupWidth, width);

                int titleHeight = !string.IsNullOrEmpty(ctrl.TitleText)
                                      ? ctrl.TitlePadding.Top + titleSize.Height + ctrl.TitlePadding.Bottom + LINE_HEIGHT
                                      : 0;

                retVal.Height = titleHeight +
                                ctrl.MessagePadding.Top + messageSize.Height + ctrl.MessagePadding.Bottom +
                                LINE_HEIGHT +
                                ctrl.F1HelpPadding.Top + f1HelpSize.Height + ctrl.F1HelpPadding.Bottom;
            }

            retVal.Width += TOTAL_BORDER_THICKNESS + ctrl.ShadowDepth;
            retVal.Height += TOTAL_BORDER_THICKNESS + ctrl.ShadowDepth;

            return retVal;
        }

        private GraphicsPath CreateRoundRect(Rectangle rect, int radius)
        {
            var gp = new GraphicsPath();

            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;

            if (width > 0 && height > 0)
            {
                radius = Math.Min(radius, height/2 - 1);
                radius = Math.Min(radius, width/2 - 1);

                gp.AddLine(x + radius, y, x + width - (radius*2), y);
                gp.AddArc(x + width - (radius*2), y, radius*2, radius*2, 270, 90);
                gp.AddLine(x + width, y + radius, x + width, y + height - (radius*2));
                gp.AddArc(x + width - (radius*2), y + height - (radius*2), radius*2, radius*2, 0, 90);
                gp.AddLine(x + width - (radius*2), y + height, x + radius, y + height);
                gp.AddArc(x, y + height - (radius*2), radius*2, radius*2, 90, 90);
                gp.AddLine(x, y + height - (radius*2), x, y + radius);
                gp.AddArc(x, y, radius*2, radius*2, 180, 90);
                gp.CloseFigure();
            }
            return gp;
        }

        private void DrawBackground(Graphics g)
        {
            var messageRect = new Rectangle(
                0,
                0,
                Width - ctrl.ShadowDepth - BORDER_THICKNESS,
                Height - ctrl.ShadowDepth - BORDER_THICKNESS);
            if (messageRect.Width > 0 && messageRect.Height > 0)
            {
                using (GraphicsPath messagePath = CreateRoundRect(messageRect, 4))
                {
                    using (Brush messageBackgroundBrush = new LinearGradientBrush(
                        messageRect, ctrl.LightBackgroundColor, ctrl.DarkBackgroundColor, LinearGradientMode.Vertical))
                    {
                        g.FillPath(messageBackgroundBrush, messagePath);
                        using (var messageBoarderPen = new Pen(ctrl.BorderColor))
                            g.DrawPath(messageBoarderPen, messagePath);
                    }
                }
            }
        }

        private void DrawBitmap(Bitmap bmp)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                DrawShadow(g);
                DrawBackground(g);
                DrawContent(g);
            }
        }

        private void DrawContent(Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Pen darkPen = null;
            Pen lightPen = null;
            try
            {
                darkPen = new Pen(ctrl.DarkLineColor);
                lightPen = new Pen(ctrl.LightLineColor);
                int y = 0;
                int x = BORDER_THICKNESS + ctrl.TitlePadding.Left;
                int x2 = Width - ctrl.TitlePadding.Right - ctrl.ShadowDepth - TOTAL_BORDER_THICKNESS;

                #region Title and Line

                if (!string.IsNullOrEmpty(ctrl.TitleText))
                {
                    #region Title

                    var titleRect = new Rectangle(
                        BORDER_THICKNESS + ctrl.TitlePadding.Left,
                        BORDER_THICKNESS + ctrl.TitlePadding.Top,
                        titleSize.Width,
                        titleSize.Height);
                    using (Brush titleBrush = new SolidBrush(ctrl.TitleForeColor))
                        g.DrawString(ctrl.TitleText, ctrl.TitleFont, titleBrush, titleRect);
                    y = titleRect.Bottom + ctrl.TitlePadding.Bottom;

                    #endregion

                    #region Line

                    g.DrawLine(darkPen, x, y, x2, y);
                    y++;
                    g.DrawLine(lightPen, x, y, x2, y);
                    y++;

                    #endregion
                }

                #endregion

                #region Message

                var messageRect = new Rectangle(
                    BORDER_THICKNESS + ctrl.MessagePadding.Left,
                    y + ctrl.MessagePadding.Top,
                    messageSize.Width,
                    messageSize.Height);
                using (Brush messageBrush = new SolidBrush(ctrl.MessageForeColor))
                    g.DrawString(ctrl.MessageText, ctrl.MessageFont, messageBrush, messageRect);
                y = messageRect.Bottom + ctrl.MessagePadding.Bottom;

                #endregion

                #region Line

                g.DrawLine(darkPen, x, y, x2, y);
                y++;
                g.DrawLine(lightPen, x, y, x2, y);
                y++;

                #endregion

                #region Press F1

                g.DrawImage(Resources.unknown,
                            new Point(BORDER_THICKNESS + ctrl.F1HelpPadding.Left, y + ctrl.F1HelpPadding.Top));

                var f1HelpRect = new Rectangle(
                    BORDER_THICKNESS + ctrl.F1HelpPadding.Left + Resources.unknown.Width + (Resources.unknown.Width/4),
                    y + ctrl.F1HelpPadding.Top,
                    f1HelpSize.Width,
                    f1HelpSize.Height);
                using (Brush f1HelpBrush = new SolidBrush(ctrl.F1HelpForeColor))
                    g.DrawString(ctrl.F1HelpText, ctrl.F1HelpFont, f1HelpBrush, f1HelpRect);

                #endregion
            }
            finally
            {
                if (darkPen != null)
                    darkPen.Dispose();

                if (lightPen != null)
                    lightPen.Dispose();
            }
        }

        private void DrawShadow(Graphics g)
        {
            if (ctrl.ShadowDepth > 0)
            {
                var shadowRect = new Rectangle(
                    ctrl.ShadowDepth,
                    ctrl.ShadowDepth,
                    Width - ctrl.ShadowDepth,
                    Height - ctrl.ShadowDepth);

                if (shadowRect.Width > 0 && shadowRect.Height > 0)
                {
                    using (GraphicsPath shadowPath = CreateRoundRect(shadowRect, 4))
                    {
                        using (var shadowBrush = new PathGradientBrush(shadowPath))
                        {
                            var colors = new Color[4];
                            var positions = new float[4];
                            var sBlend = new ColorBlend();
                            colors[0] = Color.FromArgb(0, 0, 0, 0);
                            colors[1] = Color.FromArgb(32, 0, 0, 0);
                            colors[2] = Color.FromArgb(64, 0, 0, 0);
                            colors[3] = Color.FromArgb(128, 0, 0, 0);
                            positions[0] = 0.0f;
                            positions[1] = 0.015f;
                            positions[2] = 0.030f;
                            positions[3] = 1.0f;
                            sBlend.Colors = colors;
                            sBlend.Positions = positions;

                            shadowBrush.InterpolationColors = sBlend;
                            shadowBrush.CenterPoint = new Point(
                                shadowRect.Left + (shadowRect.Width/2),
                                shadowRect.Top + (shadowRect.Height/2));

                            g.FillPath(shadowBrush, shadowPath);
                        }
                    }
                }
            }
        }

        private int GetGoldenRatioWidth()
        {
            using (Graphics g = CreateGraphics())
            {
                int goldenWidth = 0;
                float volumn = 0;
                for (int i = 0; i < ctrl.GoldenRatioSampleRate; i++)
                {
                    titleSize = MeasureString(g, ctrl.TitleText, ctrl.TitleFont, goldenWidth);
                    messageSize = MeasureString(g, ctrl.MessageText, ctrl.MessageFont, goldenWidth);
                    f1HelpSize = MeasureString(g, ctrl.F1HelpText, ctrl.F1HelpFont, goldenWidth);
                    int titleWidth = ctrl.TitlePadding.Left + titleSize.Width + ctrl.TitlePadding.Right;
                    int messageWidth = ctrl.MessagePadding.Left + messageSize.Width + ctrl.MessagePadding.Right;
                    int f1HelpWidth = ctrl.F1HelpPadding.Left + Resources.unknown.Width + (Resources.unknown.Width/4) +
                                      f1HelpSize.Width + ctrl.F1HelpPadding.Right;
                    int width = Math.Max(titleWidth, messageWidth);
                    width = Math.Max(width, f1HelpWidth);

                    int titleHeight = !string.IsNullOrEmpty(ctrl.TitleText)
                                          ? ctrl.TitlePadding.Top + titleSize.Height + ctrl.TitlePadding.Bottom +
                                            LINE_HEIGHT
                                          : 0;

                    int height =
                        titleHeight +
                        ctrl.MessagePadding.Top + messageSize.Height + ctrl.MessagePadding.Bottom + LINE_HEIGHT +
                        ctrl.F1HelpPadding.Top + f1HelpSize.Height + ctrl.F1HelpPadding.Bottom;

                    float sampleVolumn = height*width;
                    if (sampleVolumn == volumn)
                        break;
                    volumn = sampleVolumn;
                    double x = Math.Sqrt(volumn*GOLDEN_RATIO);
                    double y = volumn/x;
                    goldenWidth = 1 + (int) x;
                }
                return goldenWidth;
            }
        }

        private Size MeasureString(Graphics g, string val, Font font)
        {
            SizeF sizeF = g.MeasureString(val, font);
            return new Size((int) sizeF.Width + 1, (int) sizeF.Height + 1);
        }

        private Size MeasureString(Graphics g, string val, Font font, int maxWidth)
        {
            if (maxWidth <= 0)
                return MeasureString(g, val, font);

            SizeF sizeF = g.MeasureString(val, font, maxWidth);
            return new Size(
                (int) sizeF.Width < maxWidth ? (int) sizeF.Width + 1 : maxWidth,
                (int) sizeF.Height + 1);
        }

        private void SelectBitmap(Bitmap bmp)
        {
            IntPtr hDC = GetDC(IntPtr.Zero);
            try
            {
                IntPtr hMemDC = CreateCompatibleDC(hDC);
                try
                {
                    IntPtr hBmp = bmp.GetHbitmap(Color.FromArgb(0));
                    try
                    {
                        IntPtr previousBmp = SelectObject(hMemDC, hBmp);
                        try
                        {
                            var ptDst = new Point(Left, Top);
                            var size = new Size(bmp.Width, bmp.Height);
                            var ptSrc = new Point(0, 0);

                            var blend = new BLENDFUNCTION();
                            blend.BlendOp = AC_SRC_OVER;
                            blend.BlendFlags = 0;
                            blend.SourceConstantAlpha = 255;
                            blend.AlphaFormat = AC_SRC_ALPHA;

                            UpdateLayeredWindow(
                                Handle,
                                hDC,
                                ref ptDst,
                                ref size,
                                hMemDC,
                                ref ptSrc,
                                0,
                                ref blend,
                                ULW_ALPHA);
                        }
                        finally
                        {
                            SelectObject(hDC, previousBmp);
                        }
                    }
                    finally
                    {
                        DeleteObject(hBmp);
                    }
                }
                finally
                {
                    DeleteDC(hMemDC);
                }
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, hDC);
            }
        }

        #endregion
    }
}