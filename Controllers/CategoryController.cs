using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ProductManagementContext _context;
        public CategoryController(ProductManagementContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();
            categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult AddCategory()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category _category)
        {

            if (!ModelState.IsValid)
            {
                return View(_category);

            }
            await _context.Categories.AddAsync(_category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> EditCategory(int id)
        {
            Category category = new Category();
            category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);

            }
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCategory (int id)
        {
            Category category = new Category();
            category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsyn(int Id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Categories.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
    }
}
