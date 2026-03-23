using System.Data;
using System.Data.SqlClient;

namespace MyDVLD_DataAccess
{
    public class clsCountryData
    {

        public static bool GetCountryInfoByID(int CountryID, ref string CountryName)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Country is found
                    IsFound = true;
                    CountryName = (string)reader["CountryName"];
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch { IsFound = false; }
            finally { connection.Close(); }

            return IsFound;
        }


        public static bool GetCountryInfoByName(string CountryName, ref int CountryID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Country is found
                    IsFound = true;
                    CountryID = (int)reader["CountryID"];
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch { IsFound = false; }
            finally { connection.Close(); }

            return IsFound;
        }


        public static DataTable GetAllCountries()
        {
            DataTable CountriesDataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries ORDER BY CountryName;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    CountriesDataTable.Load(reader);
                }

                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return CountriesDataTable;
        }


    }
}
