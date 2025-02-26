using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Controllers
{
    public class UserMasterController : Controller
    {
        private readonly ProductManagementContext _context;

        public UserMasterController(ProductManagementContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Fetch the list of users from the database
            List<UserMaster> users = _context.UserMasters.ToList();
            return View(users);
        }


        //add
        public IActionResult addusermaster()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUserMaster(UserMaster _userMaster)
        {
            if (!ModelState.IsValid)
            {
                return View(_userMaster);
            }
            await _context.UserMasters.AddAsync(_userMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //edit
        public async Task<IActionResult> EditUserMaster(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var userMaster = await _context.UserMasters.FirstOrDefaultAsync(c => c.UserId == id);

            if (userMaster == null)
            {
                return NotFound();
            }

            return View(userMaster);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsrMaster(UserMaster userMaster)
        {
            if (!ModelState.IsValid)
            {
                return View(userMaster);
            }

            // Update the user record in the database
            _context.UserMasters.Update(userMaster);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //Delete
        public async Task<IActionResult> DeleteUserMaster(int userId)
        {
            UserMaster userMaster = new UserMaster();
            userMaster = await _context.UserMasters.FirstOrDefaultAsync(c => c.UserId == userId);

            return View(userMaster);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsyn(int userId)
        {
            var result = await _context.UserMasters.FirstOrDefaultAsync(c => c.UserId == userId);
            _context.UserMasters.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
