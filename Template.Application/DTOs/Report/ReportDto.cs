using ReportsBackend.Domain.Entities;
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
        public string Query { get; set; }
        public string Path { get; set; } = string.Empty;
        public List<ReportColumn> Columns { get; set; }
        public List<ReportParameter> Parameters { get; set; }
        public int PrivilegeId { get; set; }
        public string PrivilegeName { get; set; }

        public ICollection<RoleReport> RoleReports { get; set; }
    }
}
