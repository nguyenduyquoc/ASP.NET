using Microsoft.AspNetCore.Mvc;
using T2204M_Practice.Entities;
using Microsoft.EntityFrameworkCore;
using T2204M_Practice.Models;



namespace T2204M_Practice.Controllers
{
    public class ContactController : Controller
    {
        private readonly DataContext _context;
        public ContactController(DataContext dataContext)
        {
            _context = dataContext;
        }
        public IActionResult Index()
        {
            List<Contact> contacts = _context.Contacts.ToList<Contact>();
            
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }


        /*[HttpPost]
        public IActionResult Create(ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(new Contact {
                    ContactName = viewModel.ContactName,
                    ContactNumber = viewModel.ContactNumber,
                    GroupName= viewModel.GroupName,
                    HireDate= viewModel.HireDate,
                    Birthday= viewModel.Birthday
                });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }*/
    }
}


