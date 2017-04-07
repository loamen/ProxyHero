using System;
using System.Windows.Forms;

namespace ProxyHero.Common
{
    public partial class LoadingControl : UserControl
    {
        public LoadingControl()
        {
            InitializeComponent();
            LoadingText.TextChanged += text_changed;
        }

        private void text_changed(object sender, EventArgs e)
        {
            Width = pbLoading.Width + LoadingText.Width;
        }
    }
}