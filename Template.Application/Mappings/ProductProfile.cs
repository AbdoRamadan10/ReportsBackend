using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Application.DTOs.Product;
using ReportsBackend.Domain.Entities;

namespace ReportsBackend.Application.Mappings
{
    class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductPostDto, Product>();
        }
    }
}
