using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using ProductManagementSystem.Models.ViewModel;

namespace ProductManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManagementContext _context;
        private object id;
        private object await_context;

        public ProductController(ProductManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


            List<Product> productList = new List<Product>();
            List<Category> categoryList = new List<Category>();
            productList = _context.Products.ToList();
            categoryList = _context.Categories.ToList();
            var result = from product in productList
                         join category in categoryList
                         on product.CategoryId equals category.Id
                         select new ProductViewModel
                         {
                             Id = product.Id,
                             Name = product.Name,
                             CategoryId = category.Id,
                             CategoryName = category.Name,
                             ModelNo = product.ModelNo,
                             Specification = product.Specification,
                             Price = product.Price,

                         };
            return View(result);



            //List<Product> productList = _context.Products.ToList();
            //List<Category> categoryList = _context.Categories.ToList();
            //List<SubCategory> subcategoryList = _context.SubCategories.ToList();

            //var result = from product in _context.Products
            //             join category in _context.Categories
            //             on product.CategoryId equals category.Id
            //             join subcategory in _context.SubCategories
            //             on product.SubCategoryId equals subcategory.Id
            //             select new ProductViewModel
            //             {
            //                 Id = product.Id,
            //                 Name = product.Name,
            //                 CategoryId = category.Id,
            //                 CategoryName = category.Name,
            //                 SubCategoryId = subcategory.Id,
            //                 SubCategoryName = subcategory.SubName,
            //                 ModelNo = product.ModelNo,
            //                 Specification = product.Specification,
            //                 Price = product.Price
            //             };

            //return View(result.ToList());



        }

        //add 
        public async Task<IActionResult> AddProduct()
        {
            var categorylist = _context.Categories.ToList();
            Category newcatecategory = new Category();
            newcatecategory.Id = 0;
            newcatecategory.Name = "Select category";
            categorylist.Add(newcatecategory);

            
            ViewBag.categorylist = categorylist.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList().OrderBy(p=>p.Value);

            var subcategorylist = _context.SubCategories.ToList();

            ViewBag.subcategorylist = subcategorylist;
            ViewBag.subcategorylist = subcategorylist.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.SubName }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product _product)
        {
            
            if (!ModelState.IsValid)
            {
                return View(_product);

            }
            await _context.Products.AddAsync(_product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //NEW EDIT
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            // Populate dropdowns
            ViewBag.Categories = _context.Categories
                                        .Select(c => new SelectListItem
                                        {
                                            Value = c.Id.ToString(),
                                            Text = c.Name
                                        }).ToList();

            ViewBag.Subcategories = _context.SubCategories
                                            .Select(sc => new SelectListItem
                                            {
                                                Value = sc.Id.ToString(),
                                                Text = sc.SubName
                                            }).ToList();

            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _context.Products.Find(model.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = model.Name;
                existingProduct.ModelNo = model.ModelNo;
                existingProduct.Specification = model.Specification;
                existingProduct.Price = model.Price;
                existingProduct.CategoryId = model.CategoryId;
                existingProduct.SubCategoryId = model.SubCategoryId;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate dropdowns in case of validation failure
            ViewBag.Categories = _context.Categories
                                        .Select(c => new SelectListItem
                                        {
                                            Value = c.Id.ToString(),
                                            Text = c.Name
                                        }).ToList();

            ViewBag.Subcategories = _context.SubCategories
                                            .Select(sc => new SelectListItem
                                            {
                                                Value = sc.Id.ToString(),
                                                Text = sc.SubName
                                            }).ToList();

            return View(model);
        }






        //delete
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            Product product = new Product();
            product = await _context.Products.FirstOrDefaultAsync(c => c.Id == Id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsyn(int Id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //dropdown
        public List<SubCategory> getsubcategorylist(int categoryid) 
        {
            List<SubCategory> subcategorylist = new List<SubCategory>();
             subcategorylist = _context.SubCategories.Where(x => x.CategoryId == categoryid).ToList();
            SubCategory newsubcategory = new SubCategory();
            newsubcategory.Id = 0;
            newsubcategory.SubName = "Select subcategory";
            subcategorylist.Add(newsubcategory);
            return subcategorylist.OrderBy(p=>p.Id).ToList();

        }

      
        public List<Product> GetProductList(int categoryId, int subcategoryId)
        {
           
            List<Product> productList = new List<Product>();
            productList= _context.Products.Where(x => x.CategoryId == categoryId && x.SubCategoryId == subcategoryId)
                .ToList();
        
            Product newProduct = new Product();
                newProduct.Id = 0;
                newProduct.Name = "Select product";
            productList.Add(newProduct);
            return productList.OrderBy(p => p.Id).ToList();
        }

        public Product getProductdetails(int categoryId, int subcategoryId, int productid)
        {

            Product productList = new Product();
            productList = _context.Products.Where(x => x.CategoryId == categoryId && x.SubCategoryId == subcategoryId && x.Id == productid)
                .FirstOrDefault();
            return productList;
        }




    }

}

