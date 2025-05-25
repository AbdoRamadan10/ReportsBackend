using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.Report
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int PrivilegeId { get; set; }
        public string PrivilegeName { get; set; } = string.Empty;
    }
}
