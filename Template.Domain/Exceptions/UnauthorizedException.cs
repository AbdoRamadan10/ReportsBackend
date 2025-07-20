using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Exceptions
{
    public class UnauthorizedException(string message = "Unauthorized access.")
       : Exception(message)
    {
    }
}
