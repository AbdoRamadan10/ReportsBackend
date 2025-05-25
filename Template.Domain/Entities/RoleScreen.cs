using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Entities
{
    public class RoleScreen
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int ScreenId { get; set; }
        public Screen Screen { get; set; }

    }
}
