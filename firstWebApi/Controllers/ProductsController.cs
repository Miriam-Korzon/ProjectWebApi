using AutoMapper;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace firstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts(string? desc, int? minPrice, int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            List<Product> lp = await _productService.GetAllProducts(desc, minPrice, maxPrice, categoryIds);
            List<ProductDTO> lpDTO = _mapper.Map<List<Product>, List<ProductDTO>>(lp);
            if (lp == null)
            {
                return NoContent();
            }
            return Ok(lpDTO);
        }

    }
}
