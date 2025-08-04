using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Helpers
{
    public class FilterCriteria
    {
        public string ColumnName { get; set; }  // Column to filter (e.g., "Name")
        public string Operator { get; set; }    // Operator (e.g., "Equals", "Contains")
        public object Value { get; set; }       // Filter value (e.g., "John")
    }
}
