using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsBackend.Application.Services;
using ReportsBackend.Domain.Helpers;

namespace ReportsBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FindOptions findOptions)
        {
            var result = await _productService.GetAllAsync(findOptions);
            return Ok(result);
        }
    }
}
