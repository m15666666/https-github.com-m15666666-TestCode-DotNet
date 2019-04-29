namespace Zx2642DatabaseImportExport
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnu_ExportImportDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_ChangeMObjectTree = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_ChangePointLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_ExportImportDBToolStripMenuItem,
            this.mnu_ChangeMObjectTree,
            this.mnu_ChangePointLocation});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnu_ExportImportDBToolStripMenuItem
            // 
            this.mnu_ExportImportDBToolStripMenuItem.Name = "mnu_ExportImportDBToolStripMenuItem";
            this.mnu_ExportImportDBToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.mnu_ExportImportDBToolStripMenuItem.Text = "导入导出数据";
            this.mnu_ExportImportDBToolStripMenuItem.Click += new System.EventHandler(this.mnu_ExportImportDBToolStripMenuItem_Click);
            // 
            // mnu_ChangeMObjectTree
            // 
            this.mnu_ChangeMObjectTree.Name = "mnu_ChangeMObjectTree";
            this.mnu_ChangeMObjectTree.Size = new System.Drawing.Size(80, 21);
            this.mnu_ChangeMObjectTree.Text = "改变设备树";
            this.mnu_ChangeMObjectTree.Click += new System.EventHandler(this.mnu_ChangeMObjectTree_Click);
            // 
            // mnu_ChangePointLocation
            // 
            this.mnu_ChangePointLocation.Name = "mnu_ChangePointLocation";
            this.mnu_ChangePointLocation.Size = new System.Drawing.Size(92, 21);
            this.mnu_ChangePointLocation.Text = "改变测点位置";
            this.mnu_ChangePointLocation.Click += new System.EventHandler(this.mnu_ChangePointLocation_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "在线数据调整工具";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_ExportImportDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_ChangeMObjectTree;
        private System.Windows.Forms.ToolStripMenuItem mnu_ChangePointLocation;
    }
}