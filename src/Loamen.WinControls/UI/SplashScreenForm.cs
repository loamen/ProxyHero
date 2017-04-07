using System.Drawing;
using System.Windows.Forms;

namespace Loamen.WinControls.UI
{
    public partial class SplashScreenForm : Form
    {
        //private byte alpha = 30;
        //private IntPtr handle;

        /// <summary>
        ///     To ensure splash screen is closed using the API and not by keyboard or any other things
        /// </summary>
        private bool CloseSplashScreenFlag;

        /// <summary>
        ///     Base constructor
        /// </summary>
        public SplashScreenForm()
        {
            InitializeComponent();
            Text = Application.ProductName;
            //this.ShowInTaskbar = false;

            progressBar1.Show();
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
                {
                    Size = BackgroundImage.Size;
                    LoadingStatus.Left = 10;
                    LoadingStatus.Top = Height - LoadingStatus.Height - progressBar1.Height - 10;
                }
            }
        }

        #endregion

        /// <summary>
        ///     Displays the splashscreen
        /// </summary>
        public void ShowSplashScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(ShowSplashScreen));
                return;
            }
            Show();
            Application.Run(this);
        }

        /// <summary>
        ///     Closes the SplashScreen
        /// </summary>
        public void CloseSplashScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(CloseSplashScreen));
                return;
            }
            CloseSplashScreenFlag = true;
            Close();
        }

        /// <summary>
        ///     Update text in default green color of success message
        /// </summary>
        /// <param name="Text">Message</param>
        public void UdpateStatusText(string Text)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(UdpateStatusText), new object[] {Text});
                return;
            }
            // Must be on the UI thread if we've got this far
            LoadingStatus.Text = Text;
        }


        /// <summary>
        ///     Update text with message color defined as green/yellow/red/ for success/warning/failure
        /// </summary>
        /// <param name="Text">Message</param>
        /// <param name="tom">Type of Message</param>
        public void UdpateStatusTextWithStatus(string Text, TypeOfMessage tom)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterWithStatusDelegate(UdpateStatusTextWithStatus), new object[] {Text, tom});
                return;
            }
            // Must be on the UI thread if we've got this far
            switch (tom)
            {
                case TypeOfMessage.Error:
                    LoadingStatus.ForeColor = Color.Red;
                    break;
                case TypeOfMessage.Warning:
                    LoadingStatus.ForeColor = Color.Yellow;
                    break;
                case TypeOfMessage.Success:
                    LoadingStatus.ForeColor = Color.Black;
                    break;
            }
            LoadingStatus.Text = Text;
        }

        /// <summary>
        ///     Prevents the closing of form other than by calling the CloseSplashScreen function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseSplashScreenFlag == false)
                e.Cancel = true;
        }

        private delegate void SplashShowCloseDelegate();

        private delegate void StringParameterDelegate(string Text);

        private delegate void StringParameterWithStatusDelegate(string Text, TypeOfMessage tom);

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    SetupLayeredWindow();
        //}

        //protected virtual void SetupLayeredWindow()
        //{
        //    if (this.BackgroundImage == null)
        //        throw new NullReferenceException("SplashForm.BackgroundImage can not be null");

        //    if (!(this.BackgroundImage is Bitmap) ||
        //        this.BackgroundImage.PixelFormat != PixelFormat.Format32bppArgb &&
        //        this.BackgroundImage.PixelFormat != PixelFormat.Format24bppRgb)
        //        throw new Exception("SplashForm does not support this image format");

        //    this.handle = this.Handle;
        //    UpdateLayeredWindow(alpha);
        //}

        //protected virtual void UpdateLayeredWindow(byte alpha)
        //{
        //    Bitmap bitmap = (Bitmap)this.BackgroundImage;
        //    IntPtr hdcScreen = NativeMethods.GetDC(new HandleRef(this, IntPtr.Zero));
        //    IntPtr hdcMemory = NativeMethods.CreateCompatibleDC(new HandleRef(this, hdcScreen));
        //    IntPtr hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
        //    IntPtr hOldBitmap = NativeMethods.SelectObject(new HandleRef(this, hdcMemory), new HandleRef(this, hBitmap));
        //    NativeMethods.SIZESTRUCT size = new NativeMethods.SIZESTRUCT(bitmap.Width, bitmap.Height);
        //    NativeMethods.POINTSTRUCT pointSource = new NativeMethods.POINTSTRUCT(0, 0);
        //    NativeMethods.POINTSTRUCT topPos = new NativeMethods.POINTSTRUCT(Left, Top);
        //    NativeMethods.BLENDFUNCTION blend = new NativeMethods.BLENDFUNCTION();

        //    blend.BlendOp = 0;
        //    blend.BlendFlags = 0;
        //    blend.SourceConstantAlpha = alpha;
        //    blend.AlphaFormat = 1;

        //    NativeMethods.UpdateLayeredWindow(handle, hdcScreen, ref topPos, ref size, hdcMemory, ref pointSource, 0, ref blend, 0x00000002);
        //    NativeMethods.ReleaseDC(new HandleRef(this, IntPtr.Zero), new HandleRef(this, hdcScreen));
        //    if (hBitmap != IntPtr.Zero)
        //    {
        //        NativeMethods.SelectObject(new HandleRef(this, hdcMemory), new HandleRef(this, hOldBitmap));
        //        NativeMethods.DeleteObject(new HandleRef(this, hBitmap));
        //    }
        //    NativeMethods.DeleteDC(new HandleRef(this, hdcMemory));
        //}
    }
}