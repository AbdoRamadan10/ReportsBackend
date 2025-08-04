using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.AG_Grid
{
    public class SortModel
    {
        public string ColId { get; set; }
        public string Sort { get; set; } // "asc" or "desc"
    }
}
