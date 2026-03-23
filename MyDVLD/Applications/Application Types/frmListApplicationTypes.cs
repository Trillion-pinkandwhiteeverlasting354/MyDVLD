using MyDVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyDVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes;

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void FrmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Application Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 100;
            }
        }

        private void EditApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            FrmListApplicationTypes_Load(null, null);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
