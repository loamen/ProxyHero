using Loamen.WinControls.UI;
namespace ProxyHero.TabPages
{
	partial class ProxyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProxyForm));
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.DownloadProxy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TestAll = new System.Windows.Forms.ToolStripButton();
            this.StopTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportProxy = new System.Windows.Forms.ToolStripButton();
            this.ExportProxy = new System.Windows.Forms.ToolStripButton();
            this.Delete = new System.Windows.Forms.ToolStripDropDownButton();
            this.DeleteDeadProxy = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSeleted = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Option = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.CountryLabel = new System.Windows.Forms.ToolStripLabel();
            this.tstxtArea = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.PortLabel = new System.Windows.Forms.ToolStripLabel();
            this.tstxtPort = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.AnonymityLabel = new System.Windows.Forms.ToolStripLabel();
            this.tstxtAnonymity = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.Search = new System.Windows.Forms.ToolStripButton();
            this.tsslCountInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dgvProxyList = new Loamen.WinControls.UI.DataGridViewEx(this.components);
            this.Status = new Loamen.WinControls.UI.DataGridViewDoneColumn();
            this.ProxyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PortColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnonymityCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnonymityEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountryEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Test = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.SwitchTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIE = new System.Windows.Forms.ToolStripMenuItem();
            this.BuiltinBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.DoNotUseProxy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteThis = new System.Windows.Forms.ToolStripMenuItem();
            this.Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Toolbar.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProxyList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Toolbar
            // 
            this.Toolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.Toolbar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DownloadProxy,
            this.toolStripSeparator1,
            this.TestAll,
            this.StopTest,
            this.toolStripSeparator2,
            this.ImportProxy,
            this.ExportProxy,
            this.Delete,
            this.toolStripSeparator3,
            this.Option,
            this.toolStripSeparator5,
            this.CountryLabel,
            this.tstxtArea,
            this.toolStripSeparator4,
            this.PortLabel,
            this.tstxtPort,
            this.toolStripSeparator6,
            this.AnonymityLabel,
            this.tstxtAnonymity,
            this.toolStripSeparator7,
            this.Search,
            this.tsslCountInfo});
            this.Toolbar.Location = new System.Drawing.Point(3, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new System.Drawing.Size(634, 39);
            this.Toolbar.TabIndex = 21;
            this.Toolbar.Text = "toolStrip1";
            // 
            // DownloadProxy
            // 
            this.DownloadProxy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DownloadProxy.Image = global::ProxyHero.Properties.Resources.read;
            this.DownloadProxy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DownloadProxy.Name = "DownloadProxy";
            this.DownloadProxy.Size = new System.Drawing.Size(36, 36);
            this.DownloadProxy.Text = "下载代理数据";
            this.DownloadProxy.Click += new System.EventHandler(this.DownloadProxy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // TestAll
            // 
            this.TestAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TestAll.Image = ((System.Drawing.Image)(resources.GetObject("TestAll.Image")));
            this.TestAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TestAll.Name = "TestAll";
            this.TestAll.Size = new System.Drawing.Size(36, 36);
            this.TestAll.Text = "验证全部";
            this.TestAll.Click += new System.EventHandler(this.TestAll_Click);
            // 
            // StopTest
            // 
            this.StopTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopTest.Enabled = false;
            this.StopTest.Image = ((System.Drawing.Image)(resources.GetObject("StopTest.Image")));
            this.StopTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopTest.Name = "StopTest";
            this.StopTest.Size = new System.Drawing.Size(36, 36);
            this.StopTest.Text = "停止验证";
            this.StopTest.Click += new System.EventHandler(this.StopTest_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // ImportProxy
            // 
            this.ImportProxy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ImportProxy.Image = ((System.Drawing.Image)(resources.GetObject("ImportProxy.Image")));
            this.ImportProxy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportProxy.Name = "ImportProxy";
            this.ImportProxy.Size = new System.Drawing.Size(36, 36);
            this.ImportProxy.Text = "导入代理";
            this.ImportProxy.Click += new System.EventHandler(this.ImportProxy_Click);
            // 
            // ExportProxy
            // 
            this.ExportProxy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExportProxy.Image = ((System.Drawing.Image)(resources.GetObject("ExportProxy.Image")));
            this.ExportProxy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportProxy.Name = "ExportProxy";
            this.ExportProxy.Size = new System.Drawing.Size(36, 36);
            this.ExportProxy.Text = "导出代理";
            this.ExportProxy.Click += new System.EventHandler(this.ExportProxy_Click);
            // 
            // Delete
            // 
            this.Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Delete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteDeadProxy,
            this.DeleteSeleted});
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(45, 36);
            this.Delete.Text = "删除";
            // 
            // DeleteDeadProxy
            // 
            this.DeleteDeadProxy.Name = "DeleteDeadProxy";
            this.DeleteDeadProxy.Size = new System.Drawing.Size(152, 22);
            this.DeleteDeadProxy.Text = "删除无效数据";
            this.DeleteDeadProxy.Click += new System.EventHandler(this.DeleteInvalidProxy_Click);
            // 
            // DeleteSeleted
            // 
            this.DeleteSeleted.Name = "DeleteSeleted";
            this.DeleteSeleted.Size = new System.Drawing.Size(152, 22);
            this.DeleteSeleted.Text = "删除选定";
            this.DeleteSeleted.Click += new System.EventHandler(this.DeleteSeleted_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // Option
            // 
            this.Option.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Option.Image = ((System.Drawing.Image)(resources.GetObject("Option.Image")));
            this.Option.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Option.Name = "Option";
            this.Option.Size = new System.Drawing.Size(36, 36);
            this.Option.Text = "选项";
            this.Option.Click += new System.EventHandler(this.Option_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // CountryLabel
            // 
            this.CountryLabel.Name = "CountryLabel";
            this.CountryLabel.Size = new System.Drawing.Size(29, 36);
            this.CountryLabel.Text = "地区";
            // 
            // tstxtArea
            // 
            this.tstxtArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstxtArea.Name = "tstxtArea";
            this.tstxtArea.Size = new System.Drawing.Size(50, 39);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // PortLabel
            // 
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 36);
            this.PortLabel.Text = "端口";
            // 
            // tstxtPort
            // 
            this.tstxtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstxtPort.MaxLength = 10;
            this.tstxtPort.Name = "tstxtPort";
            this.tstxtPort.Size = new System.Drawing.Size(30, 39);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // AnonymityLabel
            // 
            this.AnonymityLabel.Name = "AnonymityLabel";
            this.AnonymityLabel.Size = new System.Drawing.Size(41, 36);
            this.AnonymityLabel.Text = "匿名度";
            // 
            // tstxtAnonymity
            // 
            this.tstxtAnonymity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstxtAnonymity.Name = "tstxtAnonymity";
            this.tstxtAnonymity.Size = new System.Drawing.Size(40, 39);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 39);
            // 
            // Search
            // 
            this.Search.Image = ((System.Drawing.Image)(resources.GetObject("Search.Image")));
            this.Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(65, 36);
            this.Search.Text = "查找";
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // tsslCountInfo
            // 
            this.tsslCountInfo.Name = "tsslCountInfo";
            this.tsslCountInfo.Size = new System.Drawing.Size(0, 36);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dgvProxyList);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(727, 446);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(727, 485);
            this.toolStripContainer1.TabIndex = 22;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.Toolbar);
            // 
            // dgvProxyList
            // 
            this.dgvProxyList.AllowUserToAddRows = false;
            this.dgvProxyList.AllowUserToDeleteRows = false;
            this.dgvProxyList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProxyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProxyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.ProxyColumn,
            this.PortColumn,
            this.TypeColumn,
            this.DelayColumn,
            this.AnonymityCol,
            this.LocationColumn,
            this.AnonymityEn,
            this.CountryEn,
            this.DescriptionColumn});
            this.dgvProxyList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvProxyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProxyList.Location = new System.Drawing.Point(0, 0);
            this.dgvProxyList.Name = "dgvProxyList";
            this.dgvProxyList.ReadOnly = true;
            this.dgvProxyList.RowHeadersVisible = false;
            this.dgvProxyList.RowTemplate.Height = 23;
            this.dgvProxyList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProxyList.Size = new System.Drawing.Size(727, 446);
            this.dgvProxyList.TabIndex = 2;
            this.dgvProxyList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvProxyList_ColumnHeaderMouseClick);
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Status.DataPropertyName = "status";
            this.Status.FillWeight = 20F;
            this.Status.HeaderText = "";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Status.Width = 20;
            // 
            // ProxyColumn
            // 
            this.ProxyColumn.DataPropertyName = "proxy";
            this.ProxyColumn.FillWeight = 29.69543F;
            this.ProxyColumn.HeaderText = "Proxy";
            this.ProxyColumn.Name = "ProxyColumn";
            this.ProxyColumn.ReadOnly = true;
            // 
            // PortColumn
            // 
            this.PortColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PortColumn.DataPropertyName = "port";
            this.PortColumn.FillWeight = 60F;
            this.PortColumn.HeaderText = "Port";
            this.PortColumn.Name = "PortColumn";
            this.PortColumn.ReadOnly = true;
            this.PortColumn.Width = 60;
            // 
            // TypeColumn
            // 
            this.TypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TypeColumn.DataPropertyName = "type";
            this.TypeColumn.FillWeight = 60F;
            this.TypeColumn.HeaderText = "Type";
            this.TypeColumn.Name = "TypeColumn";
            this.TypeColumn.ReadOnly = true;
            this.TypeColumn.Width = 60;
            // 
            // DelayColumn
            // 
            this.DelayColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DelayColumn.DataPropertyName = "response";
            this.DelayColumn.FillWeight = 60F;
            this.DelayColumn.HeaderText = "Response";
            this.DelayColumn.Name = "DelayColumn";
            this.DelayColumn.ReadOnly = true;
            this.DelayColumn.Width = 60;
            // 
            // AnonymityCol
            // 
            this.AnonymityCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AnonymityCol.DataPropertyName = "anonymity";
            this.AnonymityCol.HeaderText = "Anonymity";
            this.AnonymityCol.Name = "AnonymityCol";
            this.AnonymityCol.ReadOnly = true;
            // 
            // LocationColumn
            // 
            this.LocationColumn.DataPropertyName = "country";
            this.LocationColumn.FillWeight = 29.69543F;
            this.LocationColumn.HeaderText = "Country";
            this.LocationColumn.Name = "LocationColumn";
            this.LocationColumn.ReadOnly = true;
            // 
            // AnonymityEn
            // 
            this.AnonymityEn.DataPropertyName = "anonymityen";
            this.AnonymityEn.HeaderText = "AnonymityEn";
            this.AnonymityEn.Name = "AnonymityEn";
            this.AnonymityEn.ReadOnly = true;
            this.AnonymityEn.Visible = false;
            // 
            // CountryEn
            // 
            this.CountryEn.DataPropertyName = "countryen";
            this.CountryEn.HeaderText = "CountryEn";
            this.CountryEn.Name = "CountryEn";
            this.CountryEn.ReadOnly = true;
            this.CountryEn.Visible = false;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.DataPropertyName = "description";
            this.DescriptionColumn.FillWeight = 29.69543F;
            this.DescriptionColumn.HeaderText = "Description";
            this.DescriptionColumn.Name = "DescriptionColumn";
            this.DescriptionColumn.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Test,
            this.toolStripMenuItem3,
            this.SwitchTo,
            this.DoNotUseProxy,
            this.toolStripMenuItem1,
            this.Copy,
            this.Paste,
            this.Cut,
            this.toolStripMenuItem2,
            this.DeleteThis,
            this.Clear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 220);
            // 
            // Test
            // 
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(152, 22);
            this.Test.Text = "验证";
            this.Test.Click += new System.EventHandler(this.tsmiTest_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // SwitchTo
            // 
            this.SwitchTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiIE,
            this.BuiltinBrowser});
            this.SwitchTo.Name = "SwitchTo";
            this.SwitchTo.Size = new System.Drawing.Size(152, 22);
            this.SwitchTo.Text = "使用代理";
            // 
            // tsmiIE
            // 
            this.tsmiIE.Name = "tsmiIE";
            this.tsmiIE.Size = new System.Drawing.Size(130, 22);
            this.tsmiIE.Text = "IE/Chrome";
            this.tsmiIE.Click += new System.EventHandler(this.tsmiIE_Click);
            // 
            // BuiltinBrowser
            // 
            this.BuiltinBrowser.Name = "BuiltinBrowser";
            this.BuiltinBrowser.Size = new System.Drawing.Size(130, 22);
            this.BuiltinBrowser.Text = "内置浏览器";
            this.BuiltinBrowser.Click += new System.EventHandler(this.tsmiInnerBrowser_Click);
            // 
            // DoNotUseProxy
            // 
            this.DoNotUseProxy.Name = "DoNotUseProxy";
            this.DoNotUseProxy.Size = new System.Drawing.Size(152, 22);
            this.DoNotUseProxy.Text = "禁止使用代理";
            this.DoNotUseProxy.Click += new System.EventHandler(this.tsmiCancelProxy_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // Copy
            // 
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(152, 22);
            this.Copy.Text = "复制";
            this.Copy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // Paste
            // 
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(152, 22);
            this.Paste.Text = "粘贴";
            this.Paste.Click += new System.EventHandler(this.Paste_Click);
            // 
            // Cut
            // 
            this.Cut.Name = "Cut";
            this.Cut.Size = new System.Drawing.Size(152, 22);
            this.Cut.Text = "剪切";
            this.Cut.Click += new System.EventHandler(this.Cut_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // DeleteThis
            // 
            this.DeleteThis.Name = "DeleteThis";
            this.DeleteThis.Size = new System.Drawing.Size(152, 22);
            this.DeleteThis.Text = "删除";
            this.DeleteThis.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // Clear
            // 
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(152, 22);
            this.Clear.Text = "清空";
            this.Clear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.DarkOrange;
            // 
            // ProxyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 485);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProxyForm";
            this.Text = "代理公布器";
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProxyList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ToolStrip Toolbar;
        private System.Windows.Forms.ToolStripButton DownloadProxy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton TestAll;
        private System.Windows.Forms.ToolStripButton StopTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ImportProxy;
        private System.Windows.Forms.ToolStripButton ExportProxy;
        private System.Windows.Forms.ToolStripDropDownButton Delete;
        private System.Windows.Forms.ToolStripMenuItem DeleteDeadProxy;
        private System.Windows.Forms.ToolStripMenuItem DeleteSeleted;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton Option;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripLabel CountryLabel;
        private System.Windows.Forms.ToolStripTextBox tstxtArea;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel PortLabel;
        private System.Windows.Forms.ToolStripTextBox tstxtPort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel AnonymityLabel;
        private System.Windows.Forms.ToolStripTextBox tstxtAnonymity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton Search;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Copy;
        private System.Windows.Forms.ToolStripMenuItem Test;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SwitchTo;
        private System.Windows.Forms.ToolStripMenuItem tsmiIE;
        private System.Windows.Forms.ToolStripMenuItem BuiltinBrowser;
        private System.Windows.Forms.ToolStripMenuItem DoNotUseProxy;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem DeleteThis;
        private System.Windows.Forms.ToolStripMenuItem Clear;
        private DataGridViewEx dgvProxyList;
        private System.Windows.Forms.ToolStripLabel tsslCountInfo;
        private System.Windows.Forms.ToolStripMenuItem Paste;
        private System.Windows.Forms.ToolStripMenuItem Cut;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolTip toolTip1;
        private DataGridViewDoneColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PortColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DelayColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnonymityCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnonymityEn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountryEn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
	}
}