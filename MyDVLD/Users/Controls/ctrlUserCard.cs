using MyDVLD_Business;
using System.Windows.Forms;

namespace MyDVLD.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID = -1;
        private clsUser _User;

        public int UserID
        {
            get { return _UserID; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void _ResetUserInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "???";
            lblUserName.Text = "???";
            lblIsActive.Text = "???";
        }
        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";
        }

        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindByUserID(UserID);

            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }
    }
}
