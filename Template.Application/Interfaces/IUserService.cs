using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Application.DTOs.Auth;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Application.DTOs.Role;

namespace ReportsBackend.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> Authenticate(string username, string password);
        Task<User> Register(RegisterRequest request);
        Task<bool> EmailExists(string email);
        Task<bool> UsernameExists(string username);

        Task<ICollection<RoleDto>> GetUserRoles(int userId);



    }
}
