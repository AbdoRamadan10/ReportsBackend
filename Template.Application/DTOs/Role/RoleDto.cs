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
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? UserCount { get; set; }

    }
}
