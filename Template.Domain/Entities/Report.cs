namespace ReportsBackend.Domain.Entities
{
    public class Report : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Unknown";
        public string? Description { get; set; }
        public string? Query { get; set; }
        public string? Path { get; set; }
        public bool HasDetail { get; set; } = false;
        public bool Active { get; set; } = true;
        public bool Hide { get; set; } = false;
        public int? DetailId { get; set; }
        public string? DetailColumn { get; set; }


        public List<ReportColumn>? Columns { get; set; }
        public List<ReportParameter>? Parameters { get; set; }
        public int? PrivilegeId { get; set; }
        public Privilege? Privilege { get; set; }
        public ICollection<RoleReport>? RoleReports { get; set; }

    }
}