using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Exception
{
    public class NullReferenceExceptionHandling : System.Exception
    {
        public NullReferenceExceptionHandling(string message) : base(message)
        {
        }
    }
}
