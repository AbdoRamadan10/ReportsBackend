using ReportsBackend.Application.DTOs.RoleReport;
using ReportsBackend.Application.DTOs.RoleScreen;
using ReportsBackend.Application.DTOs.UserRole;
using ReportsBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.Role
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoleReportDto> ReportPermissions { get; set; }
        public ICollection<RoleScreenDto> ScreenPermissions { get; set; }
    }
}
