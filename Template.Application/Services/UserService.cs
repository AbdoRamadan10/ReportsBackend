using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportsBackend.Application.DTOs.Auth;
using ReportsBackend.Application.DTOs.Screen;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Domain.Exceptions;
using ReportsBackend.Domain.Helpers;
using ReportsBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.Services
{
    public class UserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper, IGenericRepository<Role> roleRepository, IGenericRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<PaginatedResult<UserDto>> GetAllAsync(FindOptions options)
        {
            var users = await _userRepository.GetAllAsync(options);
            return new PaginatedResult<UserDto>
            {
                Items = _mapper.Map<IEnumerable<UserDto>>(users.Items),
                PageNumber = users.PageNumber,
                PageSize = users.PageSize,
                TotalCount = users.TotalCount,

            };
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException("Screen", id.ToString());
            return _mapper.Map<UserDto>(user);
        }

        public async Task AssignRoleToUserAsync(int userId, List<int> roleIds)
        {
            var user = await _userRepository.GetByIdAsync(userId, u => u.Include(ur => ur.UserRoles));

            if (user == null)
                throw new NotFoundException("User", userId.ToString());

            var existingRoles = (await _userRoleRepository.GetAllAsync(new FindOptions { }, q => q));

            DeleteRoleFromUserAsync(userId, existingRoles.Items.Select(ur => ur.RoleId).ToList()).Wait();



            foreach (var roleId in roleIds)
            {
                // Check if user exists

                //var userRoles = await _userRoleRepository.GetAllAsync(new FindOptions { }, u => u.Include(ur => ur.Role));


                //Check if role exists
                var role = await _roleRepository.GetByIdAsync(roleId);
                if (role == null)
                    continue;

                // Check if the user already has this role
                var existing = (await _userRoleRepository.GetAllAsync(new FindOptions { }, q => q))
                    .Items.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);

                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                };

                if (existing == null)
                    await _userRoleRepository.AddAsync(userRole);

                // Assign role


            }


        }

        public async Task DeleteRoleFromUserAsync(int userId, List<int> roleIds)
        {
            foreach (var roleId in roleIds)
            {
                // Check if user exists
                var user = await _userRepository.GetByIdAsync(userId, u => u.Include(ur => ur.UserRoles));
                if (user == null)
                    throw new NotFoundException("User", userId.ToString());
                // Check if the user has this role
                var existing = (await _userRoleRepository.GetAllAsync(new FindOptions { }, q => q))
                    .Items.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
                if (existing != null)
                    await _userRoleRepository.Delete(existing);
            }
        }





        //public async Task<User> CreateAsync(User user)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException(nameof(user));
        //    await _userRepository.AddAsync(user);
        //    return user;
        //}

        //public async Task UpdateAsync(int id, User user)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException(nameof(user));
        //    var existingUser = await _userRepository.GetByIdAsync(id);
        //    if (existingUser == null)
        //        throw new KeyNotFoundException($"User with ID {id} not found.");
        //    _mapper.Map(user, existingUser);
        //    await _userRepository.Update(existingUser);
        //}




    }
}
