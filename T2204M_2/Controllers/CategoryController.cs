using Microsoft.AspNetCore.Mvc;
using T2204M_2.Entities;
using Microsoft.EntityFrameworkCore;
using T2204M_2.Models;

namespace T2204M_2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;
        public CategoryController(DataContext dataContext)
        {
            _context = dataContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories
                //.Where(c => c.Name.Contains("ash"))
                //.OrderBy(c=>c.Name)
                //.OrderByDescending(c=>c.Name)
                .Include(c => c.Products)
                //.Take(1)
                //.Skip(1)
                .ToList<Category>();
            /*var categories = _context.Categories.ToList<Category>();*/
            /*ViewData["categories"] = categories;*/
            /*ViewBag.Categories = categories;*/
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(new Category { Name = viewModel.Name });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = _context.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(new EditCategoryModel { Id = Id, Name = category.Name});
        }

        [HttpPost]
        public IActionResult Edit(EditCategoryModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(new Category { Id = viewModel.Id, Name = viewModel.Name });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var category = _context.Categories.Find(Id);
            if (category == null)
            {
                NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Upload(int Id) 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile Image)
        {
            if(Image == null)
            {
                return BadRequest("Vui long upload file đính kèm");
            }
            var path = "wwwroot/uploads";
            var filename = Guid.NewGuid().ToString() + Path.GetFileName(Image.FileName);
            var upload = Path.Combine(Directory.GetCurrentDirectory(), path, filename);
            Image.CopyTo(new FileStream(upload, FileMode.Create));
            var rs = $"{Request.Scheme}://{Request.Host}/uploads/{filename}";
            return Ok(rs);
        }
    }
}
