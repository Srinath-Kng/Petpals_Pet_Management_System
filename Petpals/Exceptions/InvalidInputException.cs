using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Exceptions
{
    public class InvalidInputException : System.Exception
    {

        public InvalidInputException(string message) : base(message)
        {
        }
    }
}
