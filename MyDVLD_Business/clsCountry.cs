using MyDVLD_DataAccess;
using System.Data;

namespace MyDVLD_Business
{
    public class clsCountry
    {
        public int ID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
        }

        private clsCountry(int ID, string CountryName)
        {
            this.ID = ID;
            this.CountryName = CountryName;
        }


        public static clsCountry Find(int ID)
        {
            string CountryName = "";

            if (clsCountryData.GetCountryInfoByID(ID, ref CountryName))

                return new clsCountry(ID, CountryName);
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;

            if (clsCountryData.GetCountryInfoByName(CountryName, ref CountryID))

                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }


    }
}
