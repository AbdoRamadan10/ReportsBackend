using AutoMapper;
using ReportsBackend.Application.DTOs.Report;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.Mappings
{
    class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportDto>()
                .ForMember(dest => dest.PrivilegeName, opt => opt.MapFrom(src => src.Privilege.Name))
                .ForMember(dest => dest.Columns, opt => opt.MapFrom(src => src.Columns))
                .ForMember(dest => dest.Parameters, opt => opt.MapFrom(src => src.Parameters))
                ;
            CreateMap<ReportCreateDto, Report>();
            CreateMap<ReportUpdateDto, Report>();

        }
    }
}
