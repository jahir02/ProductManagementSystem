using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductManagementContext _context;

        public HomeController(ILogger<HomeController> logger, ProductManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // add subcategory
            //SubCategory SubCategory = new SubCategory();
            //SubCategory.SubName = "BLDC";
            //SubCategory.Id = 4;
            //SubCategory.CategoryId = "3";
            //_context.SubCategories.Add(SubCategory);
            //_context.SaveChanges();

            //Add UserMaster
            //UserMaster u = new UserMaster();
            //u.UserId = 4;
            //u.Name = "Nasim";
            //u.Phone = "6754457346";
            //u.Email = "nasim355@gmail.com";
            //u.Address = "Babuidanga";
            //_context.UserMasters.Add(u);
            //_context.SaveChanges();


            // Add Category
            //Category category = new Category();
            //category.Id = 3;
            //category.Name = "Fan";
            //_context.Categories.Add(category);
            //_context.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
