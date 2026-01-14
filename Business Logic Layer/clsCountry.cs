using DataAccess_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_DVLD
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }

        private clsCountry(int ID, string Name)
        {
            CountryID = ID;
            CountryName = Name;

        }

        public static clsCountry FindByID(int ID)
        {

            string countryName = "";

            if (clsCountryData.getCountryInfoByID(ID, ref countryName))
            {
                return new clsCountry(ID, countryName);
            }
            else
                return null;
        }


        public static clsCountry FindByName(string Name)
        {

           int IDNumber = -1;

            if (clsCountryData.getCountryInfoByName(Name,ref IDNumber))
            {
                return new clsCountry(IDNumber, Name);
            }
            else
                return null;
        }


        public static DataTable getAllCountries()
        {
            return clsCountryData.getAllCountry();
        }


    }
}
