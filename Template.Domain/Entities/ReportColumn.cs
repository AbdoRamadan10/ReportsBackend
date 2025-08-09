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
        //public string Name { get; set; }
        //public string DisplayName { get; set; }
        //public string DataType { get; set; } // e.g., "string", "int", "date"
        public string? Field { get; set; }
        public string? HeaderName { get; set; }
        public bool Sortable { get; set; }
        public string Filter { get; set; }
        public bool Resizable { get; set; }
        public bool FloatingFilter { get; set; }
        public bool RowGroup { get; set; }
        public bool Hide { get; set; }
        public bool IsMaster { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }

    }
}
