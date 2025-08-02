using AutoMapper;
using ReportsBackend.Application.DTOs.Role;
using ReportsBackend.Application.DTOs.RoleReport;
using ReportsBackend.Application.DTOs.RoleScreen;
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
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.ReportPermissions, opt => opt.MapFrom(src => src.RoleReports))
                .ForMember(dest => dest.ScreenPermissions, opt => opt.MapFrom(src => src.RoleScreens))
                ;
            CreateMap<RoleReport, RoleReportDto>()
                .ForMember(dest => dest.ReportName, opt => opt.MapFrom(src => src.Report.Name))
                ;
            CreateMap<RoleScreen, RoleScreenDto>()
                .ForMember(dest => dest.ScreenName, opt => opt.MapFrom(src => src.Screen.Name))
                ;


            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();

            CreateMap<Role, RoleNameDto>();

        }
    }
}
