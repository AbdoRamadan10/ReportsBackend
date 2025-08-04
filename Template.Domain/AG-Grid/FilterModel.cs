using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.AG_Grid
{
    public class FilterModel
    {
        public string? FilterType { get; set; }  // "equals", "greaterThan", etc.
        public string? Filter { get; set; }      // Supports strings, numbers, dates
        public string? Type { get; set; }       // "text", "number", "date" (AG Grid-specific)
    }
}
