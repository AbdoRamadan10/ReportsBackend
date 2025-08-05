using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Entities
{
    public class StudentDetail
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? HealthInformation { get; set; }
        public string? AdditionalNotes { get; set; }
        public string? AcademicAdvisor { get; set; }
    }
}
