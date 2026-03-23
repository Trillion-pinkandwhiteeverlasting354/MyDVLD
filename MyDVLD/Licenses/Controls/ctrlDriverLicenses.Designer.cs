namespace MyDVLD.Licenses.Controls
{
    partial class ctrlDriverLicenses
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbDriverLicenses = new System.Windows.Forms.GroupBox();
            this.tcDriverLicenses = new System.Windows.Forms.TabControl();
            this.tpLocalLicenses = new System.Windows.Forms.TabPage();
            this.lblLocalLicensesRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLocalLicensesHistory = new System.Windows.Forms.DataGridView();
            this.cmsLocalLicenseHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLocalLicenseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tbInternationalLicenses = new System.Windows.Forms.TabPage();
            this.lblInternationalLicensesRecords = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvInternationalLicensesHistory = new System.Windows.Forms.DataGridView();
            this.cmsInterenationalLicenseHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInternationalLicenseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gbDriverLicenses.SuspendLayout();
            this.tcDriverLicenses.SuspendLayout();
            this.tpLocalLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).BeginInit();
            this.cmsLocalLicenseHistory.SuspendLayout();
            this.tbInternationalLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).BeginInit();
            this.cmsInterenationalLicenseHistory.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDriverLicenses
            // 
            this.gbDriverLicenses.Controls.Add(this.tcDriverLicenses);
            this.gbDriverLicenses.Location = new System.Drawing.Point(3, 3);
            this.gbDriverLicenses.Name = "gbDriverLicenses";
            this.gbDriverLicenses.Size = new System.Drawing.Size(1055, 334);
            this.gbDriverLicenses.TabIndex = 0;
            this.gbDriverLicenses.TabStop = false;
            this.gbDriverLicenses.Text = "Driver Licenses";
            // 
            // tcDriverLicenses
            // 
            this.tcDriverLicenses.Controls.Add(this.tpLocalLicenses);
            this.tcDriverLicenses.Controls.Add(this.tbInternationalLicenses);
            this.tcDriverLicenses.Location = new System.Drawing.Point(6, 25);
            this.tcDriverLicenses.Name = "tcDriverLicenses";
            this.tcDriverLicenses.SelectedIndex = 0;
            this.tcDriverLicenses.Size = new System.Drawing.Size(1032, 303);
            this.tcDriverLicenses.TabIndex = 0;
            // 
            // tpLocalLicenses
            // 
            this.tpLocalLicenses.Controls.Add(this.lblLocalLicensesRecords);
            this.tpLocalLicenses.Controls.Add(this.label2);
            this.tpLocalLicenses.Controls.Add(this.label1);
            this.tpLocalLicenses.Controls.Add(this.dgvLocalLicensesHistory);
            this.tpLocalLicenses.Location = new System.Drawing.Point(4, 29);
            this.tpLocalLicenses.Name = "tpLocalLicenses";
            this.tpLocalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocalLicenses.Size = new System.Drawing.Size(1024, 270);
            this.tpLocalLicenses.TabIndex = 0;
            this.tpLocalLicenses.Text = "Local";
            this.tpLocalLicenses.UseVisualStyleBackColor = true;
            // 
            // lblLocalLicensesRecords
            // 
            this.lblLocalLicensesRecords.AutoSize = true;
            this.lblLocalLicensesRecords.Location = new System.Drawing.Point(117, 232);
            this.lblLocalLicensesRecords.Name = "lblLocalLicensesRecords";
            this.lblLocalLicensesRecords.Size = new System.Drawing.Size(27, 20);
            this.lblLocalLicensesRecords.TabIndex = 3;
            this.lblLocalLicensesRecords.Text = "??";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Local Licenses History:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "# Records:";
            // 
            // dgvLocalLicensesHistory
            // 
            this.dgvLocalLicensesHistory.AllowUserToAddRows = false;
            this.dgvLocalLicensesHistory.AllowUserToDeleteRows = false;
            this.dgvLocalLicensesHistory.AllowUserToResizeRows = false;
            this.dgvLocalLicensesHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicensesHistory.ContextMenuStrip = this.cmsLocalLicenseHistory;
            this.dgvLocalLicensesHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLocalLicensesHistory.Location = new System.Drawing.Point(10, 49);
            this.dgvLocalLicensesHistory.MultiSelect = false;
            this.dgvLocalLicensesHistory.Name = "dgvLocalLicensesHistory";
            this.dgvLocalLicensesHistory.ReadOnly = true;
            this.dgvLocalLicensesHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalLicensesHistory.Size = new System.Drawing.Size(996, 168);
            this.dgvLocalLicensesHistory.TabIndex = 0;
            this.dgvLocalLicensesHistory.TabStop = false;
            // 
            // cmsLocalLicenseHistory
            // 
            this.cmsLocalLicenseHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLocalLicenseInfo});
            this.cmsLocalLicenseHistory.Name = "cmsLocalLicenseHistory";
            this.cmsLocalLicenseHistory.Size = new System.Drawing.Size(213, 42);
            // 
            // showLocalLicenseInfo
            // 
            this.showLocalLicenseInfo.Image = global::MyDVLD.Properties.Resources.License_View_32;
            this.showLocalLicenseInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLocalLicenseInfo.Name = "showLocalLicenseInfo";
            this.showLocalLicenseInfo.Size = new System.Drawing.Size(212, 38);
            this.showLocalLicenseInfo.Text = "Show License Info";
            this.showLocalLicenseInfo.Click += new System.EventHandler(this.ShowLocalLicenseInfo_Click);
            // 
            // tbInternationalLicenses
            // 
            this.tbInternationalLicenses.Controls.Add(this.lblInternationalLicensesRecords);
            this.tbInternationalLicenses.Controls.Add(this.lbl);
            this.tbInternationalLicenses.Controls.Add(this.label5);
            this.tbInternationalLicenses.Controls.Add(this.dgvInternationalLicensesHistory);
            this.tbInternationalLicenses.Location = new System.Drawing.Point(4, 29);
            this.tbInternationalLicenses.Name = "tbInternationalLicenses";
            this.tbInternationalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tbInternationalLicenses.Size = new System.Drawing.Size(1024, 270);
            this.tbInternationalLicenses.TabIndex = 1;
            this.tbInternationalLicenses.Text = "International";
            this.tbInternationalLicenses.UseVisualStyleBackColor = true;
            // 
            // lblInternationalLicensesRecords
            // 
            this.lblInternationalLicensesRecords.AutoSize = true;
            this.lblInternationalLicensesRecords.Location = new System.Drawing.Point(117, 232);
            this.lblInternationalLicensesRecords.Name = "lblInternationalLicensesRecords";
            this.lblInternationalLicensesRecords.Size = new System.Drawing.Size(27, 20);
            this.lblInternationalLicensesRecords.TabIndex = 7;
            this.lblInternationalLicensesRecords.Text = "??";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(15, 16);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(253, 20);
            this.lbl.TabIndex = 6;
            this.lbl.Text = "International Licenses History:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "# Records:";
            // 
            // dgvInternationalLicensesHistory
            // 
            this.dgvInternationalLicensesHistory.AllowUserToAddRows = false;
            this.dgvInternationalLicensesHistory.AllowUserToDeleteRows = false;
            this.dgvInternationalLicensesHistory.AllowUserToResizeRows = false;
            this.dgvInternationalLicensesHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternationalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicensesHistory.ContextMenuStrip = this.cmsInterenationalLicenseHistory;
            this.dgvInternationalLicensesHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvInternationalLicensesHistory.Location = new System.Drawing.Point(10, 49);
            this.dgvInternationalLicensesHistory.MultiSelect = false;
            this.dgvInternationalLicensesHistory.Name = "dgvInternationalLicensesHistory";
            this.dgvInternationalLicensesHistory.ReadOnly = true;
            this.dgvInternationalLicensesHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInternationalLicensesHistory.Size = new System.Drawing.Size(996, 168);
            this.dgvInternationalLicensesHistory.TabIndex = 4;
            this.dgvInternationalLicensesHistory.TabStop = false;
            // 
            // cmsInterenationalLicenseHistory
            // 
            this.cmsInterenationalLicenseHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInternationalLicenseInfo});
            this.cmsInterenationalLicenseHistory.Name = "cmsLocalLicenseHistory";
            this.cmsInterenationalLicenseHistory.Size = new System.Drawing.Size(213, 64);
            // 
            // showInternationalLicenseInfo
            // 
            this.showInternationalLicenseInfo.Image = global::MyDVLD.Properties.Resources.License_View_32;
            this.showInternationalLicenseInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showInternationalLicenseInfo.Name = "showInternationalLicenseInfo";
            this.showInternationalLicenseInfo.Size = new System.Drawing.Size(212, 38);
            this.showInternationalLicenseInfo.Text = "Show License Info";
            this.showInternationalLicenseInfo.Click += new System.EventHandler(this.ShowInternationalLicenseInfo_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(307, 379);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(8, 8);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(0, 0);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(0, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.gbDriverLicenses);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlDriverLicenses";
            this.Size = new System.Drawing.Size(1079, 358);
            this.gbDriverLicenses.ResumeLayout(false);
            this.tcDriverLicenses.ResumeLayout(false);
            this.tpLocalLicenses.ResumeLayout(false);
            this.tpLocalLicenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).EndInit();
            this.cmsLocalLicenseHistory.ResumeLayout(false);
            this.tbInternationalLicenses.ResumeLayout(false);
            this.tbInternationalLicenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).EndInit();
            this.cmsInterenationalLicenseHistory.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDriverLicenses;
        private System.Windows.Forms.TabControl tcDriverLicenses;
        private System.Windows.Forms.TabPage tpLocalLicenses;
        private System.Windows.Forms.TabPage tbInternationalLicenses;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lblLocalLicensesRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLocalLicensesHistory;
        private System.Windows.Forms.ContextMenuStrip cmsLocalLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem showLocalLicenseInfo;
        private System.Windows.Forms.Label lblInternationalLicensesRecords;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvInternationalLicensesHistory;
        private System.Windows.Forms.ContextMenuStrip cmsInterenationalLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem showInternationalLicenseInfo;
    }
}
