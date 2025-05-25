using AutoMapper;
using ReportsBackend.Application.DTOs.Screen;
using ReportsBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Application.Mappings
{
    class ScreenProfile : Profile
    {
        public ScreenProfile()
        {
            CreateMap<Screen, ScreenDto>();
            CreateMap<ScreenCreateDto, Screen>();
            CreateMap<ScreenUpdateDto, Screen>();
        }
    }
}
