namespace ReportsBackend.Application.DTOs.Role
{
    public class RoleCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}