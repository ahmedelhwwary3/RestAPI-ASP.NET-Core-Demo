namespace Desktop_UI
{
    partial class frmGetAllPeople
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
            components = new System.ComponentModel.Container();
            dgvPeople = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            addNewPersonToolStripMenuItem = new ToolStripMenuItem();
            editPersonToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            lblCount = new Label();
            btnAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPeople).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvPeople
            // 
            dgvPeople.AllowUserToAddRows = false;
            dgvPeople.AllowUserToDeleteRows = false;
            dgvPeople.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPeople.ContextMenuStrip = contextMenuStrip1;
            dgvPeople.Location = new Point(12, 270);
            dgvPeople.Name = "dgvPeople";
            dgvPeople.ReadOnly = true;
            dgvPeople.Size = new Size(1232, 407);
            dgvPeople.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addNewPersonToolStripMenuItem, editPersonToolStripMenuItem, toolStripMenuItem1, deleteToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(127, 76);
            // 
            // addNewPersonToolStripMenuItem
            // 
            addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            addNewPersonToolStripMenuItem.Size = new Size(126, 22);
            addNewPersonToolStripMenuItem.Text = "Add New ";
            addNewPersonToolStripMenuItem.Click += addNewPersonToolStripMenuItem_Click;
            // 
            // editPersonToolStripMenuItem
            // 
            editPersonToolStripMenuItem.Name = "editPersonToolStripMenuItem";
            editPersonToolStripMenuItem.Size = new Size(126, 22);
            editPersonToolStripMenuItem.Text = "Edit ";
            editPersonToolStripMenuItem.Click += editPersonToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(123, 6);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(126, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 691);
            label1.Name = "label1";
            label1.Size = new Size(64, 21);
            label1.TabIndex = 1;
            label1.Text = "Count :";
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCount.Location = new Point(94, 692);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(31, 21);
            lblCount.TabIndex = 2;
            lblCount.Text = "???";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(1173, 207);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(71, 47);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add New";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // frmGetAllPeople
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1256, 727);
            Controls.Add(btnAdd);
            Controls.Add(lblCount);
            Controls.Add(label1);
            Controls.Add(dgvPeople);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmGetAllPeople";
            Text = "frmGetAllPeople";
            Load += frmGetAllPeople_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPeople).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPeople;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem addNewPersonToolStripMenuItem;
        private ToolStripMenuItem editPersonToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Label label1;
        private Label lblCount;
        private Button btnAdd;
    }
}