using AutoMapper;
using ReportsBackend.Application.DTOs.Auth;
using ReportsBackend.Application.DTOs.UserRole;
using ReportsBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.Mappings
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();


            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                ;

        }
    }
}
