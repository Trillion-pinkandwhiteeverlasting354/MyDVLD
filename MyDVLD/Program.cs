using MyDVLD.Global_Classes;
using MyDVLD.Login;
using System;
using System.Windows.Forms;

namespace MyDVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmLogin());
            }
            catch (Exception ex)
            {
                EventLogger.LogException(ex);
                MessageBox.Show("Unexpected error occurred.");
            }

        }
    }
}
