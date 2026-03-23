using System.Windows.Forms;

namespace MyDVLD.People
{
    public partial class frmFindPerson : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            // Trigger the event to send data back to the caller form. 
            DataBack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID);
        }
    }
}
