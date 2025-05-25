using System.Collections.Generic;

namespace ReportsBackend.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleReport> RoleReports { get; set; }
        public ICollection<RoleScreen> RoleScreens { get; set; }
    }
}