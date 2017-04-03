using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    public class MyExceptions { }

    public class MissingBasketException : Exception { }

    public class WrongAmountException : Exception { }

    public class WrongPriceException : Exception { }

    public class WrongProductException : Exception { }

    public class WrongProductRemovalException : Exception { }

    public class InsufficientFundsException : Exception { }

    public class WrongBillingException : Exception { }
}
