using ReportsBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.UserRole
{
    public class UserRoleDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
