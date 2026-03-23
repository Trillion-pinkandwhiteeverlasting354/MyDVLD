using MyDVLD_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyDVLD.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TestType;

        public frmEditTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }


        private void FrmEditTestType_Load(object sender, System.EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType != null)
            {
                lblTestTypeID.Text = ((int)_TestTypeID).ToString();
                txtTitle.Text = _TestType.Title;
                txtDescription.Text = _TestType.Description;
                txtFees.Text = _TestType.Fees.ToString();
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.Title = txtTitle.Text.Trim();
            _TestType.Description = txtDescription.Text.Trim();
            _TestType.Fees = Convert.ToSingle(txtFees.Text.Trim());

            if (_TestType.Save())
                MessageBox.Show("Test Type Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            // WE CAN MAKE THIS FUNCTIONALITY WITH DIALOG RESULT PROPERTY OF THE BUTTON  ==> SET TO CANCEL
            //this.Close();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox tb = (TextBox)sender;
            bool isEmpty = string.IsNullOrWhiteSpace(tb.Text);

            if (isEmpty)
            {
                e.Cancel = true;
                errorProvider1.SetError(tb, "This field cannot be empty.");
            }
            else
                errorProvider1.SetError(tb, null);
        }

        private void TxtFees_Validating(object sender, CancelEventArgs e)
        {
            // 1) Run shared empty-field validationn
            ValidateEmptyTextBox(sender, e);

            // Stop if failed
            if (e.Cancel)
                return;

            // 2) Additional validation: final number check after editing
            TextBox tb = (TextBox)sender;
            if (!decimal.TryParse(tb.Text, out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(tb, "Invalid number format.");
            }
            else
                errorProvider1.SetError(tb, null);
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


    }
}
