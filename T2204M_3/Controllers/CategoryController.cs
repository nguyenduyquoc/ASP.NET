using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2204M_3.Entities;
using T2204M_3.DTOs;
using Microsoft.EntityFrameworkCore;

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
            var categories = _context.Categories
                .Include(c=>c.Products)
                .ToList();
            List<CategoryDTO> list = new List<CategoryDTO>();
            foreach (var item in categories)
            {
                List<ProductDTO> plist = new List<ProductDTO>();
                foreach(var p in item.Products)
                {
                    plist.Add(new ProductDTO
                    {
                        id = p.Id,
                        name = p.Name,
                        thumbnail= p.Thumbnail,
                        price= p.Price,
                        qty= p.Qty,
                        description= p.Description,
                        createdAt= p.CreatedAt,
                    });
                }
                list.Add(new CategoryDTO
                {
                    id = item.Id,
                    name = item.Name,
                    products = plist
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

        [HttpPut]
        public IActionResult Update(CategoryDTO data)
        {
            if(ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = data.id,
                    Name = data.name

                };
                _context.Categories.Update(category);
                _context.SaveChanges();
                return NoContent(); ;
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var category = _context.Categories.Find(id);
            if(category == null )
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent(); ;
        }
    }
}
