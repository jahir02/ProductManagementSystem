using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using ProductManagementSystem.Models.ViewModel;


namespace ProductManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProductManagementContext _context;
        public AccountController(ProductManagementContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel _login)
        {

            if (!ModelState.IsValid)
            {
                return View(_login);

            }
            LoginMaster result=new LoginMaster();

            try
            {
                result = _context.LoginMasters.Where(x => x.User_Name == _login.User_name && x.Password == _login.Password).FirstOrDefault();
            }
            catch (Exception ex) 
            { 
            
            }
           
            if (result == null) 
            {
                ViewBag.Message = "Invalid username and Password";
                return View();
            }

            //    var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.Username),
            //    new Claim("UserId", user.Id.ToString())
            //};
            var claims = new List<Claim>()
         {
             new Claim(ClaimTypes.Name, result.User_Name)
         };

           var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "SalesOrder");
        }
        public async Task<IActionResult> Logout()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(".AspNetCore.Cookies"); // Delete authentication cookie

            return RedirectToAction("Login", "Account");
        }

    }
}
