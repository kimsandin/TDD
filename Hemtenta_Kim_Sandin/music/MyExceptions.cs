using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class MyExceptions { }

    // Se till att databasklassen kastar dessa exceptions
    // när den ska göra det enligt specen.
    public class DatabaseClosedException : Exception { }
    public class DatabaseAlreadyOpenException : Exception { }

    public class SearchFailedException : Exception { }
}
