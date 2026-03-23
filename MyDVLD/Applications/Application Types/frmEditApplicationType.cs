using MyDVLD.Classes;
using MyDVLD_Business;
using System;
using System.Windows.Forms;

namespace MyDVLD.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationType _ApplicationType;

        public frmEditApplicationType(int AppTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = AppTypeID;
        }


        private void FrmEditApplicationType_Load(object sender, System.EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();

            if (_ApplicationType != null)
            {
                txtTitle.Text = _ApplicationType.ApplicationTitle.ToString();
                txtFees.Text = _ApplicationType.ApplicationFees.ToString();
            }
        }

        private void TxtTitle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
                errorProvider1.SetError(txtTitle, null);

        }

        private void TxtFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
            }
            else
                errorProvider1.SetError(txtFees, null);


            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
                errorProvider1.SetError(txtFees, null);
        }

        private void TxtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If user presses a dot AND a dot already exists ignore it
            if (e.KeyChar == '.' && txtFees.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // Allow ONLY: digits, control keys, and one dot
            e.Handled =
                !char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.';
        }

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationFees = Convert.ToSingle(txtFees.Text.Trim());

            if (_ApplicationType.Save())
                MessageBox.Show("Application Type Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


    }
}
