using ReportsBackend.Application.DTOs.Report;
using ReportsBackend.Application.DTOs.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.Role
{
    public class RoleAccessDto
    {
        public IEnumerable<ScreenDto> Screens { get; set; }
        public IEnumerable<ReportDto> Reports { get; set; }
    }
}
