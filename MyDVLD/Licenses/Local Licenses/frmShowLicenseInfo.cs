using System.Windows.Forms;

namespace MyDVLD.Licenses.Local_Licenses
{
    public partial class frmShowLicenseInfo : Form
    {
        private int _LicenseID;

        public frmShowLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void FrmShowLicenseInfo_Load(object sender, System.EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadInfo(_LicenseID);
        }
    }
}
