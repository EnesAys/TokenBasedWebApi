using LengerProje.DATA;
using LengerProje.Helpers;
using LengerProje.Helpers.Enums;
using LengerProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LengerProje.Repositories
{
    public class UserRepository
    {
        private ModelEntities db;
        public UserRepository()
        {
            db = new ModelEntities();
        }
        public Users FindUser(string username) {
           
                var user = db.Users.FirstOrDefault(x => x.Username == username);
                return user;
        }
        public Users FindUser(string username,string password)
        {

            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password==password);
            return user;
        }

        public bool AddUser(User modelUser)
        {
            var users = new Users();
            try
            {
                users.ID = Guid.NewGuid();
                users.Name = modelUser.Name;
                users.Surname = modelUser.Surname;
                users.Email = modelUser.Email;
                users.Username = modelUser.Username;
                users.Password = modelUser.Password;
                users.RoleID = Convert.ToInt32(RoleTypes.User);
                db.Users.Add(users);
                db.SaveChanges();
                Logger.SendLog("UserRepository/AddUser", modelUser, false, LogTypes.Response);
                return true;
            }
            catch (Exception ex)
            {
                Logger.SendLog("UserRepository/AddUser", modelUser+" "+ex.ToString(), true, LogTypes.Response);
                return false;
            }
        }

        public bool AddSuperUser(User modelUser)
        {
            var users = new Users();
            try
            {
                users.ID = Guid.NewGuid();
                users.Name = modelUser.Name;
                users.Surname = modelUser.Surname;
                users.Email = modelUser.Email;
                users.Username = modelUser.Username;
                users.Password = modelUser.Password;
                users.RoleID = Convert.ToInt32(RoleTypes.SuperUser);
                db.Users.Add(users);
                db.SaveChanges();
                Logger.SendLog("UserRepository/AddSuperUser", modelUser, false, LogTypes.Response);
                return true;
            }
            catch (Exception ex)
            {
                Logger.SendLog("UserRepository/AddSuperUser", modelUser + " " + ex.ToString(), true, LogTypes.Response);
                return false;
            }
        }


    }
}