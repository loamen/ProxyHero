namespace ProxyHero.Option.Panels
{
    partial class TestPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBrowseCzIpDb = new System.Windows.Forms.Button();
            this.txtCzIpDbFileName = new System.Windows.Forms.TextBox();
            this.CzIpDbFileName = new System.Windows.Forms.Label();
            this.Country = new System.Windows.Forms.CheckBox();
            this.TestThreadsCount = new System.Windows.Forms.Label();
            this.nudThreadsCount = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.TestTimeout = new System.Windows.Forms.Label();
            this.nudTestOutTime = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbbChoose = new System.Windows.Forms.ComboBox();
            this.DefaultTestWebsite = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEncoding = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.TestWebsiteEncode = new System.Windows.Forms.Label();
            this.TestWebsiteTitle = new System.Windows.Forms.Label();
            this.TestWebsite = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestOutTime)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnBrowseCzIpDb);
            this.groupBox2.Controls.Add(this.txtCzIpDbFileName);
            this.groupBox2.Controls.Add(this.CzIpDbFileName);
            this.groupBox2.Controls.Add(this.Country);
            this.groupBox2.Controls.Add(this.TestThreadsCount);
            this.groupBox2.Controls.Add(this.nudThreadsCount);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.TestTimeout);
            this.groupBox2.Controls.Add(this.nudTestOutTime);
            this.groupBox2.Location = new System.Drawing.Point(7, 236);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 144);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnBrowseCzIpDb
            // 
            this.btnBrowseCzIpDb.Location = new System.Drawing.Point(206, 107);
            this.btnBrowseCzIpDb.Name = "btnBrowseCzIpDb";
            this.btnBrowseCzIpDb.Size = new System.Drawing.Size(55, 23);
            this.btnBrowseCzIpDb.TabIndex = 17;
            this.btnBrowseCzIpDb.Text = "...";
            this.btnBrowseCzIpDb.UseVisualStyleBackColor = true;
            this.btnBrowseCzIpDb.Click += new System.EventHandler(this.btnBrowseCzIpDb_Click);
            // 
            // txtCzIpDbFileName
            // 
            this.txtCzIpDbFileName.BackColor = System.Drawing.Color.White;
            this.txtCzIpDbFileName.Location = new System.Drawing.Point(11, 108);
            this.txtCzIpDbFileName.Name = "txtCzIpDbFileName";
            this.txtCzIpDbFileName.ReadOnly = true;
            this.txtCzIpDbFileName.Size = new System.Drawing.Size(193, 21);
            this.txtCzIpDbFileName.TabIndex = 16;
            // 
            // CzIpDbFileName
            // 
            this.CzIpDbFileName.AutoSize = true;
            this.CzIpDbFileName.Location = new System.Drawing.Point(10, 80);
            this.CzIpDbFileName.Name = "CzIpDbFileName";
            this.CzIpDbFileName.Size = new System.Drawing.Size(113, 12);
            this.CzIpDbFileName.TabIndex = 15;
            this.CzIpDbFileName.Text = "纯真IP数据库地址：";
            // 
            // Country
            // 
            this.Country.AutoSize = true;
            this.Country.Location = new System.Drawing.Point(10, 48);
            this.Country.Name = "Country";
            this.Country.Size = new System.Drawing.Size(96, 16);
            this.Country.TabIndex = 13;
            this.Country.Text = "验证地理位置";
            this.Country.UseVisualStyleBackColor = true;
            // 
            // TestThreadsCount
            // 
            this.TestThreadsCount.AutoSize = true;
            this.TestThreadsCount.Location = new System.Drawing.Point(184, 20);
            this.TestThreadsCount.Name = "TestThreadsCount";
            this.TestThreadsCount.Size = new System.Drawing.Size(77, 12);
            this.TestThreadsCount.TabIndex = 11;
            this.TestThreadsCount.Text = "验证线程数：";
            // 
            // nudThreadsCount
            // 
            this.nudThreadsCount.Location = new System.Drawing.Point(305, 16);
            this.nudThreadsCount.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudThreadsCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThreadsCount.Name = "nudThreadsCount";
            this.nudThreadsCount.Size = new System.Drawing.Size(52, 21);
            this.nudThreadsCount.TabIndex = 10;
            this.nudThreadsCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(164, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "s";
            // 
            // TestTimeout
            // 
            this.TestTimeout.AutoSize = true;
            this.TestTimeout.Location = new System.Drawing.Point(10, 20);
            this.TestTimeout.Name = "TestTimeout";
            this.TestTimeout.Size = new System.Drawing.Size(89, 12);
            this.TestTimeout.TabIndex = 8;
            this.TestTimeout.Text = "验证超时时间：";
            // 
            // nudTestOutTime
            // 
            this.nudTestOutTime.Location = new System.Drawing.Point(103, 16);
            this.nudTestOutTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudTestOutTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTestOutTime.Name = "nudTestOutTime";
            this.nudTestOutTime.Size = new System.Drawing.Size(52, 21);
            this.nudTestOutTime.TabIndex = 7;
            this.nudTestOutTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbbChoose);
            this.groupBox1.Controls.Add(this.DefaultTestWebsite);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtEncoding);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.TestWebsiteEncode);
            this.groupBox1.Controls.Add(this.TestWebsiteTitle);
            this.groupBox1.Controls.Add(this.TestWebsite);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 224);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cbbChoose
            // 
            this.cbbChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbChoose.FormattingEnabled = true;
            this.cbbChoose.Location = new System.Drawing.Point(15, 37);
            this.cbbChoose.Name = "cbbChoose";
            this.cbbChoose.Size = new System.Drawing.Size(208, 20);
            this.cbbChoose.TabIndex = 22;
            this.cbbChoose.SelectedIndexChanged += new System.EventHandler(this.cbbChoose_SelectedIndexChanged);
            // 
            // DefaultTestWebsite
            // 
            this.DefaultTestWebsite.AutoSize = true;
            this.DefaultTestWebsite.Location = new System.Drawing.Point(15, 16);
            this.DefaultTestWebsite.Name = "DefaultTestWebsite";
            this.DefaultTestWebsite.Size = new System.Drawing.Size(65, 12);
            this.DefaultTestWebsite.TabIndex = 21;
            this.DefaultTestWebsite.Text = "默认网址：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 18;
            // 
            // txtEncoding
            // 
            this.txtEncoding.Location = new System.Drawing.Point(15, 189);
            this.txtEncoding.Name = "txtEncoding";
            this.txtEncoding.Size = new System.Drawing.Size(208, 21);
            this.txtEncoding.TabIndex = 17;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(15, 138);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(208, 21);
            this.txtTitle.TabIndex = 16;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(15, 87);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(208, 21);
            this.txtUrl.TabIndex = 15;
            this.txtUrl.Text = "http://www.";
            // 
            // TestWebsiteEncode
            // 
            this.TestWebsiteEncode.AutoSize = true;
            this.TestWebsiteEncode.Location = new System.Drawing.Point(15, 168);
            this.TestWebsiteEncode.Name = "TestWebsiteEncode";
            this.TestWebsiteEncode.Size = new System.Drawing.Size(65, 12);
            this.TestWebsiteEncode.TabIndex = 14;
            this.TestWebsiteEncode.Text = "网页编码：";
            // 
            // TestWebsiteTitle
            // 
            this.TestWebsiteTitle.AutoSize = true;
            this.TestWebsiteTitle.Location = new System.Drawing.Point(15, 117);
            this.TestWebsiteTitle.Name = "TestWebsiteTitle";
            this.TestWebsiteTitle.Size = new System.Drawing.Size(65, 12);
            this.TestWebsiteTitle.TabIndex = 13;
            this.TestWebsiteTitle.Text = "网站标题：";
            // 
            // TestWebsite
            // 
            this.TestWebsite.AutoSize = true;
            this.TestWebsite.Location = new System.Drawing.Point(15, 66);
            this.TestWebsite.Name = "TestWebsite";
            this.TestWebsite.Size = new System.Drawing.Size(65, 12);
            this.TestWebsite.TabIndex = 12;
            this.TestWebsite.Text = "验证网址：";
            // 
            // TestPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "选项\\验证选项";
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DisplayName = "验证选项";
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "TestPanel";
            this.Size = new System.Drawing.Size(402, 394);
            this.Load += new System.EventHandler(this.TestOptionPanel_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestOutTime)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBrowseCzIpDb;
        private System.Windows.Forms.TextBox txtCzIpDbFileName;
        private System.Windows.Forms.Label CzIpDbFileName;
        private System.Windows.Forms.CheckBox Country;
        private System.Windows.Forms.Label TestThreadsCount;
        private System.Windows.Forms.NumericUpDown nudThreadsCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label TestTimeout;
        private System.Windows.Forms.NumericUpDown nudTestOutTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbbChoose;
        private System.Windows.Forms.Label DefaultTestWebsite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEncoding;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label TestWebsiteEncode;
        private System.Windows.Forms.Label TestWebsiteTitle;
        private System.Windows.Forms.Label TestWebsite;

    }
}
