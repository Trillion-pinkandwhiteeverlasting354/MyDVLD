using MyDVLD.Classes;
using MyDVLD.Properties;
using MyDVLD_Business;
using System;
using System.Windows.Forms;

namespace MyDVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;


        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }

                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }
                }
            }
        }


        public ctrlScheduleTest()
        {
            InitializeComponent();
        }


        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            // If appointment is locked that means the person already attended for this test before
            // and we cannot update locked appointment
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, this appointment is loacked.";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            else
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            // We need to make sure that this person passed the prvious required test before apply to the new test.
            // Person cannot apply for written test unless s/he passes the vision test.
            // And person cannot apply for street test unless s/he passes the written test.

            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    // In this case there is no required previous test to pass.
                    lblUserMessage.Visible = true;
                    return true;


                case clsTestType.enTestType.WrittenTest:
                    // Written Test, you cannot sechdule it before person passes the vision test.
                    // We check if pass visiontest 1.
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;

                case clsTestType.enTestType.StreetTest:
                    // Street Test, you cannot sechdule it before person passes the written test.
                    // we check if pass writtentest 2.
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
            }

            return true;
        }

        private bool _HandleRetakeApplication()
        {
            // This will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will link it to the appoinment.

            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                // In case the mode was add new and creation mode is retake test we should create a seperate application for it.
                // Then we link it with the appointment.

                // First Create Application 
                clsApplication App = new clsApplication();

                App.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                App.ApplicationDate = DateTime.Now;
                App.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                App.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                //App.ApplicationStatus = clsApplication.enApplicationStatus.New;
                App.LastStatusDate = DateTime.Now;
                App.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                App.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!App.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = App.ApplicationID;
            }
            return true;
        }


        private bool LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            // We compare the current date with the appointment date to set the min date.
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
            }

            return true;
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            // Set Mode to AddNew if user didn't send an ID otherwise set to Update
            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            // Decide if the createion mode is retake test or not based if the person attended this test before
            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
                gbRetakeTestInfo.Enabled = true;
            }
            else
            {
                lblRetakeAppFees.Text = "0";
                lblTitle.Text = "Schedule Test";
                lblRetakeTestAppID.Text = "N/A";
                gbRetakeTestInfo.Enabled = false;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicantFullName.ToString();
            // This will show the trials for this test before  
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
