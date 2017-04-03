using System;
using HemtentaTdd2017.blog;
using System.Collections.Generic;
using System.Linq;

namespace HemtentaTdd2017.Test
{
    public class MockAuthenticator : IAuthenticator
    {
        public static List<User> DbUsers = new List<User>() { new User("Kim") { Password = "123456" }, new User("Annie") { Password = "qazwsx" }, new User("Joel") { Password = "qwerty" }, new User("Ann") { Password = "Stig" }, new User("Eva") { Password = "Trassel" } };
        public User GetUserFromDatabase(string username)
        {
            
            //Search Db for user with name of "username"
            //Returns a valid user-object if found, else "null"
            if (!string.IsNullOrEmpty(username))
            {
                var user = DbUsers.Where(u => u.Name == username).FirstOrDefault();
                return user;
            }
            else if (username == null)
            {
                return null;
            }
            else
            {
                throw new UserNotFoundException();

            }
        }
    }
}