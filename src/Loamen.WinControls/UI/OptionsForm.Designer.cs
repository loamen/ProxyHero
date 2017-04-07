namespace Loamen.WinControls.UI
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.OptionsFormSplit = new System.Windows.Forms.SplitContainer();
            this.CatDescrPanel = new System.Windows.Forms.Panel();
            this.CatDescr = new System.Windows.Forms.Label();
            this.CatTreePanel = new System.Windows.Forms.Panel();
            this.CatTree = new System.Windows.Forms.TreeView();
            this.CatHeaderPanel = new System.Windows.Forms.Panel();
            this.CatHeader = new System.Windows.Forms.Label();
            this.OptionsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.OptionPanelContainer = new System.Windows.Forms.Panel();
            this.groupBoxOptionDescr = new System.Windows.Forms.GroupBox();
            this.OptDescrSplit = new System.Windows.Forms.SplitContainer();
            this.AppRestartLabel = new System.Windows.Forms.Label();
            this.OptionDescrLabel = new System.Windows.Forms.Label();
            this.OptionsPanelPath = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsFormSplit)).BeginInit();
            this.OptionsFormSplit.Panel1.SuspendLayout();
            this.OptionsFormSplit.Panel2.SuspendLayout();
            this.OptionsFormSplit.SuspendLayout();
            this.CatDescrPanel.SuspendLayout();
            this.CatTreePanel.SuspendLayout();
            this.CatHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsSplitContainer)).BeginInit();
            this.OptionsSplitContainer.Panel1.SuspendLayout();
            this.OptionsSplitContainer.Panel2.SuspendLayout();
            this.OptionsSplitContainer.SuspendLayout();
            this.groupBoxOptionDescr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptDescrSplit)).BeginInit();
            this.OptDescrSplit.Panel1.SuspendLayout();
            this.OptDescrSplit.Panel2.SuspendLayout();
            this.OptDescrSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // OptionsFormSplit
            // 
            this.OptionsFormSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionsFormSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.OptionsFormSplit.IsSplitterFixed = true;
            this.OptionsFormSplit.Location = new System.Drawing.Point(0, 0);
            this.OptionsFormSplit.Margin = new System.Windows.Forms.Padding(0);
            this.OptionsFormSplit.MinimumSize = new System.Drawing.Size(480, 346);
            this.OptionsFormSplit.Name = "OptionsFormSplit";
            // 
            // OptionsFormSplit.Panel1
            // 
            this.OptionsFormSplit.Panel1.Controls.Add(this.CatDescrPanel);
            this.OptionsFormSplit.Panel1.Controls.Add(this.CatTreePanel);
            this.OptionsFormSplit.Panel1.Controls.Add(this.CatHeaderPanel);
            this.OptionsFormSplit.Panel1MinSize = 205;
            // 
            // OptionsFormSplit.Panel2
            // 
            this.OptionsFormSplit.Panel2.Controls.Add(this.OptionsSplitContainer);
            this.OptionsFormSplit.Panel2.Controls.Add(this.OptionsPanelPath);
            this.OptionsFormSplit.Panel2.Controls.Add(this.OKBtn);
            this.OptionsFormSplit.Panel2.Controls.Add(this.ApplyBtn);
            this.OptionsFormSplit.Panel2.Controls.Add(this.CancelBtn);
            this.OptionsFormSplit.Panel2.Controls.Add(this.label2);
            this.OptionsFormSplit.Panel2MinSize = 350;
            this.OptionsFormSplit.Size = new System.Drawing.Size(610, 383);
            this.OptionsFormSplit.SplitterDistance = 205;
            this.OptionsFormSplit.SplitterWidth = 5;
            this.OptionsFormSplit.TabIndex = 0;
            // 
            // CatDescrPanel
            // 
            this.CatDescrPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CatDescrPanel.Controls.Add(this.CatDescr);
            this.CatDescrPanel.Location = new System.Drawing.Point(4, 346);
            this.CatDescrPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CatDescrPanel.Name = "CatDescrPanel";
            this.CatDescrPanel.Size = new System.Drawing.Size(198, 34);
            this.CatDescrPanel.TabIndex = 2;
            this.CatDescrPanel.Visible = false;
            // 
            // CatDescr
            // 
            this.CatDescr.AccessibleDescription = "这里显示每个选项的说明。";
            this.CatDescr.AutoEllipsis = true;
            this.CatDescr.BackColor = System.Drawing.SystemColors.Control;
            this.CatDescr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CatDescr.Font = new System.Drawing.Font("宋体", 9F);
            this.CatDescr.Location = new System.Drawing.Point(0, 0);
            this.CatDescr.Name = "CatDescr";
            this.CatDescr.Size = new System.Drawing.Size(198, 34);
            this.CatDescr.TabIndex = 0;
            this.CatDescr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CatDescr.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.CatDescr.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // CatTreePanel
            // 
            this.CatTreePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CatTreePanel.Controls.Add(this.CatTree);
            this.CatTreePanel.Location = new System.Drawing.Point(3, 34);
            this.CatTreePanel.Margin = new System.Windows.Forms.Padding(0);
            this.CatTreePanel.Name = "CatTreePanel";
            this.CatTreePanel.Size = new System.Drawing.Size(199, 310);
            this.CatTreePanel.TabIndex = 1;
            // 
            // CatTree
            // 
            this.CatTree.AccessibleDescription = "选项类别";
            this.CatTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CatTree.Location = new System.Drawing.Point(0, 0);
            this.CatTree.Name = "CatTree";
            this.CatTree.ShowLines = false;
            this.CatTree.Size = new System.Drawing.Size(199, 310);
            this.CatTree.TabIndex = 0;
            this.CatTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CatTree_AfterSelect);
            this.CatTree.Enter += new System.EventHandler(this.MouseEnterDescr);
            this.CatTree.Leave += new System.EventHandler(this.MouseLeaveDescr);
            this.CatTree.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.CatTree.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // CatHeaderPanel
            // 
            this.CatHeaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CatHeaderPanel.Controls.Add(this.CatHeader);
            this.CatHeaderPanel.Location = new System.Drawing.Point(3, 3);
            this.CatHeaderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CatHeaderPanel.Name = "CatHeaderPanel";
            this.CatHeaderPanel.Size = new System.Drawing.Size(199, 30);
            this.CatHeaderPanel.TabIndex = 0;
            this.CatHeaderPanel.Visible = false;
            // 
            // CatHeader
            // 
            this.CatHeader.AccessibleDescription = "Categories tree Header.";
            this.CatHeader.AutoEllipsis = true;
            this.CatHeader.BackColor = System.Drawing.SystemColors.Control;
            this.CatHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CatHeader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CatHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CatHeader.Location = new System.Drawing.Point(0, 0);
            this.CatHeader.Name = "CatHeader";
            this.CatHeader.Size = new System.Drawing.Size(199, 30);
            this.CatHeader.TabIndex = 0;
            this.CatHeader.Text = "选项配置";
            this.CatHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CatHeader.Visible = false;
            this.CatHeader.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.CatHeader.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // OptionsSplitContainer
            // 
            this.OptionsSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsSplitContainer.Location = new System.Drawing.Point(7, 34);
            this.OptionsSplitContainer.Name = "OptionsSplitContainer";
            this.OptionsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // OptionsSplitContainer.Panel1
            // 
            this.OptionsSplitContainer.Panel1.Controls.Add(this.OptionPanelContainer);
            // 
            // OptionsSplitContainer.Panel2
            // 
            this.OptionsSplitContainer.Panel2.Controls.Add(this.groupBoxOptionDescr);
            this.OptionsSplitContainer.Size = new System.Drawing.Size(383, 309);
            this.OptionsSplitContainer.SplitterDistance = 234;
            this.OptionsSplitContainer.TabIndex = 9;
            // 
            // OptionPanelContainer
            // 
            this.OptionPanelContainer.AutoScroll = true;
            this.OptionPanelContainer.BackColor = System.Drawing.SystemColors.Control;
            this.OptionPanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionPanelContainer.Location = new System.Drawing.Point(0, 0);
            this.OptionPanelContainer.Margin = new System.Windows.Forms.Padding(0);
            this.OptionPanelContainer.MinimumSize = new System.Drawing.Size(340, 198);
            this.OptionPanelContainer.Name = "OptionPanelContainer";
            this.OptionPanelContainer.Size = new System.Drawing.Size(383, 234);
            this.OptionPanelContainer.TabIndex = 5;
            // 
            // groupBoxOptionDescr
            // 
            this.groupBoxOptionDescr.Controls.Add(this.OptDescrSplit);
            this.groupBoxOptionDescr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOptionDescr.Location = new System.Drawing.Point(0, 0);
            this.groupBoxOptionDescr.Name = "groupBoxOptionDescr";
            this.groupBoxOptionDescr.Size = new System.Drawing.Size(383, 71);
            this.groupBoxOptionDescr.TabIndex = 0;
            this.groupBoxOptionDescr.TabStop = false;
            this.groupBoxOptionDescr.Text = "说明";
            // 
            // OptDescrSplit
            // 
            this.OptDescrSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptDescrSplit.Location = new System.Drawing.Point(3, 17);
            this.OptDescrSplit.Name = "OptDescrSplit";
            // 
            // OptDescrSplit.Panel1
            // 
            this.OptDescrSplit.Panel1.Controls.Add(this.AppRestartLabel);
            this.OptDescrSplit.Panel1Collapsed = true;
            // 
            // OptDescrSplit.Panel2
            // 
            this.OptDescrSplit.Panel2.Controls.Add(this.OptionDescrLabel);
            this.OptDescrSplit.Size = new System.Drawing.Size(377, 51);
            this.OptDescrSplit.SplitterDistance = 125;
            this.OptDescrSplit.TabIndex = 0;
            // 
            // AppRestartLabel
            // 
            this.AppRestartLabel.AutoEllipsis = true;
            this.AppRestartLabel.BackColor = System.Drawing.SystemColors.Control;
            this.AppRestartLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AppRestartLabel.ForeColor = System.Drawing.Color.Red;
            this.AppRestartLabel.Location = new System.Drawing.Point(0, 0);
            this.AppRestartLabel.Name = "AppRestartLabel";
            this.AppRestartLabel.Size = new System.Drawing.Size(125, 100);
            this.AppRestartLabel.TabIndex = 1;
            this.AppRestartLabel.Text = "此操作需要重启软件.";
            this.AppRestartLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OptionDescrLabel
            // 
            this.OptionDescrLabel.AccessibleDescription = "这里显示每个操作的说明。";
            this.OptionDescrLabel.AutoEllipsis = true;
            this.OptionDescrLabel.BackColor = System.Drawing.SystemColors.Control;
            this.OptionDescrLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionDescrLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OptionDescrLabel.Location = new System.Drawing.Point(0, 0);
            this.OptionDescrLabel.Name = "OptionDescrLabel";
            this.OptionDescrLabel.Size = new System.Drawing.Size(377, 51);
            this.OptionDescrLabel.TabIndex = 0;
            this.OptionDescrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.OptionDescrLabel.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.OptionDescrLabel.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // OptionsPanelPath
            // 
            this.OptionsPanelPath.AccessibleDescription = "";
            this.OptionsPanelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsPanelPath.AutoEllipsis = true;
            this.OptionsPanelPath.BackColor = System.Drawing.SystemColors.Control;
            this.OptionsPanelPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OptionsPanelPath.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.OptionsPanelPath.Location = new System.Drawing.Point(3, 3);
            this.OptionsPanelPath.Name = "OptionsPanelPath";
            this.OptionsPanelPath.Padding = new System.Windows.Forms.Padding(10, 6, 0, 6);
            this.OptionsPanelPath.Size = new System.Drawing.Size(387, 30);
            this.OptionsPanelPath.TabIndex = 8;
            this.OptionsPanelPath.Text = "选项 >配置";
            this.OptionsPanelPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptionsPanelPath.Visible = false;
            this.OptionsPanelPath.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.OptionsPanelPath.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // OKBtn
            // 
            this.OKBtn.AccessibleDescription = "确定并关闭窗体。";
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(153, 353);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 26);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "(&O)确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            this.OKBtn.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.OKBtn.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.AccessibleDescription = "应用设置。";
            this.ApplyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyBtn.Enabled = false;
            this.ApplyBtn.Location = new System.Drawing.Point(234, 354);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(75, 25);
            this.ApplyBtn.TabIndex = 6;
            this.ApplyBtn.Text = "(&A)应用";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            this.ApplyBtn.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.ApplyBtn.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // CancelBtn
            // 
            this.CancelBtn.AccessibleDescription = "取消设置。";
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(315, 354);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 25);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "(&C)取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            this.CancelBtn.MouseEnter += new System.EventHandler(this.MouseEnterDescr);
            this.CancelBtn.MouseLeave += new System.EventHandler(this.MouseLeaveDescr);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(3, 346);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 2);
            this.label2.TabIndex = 0;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(610, 383);
            this.Controls.Add(this.OptionsFormSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(486, 378);
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选项";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.OptionsFormSplit.Panel1.ResumeLayout(false);
            this.OptionsFormSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionsFormSplit)).EndInit();
            this.OptionsFormSplit.ResumeLayout(false);
            this.CatDescrPanel.ResumeLayout(false);
            this.CatTreePanel.ResumeLayout(false);
            this.CatHeaderPanel.ResumeLayout(false);
            this.OptionsSplitContainer.Panel1.ResumeLayout(false);
            this.OptionsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionsSplitContainer)).EndInit();
            this.OptionsSplitContainer.ResumeLayout(false);
            this.groupBoxOptionDescr.ResumeLayout(false);
            this.OptDescrSplit.Panel1.ResumeLayout(false);
            this.OptDescrSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptDescrSplit)).EndInit();
            this.OptDescrSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer OptionsFormSplit;
        private System.Windows.Forms.Panel CatHeaderPanel;
        private System.Windows.Forms.Panel CatDescrPanel;
        private System.Windows.Forms.Panel CatTreePanel;
        private System.Windows.Forms.Label CatHeader;
        private System.Windows.Forms.Label CatDescr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label OptionDescrLabel;
        private System.Windows.Forms.Panel OptionPanelContainer;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.Label OptionsPanelPath;
        private System.Windows.Forms.TreeView CatTree;
        private System.Windows.Forms.SplitContainer OptionsSplitContainer;
        private System.Windows.Forms.GroupBox groupBoxOptionDescr;
        private System.Windows.Forms.Label AppRestartLabel;
        private System.Windows.Forms.SplitContainer OptDescrSplit;
    }
}