using AutoMapper;
using ReportsBackend.Application.DTOs.Auth;
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

        }
    }
}
