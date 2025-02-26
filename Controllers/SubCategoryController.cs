using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using ProductManagementSystem.Models.ViewModel;


namespace ProductManagementSystem.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ProductManagementContext _context;

        public int Id { get; private set; }

        public SubCategoryController(ProductManagementContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<SubCategory> sublist = new List<SubCategory>();
            List<Category> catlist = new List<Category>();
           sublist = _context.SubCategories.ToList();
            catlist = _context.Categories.ToList();

            var result = from s in sublist 
                            join st in catlist
                            on s.CategoryId equals st.Id 
                            select new SubcategoryViewModel
                            { 
                                Id = s.Id,
                                SubName = s.SubName,
                                CategoryId=st.Id,
                                CategoryName=st.Name
                            };
            return View(result);
        }

        //add
        public IActionResult AddSubCategory()
        {
            
            var categorylist=_context.Categories.ToList();
            ViewBag.Messagestatus = 0;
            ViewBag.categorylist = categorylist;
            ViewBag.categorylist = categorylist.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSubCategory
            (SubCategory _subcategory)
        {
            var categorylist = _context.Categories.ToList();

            if (!ModelState.IsValid)
            {
               
                ViewBag.Messagestatus = 0;
                ViewBag.categorylist = categorylist;
                ViewBag.categorylist = categorylist.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

                return View(_subcategory);

            }

            try {
                await _context.SubCategories.AddAsync(_subcategory);

                //error
                await _context.SaveChangesAsync();
                ViewBag.Messagestatus = 1;
            } catch(Exception ex) {

                ViewBag.Messagestatus = 2;
            }
            
            
           
            ViewBag.categorylist = categorylist;
            ViewBag.categorylist = categorylist.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View();
        }

        //edit
        public async Task<IActionResult> EditSubCategory(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var SubCategory = await _context.SubCategories.FirstOrDefaultAsync(c => c.Id == id);

            if (SubCategory == null)
            {
                return NotFound();
            }

            return View(SubCategory);

        }

        [HttpPost]
        public async Task<IActionResult> EditSubCategory(SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(subCategory);
            }

            // Update the user record in the database
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //Delete DeleteSubCategory
        public async Task<IActionResult> DeletSubCategory(int Id)
        {
            SubCategory subCategory = new SubCategory();
            subCategory = await _context.SubCategories.FirstOrDefaultAsync(c => c.Id == Id);

            return View(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsyn(int Id)
        {
            var result = await _context.SubCategories.FirstOrDefaultAsync(c => c.Id == Id);
            _context.SubCategories.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
