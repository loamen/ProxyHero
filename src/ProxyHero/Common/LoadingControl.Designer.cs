namespace ProxyHero.Common
{
    partial class LoadingControl
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
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.LoadingText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLoading
            // 
            this.pbLoading.Image = global::ProxyHero.Properties.Resources.loading;
            this.pbLoading.Location = new System.Drawing.Point(3, 3);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(18, 17);
            this.pbLoading.TabIndex = 2;
            this.pbLoading.TabStop = false;
            // 
            // LoadingText
            // 
            this.LoadingText.AutoSize = true;
            this.LoadingText.BackColor = System.Drawing.Color.Transparent;
            this.LoadingText.Location = new System.Drawing.Point(25, 6);
            this.LoadingText.Name = "LoadingText";
            this.LoadingText.Size = new System.Drawing.Size(65, 12);
            this.LoadingText.TabIndex = 3;
            this.LoadingText.Text = "Loading...";
            // 
            // LoadingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.LoadingText);
            this.Controls.Add(this.pbLoading);
            this.Name = "LoadingControl";
            this.Size = new System.Drawing.Size(94, 23);
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLoading;
        public System.Windows.Forms.Label LoadingText;
    }
}
