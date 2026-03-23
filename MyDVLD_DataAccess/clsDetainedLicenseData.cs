using System;
using System.Data;
using System.Data.SqlClient;

namespace MyDVLD_DataAccess
{
    public class clsDetainedLicenseData
    {

        public static bool GetDetainedLicenseInfoByID(int DetainID,
            ref int LicenseID, ref DateTime DetainDate, ref float FineFees,
            ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
            ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = reader["ReleaseDate"] != DBNull.Value ? (DateTime)reader["ReleaseDate"] : DateTime.MaxValue; ;
                    ReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? (int)reader["ReleasedByUserID"] : -1;
                    ReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? (int)reader["ReleaseApplicationID"] : -1;
                }
                else
                {
                    // The record was not found
                    IsFound = false;
                }

                reader.Close();
            }
            catch { IsFound = false; }
            finally { connection.Close(); }

            return IsFound;
        }


        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID,
            ref int DetainID, ref DateTime DetainDate, ref float FineFees,
            ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
            ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT TOP 1 * FROM DetainedLicenses 
                             WHERE LicenseID = @LicenseID
                             ORDER BY DetainID DESC;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = reader["ReleaseDate"] != DBNull.Value ? (DateTime)reader["ReleaseDate"] : DateTime.MaxValue; ;
                    ReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? (int)reader["ReleasedByUserID"] : -1;
                    ReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? (int)reader["ReleaseApplicationID"] : -1;
                }
                else
                {
                    // The record was not found
                    IsFound = false;
                }

                reader.Close();
            }
            catch { IsFound = false; }
            finally { connection.Close(); }

            return IsFound;
        }


        public static DataTable GetAllDetainedLicenses()
        {
            DataTable DetainedLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM DetainedLicenses_View ORDER BY IsReleased, DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    DetainedLicenses.Load(reader);
                }

                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return DetainedLicenses;
        }


        public static int AddNewDetainedLicense(int LicenseID,
            DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO DetainedLicenses(LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased)
                            VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, 0);
                            SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int NewRecord))
                {
                    DetainID = NewRecord;
                }

            }
            catch { }
            finally { connection.Close(); }

            return DetainID;
        }


        public static bool UpdateDetainedLicense(int DetainID, int LicenseID,
            DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update DetainedLicenses
                             SET LicenseID = @LicenseID,
                                 DetainDate = @DetainDate,
                                 FineFees = @FineFees,
                                 CreatedByUserID = @CreatedByUserID
                            WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


        public static bool ReleaseDetainedLicense(int DetainID,
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update DetainedLicenses
                             SET IsReleased = 1,
                                 ReleaseDate = @ReleaseDate,
                                 ReleasedByUserID = @ReleasedByUserID,
                                 ReleaseApplicationID = @ReleaseApplicationID
                            WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT IsDetained=1 
                             FROM detainedLicenses
                             WHERE LicenseID=@LicenseID 
                             AND IsReleased=0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsDetained = Convert.ToBoolean(result);
                }
            }
            catch { }
            finally { connection.Close(); }

            return IsDetained;
        }


    }
}
