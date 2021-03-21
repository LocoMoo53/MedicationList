/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 10-25-2020
 * "Week 5 Develop, Code, Test and Debug web app Assignments (Team)"
 * "Create an MVC Core web app"
 *******************************************************************/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Using directive for authorization

namespace MedicationList.Area.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // return Home/Index view
        }
    }
}