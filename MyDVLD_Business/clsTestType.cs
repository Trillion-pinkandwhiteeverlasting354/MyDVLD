using MyDVLD_DataAccess;
using System.Data;

namespace MyDVLD_Business
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public enTestType ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

        public clsTestType()
        {
            this.ID = enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;

            Mode = enMode.AddNew;
        }

        private clsTestType(enTestType ID, string TestTypeTitle, string TestDescription, float TestFees)
        {
            this.ID = ID;
            this.Title = TestTypeTitle;
            this.Description = TestDescription;
            this.Fees = TestFees;

            Mode = enMode.Update;
        }

        private bool _AddNewTestType()
        {
            this.ID = (enTestType)clsTestTypeData.AddNewTestType(this.Title, this.Description, this.Fees);
            return !(string.IsNullOrEmpty(this.Title));
        }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public static clsTestType Find(enTestType TestTypeID)
        {
            string Title = "", Description = ""; float Fees = 0;

            if (clsTestTypeData.GetTestTypeInfoByID((int)TestTypeID, ref Title, ref Description, ref Fees))
                return new clsTestType(TestTypeID, Title, Description, Fees);
            else

                return null;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTestType();
            }

            return false;
        }


    }
}
