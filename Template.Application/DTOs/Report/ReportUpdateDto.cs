namespace ReportsBackend.Application.DTOs.Report
{
    public class ReportUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public bool HasDetail { get; set; }
        public int? DetailId { get; set; }
        public string? DetailColumn { get; set; }


        public int PrivilegeId { get; set; }
    }
}