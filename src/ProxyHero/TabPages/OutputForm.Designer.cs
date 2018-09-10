namespace ProxyHero.TabPages
{
    partial class OutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.spInfo = new System.Windows.Forms.SplitContainer();
            this.InfoBox = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.txtClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.ThreadsInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridThreads = new System.Windows.Forms.DataGridView();
            this.ThreadName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThreadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAlive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TestedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spInfo)).BeginInit();
            this.spInfo.Panel1.SuspendLayout();
            this.spInfo.Panel2.SuspendLayout();
            this.spInfo.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.spInfo);
            this.splitContainer1.Size = new System.Drawing.Size(599, 376);
            this.splitContainer1.SplitterDistance = 422;
            this.splitContainer1.TabIndex = 0;
            // 
            // spInfo
            // 
            this.spInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spInfo.Location = new System.Drawing.Point(0, 0);
            this.spInfo.Name = "spInfo";
            this.spInfo.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spInfo.Panel1
            // 
            this.spInfo.Panel1.Controls.Add(this.InfoBox);
            // 
            // spInfo.Panel2
            // 
            this.spInfo.Panel2.Controls.Add(this.dataGridThreads);
            this.spInfo.Size = new System.Drawing.Size(422, 376);
            this.spInfo.SplitterDistance = 202;
            this.spInfo.TabIndex = 2;
            // 
            // InfoBox
            // 
            this.InfoBox.ContextMenuStrip = this.contextMenuStrip2;
            this.InfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoBox.Location = new System.Drawing.Point(0, 0);
            this.InfoBox.Name = "InfoBox";
            this.InfoBox.Size = new System.Drawing.Size(422, 202);
            this.InfoBox.TabIndex = 1;
            this.InfoBox.Text = "";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtCopy,
            this.txtClear,
            this.toolStripMenuItem3,
            this.ThreadsInfo});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(153, 98);
            // 
            // txtCopy
            // 
            this.txtCopy.Name = "txtCopy";
            this.txtCopy.Size = new System.Drawing.Size(152, 22);
            this.txtCopy.Text = "复制";
            this.txtCopy.Click += new System.EventHandler(this.txtCopy_Click);
            // 
            // txtClear
            // 
            this.txtClear.Name = "txtClear";
            this.txtClear.Size = new System.Drawing.Size(152, 22);
            this.txtClear.Text = "清空";
            this.txtClear.Click += new System.EventHandler(this.txtClear_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // ThreadsInfo
            // 
            this.ThreadsInfo.Name = "ThreadsInfo";
            this.ThreadsInfo.Size = new System.Drawing.Size(152, 22);
            this.ThreadsInfo.Text = "验证线程信息";
            this.ThreadsInfo.Click += new System.EventHandler(this.ThreadsInfo_Click);
            // 
            // dataGridThreads
            // 
            this.dataGridThreads.AllowUserToAddRows = false;
            this.dataGridThreads.AllowUserToDeleteRows = false;
            this.dataGridThreads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridThreads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridThreads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ThreadName,
            this.Status,
            this.ThreadState,
            this.IsAlive,
            this.TestedCount,
            this.Description});
            this.dataGridThreads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridThreads.Location = new System.Drawing.Point(0, 0);
            this.dataGridThreads.Name = "dataGridThreads";
            this.dataGridThreads.ReadOnly = true;
            this.dataGridThreads.RowHeadersVisible = false;
            this.dataGridThreads.RowTemplate.Height = 23;
            this.dataGridThreads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridThreads.Size = new System.Drawing.Size(422, 170);
            this.dataGridThreads.TabIndex = 0;
            // 
            // ThreadName
            // 
            this.ThreadName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ThreadName.DataPropertyName = "Name";
            this.ThreadName.HeaderText = "Name";
            this.ThreadName.Name = "ThreadName";
            this.ThreadName.ReadOnly = true;
            this.ThreadName.Width = 60;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 60;
            // 
            // ThreadState
            // 
            this.ThreadState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ThreadState.DataPropertyName = "ThreadState";
            this.ThreadState.HeaderText = "ThreadState";
            this.ThreadState.Name = "ThreadState";
            this.ThreadState.ReadOnly = true;
            this.ThreadState.Width = 60;
            // 
            // IsAlive
            // 
            this.IsAlive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsAlive.DataPropertyName = "IsAlive";
            this.IsAlive.HeaderText = "IsAlive";
            this.IsAlive.Name = "IsAlive";
            this.IsAlive.ReadOnly = true;
            this.IsAlive.Width = 59;
            // 
            // TestedCount
            // 
            this.TestedCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestedCount.DataPropertyName = "TestedCount";
            this.TestedCount.HeaderText = "TestedCount";
            this.TestedCount.Name = "TestedCount";
            this.TestedCount.ReadOnly = true;
            this.TestedCount.Width = 60;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.DarkOrange;
            // 
            // InfomationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 376);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InfomationForm";
            this.Text = "输出";
            this.Load += new System.EventHandler(this.InfomationForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.spInfo.Panel1.ResumeLayout(false);
            this.spInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spInfo)).EndInit();
            this.spInfo.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridThreads)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox InfoBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem txtCopy;
        private System.Windows.Forms.ToolStripMenuItem txtClear;
        private System.Windows.Forms.SplitContainer spInfo;
        private System.Windows.Forms.DataGridView dataGridThreads;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ThreadsInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThreadName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThreadState;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAlive;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestedCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;

    }
}