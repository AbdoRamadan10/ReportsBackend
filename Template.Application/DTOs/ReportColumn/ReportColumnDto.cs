using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.ReportColumn
{
    public class ReportColumnDto
    {
        //public int Id { get; set; }

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
    }
}
