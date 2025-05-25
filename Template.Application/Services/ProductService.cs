using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Application.DTOs.Product;
using ReportsBackend.Domain.Entities;
using ReportsBackend.Domain.Helpers;
using ReportsBackend.Domain.Interfaces;

namespace ReportsBackend.Application.Services
{
    public class ProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductGetDto>> GetAllAsync(FindOptions options)
        {
            var products = await _productRepository.GetAllAsync(options);
            return new PaginatedResult<ProductGetDto>
            {
                Items = _mapper.Map<IEnumerable<ProductGetDto>>(products.Items),
                PageNumber = products.PageNumber,
                PageSize = products.PageSize,
                TotalCount = products.TotalCount,
                
            };
        }
    }
}
