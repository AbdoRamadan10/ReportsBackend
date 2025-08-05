using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Infrastracture.Seeders
{

    public static class StudentSeedData
    {
        private static readonly Random random = new Random();

        // Data pools for random generation
        private static readonly string[] firstNames =
        {
        "James", "Mary", "John", "Patricia", "Robert", "Jennifer",
        "Michael", "Linda", "William", "Elizabeth", "David", "Barbara",
        "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah"
    };

        private static readonly string[] lastNames =
        {
        "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia",
        "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez",
        "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore"
    };

        private static readonly string[] grades = { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "D+", "D", "F" };

        private static readonly string[] genders = { "Male", "Female" };

        private static readonly string[] streetNames =
        {
        "Main", "Oak", "Pine", "Maple", "Cedar", "Elm", "View", "Washington",
        "Lake", "Hill", "Walnut", "Spring", "North", "South", "Park", "Church"
    };

        private static readonly string[] streetTypes = { "St", "Ave", "Blvd", "Rd", "Ln", "Dr" };

        private static readonly string[] cities =
        {
        "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia",
        "San Antonio", "San Diego", "Dallas", "San Jose", "Austin", "Jacksonville"
    };

        private static readonly string[] domains = { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "icloud.com" };

        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Students.Any())
            {
                return;
            }

            var students = GetStudents(1000); // Generate 100 random students
            context.Students.AddRange(students);
            context.SaveChanges();

            // Generate 3-5 details for EACH student
            var studentDetails = GetMultipleDetailsPerStudent(students, minDetails: 3, maxDetails: 5);
            context.StudentDetails.AddRange(studentDetails);
            context.SaveChanges();
        }


        public static List<Student> GetStudents(int count)
        {
            var students = new List<Student>();

            for (int i = 0; i < count; i++)
            {
                var firstName = firstNames[random.Next(firstNames.Length)];
                var lastName = lastNames[random.Next(lastNames.Length)];
                var email = $"{firstName.ToLower()}.{lastName.ToLower()}{random.Next(1, 99)}@{domains[random.Next(domains.Length)]}";

                students.Add(new Student
                {
                    Id = i + 1, // Or generate unique IDs if needed
                    Name = $"{firstName} {lastName}",
                    Grade = grades[random.Next(grades.Length)],
                    Gender = genders[random.Next(genders.Length)],
                    DateOfBirth = GenerateRandomBirthDate(),
                    Email = email,
                    PhoneNumber = GenerateRandomPhoneNumber(),
                    Address = GenerateRandomAddress()
                });
            }

            return students;
        }

        public static List<StudentDetail> GetMultipleDetailsPerStudent(List<Student> students, int minDetails, int maxDetails)
        {
            var details = new List<StudentDetail>();
            var advisorNames = new[] { "Dr. Smith", "Prof. Johnson", "Dr. Williams", "Prof. Brown", "Dr. Davis" };

            foreach (var student in students)
            {
                // Generate between minDetails and maxDetails per student
                int detailCount = random.Next(minDetails, maxDetails + 1);

                for (int i = 0; i < detailCount; i++)
                {
                    details.Add(new StudentDetail
                    {
                        StudentId = student.Id,
                        EmergencyContactName = $"{lastNames[random.Next(lastNames.Length)]}, {firstNames[random.Next(firstNames.Length)]}",
                        EmergencyContactPhone = GenerateRandomPhoneNumber(),
                        HealthInformation = random.Next(10) > 7 ? "Allergies" : "None reported",
                        AdditionalNotes = random.Next(10) > 8 ? $"Note {i + 1}: Special needs" : $"Note {i + 1}: General",
                        AcademicAdvisor = advisorNames[random.Next(advisorNames.Length)],
                    });
                }
            }

            return details;
        }


        private static DateTime GenerateRandomBirthDate()
        {
            // Students between 15-25 years old
            var startDate = DateTime.Now.AddYears(-25);
            var endDate = DateTime.Now.AddYears(-15);
            var range = (endDate - startDate).Days;
            return startDate.AddDays(random.Next(range));
        }

        private static string GenerateRandomPhoneNumber()
        {
            return $"{random.Next(200, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
        }

        private static string GenerateRandomAddress()
        {
            return $"{random.Next(1, 9999)} {streetNames[random.Next(streetNames.Length)]} {streetTypes[random.Next(streetTypes.Length)]}, " +
                   $"{cities[random.Next(cities.Length)]}, {GetRandomState()} {random.Next(10000, 99999)}";
        }

        private static string GetRandomState()
        {
            // US states abbreviations
            string[] states =
            {
            "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };
            return states[random.Next(states.Length)];
        }
    }
}

