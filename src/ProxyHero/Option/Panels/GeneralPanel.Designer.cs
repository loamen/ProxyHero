namespace ProxyHero.Option.Panels
{
    partial class GeneralPanel
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ExportTxtFormat = new System.Windows.Forms.Label();
            this.cbbExportMode = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.AutoSwitchingProxyMaxDelay = new System.Windows.Forms.Label();
            this.nupAutoProxySpeed = new System.Windows.Forms.NumericUpDown();
            this.AutoSwitchingInterval = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudChangeProxyInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UseSystemProxySetting = new System.Windows.Forms.CheckBox();
            this.BuiltinBrowserScriptErrorsSuppressed = new System.Windows.Forms.CheckBox();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupAutoProxySpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeProxyInterval)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.ExportTxtFormat);
            this.groupBox5.Controls.Add(this.cbbExportMode);
            this.groupBox5.Location = new System.Drawing.Point(6, 247);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(390, 49);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // ExportTxtFormat
            // 
            this.ExportTxtFormat.AutoSize = true;
            this.ExportTxtFormat.Location = new System.Drawing.Point(10, 21);
            this.ExportTxtFormat.Name = "ExportTxtFormat";
            this.ExportTxtFormat.Size = new System.Drawing.Size(107, 12);
            this.ExportTxtFormat.TabIndex = 1;
            this.ExportTxtFormat.Text = "导出TXT文件格式：";
            // 
            // cbbExportMode
            // 
            this.cbbExportMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbExportMode.FormattingEnabled = true;
            this.cbbExportMode.Items.AddRange(new object[] {
            "标准格式",
            "公布器格式"});
            this.cbbExportMode.Location = new System.Drawing.Point(165, 18);
            this.cbbExportMode.Name = "cbbExportMode";
            this.cbbExportMode.Size = new System.Drawing.Size(99, 20);
            this.cbbExportMode.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.AutoSwitchingProxyMaxDelay);
            this.groupBox4.Controls.Add(this.nupAutoProxySpeed);
            this.groupBox4.Controls.Add(this.AutoSwitchingInterval);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.nudChangeProxyInterval);
            this.groupBox4.Location = new System.Drawing.Point(6, 86);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(390, 157);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(70, 115);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 6;
            this.label13.Text = "s";
            // 
            // AutoSwitchingProxyMaxDelay
            // 
            this.AutoSwitchingProxyMaxDelay.AutoSize = true;
            this.AutoSwitchingProxyMaxDelay.Location = new System.Drawing.Point(10, 85);
            this.AutoSwitchingProxyMaxDelay.Name = "AutoSwitchingProxyMaxDelay";
            this.AutoSwitchingProxyMaxDelay.Size = new System.Drawing.Size(125, 12);
            this.AutoSwitchingProxyMaxDelay.TabIndex = 5;
            this.AutoSwitchingProxyMaxDelay.Text = "自动代理最大延迟小于";
            // 
            // nupAutoProxySpeed
            // 
            this.nupAutoProxySpeed.Location = new System.Drawing.Point(10, 111);
            this.nupAutoProxySpeed.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nupAutoProxySpeed.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nupAutoProxySpeed.Name = "nupAutoProxySpeed";
            this.nupAutoProxySpeed.Size = new System.Drawing.Size(52, 21);
            this.nupAutoProxySpeed.TabIndex = 4;
            this.nupAutoProxySpeed.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // AutoSwitchingInterval
            // 
            this.AutoSwitchingInterval.AutoSize = true;
            this.AutoSwitchingInterval.Location = new System.Drawing.Point(10, 24);
            this.AutoSwitchingInterval.Name = "AutoSwitchingInterval";
            this.AutoSwitchingInterval.Size = new System.Drawing.Size(125, 12);
            this.AutoSwitchingInterval.TabIndex = 2;
            this.AutoSwitchingInterval.Text = "自动切换代理时间间隔";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(70, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "s";
            // 
            // nudChangeProxyInterval
            // 
            this.nudChangeProxyInterval.Location = new System.Drawing.Point(10, 50);
            this.nudChangeProxyInterval.Maximum = new decimal(new int[] {
            864,
            0,
            0,
            0});
            this.nudChangeProxyInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudChangeProxyInterval.Name = "nudChangeProxyInterval";
            this.nudChangeProxyInterval.Size = new System.Drawing.Size(52, 21);
            this.nudChangeProxyInterval.TabIndex = 1;
            this.nudChangeProxyInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.UseSystemProxySetting);
            this.groupBox3.Controls.Add(this.BuiltinBrowserScriptErrorsSuppressed);
            this.groupBox3.Location = new System.Drawing.Point(6, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 73);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // UseSystemProxySetting
            // 
            this.UseSystemProxySetting.AutoSize = true;
            this.UseSystemProxySetting.Location = new System.Drawing.Point(10, 42);
            this.UseSystemProxySetting.Name = "UseSystemProxySetting";
            this.UseSystemProxySetting.Size = new System.Drawing.Size(168, 16);
            this.UseSystemProxySetting.TabIndex = 8;
            this.UseSystemProxySetting.Text = "使用系统代理设置访问网络";
            this.UseSystemProxySetting.UseVisualStyleBackColor = true;
            // 
            // BuiltinBrowserScriptErrorsSuppressed
            // 
            this.BuiltinBrowserScriptErrorsSuppressed.AutoSize = true;
            this.BuiltinBrowserScriptErrorsSuppressed.Location = new System.Drawing.Point(10, 20);
            this.BuiltinBrowserScriptErrorsSuppressed.Name = "BuiltinBrowserScriptErrorsSuppressed";
            this.BuiltinBrowserScriptErrorsSuppressed.Size = new System.Drawing.Size(180, 16);
            this.BuiltinBrowserScriptErrorsSuppressed.TabIndex = 4;
            this.BuiltinBrowserScriptErrorsSuppressed.Text = "禁止显示内置浏览器脚本错误";
            this.BuiltinBrowserScriptErrorsSuppressed.UseVisualStyleBackColor = true;
            // 
            // GeneralPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "选项\\基本选项";
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.DisplayName = "基本选项";
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "GeneralPanel";
            this.Size = new System.Drawing.Size(402, 302);
            this.Load += new System.EventHandler(this.GeneralPanel_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupAutoProxySpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeProxyInterval)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label ExportTxtFormat;
        private System.Windows.Forms.ComboBox cbbExportMode;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label AutoSwitchingProxyMaxDelay;
        private System.Windows.Forms.NumericUpDown nupAutoProxySpeed;
        private System.Windows.Forms.Label AutoSwitchingInterval;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudChangeProxyInterval;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox UseSystemProxySetting;
        private System.Windows.Forms.CheckBox BuiltinBrowserScriptErrorsSuppressed;

    }
}
