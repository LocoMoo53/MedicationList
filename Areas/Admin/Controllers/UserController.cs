/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MedicationList.Models;

namespace MedicationList.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private UserManager<User> userManager; // Create variable for user manager
        private RoleManager<IdentityRole> roleManager; // Create variable for role manager
        public UserController(UserManager<User> userMngr,
            RoleManager<IdentityRole> roleMngr) // Method for setting user manager & role manager
        {
            userManager = userMngr;
            roleManager = roleMngr;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>(); // Create & assign variable for list of users
            foreach (User user in userManager.Users) // Loop through each user
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }
            UserViewModel model = new UserViewModel // Create & assign variable for new UserViewModel
            {
                Users = users,
                Roles = roleManager.Roles
            };
            return View(model); // Return UserViewModel to view
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id); // Create & set variable for finding specific user
            if (user != null) // Check if user found
            {
                IdentityResult result = await userManager.DeleteAsync(user); // Create & set variable for deleting user result
                if (!result.Succeeded) // Check if result succeeded
                {
                    string errorMessage = ""; // Create variable for error message
                    foreach (IdentityError error in result.Errors) // Loop through errors
                    {
                        errorMessage += error.Description + " | "; // Display given error
                    }
                    TempData["message"] = errorMessage; // Store message in TempData
                }
            }
            return RedirectToAction("Index"); // Redirect to Index view
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(); // Return view
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {
            if (ModelState.IsValid) // Check if entries valid
            {
                var user = new User { UserName = model.Username }; // Create & assign variable for new user
                var result = await userManager.CreateAsync(user, model.Password); // Create & assign variable for creating user result
                if (result.Succeeded) // Check if user added successfully
                {
                    return RedirectToAction("Index"); // Redirect to Index view
                }
                else // If result failed
                {
                    foreach (var error in result.Errors) // Loop through errors
                    {
                        ModelState.AddModelError("", error.Description); // Add given error
                    }
                }
            }
            return View(model); // Return RegisterViewModel to view
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin"); // Create & assign variable for adding user to Admin role
            if (adminRole == null) // Check if admin role exists
            {
                TempData["message"] = "Admin role does not exist. "
                    + "Click 'Create Admin Role' button to create it."; // Create message using TempData
            }
            else // If Admin role exists
            {
                User user = await userManager.FindByIdAsync(id); // Create & set variable for adding role
                await userManager.AddToRoleAsync(user, adminRole.Name); // Add role
            }
            return RedirectToAction("Index"); // Redirect to Index view
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            User user = await userManager.FindByIdAsync(id); // Create & set variable for user to be removed from Admin role
            var result = await userManager.RemoveFromRoleAsync(user, "Admin"); // Create & set variable for removing user from Admin role result
            if (result.Succeeded) { } // Check if user removed from Admin role successfully
            return RedirectToAction("Index"); // Redirect to Index view
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id); // Create & set variable for role to be deleted
            var result = await roleManager.DeleteAsync(role); // Create & set variable for deleting role result
            if (result.Succeeded) { } // Check if role removed successfully
            return RedirectToAction("Index"); // Redirect to Index view
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            var result = await roleManager.CreateAsync(new IdentityRole("Admin")); // Create & set variable for creating Admin role result
            if (result.Succeeded) { } // Check if role created successfully
            return RedirectToAction("Index"); // Redirect to Index view
        }
    }
}