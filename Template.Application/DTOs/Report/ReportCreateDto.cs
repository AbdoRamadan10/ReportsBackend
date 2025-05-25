namespace ReportsBackend.Application.DTOs.Report
{ 
    public class ReportCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

    }
}