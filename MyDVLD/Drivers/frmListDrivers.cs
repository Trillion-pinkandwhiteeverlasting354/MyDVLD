using MyDVLD.Licenses;
using MyDVLD.People;
using MyDVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyDVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        private DataTable _dtAllDrivers;

        public frmListDrivers()
        {
            InitializeComponent();
        }


        private void FrmListDrivers_Load(object sender, EventArgs e)
        {
            _dtAllDrivers = clsDriver.GetAllDrivers();

            dgvDrivers.DataSource = _dtAllDrivers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = _dtAllDrivers.Rows.Count.ToString();

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 120;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 320;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 170;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 150;
            }
        }

        private void CbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool EnableFilter = cbFilterBy.Text != "None";

            txtFilterValue.Visible = EnableFilter;
            txtFilterValue.Enabled = EnableFilter;

            txtFilterValue.Text = "";

            if (EnableFilter)
                txtFilterValue.Focus();
        }

        private void TxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            // Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Driver ID": FilterColumn = "DriverID"; break;
                case "Person ID": FilterColumn = "PersonID"; break;
                case "National No.": FilterColumn = "NationalNo"; break;
                case "Full Name": FilterColumn = "FullName"; break;
                default: FilterColumn = "None"; break;
            }

            string value = txtFilterValue.Text.Trim();

            // Reset the filters in case nothing selected or filter value conains nothing.
            if (string.IsNullOrWhiteSpace(value) || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            // Decide if the column is numeric
            bool isNumericColumn = FilterColumn == "DriverID" || FilterColumn == "PersonID";

            string filterExpression;
            if (isNumericColumn)
                // numeric filter (safe)
                if (int.TryParse(value, out int id))
                    filterExpression = $"[{FilterColumn}] = {id}";
                else
                {
                    _dtAllDrivers.DefaultView.RowFilter = "";
                    lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                    return;
                }
            else
                filterExpression = $"[{FilterColumn}] LIKE '{value}%'";

            _dtAllDrivers.DefaultView.RowFilter = filterExpression;
            lblRecordsCount.Text = _dtAllDrivers.Rows.Count.ToString();
        }

        private void TxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // We let the user enter numbers only if person id or user id selected.
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ShowPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            // Refresh
            FrmListDrivers_Load(null, null);
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            Form frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
