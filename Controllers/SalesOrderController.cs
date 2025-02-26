using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using ProductManagementSystem.Models.ViewModel;

namespace ProductManagementSystem.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly ProductManagementContext _context;
        public SalesOrderController(ProductManagementContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<SalesOrderHeader> OrderHeader = new List<SalesOrderHeader>();
            List<UserMaster> uMaster = new List<UserMaster>();
            OrderHeader = _context.SalesOrderHeaders.ToList();
            uMaster = _context.UserMasters.ToList();

            var result = from s in OrderHeader
                         join st in uMaster
                         on s.UserId equals st.UserId
                         select new SalesOrderViewModel
                         {
                             HeaderId = s.HeaderId,
                             Username = st.Name,
                             SalesType = s.SalesType,
                             SalesDate = s.SalesDate,
                             TotalGst=s.TotalGst,   
                             TotalAmount=s.TotalAmount
                         };
            return View(result);
        }

        //AddNewSalesOrder
        public async Task<IActionResult> AddNewSalesOrder()
        {
            var userlist = _context.UserMasters.ToList();
            UserMaster newUserMaster = new UserMaster();
            newUserMaster.UserId = 0;
            newUserMaster.Name = "Select Party Name";
            userlist.Add(newUserMaster);


            ViewBag.UserId = userlist.Select(x => new SelectListItem { Value = x.UserId.ToString(), Text = x.Name }).ToList().OrderBy(p => p.Value);
            var categorylist = _context.Categories.ToList();
            Category newcatecategory = new Category();
            newcatecategory.Id = 0;
            newcatecategory.Name = "Select category";
            categorylist.Add(newcatecategory);


            ViewBag.categorylist = categorylist.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList().OrderBy(p => p.Value);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSalesOrder([FromBody]  List<SalesOrderViewModel> salesOrderViewModel)
        {
            // Check if the model state is valid
            

            // Create a new sales order header entity from the view model
            SalesOrderHeader salesOrderHeader = new SalesOrderHeader
            {
                // Assign values from the view model to the entity properties
                UserId = salesOrderViewModel[0].UserId,
                SalesDate = salesOrderViewModel[0].SalesDate,
                TotalAmount = salesOrderViewModel[0].TotalAmount+ salesOrderViewModel[0].TotalGst,
                TotalGst = salesOrderViewModel[0].TotalGst,
                TotalSgst = salesOrderViewModel[0].TotalSgst,
                TotalIgst = salesOrderViewModel[0].TotalSgst,
                SalesType = salesOrderViewModel[0].SalesType
            };

            // Add the sales order header to the database
            _context.SalesOrderHeaders.Add(salesOrderHeader);

            // Save changes to the database
            await _context.SaveChangesAsync();

            List<SalesOrderDetail> _orderlist = new List<SalesOrderDetail>();
            int count = 0;
            foreach (var item in salesOrderViewModel)
            {
                count++;
                 SalesOrderDetail salesOrderdetails = new SalesOrderDetail
                {
                    // Assign values from the view model to the entity properties
                    
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Amount = item.Amount + ((item.Amount) * item.Quantity * 18) / 100,
                    Gst = ((item.Amount) * item.Quantity * 18) / 100,
                    Cgst = ((item.Amount) * item.Quantity * 18) / 200,
                    Igst = ((item.Amount) * item.Quantity * 18) / 200,
                    SalesHeaderId = salesOrderHeader.HeaderId,
                    

                };
                _orderlist.Add(salesOrderdetails);
            }

            try {
                _context.SalesOrderDetails.AddRange(_orderlist);
                await _context.SaveChangesAsync();
            }
            catch( Exception ex){

            }

           

            // Save changes to the database
            

            // Redirect to a list or confirmation page after successful save
            return RedirectToAction("Index", "SalesOrder");
        }



    }
}
