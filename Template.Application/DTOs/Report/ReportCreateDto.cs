namespace ReportsBackend.Application.DTOs.Report
{ 
    public class ReportCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int PrivilegeId { get; set; }

    }
}