using MyDVLD_DataAccess;
using System.Data;

namespace MyDVLD_Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public string UserName { get; set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        private bool _PasswordChanged = false;
        public bool IsActive { get; set; }


        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.PasswordHash = "";
            this.PasswordSalt = "";
            this.IsActive = true;
            Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID, string Username, string passwordHash, string passwordSalt, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = Username;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        // ================= Password Logic =================
        public void SetPassword(string plainPassword)
        {
            PasswordSalt = clsSecurity.GenerateSalt();
            PasswordHash = clsSecurity.HashPassword(plainPassword, PasswordSalt);
            _PasswordChanged = true;
        }

        public bool VerifyPassword(string plainPassword)
        {
            return clsSecurity.VerifyPassword(plainPassword, PasswordHash, PasswordSalt);
        }

        // ================= DAL Interaction =================

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.PasswordHash, this.PasswordSalt, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            bool result;

            if (_PasswordChanged)
            {
                result = clsUserData.UpdateUserWithPassword(
                    this.UserID,
                    this.PersonID,
                    this.UserName,
                    this.PasswordHash,
                    this.PasswordSalt,
                    this.IsActive);

                if (result)
                    _PasswordChanged = false;

                return result;
            }

            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.IsActive);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        // ================= Find Methods =================
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", hash = "", salt = "";
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref hash, ref salt, ref IsActive);

            if (IsFound)
            {
                return new clsUser(UserID, PersonID, UserName, hash, salt, IsActive);
            }
            return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", hash = "", salt = "";
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref hash, ref salt, ref IsActive);

            if (IsFound)
            {
                return new clsUser(UserID, PersonID, UserName, hash, salt, IsActive);
            }
            return null;
        }

        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            string hash = "", salt = "";
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUsername(UserName, ref UserID, ref PersonID, ref hash, ref salt, ref IsActive);

            if (!IsFound)
                return null;

            clsUser user = new clsUser(UserID, PersonID, UserName, hash, salt, IsActive);

            return user.VerifyPassword(Password) ? user : null;
        }

        // ================= Other Helpers =================
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        /* Temporary migration methods */
        //public static void MigratePlainPasswords()
        //{
        //    DataTable dt = clsUserData.GetUsersWithoutHash();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        int userID = (int)row["UserID"];
        //        string plainPassword = row["Password"].ToString();

        //        string salt = clsSecurity.GenerateSalt();
        //        string hash = clsSecurity.HashPassword(plainPassword, salt);

        //        clsUserData.UpdatePasswordOnly(userID, hash, salt);
        //    }
        //}

    }
}
