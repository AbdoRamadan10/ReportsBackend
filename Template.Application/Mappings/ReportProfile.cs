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
                .ForMember(dest=>dest.PrivilegeName,opt=>opt.MapFrom(src=>src.Privilege.Name));
            CreateMap<ReportCreateDto, Report>();
            CreateMap<ReportUpdateDto, Report>();

        }
    }
}
