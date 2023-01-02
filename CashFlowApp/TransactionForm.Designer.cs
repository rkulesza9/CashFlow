namespace CashFlowApp
{
    partial class TransactionForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvDataView = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnRemove = new System.Windows.Forms.ToolStripButton();
            this.btnArchive = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.pgEditor = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.lvDataView);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pgEditor);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.TabIndex = 2;
            // 
            // lvDataView
            // 
            this.lvDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDataView.FullRowSelect = true;
            this.lvDataView.GridLines = true;
            this.lvDataView.HideSelection = false;
            this.lvDataView.Location = new System.Drawing.Point(0, 25);
            this.lvDataView.Name = "lvDataView";
            this.lvDataView.Size = new System.Drawing.Size(800, 256);
            this.lvDataView.TabIndex = 0;
            this.lvDataView.UseCompatibleStateImageBehavior = false;
            this.lvDataView.View = System.Windows.Forms.View.Details;
            this.lvDataView.SelectedIndexChanged += new System.EventHandler(this.lvDataView_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tbSearch,
            this.btnSearch,
            this.btnRemove,
            this.btnArchive,
            this.btnAdd,
            this.btnRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel1.Text = "Search:";
            // 
            // tbSearch
            // 
            this.tbSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(300, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::CashFlowApp.Properties.Resources.search;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 22);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnRemove.Image = global::CashFlowApp.Properties.Resources.remove;
            this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(70, 22);
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnArchive
            // 
            this.btnArchive.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnArchive.Image = global::CashFlowApp.Properties.Resources.archive;
            this.btnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size(67, 22);
            this.btnArchive.Text = "Archive";
            this.btnArchive.Click += new System.EventHandler(this.btnArchive_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAdd.Image = global::CashFlowApp.Properties.Resources.add;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(49, 22);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::CashFlowApp.Properties.Resources.refresh_page_option;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pgEditor
            // 
            this.pgEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgEditor.HelpVisible = false;
            this.pgEditor.Location = new System.Drawing.Point(0, 0);
            this.pgEditor.Name = "pgEditor";
            this.pgEditor.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.pgEditor.Size = new System.Drawing.Size(800, 165);
            this.pgEditor.TabIndex = 0;
            this.pgEditor.ToolbarVisible = false;
            // 
            // TransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TransactionForm";
            this.Text = "Transactions";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvDataView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbSearch;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripButton btnRemove;
        private System.Windows.Forms.ToolStripButton btnArchive;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.PropertyGrid pgEditor;
    }
}