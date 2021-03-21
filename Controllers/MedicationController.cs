/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicationList.Models; // Using directive for medication list models
using Microsoft.AspNetCore.Http; // Using directive for HTTP
using Microsoft.AspNetCore.Authorization; // Using directive for authorization

namespace MedicationList.Controllers
{
    [Authorize]
    public class MedicationController : Controller
    {
        private MedicationContext context; // Create medication context
        public MedicationController(MedicationContext ctx) => context = ctx;

        public IActionResult Index()
        {
            return RedirectToAction("List", "Medication"); // Redirect to Medication/List view
        }

        public IActionResult List(string id)
        {
            var filters = new Filters(id); // Create Filters variable for current filters
            var model = new MedicationViewModel // Create MedicationViewModel object
            {
                Filters = filters
                , DrugClasses = context.DrugClasses.ToList()
                , Uoms = context.Uoms.ToList()
                , DosageForms = context.DosageForms.ToList()
                , Routes = context.Routes.ToList()
            };

            IQueryable<Medication> query = context.Medications
                .Include(m => m.DrugClass).Include(m => m.DosageForm).Include(m => m.Route); // Create query for medications based on filters
            if (filters.HasDrugClass) // Check if drug class is filtered
            {
                query = query.Where(m => m.DrugClassId.ToString() == filters.DrugClassId); // Filter on drug class
            }
            if (filters.HasDosageForm) // Check if dosage form is filtered
            {
                query = query.Where(m => m.DosageFormId.ToString() == filters.DosageFormId); // Filter on dosage form
            }
            if (filters.HasRoute) // Check if route is filtered
            {
                query = query.Where(m => m.RouteId.ToString() == filters.RouteId); // Filter on route
            }
            var medications = query.OrderBy(m => m.Name).ToList(); // Create & set variable to filtered medications listed alphabetically
            model.Medications = medications; // Set medications
            return View(model); // Return MedicationViewModel to view
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add"; // set ViewBag.Action to "Add" for setting page title
            var model = new MedicationViewModel() // Create MedicationViewModel object
            {
                DrugClasses = context.DrugClasses.ToList()
                , Uoms = context.Uoms.ToList()
                , DosageForms = context.DosageForms.ToList()
                , Routes = context.Routes.ToList()
            };
            return View("Edit", model); // Return MedicationViewModel to view
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var model = new MedicationViewModel() // Create MedicationViewModel object
            {
                DrugClasses = context.DrugClasses.ToList()
                , Uoms = context.Uoms.ToList()
                , DosageForms = context.DosageForms.ToList()
                , Routes = context.Routes.ToList()
                , CurrentMedication = context.Medications.Find(id)
            };
            return View(model); // Return "Edit" page populated w/ selected medication data
        }

        [HttpPost]
        public IActionResult Edit(MedicationViewModel model)
        {
            model.Uoms = context.Uoms.ToList(); // Set list of UoMs
            if (ModelState.IsValid) // Check if entries are valid
            {
                if (model.CurrentMedication.MedicationId == 0) { // Check if new medication
                    context.Medications.Add(model.CurrentMedication); // Add new medication
                    TempData["message"] =
                        $"{model.CurrentMedication.Name} {model.CurrentMedication.Strength} {model.CurrentMedication.Uom.Name} " +
                        $"added to Medications List"; // Add confirmation user message using TempData
                }
                else { // If update to existing medication
                    context.Medications.Update(model.CurrentMedication); // Update selected medication
                    TempData["message"] =
                        $"{model.CurrentMedication.Name} {model.CurrentMedication.Strength} {model.CurrentMedication.Uom.Name} " +
                        $"updated in Medications List"; // Add confirmation user message using TempData
                }
                context.SaveChanges(); // Save updated list of medications
                return RedirectToAction("List"); // Redirect to Index view
            }
            else // If entries are not valid
            {
                ViewBag.Action = (model.CurrentMedication.MedicationId == 0) ? "Add" : "Edit"; // check if medication is add or edit & return
                model.DrugClasses = context.DrugClasses.ToList(); // Set list of drug classes
                model.DosageForms = context.DosageForms.ToList(); // Set list of dosage forms
                model.Routes = context.Routes.ToList(); // Set list of routes
                return View(model); // Return MedicationViewModel to view
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = new MedicationViewModel() // Create MedicationViewModel object
            {
                Uoms = context.Uoms.ToList()
                , CurrentMedication = context.Medications.Find(id)
            };
            return View(model); // Return MedicationViewModel to view
        }

        [HttpPost]
        public IActionResult Delete(MedicationViewModel model)
        {
            context.Medications.Remove(model.CurrentMedication); // Remove selected medication from list
            context.SaveChanges(); // Save updated medication list
            return RedirectToAction("List", "Medication"); // Redirect to Medication/List view
        }

        [Route("[action]")]
        public RedirectToActionResult Refill(int id)
        {
            var model = new MedicationViewModel() // Create MedicationViewModel object
            {
                Uoms = context.Uoms.ToList()
                , DosageForms = context.DosageForms.ToList()
                , Routes = context.Routes.ToList()
                , CurrentMedication = context.Medications.Find(id)
            };

            var session = new MedicationSession(HttpContext.Session); // Create & set variable for new session
            var medications = session.GetMyMedications(); // Create & set variable for getting medication refills
            medications.Add(model.CurrentMedication); // Add selected medications to medication refills
            session.SetMyMedications(medications); // Update medication refills session w/ selected medication

            var cookies = new MedicationCookies(HttpContext.Response.Cookies); // Create & set variable for new cookie
            cookies.SetMyMedicationIds(medications); // Update medication refills cookie w/ selected medication

            TempData["message"] =
                $"{model.CurrentMedication.Name} {model.CurrentMedication.Strength} {model.CurrentMedication.Uom.Name} " +
                $"added to Refills"; // Add confirmation user message using TempData

            return RedirectToAction("List"); // Redirect to Medication/List view
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter); // Update filter string
            return RedirectToAction("List", new { ID = id }); // Redirect to Medication/List view
        }
    }
}