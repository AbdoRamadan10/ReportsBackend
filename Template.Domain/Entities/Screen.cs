namespace ReportsBackend.Domain.Entities
{
    public class Screen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public ICollection<RoleScreen> RoleScreens { get; set; }
    }
}