using System.Collections.ObjectModel;

namespace ReportsBackend.Domain.Entities
{
    public class Privilege
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public ICollection<Report> Reports { get; set; } = new Collection<Report>();
    }
}