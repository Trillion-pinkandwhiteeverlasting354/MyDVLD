using MyDVLD.Applications.Application_Types;
using MyDVLD.Applications.International_License;
using MyDVLD.Applications.Local_Driving_License;
using MyDVLD.Applications.Release_Detained_License;
using MyDVLD.Applications.Renew_Local_License;
using MyDVLD.Applications.Replace_Lost_Or_Damaged_License;
using MyDVLD.Classes;
using MyDVLD.Drivers;
using MyDVLD.Licenses.Detain_License;
using MyDVLD.Login;
using MyDVLD.People;
using MyDVLD.Tests.Test_Types;
using MyDVLD.Users;
using System.Windows.Forms;

namespace MyDVLD
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }

        private void PeopleToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void SignOutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
        }

        private void CurrentUserInfoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void UsersToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void ManageApplicationsTypesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void ManageTestTypesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void LocalLicenseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void LocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void RetakeTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void RenewDrivingLicenseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void ReplacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void DriversToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void DetainLicenseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void ReleaseDetainedLicenseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void ManageDetainedLicensesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void InternationalLicenseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void InternationalLicenseApplicationsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }


        /*
          Lesson: Manage People: Person Card Control 
            
          Comment on it
            we could use delegation:

            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.DataBack += LoadPersonInfo_DataBack; // Subscribe to the event

            frm.ShowDialog(); 
        */


    }
}
