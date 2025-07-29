using System.Collections.Generic;

namespace ReportsBackend.Application.DTOs.Report
{
    public class ReportQueryResultDto
    {
        public List<Dictionary<string, object>> Rows { get; set; } = new List<Dictionary<string, object>>();
        public List<string> ColumnNames { get; set; } = new List<string>();
    }
}
