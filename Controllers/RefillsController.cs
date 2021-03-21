/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using Microsoft.AspNetCore.Mvc;
using MedicationList.Models; // using directive for MedicationList models
using Microsoft.AspNetCore.Authorization; // Using directive for authorization

namespace MedicationList.Controllers
{
    [Authorize]
    public class RefillsController : Controller
    {
        private MedicationContext context; // Create medication context
        public RefillsController(MedicationContext ctx) => context = ctx;

        [HttpGet]
        public ViewResult Index()
        {
            var session = new MedicationSession(HttpContext.Session); // Create variable for MedicationSession
            var model = new MedicationViewModel // Create variable for MedicationViewModel
            {
                Medications = session.GetMyMedications()
            };

            return View(model); // Return MedicationViewModel to view
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            var session = new MedicationSession(HttpContext.Session); // Create variable for MedicationSession
            var cookies = new MedicationCookies(HttpContext.Response.Cookies); // Create variable for MedicationCookies

            session.RemoveMyMedications(); // Remove medication refills from session
            cookies.RemoveMyMedicationIds(); // Remove medication refill cookies

            TempData["message"] = "Refills cleared";

            return RedirectToAction("List", "Medication");
        }
    }
}