namespace Zx2642DatabaseImportExport
{
    partial class ChangeMObjectTreeForm
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
            this.tv_MObject = new System.Windows.Forms.TreeView();
            this.btn_LoadMObjectTree = new System.Windows.Forms.Button();
            this.btn_Cut = new System.Windows.Forms.Button();
            this.btn_Paste = new System.Windows.Forms.Button();
            this.lv_ChildNodes = new System.Windows.Forms.ListView();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tv_MObject
            // 
            this.tv_MObject.CheckBoxes = true;
            this.tv_MObject.Dock = System.Windows.Forms.DockStyle.Left;
            this.tv_MObject.HideSelection = false;
            this.tv_MObject.Location = new System.Drawing.Point(0, 0);
            this.tv_MObject.Name = "tv_MObject";
            this.tv_MObject.Size = new System.Drawing.Size(475, 500);
            this.tv_MObject.TabIndex = 0;
            this.tv_MObject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_MObject_AfterSelect);
            // 
            // btn_LoadMObjectTree
            // 
            this.btn_LoadMObjectTree.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_LoadMObjectTree.Location = new System.Drawing.Point(475, 0);
            this.btn_LoadMObjectTree.Name = "btn_LoadMObjectTree";
            this.btn_LoadMObjectTree.Size = new System.Drawing.Size(344, 23);
            this.btn_LoadMObjectTree.TabIndex = 1;
            this.btn_LoadMObjectTree.Text = "加载设备";
            this.btn_LoadMObjectTree.UseVisualStyleBackColor = true;
            this.btn_LoadMObjectTree.Click += new System.EventHandler(this.btn_LoadMObjectTree_Click);
            // 
            // btn_Cut
            // 
            this.btn_Cut.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Cut.Location = new System.Drawing.Point(475, 23);
            this.btn_Cut.Name = "btn_Cut";
            this.btn_Cut.Size = new System.Drawing.Size(344, 23);
            this.btn_Cut.TabIndex = 1;
            this.btn_Cut.Text = "剪切设备";
            this.btn_Cut.UseVisualStyleBackColor = true;
            this.btn_Cut.Click += new System.EventHandler(this.btn_Cut_Click);
            // 
            // btn_Paste
            // 
            this.btn_Paste.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Paste.Location = new System.Drawing.Point(475, 46);
            this.btn_Paste.Name = "btn_Paste";
            this.btn_Paste.Size = new System.Drawing.Size(344, 23);
            this.btn_Paste.TabIndex = 1;
            this.btn_Paste.Text = "粘贴设备";
            this.btn_Paste.UseVisualStyleBackColor = true;
            this.btn_Paste.Click += new System.EventHandler(this.btn_Paste_Click);
            // 
            // lv_ChildNodes
            // 
            this.lv_ChildNodes.CheckBoxes = true;
            this.lv_ChildNodes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lv_ChildNodes.Location = new System.Drawing.Point(475, 203);
            this.lv_ChildNodes.Name = "lv_ChildNodes";
            this.lv_ChildNodes.ShowGroups = false;
            this.lv_ChildNodes.Size = new System.Drawing.Size(344, 297);
            this.lv_ChildNodes.TabIndex = 2;
            this.lv_ChildNodes.UseCompatibleStateImageBehavior = false;
            this.lv_ChildNodes.View = System.Windows.Forms.View.List;
            // 
            // rtb_log
            // 
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_log.Location = new System.Drawing.Point(475, 69);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(344, 134);
            this.rtb_log.TabIndex = 4;
            this.rtb_log.Text = "";
            // 
            // ChangeMObjectTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 500);
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.lv_ChildNodes);
            this.Controls.Add(this.btn_Paste);
            this.Controls.Add(this.btn_Cut);
            this.Controls.Add(this.btn_LoadMObjectTree);
            this.Controls.Add(this.tv_MObject);
            this.Name = "ChangeMObjectTreeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeMObjectTreeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv_MObject;
        private System.Windows.Forms.Button btn_LoadMObjectTree;
        private System.Windows.Forms.Button btn_Cut;
        private System.Windows.Forms.Button btn_Paste;
        private System.Windows.Forms.ListView lv_ChildNodes;
        private System.Windows.Forms.RichTextBox rtb_log;
    }
}