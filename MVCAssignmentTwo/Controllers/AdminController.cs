using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAssignmentTwo.Models.Identity;

namespace MVCAssignmentTwo.Controllers
{
    [Authorize(Roles = "Peach")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public async Task<IActionResult> AddAdmin(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if(appUser != null)
            {
                await _userManager.AddToRoleAsync(appUser, "Banana");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)                                // BAD check, seed users with a super super user role, that cant ever be changed or removed 
            {                                                    // This super user can only access server if on site physically :)
                await _userManager.RemoveFromRoleAsync(appUser, "Banana");
            }

            return RedirectToAction("Index");
        }
    }
}
