namespace ProxyHero.Option.Panels
{
    partial class SystemTestPanel
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
            this.ResetAllSetting = new System.Windows.Forms.Button();
            this.ClickForSystemTesting = new System.Windows.Forms.Button();
            this.rbSystemInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ResetAllSetting
            // 
            this.ResetAllSetting.Location = new System.Drawing.Point(153, 5);
            this.ResetAllSetting.Name = "ResetAllSetting";
            this.ResetAllSetting.Size = new System.Drawing.Size(150, 23);
            this.ResetAllSetting.TabIndex = 8;
            this.ResetAllSetting.Text = "初始化所有设置";
            this.ResetAllSetting.UseVisualStyleBackColor = true;
            this.ResetAllSetting.Click += new System.EventHandler(this.ResetAllSetting_Click);
            // 
            // ClickForSystemTesting
            // 
            this.ClickForSystemTesting.Location = new System.Drawing.Point(5, 5);
            this.ClickForSystemTesting.Name = "ClickForSystemTesting";
            this.ClickForSystemTesting.Size = new System.Drawing.Size(141, 23);
            this.ClickForSystemTesting.TabIndex = 7;
            this.ClickForSystemTesting.Text = "点击进行系统检测";
            this.ClickForSystemTesting.UseVisualStyleBackColor = true;
            this.ClickForSystemTesting.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // rbSystemInfo
            // 
            this.rbSystemInfo.Location = new System.Drawing.Point(5, 34);
            this.rbSystemInfo.Name = "rbSystemInfo";
            this.rbSystemInfo.Size = new System.Drawing.Size(382, 254);
            this.rbSystemInfo.TabIndex = 6;
            this.rbSystemInfo.Text = "";
            // 
            // SystemTestPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "选项\\系统检测";
            this.Controls.Add(this.ResetAllSetting);
            this.Controls.Add(this.ClickForSystemTesting);
            this.Controls.Add(this.rbSystemInfo);
            this.DisplayName = "系统检测";
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "SystemTestPanel";
            this.Size = new System.Drawing.Size(402, 302);
            this.Load += new System.EventHandler(this.SystemTestPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ResetAllSetting;
        private System.Windows.Forms.Button ClickForSystemTesting;
        private System.Windows.Forms.RichTextBox rbSystemInfo;
    }
}
