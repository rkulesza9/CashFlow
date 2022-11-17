namespace CashFlow
{
    partial class ArchiveForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnUnarchive = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.lvArchive = new System.Windows.Forms.ListView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUnarchive,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnUnarchive
            // 
            this.btnUnarchive.Image = global::CashFlow.Properties.Resources.add;
            this.btnUnarchive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnarchive.Name = "btnUnarchive";
            this.btnUnarchive.Size = new System.Drawing.Size(80, 22);
            this.btnUnarchive.Text = "Unarchive";
            this.btnUnarchive.Click += new System.EventHandler(this.btnUnarchive_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::CashFlow.Properties.Resources.remove;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lvArchive
            // 
            this.lvArchive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvArchive.FullRowSelect = true;
            this.lvArchive.GridLines = true;
            this.lvArchive.HideSelection = false;
            this.lvArchive.Location = new System.Drawing.Point(0, 25);
            this.lvArchive.Name = "lvArchive";
            this.lvArchive.Size = new System.Drawing.Size(800, 425);
            this.lvArchive.TabIndex = 1;
            this.lvArchive.UseCompatibleStateImageBehavior = false;
            this.lvArchive.View = System.Windows.Forms.View.Details;
            // 
            // ArchiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvArchive);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ArchiveForm";
            this.Text = "ArchiveForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnUnarchive;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ListView lvArchive;
    }
}