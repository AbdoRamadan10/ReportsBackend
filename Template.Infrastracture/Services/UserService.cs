
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Application.Interfaces;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using Microsoft.EntityFrameworkCore;
using ReportsBackend.Application.DTOs.Auth;
using AutoMapper;
using ReportsBackend.Application.DTOs.Role;


namespace ReportsBackend.Infrastracture.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher; // Optional: For password hashing
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !_passwordHasher.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User> Register(RegisterRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                Status = true,

                //Role = "User" // Default role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<ICollection<RoleDto>> GetUserRoles(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return null;

            var roles = user.UserRoles
             .Select(ur => ur.Role);

            var rolesDto = _mapper.Map<ICollection<RoleDto>>(roles);



            return rolesDto;


        }
    }
}
