using System.Windows.Forms;

namespace MyDVLD.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID = -1;

        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }

        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void FrmShowPersonLicenseHistory_Load(object sender, System.EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.FilterEnabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void CtrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;

            if (_PersonID == -1)
                ctrlDriverLicenses1.Clear();
            else
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
        }


        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
