using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Entities
{
    public class RoleReport
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
    }
}
