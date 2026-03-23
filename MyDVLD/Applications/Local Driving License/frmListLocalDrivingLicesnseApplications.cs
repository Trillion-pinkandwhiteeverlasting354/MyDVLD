using MyDVLD.Licenses;
using MyDVLD.Licenses.Local_Licenses;
using MyDVLD.Tests;
using MyDVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;


namespace MyDVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        //public static void ApplyFilter(DataTable table, string columnName, string filterValue)
        //{
        //    if (table == null || table.DefaultView == null)
        //        return;

        //    filterValue = filterValue.Trim();

        //    // If filter is empty => clear filter
        //    if (string.IsNullOrEmpty(filterValue))
        //    {
        //        table.DefaultView.RowFilter = "";
        //        return;
        //    }

        //    // Determine data type of the column dynamically
        //    Type columnType = table.Columns[columnName].DataType;

        //    if (columnType == typeof(int) || columnType == typeof(long) ||
        //        columnType == typeof(short))
        //    {
        //        // Integer column => validate
        //        if (int.TryParse(filterValue, out int number))
        //            table.DefaultView.RowFilter = $"[{columnName}] = {number}";
        //        else
        //            table.DefaultView.RowFilter = ""; // invalid input → clear filter
        //    }
        //    else if (columnType == typeof(DateTime))
        //    {
        //        // Date column => attempt parsing
        //        if (DateTime.TryParse(filterValue, out DateTime date))
        //            table.DefaultView.RowFilter = $"[{columnName}] = #{date:MM/dd/yyyy}#";
        //        else
        //            table.DefaultView.RowFilter = "";
        //    }
        //    else
        //    {
        //        // String or other types => escape single quotes
        //        string safeValue = filterValue.Replace("'", "''");
        //        table.DefaultView.RowFilter = $"[{columnName}] LIKE '%{safeValue}%'";
        //    }
        //}
        //ApplyFilter(_dtAllLocalDrivingLicenseApplications, FilterColumn, txtFilterValue.Text);



        private DataTable _dtAllLocalDrivingLicenseApplications;

        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }

        private void FrmListLocalDrivingLicesnseApplications_Load(object sender, System.EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 130;
            }

            cbFilterBy.SelectedIndex = 0;
        }

        private void TxtFilterValue_TextChanged(object sender, System.EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID": FilterColumn = "LocalDrivingLicenseApplicationID"; break;
                case "National No.": FilterColumn = "NationalNo"; break;
                case "Full Name": FilterColumn = "FullName"; break;
                case "Status": FilterColumn = "Status"; break;
                default: FilterColumn = "None"; break;
            }

            if (FilterColumn == "None" || string.IsNullOrWhiteSpace(txtFilterValue.Text))
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            //if (FilterColumn == "LocalDrivingLicenseApplicationID")
            //    //in this case we deal with integer not string.
            //    _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            //else
            //    _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                // In this case we deal with integer not string.
                if (int.TryParse(txtFilterValue.Text.Trim(), out int id))
                    _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = $"[{FilterColumn}] = {id}";
                else
                    _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = ""; // invalid number
            }
            else
            {
                string safeValue = txtFilterValue.Text.Trim().Replace("'", "''");
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{safeValue}%'";
            }

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void TxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // We allow number in case L.D.L.AppID is selected.
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void CbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enableFilter = cbFilterBy.Text != "None";
            txtFilterValue.Visible = enableFilter;

            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (enableFilter)
            {
                txtFilterValue.Clear();
                txtFilterValue.Focus();
            }
            else
                txtFilterValue.Clear(); // ensure the filter text is empty

            // Reset filter
            if (_dtAllLocalDrivingLicenseApplications?.DefaultView != null)
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";

            // Update record count *after clearing filter*
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void BtnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            // Refresh
            FrmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void ShowApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            // Refresh
            FrmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void EditApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateLocalDrivingLicesnseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            // Refresh
            FrmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalAppID);

            if (LocalApp != null)
            {
                if (LocalApp.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh the form again.
                    FrmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                    MessageBox.Show("Could not delete application, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalAppID);

            if (LocalApp != null)
            {
                if (LocalApp.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh the form again.
                    FrmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int LocalAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalAppID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
            bool LicenseExists = LocalApp.IsLicenseIssued();

            // Enabled only if person passed all tests and Does not have license. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists && LocalApp.ApplicationStatus == clsApplication.enApplicationStatus.New;
            //issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && (LocalApp.ApplicationStatus == clsApplication.enApplicationStatus.New);
            //sechduleTestsToolStripMenuItem.Enabled = !LicenseExists && (LocalApp.ApplicationStatus == clsApplication.enApplicationStatus.New);
            sechduleTestsToolStripMenuItem.Enabled = !LicenseExists;

            // Enable / Disable Cancel Menu Item. We only canel the applications with status=new.
            cancelApplicationToolStripMenuItem.Enabled = LocalApp.ApplicationStatus == clsApplication.enApplicationStatus.New;

            // Enable / Disable Delete Menue Item
            // We only allow delete in case the application status is new not completed or cancelled.
            //deleteApplicationToolStripMenuItem.Enabled = LocalApp.ApplicationStatus == clsApplication.enApplicationStatus.New && (TotalPassedTests == 0);
            deleteApplicationToolStripMenuItem.Enabled = LocalApp.ApplicationStatus == clsApplication.enApplicationStatus.New;

            // Enable / Disable Schedule menu and it's sub menu
            bool PassedVisionTest = LocalApp.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = LocalApp.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalApp.DoesPassTestType(clsTestType.enTestType.StreetTest);

            sechduleTestsToolStripMenuItem.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest)
                                                     && (LocalApp.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (sechduleTestsToolStripMenuItem.Enabled)
            {
                // To Allow schedule vision test, Person must not passed the same test before.
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                // To Allow schedule written test, Person must pass the vision test and must not passed the same test before.
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;
                // To Allow schedule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
        }

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            Form frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            // refresh
            FrmListLocalDrivingLicesnseApplications_Load(null, null);

        }





        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            Form frm = new frmShowPersonLicenseHistory(LocalApp.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void ShowLicenseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                Form frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void IssueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            Form frm = new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            FrmListLocalDrivingLicesnseApplications_Load(null, null);
            //MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ScheduleVisionTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
            //MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ScheduleWrittenTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);
            //MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ScheduleStreetTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
            //MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


    }
}
