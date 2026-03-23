using MyDVLD_Business;
using System.Windows.Forms;

namespace MyDVLD.Users
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;


        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void FrmChangePassword_Load(object sender, System.EventArgs e)
        {
            _ResetDefualtValues();

            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                // Here the form is not valid and we close it
                MessageBox.Show("Could not Find User with id = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                // Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_User.VerifyPassword(txtCurrentPassword.Text.Trim()))
            {
                MessageBox.Show("Current password is incorrect.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentPassword.Focus();
                return;
            }

            // Set new password
            _User.SetPassword(txtNewPassword.Text.Trim());

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.", "Saved.",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualtValues();
            }
            else
            {
                MessageBox.Show("An error occurred, password was not changed.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void TxtCurrentPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password cannot be empty.");
                return;
            }

            errorProvider1.SetError(txtCurrentPassword, null);
        }

        private void TxtNewPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be empty.");
                return;
            }

            if (txtNewPassword.Text.Trim() == txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be the same as current password.");
                return;
            }

            errorProvider1.SetError(txtNewPassword, null);
        }

        private void TxtConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtNewPassword.Text.Trim() == txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be the same as current password.");
                return;
            }

            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password does not match New Password!");
                return;
            }

            errorProvider1.SetError(txtConfirmPassword, null);
        }


    }
}
