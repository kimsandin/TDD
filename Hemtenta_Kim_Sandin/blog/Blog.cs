using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    public class Blog : IBlog
    {
        private IAuthenticator _authenticator;

        private User loggedInUser;

        public Blog(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        
        

    // True om användaren är inloggad (behöver
    // inte testas separat)

    public bool UserIsLoggedIn
        {
            get
            {
                if (loggedInUser != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void LoginUser(User u)
        {
            if (u != null)
            {
                User user = _authenticator.GetUserFromDatabase(u.Name);
                if (u.Name == user.Name)
                {
                    if (u.Password == user.Password)
                    {
                        loggedInUser = user;
                    }
                    else
                    {
                        throw new NoAuthenticationException();
                    }
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
            else
            {
                throw new NoUserNameGivenException();
            }
        }

        public void LogoutUser(User u)
        {
            if (u != null)
            {
                User user = _authenticator.GetUserFromDatabase(u.Name);
                if (u.Name == user.Name)
                {
                    loggedInUser = null;
                }               
                else
                {
                    throw new UserNotFoundException();
                }
            }
            else
            {
                throw new NoUserExistsException();
            }
        }

        public bool PublishPage(Page p)
        {
            //Kolla om både title och content är korretkt och utgör ett objekt, 
            //eller något är null, om inte null - är en user inloggad?
            if (p != null)
            {
                if (p.Title != null)
                {
                    if (p.Content != null)
                    {
                        if (UserIsLoggedIn == true)
                        {
                            return true;
                        }

                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new PageIsNotCorrectObjektException();
                    }
                }
                else
                {
                    throw new PageIsNotCorrectObjektException();
                }
            }
            else
            {
                throw new PageIsNotCorrectObjektException();
            }
        }

        public int SendEmail(string address, string caption, string body)
        {
            if (!String.IsNullOrEmpty(address) && !String.IsNullOrEmpty(caption) && !String.IsNullOrEmpty(body))
            {
                if (emailIsValid(address))
                {
                    if (UserIsLoggedIn == true)
                    {
                        return 1;
                    }

                    else
                    {
                        return 0;
                    }
                }
                //Om email-adressen inte har rätt format
                throw new WrongEmailContentException();
            }

            else
            {
                //Om någon av parametrarna har ett felaktigt värde.
                throw new WrongEmailContentException();
            }
        }



        public static bool emailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
