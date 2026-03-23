using System;
using System.Data;
using System.Data.SqlClient;

namespace MyDVLD_DataAccess
{
    public class clsApplicationTypeData
    {
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID,
            ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch { IsFound = false; }
            finally { connection.Close(); }

            return IsFound;

        }


        public static DataTable GetAllApplicationTypes()
        {
            DataTable ApplicationsDataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes ORDER BY ApplicationTypeID;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    ApplicationsDataTable.Load(reader);
                }

                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return ApplicationsDataTable;
        }


        public static int AddNewApplicationType(string Title, float Fees)
        {
            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO ApplicationTypes(ApplicationTypeTitle, ApplicationFees)
                            VALUES (@Title, @Fees);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int NewID))
                {
                    ApplicationTypeID = NewID;
                }

            }
            catch { }
            finally { connection.Close(); }

            return ApplicationTypeID;
        }


        public static bool UpdateApplicationType(int ApplicationTypeID, string Title, float Fees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE ApplicationTypes
                             SET ApplicationTypeTitle = @Title,
                                 ApplicationFees = @Fees
                             WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


    }
}
