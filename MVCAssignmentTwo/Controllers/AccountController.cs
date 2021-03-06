﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVCAssignmentTwo.Models.Identity;
using MVCAssignmentTwo.Models.ViewModels;

namespace MVCAssignmentTwo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;  // Has many nice pre-made features ! look here first 
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(IdentityLoginViewModel identityLogin) 
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(identityLogin.Username, identityLogin.Password, false, false); // Persist (false) is saving cookie for keep logged in. Lockedout (false) is for spam attempts

                if (result.Succeeded) // There are more states u can check and give user info about them.
                {
                    return RedirectToAction("People", "Register");
                }
            }
            ViewBag.Msg = "Failed to login.";

            return View();
        }


        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(IdentityCreateViewModel identityCreate) // Mek a view model insted 
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser() {
                    FirstName =  identityCreate.FirstName, 
                    LastName = identityCreate.LastName, 
                    Birthdate = identityCreate.Birthdate, 
                    UserName = identityCreate.Username };

                var result = await _userManager.CreateAsync(appUser, identityCreate.Password);
                if (result.Succeeded)
                {
                    appUser = await _userManager.FindByNameAsync(appUser.UserName);
                    if(appUser != null) 
                        await _userManager.AddToRoleAsync(appUser, "Apple"); // Not entirely sure what to do if failing to give role ? Delete the user again and say it failed ? but what if that delete fails ? :D
                    return RedirectToAction("Login");
                }
            }
            // Change the error message of birthdate to something more comprehensible. Wish I could do this in ViewModel, but the ModelBindingError happens above its head
            ModelError birthdateModelError = ModelState["Birthdate"]?.Errors?.FirstOrDefault();
            if (birthdateModelError != null && birthdateModelError.ErrorMessage.StartsWith("The value"))
            {
                ModelState["Birthdate"].Errors.Clear();
                ModelState.AddModelError("Birthdate", "Please Select a Date.");
            }
            
            ViewBag.Msg = "Sign up failed.";
            return View(identityCreate);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> MyProfile()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            if(appUser != null)
            {
                return View(appUser);
            }
            return RedirectToAction("SignOut");
        }
    }
}
