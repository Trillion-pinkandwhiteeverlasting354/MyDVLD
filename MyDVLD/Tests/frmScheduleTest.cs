using MyDVLD_Business;
using System.Windows.Forms;

namespace MyDVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _AppointmentID = -1;


        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID, int AppointmentID = -1)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _AppointmentID = AppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void FrmScheduleTest_Load(object sender, System.EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestTypeID;
            ctrlScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _AppointmentID);
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
