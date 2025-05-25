namespace ReportsBackend.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public int PrivilegeId { get; set; }

        public Privilege Privilege { get; set; }

        public ICollection<RoleReport> RoleReports { get; set; }



        //public int? ScreenId { get; set; }
        //public Screen Screen { get; set; }
    }
}