using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Loamen.WinControls.UI
{
    public partial class SplashForm : Form
    {
        private byte alpha = 30;
        private IntPtr handle;

        public SplashForm()
        {
            InitializeComponent();
        }

        #region Properties

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= NativeMethods.WS_EX_LAYERED;
                cp.ExStyle &= ~NativeMethods.WS_EX_APPWINDOW;
                return cp;
            }
        }

        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set
            {
                base.BackgroundImage = value;
                if (value != null)
                    Size = BackgroundImage.Size;
            }
        }

        #endregion

        public void AnimatedHide()
        {
            UpdateLayeredWindow(alpha);
            new Thread(AnimateFuncHide).Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupLayeredWindow();
        }

        protected virtual void SetupLayeredWindow()
        {
            if (BackgroundImage == null)
                throw new NullReferenceException("SplashForm.BackgroundImage can not be null");

            if (!(BackgroundImage is Bitmap) ||
                BackgroundImage.PixelFormat != PixelFormat.Format32bppArgb &&
                BackgroundImage.PixelFormat != PixelFormat.Format24bppRgb)
                throw new Exception("SplashForm does not support this image format");

            handle = Handle;
            UpdateLayeredWindow(alpha);
            new Thread(AnimateFuncShow).Start();
        }

        protected virtual void UpdateLayeredWindow(byte alpha)
        {
            var bitmap = (Bitmap) BackgroundImage;
            IntPtr hdcScreen = NativeMethods.GetDC(new HandleRef(this, IntPtr.Zero));
            IntPtr hdcMemory = NativeMethods.CreateCompatibleDC(new HandleRef(this, hdcScreen));
            IntPtr hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
            IntPtr hOldBitmap = NativeMethods.SelectObject(new HandleRef(this, hdcMemory), new HandleRef(this, hBitmap));
            var size = new NativeMethods.SIZESTRUCT(bitmap.Width, bitmap.Height);
            var pointSource = new NativeMethods.POINTSTRUCT(0, 0);
            var topPos = new NativeMethods.POINTSTRUCT(Left, Top);
            var blend = new NativeMethods.BLENDFUNCTION();

            blend.BlendOp = 0;
            blend.BlendFlags = 0;
            blend.SourceConstantAlpha = alpha;
            blend.AlphaFormat = 1;

            NativeMethods.UpdateLayeredWindow(handle, hdcScreen, ref topPos, ref size, hdcMemory, ref pointSource, 0,
                                              ref blend, 0x00000002);
            NativeMethods.ReleaseDC(new HandleRef(this, IntPtr.Zero), new HandleRef(this, hdcScreen));
            if (hBitmap != IntPtr.Zero)
            {
                NativeMethods.SelectObject(new HandleRef(this, hdcMemory), new HandleRef(this, hOldBitmap));
                NativeMethods.DeleteObject(new HandleRef(this, hBitmap));
            }
            NativeMethods.DeleteDC(new HandleRef(this, hdcMemory));
        }

        protected virtual void AnimateFuncShow()
        {
            while (alpha + 5 <= 255)
            {
                Thread.Sleep(5);
                alpha += 5;
                UpdateLayeredWindow(alpha);
            }
            if (alpha != 255)
                UpdateLayeredWindow(255);
        }

        protected virtual void AnimateFuncHide()
        {
            while (alpha > 5)
            {
                Thread.Sleep(5);
                alpha -= 5;
                UpdateLayeredWindow(alpha);
            }
            Hide();
        }
    }
}