/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; // Using directive for MVC
using Microsoft.AspNetCore.Identity; // Using directive for Identity namespace
using MedicationList.Models; // Using directive for MedicationList models

namespace MedicationList.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager; // Create variable for user manager
        private SignInManager<User> signInManager; // Create variable for sign-in manager

        public AccountController(UserManager<User> userMngr, SignInManager<User> signInMngr)
        {
            userManager = userMngr; // Set user manager
            signInManager = signInMngr; // Set sign-in manager
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Return Account/Register view
        }

        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL }; // Create & set variable for new LoginViewModel
            return View(model); // Return LoginViewModel to view
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // Check if entries are valid
            {
                var user = new User { UserName = model.Username }; // Create & set variable for username
                var result = await userManager.CreateAsync(user, model.Password); // Create & set variable for signing in
                if (result.Succeeded) // Check if log in successful
                {
                    await signInManager.SignInAsync(user, isPersistent: false); // Create session cookie for current login
                    return RedirectToAction("List", "Medication"); // Redirect to Medication/List view
                }
                else // If entries are not valid
                {
                    foreach (var error in result.Errors) // Loop through errors
                    {
                        ModelState.AddModelError("", error.Description); // Display errors
                    }
                }
            }
            return View(model); // Return RegisterViewModel to view
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid) // Check if entries are valid
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Username, model.Password, isPersistent: model.RememberMe
                    , lockoutOnFailure: false); // Create & set variable for login result

                if (result.Succeeded) // Check if login succeeded
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) // Check if return URL populated & valid
                    {
                        return Redirect(model.ReturnUrl); // Redirect to return URL
                    }
                    else // If return URL not populated and/or not valid
                    {
                        return RedirectToAction("Index", "Home"); // Redirect to Home/Index view
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password."); // Add validation error message
            return View(model); // Return LoginViewModel to view
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync(); // Sign out of current login
            return RedirectToAction("Index", "Home"); // Redirect to Home/Index view
        }

        public ViewResult AccessDenied()
        {
            return View(); // Return Account/AccessDenied view
        }
    }
}