using MyDVLD_DataAccess;
using System.Data;

namespace MyDVLD_Business
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationID { set; get; }
        public string ApplicationTitle { set; get; }
        public float ApplicationFees { set; get; }

        public clsApplicationType()
        {
            this.ApplicationID = -1;
            this.ApplicationTitle = "";
            this.ApplicationFees = 0;
            Mode = enMode.AddNew;
        }

        private clsApplicationType(int ID, string ApplicationTypeTitel, float ApplicationTypeFees)
        {
            this.ApplicationID = ID;
            this.ApplicationTitle = ApplicationTypeTitel;
            this.ApplicationFees = ApplicationTypeFees;
            Mode = enMode.Update;
        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationID = clsApplicationTypeData.AddNewApplicationType(this.ApplicationTitle, this.ApplicationFees);
            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.ApplicationID, this.ApplicationTitle, this.ApplicationFees);
        }

        public static clsApplicationType Find(int ID)
        {
            string Title = "";
            float Fees = 0;

            if (clsApplicationTypeData.GetApplicationTypeInfoByID(ID, ref Title, ref Fees))
                return new clsApplicationType(ID, Title, Fees);
            else
                return null;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplicationType();

            }
            return false;
        }


    }
}