using ReportsBackend.Application.DTOs.ReportColumn;
using ReportsBackend.Application.DTOs.ReportParameter;
using ReportsBackend.Application.DTOs.RoleReport;
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
        public string? Description { get; set; }
        public string? Query { get; set; }
        public string? Path { get; set; }
        public bool HasDetail { get; set; } = false;
        public bool Active { get; set; }
        public bool Hide { get; set; }
        public int? DetailId { get; set; }
        public string? DetailColumn { get; set; }

        public List<ReportColumnDto>? Columns { get; set; }
        public List<ReportParameterDto>? Parameters { get; set; }
        public int? PrivilegeId { get; set; }
        public string? PrivilegeName { get; set; }

        public ICollection<RoleReportDto>? RoleReports { get; set; }
    }
}
