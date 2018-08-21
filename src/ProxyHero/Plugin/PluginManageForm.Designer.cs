namespace ProxyHero.Plugin
{
    partial class PluginManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManageForm));
            this.lvPlugin = new System.Windows.Forms.ListView();
            this.PluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LPHVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.Compile = new System.Windows.Forms.Button();
            this.p_OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DownloadPlugins = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvPlugin
            // 
            this.lvPlugin.CheckBoxes = true;
            this.lvPlugin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PluginName,
            this.Author,
            this.Version,
            this.LPHVersion,
            this.Description,
            this.FileName});
            this.lvPlugin.FullRowSelect = true;
            this.lvPlugin.GridLines = true;
            this.lvPlugin.Location = new System.Drawing.Point(14, 12);
            this.lvPlugin.Name = "lvPlugin";
            this.lvPlugin.Size = new System.Drawing.Size(607, 278);
            this.lvPlugin.TabIndex = 1;
            this.lvPlugin.UseCompatibleStateImageBehavior = false;
            this.lvPlugin.View = System.Windows.Forms.View.Details;
            // 
            // PluginName
            // 
            this.PluginName.Text = "Name";
            this.PluginName.Width = 100;
            // 
            // Author
            // 
            this.Author.Text = "Author";
            this.Author.Width = 100;
            // 
            // Version
            // 
            this.Version.Text = "Version";
            // 
            // LPHVersion
            // 
            this.LPHVersion.Text = "LPHVersion";
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 288;
            // 
            // FileName
            // 
            this.FileName.Text = "FileName";
            this.FileName.Width = 288;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(463, 302);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 2;
            this.OK.Text = "&OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(546, 302);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "&Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(14, 302);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 4;
            this.Add.Text = "&Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(95, 302);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(75, 23);
            this.Remove.TabIndex = 5;
            this.Remove.Text = "&Remove";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // Compile
            // 
            this.Compile.Location = new System.Drawing.Point(176, 302);
            this.Compile.Name = "Compile";
            this.Compile.Size = new System.Drawing.Size(145, 23);
            this.Compile.TabIndex = 6;
            this.Compile.Text = "Compile(.cs->.dll)";
            this.Compile.UseVisualStyleBackColor = true;
            this.Compile.Click += new System.EventHandler(this.Compile_Click);
            // 
            // DownloadPlugins
            // 
            this.DownloadPlugins.Location = new System.Drawing.Point(328, 302);
            this.DownloadPlugins.Name = "DownloadPlugins";
            this.DownloadPlugins.Size = new System.Drawing.Size(107, 23);
            this.DownloadPlugins.TabIndex = 7;
            this.DownloadPlugins.Text = "Download";
            this.DownloadPlugins.UseVisualStyleBackColor = true;
            this.DownloadPlugins.Click += new System.EventHandler(this.DownloadPlugins_Click);
            // 
            // PluginManageForm
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(633, 333);
            this.Controls.Add(this.DownloadPlugins);
            this.Controls.Add(this.Compile);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.lvPlugin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginManageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plugins Manage";
            this.Load += new System.EventHandler(this.PluginManageForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPlugin;
        private System.Windows.Forms.ColumnHeader PluginName;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ColumnHeader LPHVersion;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button Compile;
        private System.Windows.Forms.OpenFileDialog p_OpenFileDialog;
        private System.Windows.Forms.ColumnHeader FileName;
        private System.Windows.Forms.Button DownloadPlugins;
    }
}