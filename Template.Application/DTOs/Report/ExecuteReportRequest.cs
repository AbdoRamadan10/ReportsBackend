using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.Report
{
    public class ExecuteReportRequest
    {
        public Dictionary<string, object> Parameters { get; set; }
        public Dictionary<string, object> FilterParameters { get; set; }
    }
}
