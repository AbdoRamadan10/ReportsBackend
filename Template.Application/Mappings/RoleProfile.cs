using AutoMapper;
using ReportsBackend.Application.DTOs.Role;
using ReportsBackend.Application.DTOs.RoleReport;
using ReportsBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.Mappings
{
    class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleReport, RoleReportDto>();

            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();

        }
    }
}
