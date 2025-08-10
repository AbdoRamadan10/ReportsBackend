namespace ReportsBackend.Application.DTOs.Report
{
    public class ReportUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Query { get; set; }
        public string? Path { get; set; }
        public bool HasDetail { get; set; } = false;
        public int? DetailId { get; set; }
        public string? DetailColumn { get; set; }
        public int? PrivilegeId { get; set; }
    }
}