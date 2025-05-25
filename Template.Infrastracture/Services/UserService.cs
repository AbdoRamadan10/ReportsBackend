
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


namespace ReportsBackend.Infrastracture.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher; // Optional: For password hashing

        public UserService(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !_passwordHasher.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User> Register(RegisterRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                Role = "User" // Default role
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
    }
}
