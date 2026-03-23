using MyDVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyDVLD.People
{
    public partial class frmListPeople : Form
    {
        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();
        //Here we only select the columns that we want to show in the grid
        private static DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gender", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gender", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");
            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void FrmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

            if (dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gender";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;
            }
        }

        private void ShowDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
            //frm.DataBack += _RefreshPeopleList;
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are you sure you want to delete Person [" + PersonID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void PhoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void DgvPeople_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void TxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumnBy = "";

            // Map Selected Filter to real Column name 
            switch (cbFilterBy.Text.Trim())
            {
                case "Person ID": FilterColumnBy = "PersonID"; break;
                case "National No.": FilterColumnBy = "NationalNo"; break;
                case "First Name": FilterColumnBy = "FirstName"; break;
                case "Second Name": FilterColumnBy = "SecondName"; break;
                case "Third Name": FilterColumnBy = "ThirdName"; break;
                case "Last Name": FilterColumnBy = "LastName"; break;
                case "Nationality": FilterColumnBy = "CountryName"; break;
                case "Gendor": FilterColumnBy = "GendorCaption"; break;
                case "Phone": FilterColumnBy = "Phone"; break;
                case "Email": FilterColumnBy = "Email"; break;
                default: FilterColumnBy = "None"; break;
            }
            // ممكن نختصر ال Switch Case كدا
            //cbFilterBy.SelectedItem.ToString().Replace(" ", "");

            // Reset the filters in case nothing selected or filter value conains nothing.
            if (FilterColumnBy == "None" || string.IsNullOrWhiteSpace(txtFilterValue.Text))
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            // Apply filters
            if (FilterColumnBy == "PersonID")
            {
                // In this case we deal with integer not string.
                if (int.TryParse(txtFilterValue.Text.Trim(), out int id))
                    _dtPeople.DefaultView.RowFilter = $"[{FilterColumnBy}] = {id}";
                else
                    _dtPeople.DefaultView.RowFilter = ""; // invalid number
            }
            else
            {
                string safeValue = txtFilterValue.Text.Trim().Replace("'", "''");
                _dtPeople.DefaultView.RowFilter = $"[{FilterColumnBy}] LIKE '{safeValue}%'";
            }

            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void CbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtFilterValue.Visible = (cbFilterBy.Text != "None");
            // This Ignores Case Sensitive
            txtFilterValue.Visible = !cbFilterBy.Text.Trim().Equals("None", StringComparison.OrdinalIgnoreCase);

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void TxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits if filtering by Person ID
            if (cbFilterBy.Text.Trim().Equals("Person ID", StringComparison.OrdinalIgnoreCase))
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void BtnAddPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
