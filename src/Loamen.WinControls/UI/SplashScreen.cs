using System.Drawing;

namespace Loamen.WinControls.UI
{
    /// <summary>
    ///     Defined types of messages: Success/Warning/Error.
    /// </summary>
    public enum TypeOfMessage
    {
        Success,
        Warning,
        Error,
    }

    /// <summary>
    ///     Initiate instance of SplashScreen
    /// </summary>
    public static class SplashScreen
    {
        private static SplashScreenForm sf;

        private static Image backgroundImage;

        private static Color fontForeColor = Color.Black;

        public static Image BackgroundImage
        {
            get { return backgroundImage; }
            set { backgroundImage = value; }
        }

        /// <summary>
        ///     字体颜色
        /// </summary>
        public static Color FontForeColor
        {
            get { return fontForeColor; }
            set { fontForeColor = value; }
        }

        /// <summary>
        ///     Displays the splashscreen
        /// </summary>
        public static void ShowSplashScreen()
        {
            if (sf == null)
            {
                sf = new SplashScreenForm();
                sf.BackgroundImage = BackgroundImage;
                sf.LoadingStatus.ForeColor = FontForeColor;
                sf.ShowSplashScreen();
            }
        }

        /// <summary>
        ///     Closes the SplashScreen
        /// </summary>
        public static void CloseSplashScreen()
        {
            if (sf != null)
            {
                sf.CloseSplashScreen();
                sf = null;
            }
        }

        /// <summary>
        ///     Update text in default green color of success message
        /// </summary>
        /// <param name="Text">Message</param>
        public static void UpdateStatusText(string Text)
        {
            if (sf != null)
                sf.UdpateStatusText(Text + "...");
        }

        /// <summary>
        ///     Update text with message color defined as green/yellow/red/ for success/warning/failure
        /// </summary>
        /// <param name="Text">Message</param>
        /// <param name="tom">Type of Message</param>
        public static void UdpateStatusTextWithStatus(string Text, TypeOfMessage tom)
        {
            if (sf != null)
                sf.UdpateStatusTextWithStatus(Text, tom);
        }
    }
}