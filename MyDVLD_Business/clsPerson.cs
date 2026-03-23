using MyDVLD_DataAccess;
using System;
using System.Data;
using System.IO;

namespace MyDVLD_Business
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName;
        //{
        //get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        //}
        public string NationalNo { set; get; }
        public DateTime DateOfBirth { set; get; }
        public short Gender { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }

        public clsCountry CountryInfo;

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        private string _SetFullName()
        {
            if (!string.IsNullOrEmpty(ThirdName))
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            else
                return FirstName + " " + SecondName + " " + LastName;
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, string NationalNo, DateTime DateOfBirth, short Gender,
             string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.FullName = _SetFullName();
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);

            Mode = enMode.Update;
        }


        private bool _AddNewPerson()
        {
            // Call DataAccess Layer
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.NationalNo, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email,
                this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            // Call DataAccess Layer
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.NationalNo, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email,
                this.NationalityCountryID, this.ImagePath);
        }

        public static clsPerson Find(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gender = 0;

            bool IsFound = clsPersonData.GetPesonInfoByID
                            (
                                PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                                ref NationalNo, ref DateOfBirth, ref Gender, ref Address, ref Phone,
                                ref Email, ref NationalityCountryID, ref ImagePath
                            );

            if (IsFound)
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName,
                                    NationalNo, DateOfBirth, Gender, Address, Phone, Email,
                                    NationalityCountryID, ImagePath);
            else
                return null;

        }

        public static clsPerson Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonID = -1, NationalityCountryID = -1;
            short Gender = 0;

            bool IsFound = clsPersonData.GetPersonInfoByNationalNo
                            (
                                NationalNo, ref PersonID, ref FirstName, ref SecondName, ref ThirdName,
                                ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email,
                                ref NationalityCountryID, ref ImagePath
                            );

            if (IsFound)
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName,
                                    NationalNo, DateOfBirth, Gender, Address, Phone, Email,
                                    NationalityCountryID, ImagePath);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool DeletePerson(int PersonID)
        {
            string ImagePath = Find(PersonID).ImagePath;

            if (File.Exists(ImagePath))
                File.Delete(ImagePath);

            return clsPersonData.DeletePerson(PersonID);
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static bool IsPersonExist(string NationalID)
        {
            return clsPersonData.IsPersonExist(NationalID);
        }

    }
}
