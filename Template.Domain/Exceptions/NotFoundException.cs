using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Exceptions
{
    public class NotFoundException(string resourceType, string resourceIdentifier)
    : Exception($"{resourceType} with id : {resourceIdentifier} does not exist")
    {
    }
}
