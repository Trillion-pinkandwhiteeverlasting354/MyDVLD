using System;
using System.Windows.Forms;

namespace MyDVLD.Users
{
    public partial class frmUserInfo : Form
    {
        private int _UserID;

        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void FrmUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
