using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2204M_3.Entities;
using T2204M_3.DTOs;

namespace T2204M_3.Controllers
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly T2204mApiContext _context;

        public BrandController(T2204mApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var brands = _context.Brands.ToList();
            List<BrandDTO> listbrand = new List<BrandDTO>();
            foreach(var item in brands)
            {
                listbrand.Add(new BrandDTO
                {
                    id = item.Id,
                    name = item.Name,
                    logo = item.Logo
                });
            }
            return Ok(listbrand);
        }

        [HttpGet]
        [Route("get-by-id-brand")]
        public IActionResult Get(int id)
        {
            var brand = _context.Brands.Find(id);
            if( brand !=  null)
            {
                return Ok(new BrandDTO
                {
                    id = brand.Id,
                    name = brand.Name,
                    logo = brand.Logo
                });
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(BrandDTO data)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand 
                    { 
                    Name = data.name,
                    Logo = data.logo
                    };
                _context.Brands.Add(brand);
                _context.SaveChanges();
                return Created($"get-by-id-brand?id={brand.Id}", new BrandDTO { id = brand.Id, name = brand.Name, logo = brand.Logo });
            }
            return BadRequest();
        }
    }

    
}
