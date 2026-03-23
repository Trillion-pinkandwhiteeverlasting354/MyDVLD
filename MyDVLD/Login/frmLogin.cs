using MyDVLD.Classes;
using MyDVLD_Business;
using System.Windows.Forms;

namespace MyDVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, System.EventArgs e)
        {
            string Username = "";

            if (clsGlobal.GetStoredCredentialFromRegistry(ref Username))
            {
                txtUserName.Text = Username;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

            txtPassword.Text = "";
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (User == null)
            {
                txtUserName.Focus();
                MessageBox.Show(
                    "Invalid Username/Password.",
                    "Wrong Credentials",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if (!User.IsActive)
            {
                MessageBox.Show(
                    "Your account is not Active, Please contact your Administrator.",
                    "Inactive Account",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtUserName.Focus();
                return;
            }

            if (chkRememberMe.Checked)
                clsGlobal.RememberUsernameToRegistry(txtUserName.Text.Trim());
            else
                clsGlobal.ClearStoredUsername();

            clsGlobal.CurrentUser = User;

            this.Hide();
            Form frm = new frmMain(this);
            frm.Show();
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
