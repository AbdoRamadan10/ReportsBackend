using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.DTOs.Auth
{
    public class RegisterRequest
    {
        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid email format")]
        //[StringLength(100, ErrorMessage = "Email must be between 5 and 100 characters", MinimumLength = 5)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(100, ErrorMessage = "Password must be between 8 and 100 characters", MinimumLength = 8)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        //    ErrorMessage = "Password must contain at least one uppercase, one lowercase, one number and one special character")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm Password is required")]
        //[Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters", MinimumLength = 3)]
        public string Username { get; set; }

    }
}
