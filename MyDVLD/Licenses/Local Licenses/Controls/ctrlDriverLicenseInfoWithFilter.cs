using MyDVLD_Business;
using System;
using System.Windows.Forms;

namespace MyDVLD.Licenses.Local_Licenses.Controls
{

    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;

            if (OnLicenseSelected != null && FilterEnabled)
            {
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);
            }
        }

        private void TxtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // If Enter is pressed → trigger search
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFind.PerformClick();
            }
        }

        private void TxtLicenseID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // If user didn't type anything => skip validation
            if (txtLicenseID.TextLength == 0)
            {
                errorProvider1.SetError(txtLicenseID, null);
                return;
            }

            // If the user typed but left it empty => show error
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }

            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
        }

    }
}
