using Microsoft.AspNetCore.Mvc;
using T2204M_2.Entities;
using Microsoft.EntityFrameworkCore;
using T2204M_2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace T2204M_2.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        public ProductController(DataContext dataContext)
        {
            _context = dataContext;
        }
        public IActionResult Index(string nameSearch)
        {
            List<Product> products;

            if (!string.IsNullOrEmpty(nameSearch))
            {
                products = _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.Name.Contains(nameSearch))
                    .ToList();
            }
            else
            {
                products = _context.Products
                    .Include(p => p.Category)
                    .ToList();
            }

            return View(products);
        }


        public IActionResult Create()
        {
            /*var categories = _context.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");*/
            var categories = _context.Categories.ToList();
            ViewData["Category"] = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel viewModel) 
        {

            if (ModelState.IsValid)
            {
                if (viewModel.Image == null)
                {
                    return BadRequest("Vui long upload file đính kèm");
                }
                var path = "wwwroot/uploads/products";
                var filename = Guid.NewGuid().ToString() + Path.GetFileName(viewModel.Image.FileName);
                var upload = Path.Combine(Directory.GetCurrentDirectory(), path, filename);
                viewModel.Image.CopyTo(new FileStream(upload, FileMode.Create));
                var rs = $"{Request.Scheme}://{Request.Host}/uploads/products/{filename}";

                _context.Products.Add(new Product 
                {
                    Name = viewModel.Name ,
                    Price = viewModel.Price ,
                    Description= viewModel.Description ,
                    Image = rs ,
                    CategoryId = viewModel.CategoryId
                });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
            {
                NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var categories = _context.Categories.ToList();
            ViewData["Category"] = new SelectList(categories, "Id", "Name");


            var product = _context.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            return View(new EditProductViewModel 
            {
                Id = Id, 
                Name = product.Name, 
                Price = product.Price, 
                ImageOld = product.Image,
                Description = product.Description, 
                CategoryId = product.CategoryId
            });
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ImageNew == null)
                {
                    // Không có thay đổi ảnh, giữ nguyên ảnh cũ
                    var product = _context.Products.FirstOrDefault(p => p.Id == viewModel.Id);
                    if (product != null)
                    {
                        product.Name = viewModel.Name;
                        product.Price = viewModel.Price;
                        product.Description = viewModel.Description;
                        product.CategoryId = viewModel.CategoryId;

                        _context.Products.Update(product);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                var path = "wwwroot/uploads/products";
                var filename = Guid.NewGuid().ToString() + Path.GetFileName(viewModel.ImageNew.FileName);
                var upload = Path.Combine(Directory.GetCurrentDirectory(), path, filename);
                viewModel.ImageNew.CopyTo(new FileStream(upload, FileMode.Create));
                var rs = $"{Request.Scheme}://{Request.Host}/uploads/products/{filename}";

                _context.Products.Update(new Product 
                { 
                    Id = viewModel.Id, 
                    Name = viewModel.Name, 
                    Price = viewModel.Price, 
                    Image = rs, 
                    Description = viewModel.Description, 
                    CategoryId = viewModel.CategoryId 
                });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(viewModel);
        }

       
    }
}
