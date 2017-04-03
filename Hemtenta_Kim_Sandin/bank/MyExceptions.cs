using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.bank
{
    public class MyExceptions { }

    // Kastas när beloppet på kontot inte tillåter
    // ett uttag eller en överföring
    public class InsufficientFundsException : Exception { }

    // Kastas för ogiltiga siffror
    public class IllegalAmountException : Exception { }

    // Kastas om en operation på kontot inte tillåts av någon
    // anledning som inte de andra exceptions täcker in
    public class OperationNotPermittedException : Exception { }
    
}
