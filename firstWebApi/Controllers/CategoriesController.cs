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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            List<Category> lc = await _categoryService.GetAllCategories(); 
            List<CategoryDTO> lcDTO = _mapper.Map<List<Category>, List<CategoryDTO>>(lc);
            if(lc == null)
            {
                return NoContent();
            }
            return Ok(lcDTO);
        }

    }
}
