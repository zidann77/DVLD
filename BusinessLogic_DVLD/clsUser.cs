using DataAccess_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_DVLD
{
    public class clsUser
    {
        enum enMode { Addnew = 0, Update = 1 }

        enMode Mode = enMode.Addnew;
        public int id { get; set; }
        public int Personid { get; set; }
        public  bool isActive { get; set; }

        public clsPerson PersonInfo { get; set; }

        public string UserName { get; set; }


        public string Password { get; set; }

        public clsUser()
        {
            id = -1;
             Personid = -1;
            isActive = true;
            PersonInfo = null;
            UserName = null;
            Mode = enMode.Addnew;
            
        }

        public clsUser(int id, int personid, string UserPasssword, bool isActive, string name)
        {
            this.id = id;
            this.Personid = personid;
            this.isActive = isActive;
            this.PersonInfo = clsPerson.Find(personid);
            this.Password = UserPasssword;
            this.UserName = name;

            Mode = enMode.Update;
        }

        public static clsUser FindUser(int UserID)
        {
            string Name = null;
            int personID = -1;
            string Userpassword = null;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUserID(UserID, ref personID, ref Name, ref Userpassword, ref IsActive))
            {
                return new clsUser(UserID, personID, Userpassword, IsActive, Name);
            }

            else
                return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByPersonID
                                (PersonID, ref UserID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserID, UserID, Password, IsActive , UserName);
            else
                return null;
        }


        public static clsUser FindUser(string UserName, string Password)
        {
            string Name = null;
            int personID = -1;
            int UserID = -1;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUsernameAndPassword(UserName, Password, ref UserID, ref personID, ref IsActive))
            {
                return new clsUser(UserID , personID , Password, IsActive, Name);
            }

            else
                return null;
        }

        private bool _AddNewUser()
        {
            this.id = clsUserData.AddNewUser(this.Personid, this.UserName, this.Password, this.isActive);

            return this.id != -1;
        }

        private bool _UPdateUser()
        {
            if(clsUserData.UpdateUser(this.id, this.Personid, this.UserName, this.Password, this.isActive))
            {
                return true;
            }
            
            return false;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Addnew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UPdateUser();
            }
            return false;
        }





        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }


    }
}
