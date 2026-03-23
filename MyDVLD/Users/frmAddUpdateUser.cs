using MyDVLD_Business;
using System.Windows.Forms;

namespace MyDVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _UserID = -1;
        clsUser _User;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
        }


        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                this.Text = "Add New User";
                lblTitle.Text = "Add New User";
                _User = new clsUser();

                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                this.Text = "Update User";
                lblTitle.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            // The following code will not be executed if the person was not found
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            //txtPassword.Text = _User.Password;
            //txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void FrmAddUpdateUser_Load(object sender, System.EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void FrmAddUpdateUser_Activated(object sender, System.EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void BtnPersonInfoNext_Click(object sender, System.EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            // In case we are in AddNew mode, we need to make sure the user select a person
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                // We make sure the selected person is not already a user in the system 
                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
            }
            else // if we are in AddNew mode and he press next without selecting a person
            {
                MessageBox.Show("Please Select a Person first..!", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                // Here we don't continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.IsActive = chkIsActive.Checked;
            //_User.Password = txtPassword.Text.Trim();
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                _User.SetPassword(txtPassword.Text.Trim());
            }

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
            }
            else
                MessageBox.Show("Error: Data is not saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void TxtUserName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "You should have a Username.");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }


            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "Username is already used by another person.");
                }
                else
                    errorProvider1.SetError(txtUserName, null);
            }
            else
            {
                // In case the user tried to use already taken username
                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "Username is already used by another person.");
                }
                else
                    errorProvider1.SetError(txtUserName, null);
            }
        }

        private void TxtPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtPassword, "Password cannot be empty.");
                    return;
                }
            }

            errorProvider1.SetError(txtPassword, null);
        }

        private void TxtConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_Mode == enMode.AddNew || !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be empty.");
                    return;
                }

                if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtConfirmPassword, "Passwords need to match.");
                    return;
                }
            }

            errorProvider1.SetError(txtConfirmPassword, null);
        }


    }
}
