using System.Data;
using System.Data.SqlClient;

namespace MyDVLD_DataAccess
{
    public class clsUserData
    {

        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName,
            ref string PasswordHash, ref string PasswordSalt, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT PersonID, UserName, PasswordHash, PasswordSalt, IsActive
                             FROM Users 
                             WHERE UserID = @UserID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // User is found
                    IsFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    PasswordHash = (string)reader["PasswordHash"];
                    PasswordSalt = (string)reader["PasswordSalt"];
                    IsActive = (bool)reader["IsActive"];
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


        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
            ref string PasswordHash, ref string PasswordSalt, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT UserID, UserName, PasswordHash, PasswordSalt, IsActive
                             FROM Users
                             WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // User is found
                    IsFound = true;

                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    PasswordHash = (string)reader["PasswordHash"];
                    PasswordSalt = (string)reader["PasswordSalt"];
                    IsActive = (bool)reader["IsActive"];
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


        public static bool GetUserInfoByUsername(string UserName,
            ref int UserID, ref int PersonID, ref string PasswordHash,
            ref string PasswordSalt, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // User is found
                    IsFound = true;

                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    PasswordHash = (string)reader["PasswordHash"];
                    PasswordSalt = (string)reader["PasswordSalt"];
                    IsActive = (bool)reader["IsActive"];
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


        public static int AddNewUser(int PersonID, string UserName,
             string PasswordHash, string PasswordSalt, bool IsActive)
        {
            // This function will return the new UserID if succeeded and -1 if not.
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Users (PersonID, UserName, PasswordHash, PasswordSalt, IsActive)
                            VALUES (@PersonID, @UserName, @PasswordHash, @PasswordSalt, @IsActive);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            command.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int NewID))
                {
                    UserID = NewID;
                }

            }
            catch { }
            finally { connection.Close(); }

            return UserID;
        }


        public static bool UpdateUser(int UserID, int PersonID, string UserName,
             bool IsActive)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update Users 
                             SET PersonID = @PersonID,
                                 UserName = @UserName,
                                 IsActive = @IsActive
                             WHERE UserID = @UserID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


        public static bool UpdateUserWithPassword(int UserID, int PersonID, string UserName,
             string PasswordHash, string PasswordSalt, bool IsActive)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update Users 
                             SET PersonID = @PersonID,
                                 UserName = @UserName,
                                 PasswordHash = @PasswordHash,
                                 PasswordSalt = @PasswordSalt,
                                 IsActive = @IsActive
                             WHERE UserID = @UserID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            command.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


        public static DataTable GetAllUsers()
        {
            DataTable UsersDataTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            //string query = @" SELECT Users.UserID, Users.PersonID,
            //                        People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName, '') + ' ' + People.LastName AS FullName,
            //                        Users.UserName, Users.IsActive
            //                  FROM Users INNER JOIN
            //                  People ON Users.PersonID = People.PersonID";
            string query = @"SELECT 
                                Users.UserID, 
                                Users.PersonID,
                                CONCAT_WS(' ', People.FirstName, People.SecondName, People.ThirdName, People.LastName) AS FullName,
                                Users.UserName, 
                                Users.IsActive
                            FROM Users
                            INNER JOIN People ON Users.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    UsersDataTable.Load(reader);
                }
                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return UsersDataTable;
        }


        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"DELETE Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


        public static bool IsUserExist(int UserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Found=1 FROM Users WHERE UserID=@UserID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    IsFound = true;
                }
            }
            catch { IsFound = false; }
            finally { connection.Close(); }

            return IsFound;
        }


        public static bool IsUserExist(string UserName)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Found=1 FROM Users WHERE UserName=@UserName;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    IsFound = true;
                }
            }
            catch { IsFound = false; }
            finally { connection.Close(); }

            return IsFound;
        }


        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Found=1 FROM Users WHERE PersonID=@PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    IsFound = true;
                }
            }
            catch { IsFound = false; }
            finally { connection.Close(); }

            return IsFound;
        }


        public static bool ChangePassword(int UserID, string PasswordHash, string PasswordSalt)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update Users 
                             SET PasswordHash  = @PasswordHash,
                                 PasswordSalt = @PasswordSalt
                             WHERE UserID = @UserID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            command.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        /* Temporary migration methods */
        //public static DataTable GetUsersWithoutHash()
        //{
        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"SELECT UserID, Password
        //             FROM Users
        //             WHERE PasswordHash = '' OR PasswordSalt = ''";

        //    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        //    DataTable dt = new DataTable();
        //    adapter.Fill(dt);

        //    return dt;
        //}

        //public static bool UpdatePasswordOnly(int userID, string hash, string salt)
        //{
        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"UPDATE Users
        //             SET PasswordHash = @Hash,
        //                 PasswordSalt = @Salt
        //             WHERE UserID = @UserID";

        //    SqlCommand cmd = new SqlCommand(query, connection);
        //    cmd.Parameters.AddWithValue("@UserID", userID);
        //    cmd.Parameters.AddWithValue("@Hash", hash);
        //    cmd.Parameters.AddWithValue("@Salt", salt);

        //    try
        //    {
        //        connection.Open();
        //        return cmd.ExecuteNonQuery() > 0;
        //    }
        //    catch { return false; }
        //    finally { connection.Close(); }
        //}


    }
}
