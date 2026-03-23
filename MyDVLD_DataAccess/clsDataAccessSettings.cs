using System.Configuration;
namespace MyDVLD_DataAccess
{
    static class clsDataAccessSettings
    {
        // Old way (Hard coded)
        //public static string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=123456;";

        // New way using app.config file
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DVLDConnection"].ConnectionString;
            }
        }
    }
}
