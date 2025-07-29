using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Entities
{
    public class ReportData
    {
        public List<Dictionary<string, object>> Rows { get; set; }
        public List<ReportColumn> Columns { get; set; }
    }
}
