namespace ProxyHero.TabPages
{
    partial class IEBrowserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IEBrowserForm));
            this.txtUrl = new Loamen.WinControls.UI.ToolStripSpringTextBox();
            this.fileToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.fileBrowserToolStrip = new System.Windows.Forms.ToolStrip();
            this.HomePage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFavorites = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Back = new System.Windows.Forms.ToolStripButton();
            this.Forward = new System.Windows.Forms.ToolStripButton();
            this.Stop = new System.Windows.Forms.ToolStripButton();
            this.Fresh = new System.Windows.Forms.ToolStripButton();
            this.LockAndRefresh = new System.Windows.Forms.ToolStripButton();
            this.Go = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AddToTestingList = new System.Windows.Forms.ToolStripButton();
            this.Setting = new System.Windows.Forms.ToolStripButton();
            this.wbMain = new Loamen.WinControls.UI.WebBrowserEx();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvFavirate = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.fileBrowserToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUrl.Margin = new System.Windows.Forms.Padding(1);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(264, 29);
            this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUrl_KeyDown);
            this.txtUrl.Click += new System.EventHandler(this.txtUrl_Click);
            this.txtUrl.DoubleClick += new System.EventHandler(this.txtUrl_DoubleClick);
            // 
            // fileToolStripSeparator
            // 
            this.fileToolStripSeparator.Name = "fileToolStripSeparator";
            this.fileToolStripSeparator.Size = new System.Drawing.Size(6, 31);
            // 
            // fileBrowserToolStrip
            // 
            this.fileBrowserToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.fileBrowserToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HomePage,
            this.toolStripSeparator1,
            this.tsbFavorites,
            this.toolStripSeparator3,
            this.Back,
            this.Forward,
            this.Stop,
            this.Fresh,
            this.LockAndRefresh,
            this.fileToolStripSeparator,
            this.txtUrl,
            this.Go,
            this.toolStripSeparator2,
            this.AddToTestingList,
            this.Setting});
            this.fileBrowserToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fileBrowserToolStrip.Name = "fileBrowserToolStrip";
            this.fileBrowserToolStrip.Size = new System.Drawing.Size(566, 31);
            this.fileBrowserToolStrip.Stretch = true;
            this.fileBrowserToolStrip.TabIndex = 11;
            this.fileBrowserToolStrip.Text = "toolStrip2";
            // 
            // HomePage
            // 
            this.HomePage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HomePage.Image = ((System.Drawing.Image)(resources.GetObject("HomePage.Image")));
            this.HomePage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.HomePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HomePage.Name = "HomePage";
            this.HomePage.Size = new System.Drawing.Size(28, 28);
            this.HomePage.Text = "主页";
            this.HomePage.Click += new System.EventHandler(this.tsbHome_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbFavorites
            // 
            this.tsbFavorites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFavorites.Image = ((System.Drawing.Image)(resources.GetObject("tsbFavorites.Image")));
            this.tsbFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFavorites.Name = "tsbFavorites";
            this.tsbFavorites.Size = new System.Drawing.Size(28, 28);
            this.tsbFavorites.Text = "收藏夹";
            this.tsbFavorites.Click += new System.EventHandler(this.tsbFavorites_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // Back
            // 
            this.Back.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Back.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(28, 28);
            this.Back.Text = "后退";
            this.Back.Click += new System.EventHandler(this.backFileToolStripButton_Click);
            // 
            // Forward
            // 
            this.Forward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Forward.Image = ((System.Drawing.Image)(resources.GetObject("Forward.Image")));
            this.Forward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Forward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(28, 28);
            this.Forward.Text = "前进";
            this.Forward.Click += new System.EventHandler(this.forwardFileToolStripButton_Click);
            // 
            // Stop
            // 
            this.Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Stop.Image = ((System.Drawing.Image)(resources.GetObject("Stop.Image")));
            this.Stop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(28, 28);
            this.Stop.Text = "停止";
            this.Stop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // Fresh
            // 
            this.Fresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Fresh.Image = ((System.Drawing.Image)(resources.GetObject("Fresh.Image")));
            this.Fresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Fresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Fresh.Name = "Fresh";
            this.Fresh.Size = new System.Drawing.Size(28, 28);
            this.Fresh.Text = "刷新";
            this.Fresh.Click += new System.EventHandler(this.tsbFresh_Click);
            // 
            // LockAndRefresh
            // 
            this.LockAndRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LockAndRefresh.Image = global::ProxyHero.Properties.Resources.lock1;
            this.LockAndRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LockAndRefresh.Name = "LockAndRefresh";
            this.LockAndRefresh.Size = new System.Drawing.Size(28, 28);
            this.LockAndRefresh.Text = "锁定当前页并自动刷新";
            this.LockAndRefresh.Click += new System.EventHandler(this.tsbFreshPage_Click);
            // 
            // Go
            // 
            this.Go.Image = ((System.Drawing.Image)(resources.GetObject("Go.Image")));
            this.Go.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Go.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(28, 28);
            this.Go.Click += new System.EventHandler(this.btn_Go_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // AddToTestingList
            // 
            this.AddToTestingList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddToTestingList.Image = ((System.Drawing.Image)(resources.GetObject("AddToTestingList.Image")));
            this.AddToTestingList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddToTestingList.Name = "AddToTestingList";
            this.AddToTestingList.Size = new System.Drawing.Size(28, 28);
            this.AddToTestingList.Tag = "";
            this.AddToTestingList.Text = "添加当前页代理到验证窗口";
            this.AddToTestingList.Click += new System.EventHandler(this.tsbAddToTest_Click);
            // 
            // Setting
            // 
            this.Setting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Setting.Image = ((System.Drawing.Image)(resources.GetObject("Setting.Image")));
            this.Setting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Setting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Setting.Name = "Setting";
            this.Setting.Size = new System.Drawing.Size(28, 28);
            this.Setting.Text = "设置";
            this.Setting.Click += new System.EventHandler(this.tsbSetting_Click);
            // 
            // wbMain
            // 
            this.wbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbMain.Location = new System.Drawing.Point(0, 0);
            this.wbMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbMain.Name = "wbMain";
            this.wbMain.ProxyServer = null;
            this.wbMain.Size = new System.Drawing.Size(470, 409);
            this.wbMain.TabIndex = 12;
            this.wbMain.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2" +
                ".0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C;" +
                " .NET4.0E)";
            this.wbMain.AfterProxyChanged += new Loamen.WinControls.UI.WebBrowserEx.OnProxyChanged(this.wbMain_AfterProxyChanged);
            this.wbMain.NavigateError += new Loamen.WinControls.UI.WebBrowserEx.WebBrowserNavigateErrorEventHandler(this.wbMain_NavigateError);
            this.wbMain.BeforeNewWindow += new System.EventHandler(this.wbMain_BeforeNewWindow);
            this.wbMain.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wbMain_Navigated);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvFavirate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wbMain);
            this.splitContainer1.Size = new System.Drawing.Size(566, 409);
            this.splitContainer1.SplitterDistance = 92;
            this.splitContainer1.TabIndex = 13;
            // 
            // tvFavirate
            // 
            this.tvFavirate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFavirate.ImageIndex = 0;
            this.tvFavirate.ImageList = this.imageList1;
            this.tvFavirate.Location = new System.Drawing.Point(0, 0);
            this.tvFavirate.Name = "tvFavirate";
            this.tvFavirate.SelectedImageKey = "folder.png";
            this.tvFavirate.Size = new System.Drawing.Size(92, 409);
            this.tvFavirate.TabIndex = 0;
            this.tvFavirate.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFavirate_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "page.png");
            this.imageList1.Images.SetKeyName(2, "page2.png");
            // 
            // IEBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 440);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.fileBrowserToolStrip);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IEBrowserForm";
            this.ShowInTaskbar = false;
            this.Text = "Loamen Browser";
            this.fileBrowserToolStrip.ResumeLayout(false);
            this.fileBrowserToolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Loamen.WinControls.UI.ToolStripSpringTextBox txtUrl;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator;
        private System.Windows.Forms.ToolStripButton Forward;
        private System.Windows.Forms.ToolStripButton Back;
        private System.Windows.Forms.ToolStrip fileBrowserToolStrip;
        private System.Windows.Forms.ToolStripButton Go;
        private System.Windows.Forms.ToolStripButton HomePage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Stop;
        private System.Windows.Forms.ToolStripButton Fresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton Setting;
        private System.Windows.Forms.ToolStripButton LockAndRefresh;
        private System.Windows.Forms.ToolStripButton AddToTestingList;
        private Loamen.WinControls.UI.WebBrowserEx wbMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvFavirate;
        private System.Windows.Forms.ToolStripButton tsbFavorites;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ImageList imageList1;
    }
}