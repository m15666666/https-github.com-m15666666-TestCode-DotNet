namespace Zx2642DatabaseImportExport
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnu_exportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_importExcelToDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_exportToExcel,
            this.mnu_importExcelToDatabase});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // mnu_exportToExcel
            // 
            this.mnu_exportToExcel.Name = "mnu_exportToExcel";
            resources.ApplyResources(this.mnu_exportToExcel, "mnu_exportToExcel");
            this.mnu_exportToExcel.Click += new System.EventHandler(this.mnu_exportToExcel_Click);
            // 
            // mnu_importExcelToDatabase
            // 
            this.mnu_importExcelToDatabase.Name = "mnu_importExcelToDatabase";
            resources.ApplyResources(this.mnu_importExcelToDatabase, "mnu_importExcelToDatabase");
            this.mnu_importExcelToDatabase.Click += new System.EventHandler(this.mnu_importExcelToDatabase_Click);
            // 
            // rtb_log
            // 
            resources.ApplyResources(this.rtb_log, "rtb_log");
            this.rtb_log.Name = "rtb_log";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_exportToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnu_importExcelToDatabase;
        private System.Windows.Forms.RichTextBox rtb_log;
    }
}

