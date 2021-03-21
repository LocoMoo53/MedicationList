/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicationList.Models; // Using directive for medication list models

namespace MedicationList.Controllers
{
    public class HomeController : Controller
    {
        private MedicationContext context; // Create variable for medication context

        public HomeController(MedicationContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var session = new MedicationSession(HttpContext.Session); // Create & set new session

            int? count = session.GetMyMedicationCount(); // Create & set variable for medication count if available
            if (count == null) // Check if count is NULL
            {
                var cookies = new MedicationCookies(HttpContext.Request.Cookies); // Create & set variable for cookies
                string[] ids = cookies.GetMyMedicationIds(); // Create & set variable for medication IDs

                List<Medication> mymedications = new List<Medication>(); // Create new medication list for medication refills
                if (ids.Length > 0) // Check if medication IDs exist
                    mymedications = context.Medications // Set medications
                        .Include(m => m.Uom)
                        .Include(m => m.DosageForm)
                        .Include(m => m.Route)
                        .Where(m => ids.Contains(m.MedicationId.ToString())).ToList();
                session.SetMyMedications(mymedications); // Set medication refills in session
            }

            return View(); // return Home/Index view
        }

        [Route("[action]")]
        public IActionResult About() // return Home/About view
        {
            return View();
        }
    }
}