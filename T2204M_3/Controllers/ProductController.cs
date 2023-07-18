using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T2204M_3.Entities;
using T2204M_3.DTOs;
using Microsoft.EntityFrameworkCore;

namespace T2204M_3.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly T2204mApiContext _context;
        public ProductController(T2204mApiContext context)
        {
            _context = context;
        }

        //GET /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .ToList();
            List<ProductDTO> listProducts = new List<ProductDTO>();
            foreach (var item in products)
            {
                listProducts.Add(new ProductDTO
                {
                    id = item.Id,
                    name = item.Name,
                    thumbnail = item.Thumbnail,
                    price = item.Price,
                    qty = item.Qty,
                    description = item.Description,
                    createdAt = item.CreatedAt,
                    category = new CategoryDTO
                    {
                        id = item.Category.Id,
                        name = item.Category.Name,
                    },
                    brand = new BrandDTO
                    {
                        id = item.Brand.Id,
                        name = item.Brand.Name,
                        logo = item.Brand.Logo
                    }
                });
            }
            return Ok(listProducts);
        }

        [HttpGet]
        [Route("get-by-id-product")]
        public IActionResult Get(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return Ok(new ProductDTO
                {
                    id = product.Id,
                    name = product.Name,
                    thumbnail = product.Thumbnail,
                    price = product.Price,
                    qty = product.Qty,
                    description = product.Description,
                    createdAt = product.CreatedAt,
                    category = new CategoryDTO
                    {
                        id = product.Category.Id,
                        name = product.Category.Name,
                    },
                    brand = new BrandDTO
                    {
                        id = product.Brand.Id,
                        name = product.Brand.Name,
                        logo = product.Brand.Logo
                    }
                });
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(ProductDTO data)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (ModelState.IsValid)
            {
                // Tạo một đối tượng Product mới từ dữ liệu đầu vào (ProductDTO)
                var product = new Product 
                { 
                    Name = data.name,
                    Thumbnail= data.thumbnail,
                    Price = data.price,
                    Qty = data.qty,
                    Description = data.description,
                    CreatedAt = data.createdAt,
                    CategoryId = data.category.id,
                    BrandId = data.brand.id.Value

                };

                // Thêm sản phẩm mới vào cơ sở dữ liệu
                _context.Products.Add(product);
                _context.SaveChanges();
                // Trả về phản hồi HTTP Created (201) và thông tin chi tiết về sản phẩm vừa tạo
                return Created($"get-by-id-product?id={product.Id}", new ProductDTO 
                    {
                        id = product.Id,
                        name = product.Name,
                        thumbnail= product.Thumbnail,
                        price= product.Price,
                        qty= product.Qty,
                        description= product.Description,
                        createdAt= product.CreatedAt,
                    category = new CategoryDTO
                    {
                        id = product.Category.Id,
                        name = product.Category.Name,
                    },
                    brand = new BrandDTO
                    {
                        id = product.Brand.Id,
                        name = product.Brand.Name,
                        logo = product.Brand.Logo
                    }
                });
            }
            return BadRequest();
        }
    }
}
