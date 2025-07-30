using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.RoleScreen
{
    public class RoleScreenDto
    {
        public int RoleId { get; set; }
        public int ScreenId { get; set; }
        public string ScreenName { get; set; }
    }
}
