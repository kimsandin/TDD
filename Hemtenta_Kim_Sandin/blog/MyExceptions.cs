using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017
{
    public class MyExceptions : Exception
    {
    }

    public class WrongUserException : Exception
    {
    }

    public class NoAuthenticationException : Exception
    {
    }

    public class UserNotFoundException : Exception
    {
    }

    public class NoUserNameGivenException : Exception
    {
    }

    public class NoUserExistsException : Exception
    {
    }

    public class UserIsLoggedOutExeption : Exception
    {
    }

    public class PageIsNotCorrectObjektException : Exception
    {
    }

    public class WrongEmailContentException : Exception
    {
    }
}
