using System;
using System.Data;
using System.Data.SqlClient;


namespace MyDVLD_DataAccess
{
    public class clsPersonData
    {
        // FROM A STUDENT 
        private static void _AddParameterOrNull<T>(SqlCommand cmd, string paramName, object value)
        {
            value = value ?? ""; // If value != null (value = value), otherwisw value = "";
            if (value.ToString() != "")
                cmd.Parameters.AddWithValue(paramName, (T)value);
            else
                cmd.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        // IMPROVED ONE USING CHATGPT
        private static void _AddParameterOrNullGPT<T>(SqlCommand cmd, string paramName, T value)
        {
            if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
                cmd.Parameters.AddWithValue(paramName, DBNull.Value);
            else
                cmd.Parameters.AddWithValue(paramName, value);
        }


        public static bool GetPesonInfoByID(int PersonID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth,
            ref short Gender, ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Person is found
                    IsFound = true;

                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    // ThirdName: allows null in database so we should handle null, here we returned empty string
                    ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    LastName = (string)reader["LastName"];
                    NationalNo = (string)reader["NationalNo"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gender"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    // Email: allows null in database so we should handle null, here we returned empty string
                    Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    // ImagePath: allows null in database so we should handle null, here we returned empty string
                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "NULL";
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


        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName,
             ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref short Gender, ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Person is found
                    IsFound = true;

                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    // ThirdName: allows null in database so we should handle null, here we returned empty string
                    ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    /*
                    // Another version from a student
                    // ThirdName = reader["ThirdName"]?.ToString() ?? "";
                    */
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gender"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    // Email: allows null in database so we should handle null, here we returned empty string
                    Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    // ImagePath: allows null in database so we should handle null, here we returned empty string
                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "NULL";
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


        public static int AddNewPerson(string FirstName, string SecondName,
            string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
            short Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            // This function will return the new PersonID if succeeded and -1 if not.
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO People(FirstName, SecondName, ThirdName, LastName, NationalNo,
                                    DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
                            VALUES (@FirstName, @SecondName, @ThirdName, @LastName, @NationalNo,
                                @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            //_AddParameterOrNull<string>(command, "@ThirdName", ThirdName);
            if (ThirdName != null && ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            if (Email != null && Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);


            if (ImagePath != null && ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int NewID))
                {
                    PersonID = NewID;
                }

            }
            catch { }
            finally { connection.Close(); }

            return PersonID;
        }


        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName,
            string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
            short Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update People 
                             SET FirstName = @FirstName,
                                 SecondName = @SecondName,
                                 ThirdName = @ThirdName,
                                 LastName = @LastName,
                                 NationalNo = @NationalNo,
                                 DateOfBirth = @DateOfBirth,
                                 Gender = @Gender,
                                 Address = @Address,
                                 Phone = @Phone,
                                 Email = @Email,
                                 NationalityCountryID = @NationalityCountryID,
                                 ImagePath = @ImagePath
                             WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ThirdName != null && ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            if (Email != null && Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            if (ImagePath != null && ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


        public static DataTable GetAllPeople()
        {
            DataTable PeopleDataTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName,
                                    People.ThirdName, People.LastName, People.DateOfBirth, 
                                    CASE 
                                        WHEN People.Gender = 0 THEN 'Male'
                                        ELSE 'Female'
                                    END as Gender,
                                    People.Address, People.Phone, People.Email, People.NationalityCountryID, 
                                    Countries.CountryName, People.ImagePath
                             FROM People INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID
                             ORDER BY People.FirstName; ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    PeopleDataTable.Load(reader);
                }
                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return PeopleDataTable;
        }


        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"DELETE People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }


        public static bool IsPersonExist(int PersonID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Found=1 FROM People WHERE PersonID=@PersonID;";

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


        public static bool IsPersonExist(string NationalNo)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Found=1 FROM People WHERE NationalNo=@NationalNo;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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




        public static int AddNewPersonGPT(string FirstName, string SecondName,
            string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
            short Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO People(FirstName, SecondName, ThirdName, LastName, NationalNo,
                            DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
                        VALUES (@FirstName, @SecondName, @ThirdName, @LastName, @NationalNo,
                            @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                        SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Helper local function
                    void AddParam(string name, object value)
                    {
                        command.Parameters.AddWithValue(name, string.IsNullOrEmpty(value?.ToString()) ? DBNull.Value : value);
                    }

                    AddParam("@FirstName", FirstName);
                    AddParam("@SecondName", SecondName);
                    AddParam("@ThirdName", ThirdName);
                    AddParam("@LastName", LastName);
                    AddParam("@NationalNo", NationalNo);
                    AddParam("@DateOfBirth", DateOfBirth);
                    AddParam("@Gender", Gender);
                    AddParam("@Address", Address);
                    AddParam("@Phone", Phone);
                    AddParam("@Email", Email);
                    AddParam("@NationalityCountryID", NationalityCountryID);
                    AddParam("@ImagePath", ImagePath);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int NewID))
                            PersonID = NewID;
                    }
                    catch
                    {
                        // optional: log or handle the exception
                    }
                }
            }

            return PersonID;
        }


    }
}
