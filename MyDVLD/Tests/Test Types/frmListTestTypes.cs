using MyDVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyDVLD.Tests.Test_Types
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtAllTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void FrmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 80;

                dgvTestTypes.Columns[1].HeaderText = "Test Type Title";
                dgvTestTypes.Columns[1].Width = 200;

                dgvTestTypes.Columns[2].HeaderText = "Test Type Description";
                dgvTestTypes.Columns[2].Width = 400;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;
            }
        }

        private void EditTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditTestType((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            FrmListTestTypes_Load(null, null);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
