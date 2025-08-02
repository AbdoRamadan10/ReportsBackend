using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Entities
{
    public class ReportColumn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; } // e.g., "string", "int", "date"

        public int ReportId { get; set; }
        public Report Report { get; set; }

    }
}
