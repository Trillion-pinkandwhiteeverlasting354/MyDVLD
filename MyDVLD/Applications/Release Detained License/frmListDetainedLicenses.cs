using MyDVLD.Licenses;
using MyDVLD.Licenses.Detain_License;
using MyDVLD.Licenses.Local_Licenses;
using MyDVLD.People;
using MyDVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyDVLD.Applications.Release_Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _dtDetainedLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void FrmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            cbFilterBy.SelectedIndex = 0;
            lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;
            }

        }

        private void TxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            // Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Detain ID": FilterColumn = "DetainID"; break;
                case "Is Released": FilterColumn = "IsReleased"; break;
                case "National No.": FilterColumn = "NationalNo"; break;
                case "Full Name": FilterColumn = "FullName"; break;
                case "Release Application ID": FilterColumn = "ReleaseApplicationID"; break;
                default: FilterColumn = "None"; break;
            }

            // Reset the filters in case nothing selected or filter value conains nothing.
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()) || FilterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
            {
                // In this case we deal with integer not string.
                if (int.TryParse(txtFilterValue.Text.Trim(), out int id))
                    _dtDetainedLicenses.DefaultView.RowFilter = $"[{FilterColumn}] = {id}";
                else
                    _dtDetainedLicenses.DefaultView.RowFilter = ""; // invalid number
            }
            else
            {
                string safeValue = txtFilterValue.Text.Trim().Replace("'", "''");
                _dtDetainedLicenses.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{safeValue}%'";
            }

            lblTotalRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }

        private void TxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void CbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                bool showTextBox = (cbFilterBy.Text != "None");

                txtFilterValue.Visible = showTextBox;
                cbIsReleased.Visible = false;

                txtFilterValue.Text = "";

                if (showTextBox)
                    txtFilterValue.Focus();
            }

        }

        private void CbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All": break;
                case "Yes": FilterValue = "1"; break;
                case "No": FilterValue = "0"; break;
            }

            if (FilterValue == "All")
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblTotalRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }

        private void BtnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            FrmListDetainedLicenses_Load(null, null);
        }

        private void BtnDetainLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            FrmListDetainedLicenses_Load(null, null);
        }

        private void ShowPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            Form frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void ShowLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            Form frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            Form frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void ReleaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            Form frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            FrmListDetainedLicenses_Load(null, null);
        }

        private void CmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
