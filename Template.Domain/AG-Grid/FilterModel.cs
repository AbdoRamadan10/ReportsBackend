using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.AG_Grid
{
    public class FilterModel
    {
        public string? Type { get; set; }  // "equals", "greaterThan", etc.
        public string? Filter { get; set; }      // Supports strings, numbers, dates
    }
}
