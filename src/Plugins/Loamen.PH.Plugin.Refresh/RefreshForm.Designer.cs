namespace Loamen.PH.Plugin.Refresh
{
    partial class RefreshForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbUseProxy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbWebBorwser = new System.Windows.Forms.RadioButton();
            this.rbQuickly = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudSleepInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbPageList = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ThreadName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThreadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuccessCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FailedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleepInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(694, 438);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(688, 201);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cbUseProxy);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.rbWebBorwser);
            this.groupBox3.Controls.Add(this.rbQuickly);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.nudTimeout);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.nudSleepInterval);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(346, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(252, 174);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数";
            // 
            // cbUseProxy
            // 
            this.cbUseProxy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUseProxy.FormattingEnabled = true;
            this.cbUseProxy.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cbUseProxy.Location = new System.Drawing.Point(104, 137);
            this.cbUseProxy.Name = "cbUseProxy";
            this.cbUseProxy.Size = new System.Drawing.Size(121, 20);
            this.cbUseProxy.TabIndex = 8;
            this.toolTip1.SetToolTip(this.cbUseProxy, "此设置对快速模式无效");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "是否使用代理：";
            // 
            // rbWebBorwser
            // 
            this.rbWebBorwser.AutoSize = true;
            this.rbWebBorwser.Location = new System.Drawing.Point(111, 105);
            this.rbWebBorwser.Name = "rbWebBorwser";
            this.rbWebBorwser.Size = new System.Drawing.Size(83, 16);
            this.rbWebBorwser.TabIndex = 6;
            this.rbWebBorwser.TabStop = true;
            this.rbWebBorwser.Text = "模拟浏览器";
            this.toolTip1.SetToolTip(this.rbWebBorwser, "刷新速度较慢，可用于刷任何网页的PV和IP。");
            this.rbWebBorwser.UseVisualStyleBackColor = true;
            this.rbWebBorwser.CheckedChanged += new System.EventHandler(this.rbWebBorwser_CheckedChanged);
            // 
            // rbQuickly
            // 
            this.rbQuickly.AutoSize = true;
            this.rbQuickly.Checked = true;
            this.rbQuickly.Location = new System.Drawing.Point(9, 105);
            this.rbQuickly.Name = "rbQuickly";
            this.rbQuickly.Size = new System.Drawing.Size(71, 16);
            this.rbQuickly.TabIndex = 5;
            this.rbQuickly.TabStop = true;
            this.rbQuickly.Text = "快速模式";
            this.toolTip1.SetToolTip(this.rbQuickly, "刷新速度较快。可用于刷淘宝、拍拍的浏览量。对使用JS的统计代码无效。比如：百度统计、站长统计");
            this.rbQuickly.UseVisualStyleBackColor = true;
            this.rbQuickly.CheckedChanged += new System.EventHandler(this.rbQuickly_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "刷新方式：";
            // 
            // nudTimeout
            // 
            this.nudTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTimeout.Location = new System.Drawing.Point(103, 48);
            this.nudTimeout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(120, 21);
            this.nudTimeout.TabIndex = 3;
            this.nudTimeout.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "刷新超时时间：";
            // 
            // nudSleepInterval
            // 
            this.nudSleepInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSleepInterval.Location = new System.Drawing.Point(103, 17);
            this.nudSleepInterval.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudSleepInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSleepInterval.Name = "nudSleepInterval";
            this.nudSleepInterval.Size = new System.Drawing.Size(120, 21);
            this.nudSleepInterval.TabIndex = 1;
            this.nudSleepInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "暂停时间间隔：";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(605, 56);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止刷新";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(605, 27);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "开始刷新";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rtbPageList);
            this.groupBox2.Location = new System.Drawing.Point(10, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 174);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "网页列表，每一行一个网址";
            // 
            // rtbPageList
            // 
            this.rtbPageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbPageList.Location = new System.Drawing.Point(3, 17);
            this.rtbPageList.Name = "rtbPageList";
            this.rtbPageList.Size = new System.Drawing.Size(323, 154);
            this.rtbPageList.TabIndex = 0;
            this.rtbPageList.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ThreadName,
            this.Url,
            this.Status,
            this.ThreadState,
            this.ProxyCount,
            this.TotalCount,
            this.SuccessCount,
            this.FailedCount,
            this.Description});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(694, 222);
            this.dataGridView1.TabIndex = 0;
            // 
            // ThreadName
            // 
            this.ThreadName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ThreadName.DataPropertyName = "Name";
            this.ThreadName.HeaderText = "ID";
            this.ThreadName.Name = "ThreadName";
            this.ThreadName.ReadOnly = true;
            this.ThreadName.Width = 30;
            // 
            // Url
            // 
            this.Url.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Url.DataPropertyName = "Url";
            this.Url.HeaderText = "网址";
            this.Url.Name = "Url";
            this.Url.ReadOnly = true;
            this.Url.Width = 120;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "状态";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 80;
            // 
            // ThreadState
            // 
            this.ThreadState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ThreadState.DataPropertyName = "ThreadState";
            this.ThreadState.HeaderText = "线程状态";
            this.ThreadState.Name = "ThreadState";
            this.ThreadState.ReadOnly = true;
            this.ThreadState.Width = 80;
            // 
            // ProxyCount
            // 
            this.ProxyCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyCount.DataPropertyName = "ProxyCount";
            this.ProxyCount.HeaderText = "代理总数";
            this.ProxyCount.Name = "ProxyCount";
            this.ProxyCount.ReadOnly = true;
            this.ProxyCount.Width = 80;
            // 
            // TotalCount
            // 
            this.TotalCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TotalCount.DataPropertyName = "TotalCount";
            this.TotalCount.HeaderText = "刷新总数";
            this.TotalCount.Name = "TotalCount";
            this.TotalCount.ReadOnly = true;
            this.TotalCount.Width = 80;
            // 
            // SuccessCount
            // 
            this.SuccessCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SuccessCount.DataPropertyName = "SuccessCount";
            this.SuccessCount.HeaderText = "成功数";
            this.SuccessCount.Name = "SuccessCount";
            this.SuccessCount.ReadOnly = true;
            this.SuccessCount.Width = 70;
            // 
            // FailedCount
            // 
            this.FailedCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FailedCount.DataPropertyName = "FailedCount";
            this.FailedCount.HeaderText = "失败数";
            this.FailedCount.Name = "FailedCount";
            this.FailedCount.ReadOnly = true;
            this.FailedCount.Width = 70;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.Gold;
            // 
            // RefreshForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 438);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "RefreshForm";
            this.ShowIcon = false;
            this.Text = "喜刷刷";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RefreshForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleepInterval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbPageList;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudSleepInterval;
        private System.Windows.Forms.NumericUpDown nudTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThreadName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Url;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThreadState;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SuccessCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn FailedCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbWebBorwser;
        private System.Windows.Forms.RadioButton rbQuickly;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbUseProxy;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}