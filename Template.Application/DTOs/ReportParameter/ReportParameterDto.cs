using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.ReportParameter
{
    public class ReportParameterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        public string? DataType { get; set; }
        public string? ParameterType { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }
        public string? QueryForDropdown { get; set; }
        public int? Sort { get; set; }
        public int ReportId { get; set; }
    }
}
