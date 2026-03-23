using MyDVLD_Business;
using System;
using System.Windows.Forms;

namespace MyDVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        // Difine a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);  // Raise the event with the parameter
            }
        }


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }


        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }


        private int _PersonID = -1;
        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }


        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }


        private void CtrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void _FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;

                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);  // Raise the event with a parameter
            }
        }

        private void CbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                // Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            _FindNow();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void TxtFilterValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtFilterValue.Text.Trim() == "")
                return;

            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void BtnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;  // Subscribe to the event
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text += PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        private void TxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (Charater code 13)
            // if (e.KeyChar == (char)13)
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFind.PerformClick();
            }

            // This will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


    }
}
