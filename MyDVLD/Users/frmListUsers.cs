using MyDVLD_Business;
using System.Data;
using System.Windows.Forms;

namespace MyDVLD.Users
{
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers;

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void FrmListUsers_Load(object sender, System.EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = _dtAllUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 380;

                dgvUsers.Columns[3].HeaderText = "Username";
                dgvUsers.Columns[3].Width = 140;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;
            }
        }

        private void CbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                //if (cbFilterBy.Text == "None")
                //{
                //    txtFilterValue.Enabled = false;
                //}
                //else
                //{
                //    txtFilterValue.Enabled = true;
                //}

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void CbIsActive_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All": break;
                case "Yes": FilterValue = "1"; break;
                case "No": FilterValue = "0"; break;
            }

            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void TxtFilterValue_TextChanged(object sender, System.EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            //switch (cbFilterBy.Text)
            //{
            //    case "User ID": FilterColumn = "UserID"; break;
            //    case "UserName": FilterColumn = "UserName"; break;
            //    case "Person ID": FilterColumn = "PersonID"; break;
            //    case "Full Name": FilterColumn = "FullName"; break;
            //    default: FilterColumn = "None"; break;
            //}
            // ممكن نختصر ال Switch Case كدا
            FilterColumn = cbFilterBy.SelectedItem.ToString().Replace(" ", "");

            // Reset the filters in case nothing selected or filter value contains nothing
            if (FilterColumn == "None" || string.IsNullOrWhiteSpace(txtFilterValue.Text))
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            // Apply filters
            if (FilterColumn != "FullName" && FilterColumn != "UserName")
            {
                // In this case we deal with integer not string.
                if (int.TryParse(txtFilterValue.Text.Trim(), out int id))
                    _dtAllUsers.DefaultView.RowFilter = $"[{FilterColumn}] = {id}";
                else
                    _dtAllUsers.DefaultView.RowFilter = ""; // invalid number
            }
            else
            {
                string safeValue = txtFilterValue.Text.Trim().Replace("'", "''");
                _dtAllUsers.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{safeValue}%'";
            }

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void BtnAddUser_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmAddUpdateUser();
            frm.ShowDialog();
            FrmListUsers_Load(null, null);
        }

        private void TxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // We allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void DgvUsers_DoubleClick(object sender, System.EventArgs e)
        {
            Form frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            FrmListUsers_Load(null, null);
        }

        private void ShowDetailsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void AddNewUserToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmAddUpdateUser();
            frm.ShowDialog();
            FrmListUsers_Load(null, null);
        }

        private void EditToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            FrmListUsers_Load(null, null);
        }

        private void DeleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;

            //(Current User)
            //if ((dgvUsers.CurrentRow.Cells[0].Value).ToString() == (clsGlobal.CurrentUser.UserID).ToString())
            //{
            //    MessageBox.Show("Can't Delete This User because it is The Current User.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmListUsers_Load(null, null);
                }
                else
                    MessageBox.Show("User cannot be deleted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void SendEmailToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void PhoneCallToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


    }
}
