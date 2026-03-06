using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_DVLD
{
   public static class clsCountryData
    {
     public static bool getCountryInfoByID(int ID , ref string Name)
        {
            bool IsFound=false;

            SqlConnection   Conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Countries where CountryID = @ID";


            SqlCommand Command = new SqlCommand(query,Conn);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Conn.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if(reader.Read())
                {
                    IsFound = true;
                    Name = (string)reader["CountryName"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound=false;
            }
            finally
            {
                
                Conn.Close();
            }

            return IsFound;
        }

       public static bool getCountryInfoByName( string CountryName , ref int ID)
        {
            bool IsFound = false;

            SqlConnection Conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Countries where CountryName = @CountryName";


            SqlCommand Command = new SqlCommand(query, Conn);

            Command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                Conn.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ID = (int)reader["CountryID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {

                Conn.Close();
            }

            return IsFound;
        }

        public static DataTable getAllCountry()
        {
            DataTable dt = new DataTable();

            string query = "select * from Countries";

            SqlConnection Conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand Command = new SqlCommand(query, Conn);

            try
            {
                Conn.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();


            }
            catch (Exception ex)
            {

            }
            finally { 
                Conn.Close() ;
            }

            return dt;  

        }

    }
}
