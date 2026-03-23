using System;
using System.Data;
using System.Data.SqlClient;

namespace MyDVLD_DataAccess
{
    public class clsTestData
    {

        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People Tests TestID = @TestID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Test is found
                    IsFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : "";
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


        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID,
            int TestTypeID, ref int TestID, ref int TestAppointmentID, ref bool TestResult,
            ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT  top 1 Tests.TestID, 
                                Tests.TestAppointmentID, Tests.TestResult, Tests.Notes,
                                Tests.CreatedByUserID, Applications.ApplicantPersonID
                            FROM  LocalDrivingLicenseApplications 
                                INNER JOIN
                                    Tests 
                                INNER JOIN
                                    TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                                    ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                                INNER JOIN
                                    Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                            WHERE (Applications.ApplicantPersonID = @PersonID) 
                            AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                            AND ( TestAppointments.TestTypeID=@TestTypeID)
                            ORDER BY Tests.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Test is found
                    IsFound = true;

                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : "";
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


        public static DataTable GetAllTests()
        {
            DataTable TestsDataTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Tests ORDER BY TestID; ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    TestsDataTable.Load(reader);
                }
                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return TestsDataTable;
        }


        public static int AddNewTest(int TestAppointmentID, bool TestResult,
             string Notes, int CreatedByUserID)
        {
            // This function will return the new TestID if succeeded and -1 if not.
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Tests(TestAppointmentID, TestResult, Notes, CreatedByUserID)
                            VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                            
                            UPDATE TestAppointments SET IsLocked=1
                            WHERE TestAppointmentID = @TestAppointmentID;

                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    TestID = ID;
                }

            }
            catch { }
            finally { connection.Close(); }

            return TestID;
        }


        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult,
             string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update Tests 
                             SET TestAppointmentID = @TestAppointmentID,
                                 TestResult = @TestResult,
                                 Notes = @Notes,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE TestID = @TestID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT  COUNT(Tests.TestID) AS PassedTests
                             FROM Tests INNER JOIN
                             TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                             AND TestResult=1;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte PassedNumber))
                {
                    PassedTestCount = PassedNumber;
                }

            }
            catch { }
            finally { connection.Close(); }

            return PassedTestCount;
        }


    }
}
