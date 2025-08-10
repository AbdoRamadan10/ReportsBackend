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
        public string? Field { get; set; }
        public string? HeaderName { get; set; }
        public bool Sortable { get; set; } = true;
        public string? Filter { get; set; }
        public bool Resizable { get; set; } = true;
        public bool FloatingFilter { get; set; } = false;
        public bool RowGroup { get; set; } = false;
        public bool Hide { get; set; } = false;
        public bool IsMaster { get; set; } = false;
        public int Sort { get; set; } = 0;
        public int ReportId { get; set; }
        public Report Report { get; set; }

    }
}
