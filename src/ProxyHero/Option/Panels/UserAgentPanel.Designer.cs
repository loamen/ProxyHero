namespace ProxyHero.Option.Panels
{
    partial class UserAgentPanel
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rtbUserAgent = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbUserAgent = new System.Windows.Forms.ComboBox();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.rtbUserAgent);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.cbbUserAgent);
            this.groupBox7.Location = new System.Drawing.Point(5, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(392, 284);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "User-Agent";
            // 
            // rtbUserAgent
            // 
            this.rtbUserAgent.Location = new System.Drawing.Point(8, 60);
            this.rtbUserAgent.Name = "rtbUserAgent";
            this.rtbUserAgent.Size = new System.Drawing.Size(369, 218);
            this.rtbUserAgent.TabIndex = 2;
            this.rtbUserAgent.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "User-Agent:";
            // 
            // cbbUserAgent
            // 
            this.cbbUserAgent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbUserAgent.FormattingEnabled = true;
            this.cbbUserAgent.Location = new System.Drawing.Point(82, 29);
            this.cbbUserAgent.Name = "cbbUserAgent";
            this.cbbUserAgent.Size = new System.Drawing.Size(295, 20);
            this.cbbUserAgent.TabIndex = 0;
            this.cbbUserAgent.SelectedIndexChanged += new System.EventHandler(this.cbbUserAgent_SelectedIndexChanged);
            // 
            // UserAgentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CategoryPath = "选项\\User-Agent";
            this.Controls.Add(this.groupBox7);
            this.DisplayName = "User-Agent";
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UserAgentPanel";
            this.Size = new System.Drawing.Size(402, 302);
            this.Load += new System.EventHandler(this.UserAgentPanel_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RichTextBox rtbUserAgent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbUserAgent;
    }
}
