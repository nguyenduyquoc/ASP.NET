using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2204M_3.Entities;
using T2204M_3.DTOs;

namespace T2204M_3.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly T2204mApiContext _context;
        public CategoryController(T2204mApiContext context)
        {
            _context = context;
        }

        //GET /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            List<CategoryDTO> list = new List<CategoryDTO>();
            foreach (var item in categories)
            {
                list.Add(new CategoryDTO
                {
                    id = item.Id,
                    name = item.Name,
                });
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.Find(id);
            if(category != null)
            {
                return Ok(new CategoryDTO { id = category.Id, name = category.Name }) ;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO data)
        {
            if(ModelState.IsValid)
            {
                var category = new Category { Name = data.name };
                _context.Categories.Add(category);
                _context.SaveChanges();
                return Created($"get-by-id?id={category.Id}", new CategoryDTO { id = category.Id, name = category.Name });
            }
            return BadRequest();
        }
    }
}
