using System;
using System.Windows.Forms;

namespace MyDVLD.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        private int _InternationalLicenseID;

        public frmShowInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }


        private void FrmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.LoadInfo(_InternationalLicenseID);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
