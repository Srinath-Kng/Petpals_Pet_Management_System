using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Exception
{
    public class InvalidPetAgeException : System.Exception
    {
        public InvalidPetAgeException(string message) : base(message)
        {
            
        }
        public int InvalidAge { get; set; } 
    }
}
