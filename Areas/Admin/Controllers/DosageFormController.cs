/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 10-25-2020
 * "Week 5 Develop, Code, Test and Debug web app Assignments (Team)"
 * "Create an MVC Core web app"
 *******************************************************************/

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MedicationList.Models;
using Microsoft.AspNetCore.Authorization; // Using directive for authorization

namespace MedicationList.Area.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DosageFormController : Controller
    {
        private MedicationContext context; // create variable for MedicationContext

        public DosageFormController(MedicationContext ctx)
        {
            context = ctx; // set context variable to parameter
        }

        public IActionResult Index()
        {
            return RedirectToAction("List"); // redirect to List page
        }

        [Route("[area]/[controller]s/{id?}")]
        public IActionResult List()
        {
            var dosageForms = context.DosageForms
                .OrderBy(df => df.DosageFormId).ToList(); // create & set variable for dosage forms listed alphabetically
            return View(dosageForms); // return list of dosage forms
        }

        [HttpGet]
        public IActionResult Add() // method for adding new dosage form
        {
            ViewBag.Action = "Add"; // set to "Add" action
            return View("AddUpdate", new DosageForm()); // return page for adding new dosage form
        }

        [HttpGet]
        public IActionResult Update(int id) // method for updating existing dosage form
        {
            ViewBag.Action = "Update"; // set to "Update" action
            var dosageForm = context.DosageForms.Find(id); // create & set variable for selected dosage form
            return View("AddUpdate", dosageForm); // return page for editing selected dosage forms
        }

        [HttpPost]
        public IActionResult Update(DosageForm dosageForm) // method for submitting dosage form add/edit
        {
            if (ModelState.IsValid) // check if entries are valid
            {
                if (dosageForm.DosageFormId == 0) // check if new dosage form
                {
                    context.DosageForms.Add(dosageForm); // add new dosage form
                }
                else // if edit to existing dosage form
                {
                    context.DosageForms.Update(dosageForm); // update selected dosage form
                }
                context.SaveChanges(); // save add/edit
                return RedirectToAction("List"); // redirect to List page
            }
            else // if entries are invalid
            {
                ViewBag.Action = "Save"; // set to "Save" action
                return View("AddUpdate"); // return current add/update page
            }
        }

        [HttpGet]
        public IActionResult Delete(int id) // method for deleting existing dosage form
        {
            DosageForm dosageForm = context.DosageForms.Find(id); // create & set variable for selected dosage form
            return View(dosageForm); // return page w/ selected dosage form
        }

        [HttpPost]
        public IActionResult Delete(DosageForm dosageForm) // method for submitting dosage form deletion
        {
            context.DosageForms.Remove(dosageForm); // remove selected dosage form
            context.SaveChanges(); // save updated list of dosage forms
            return RedirectToAction("List"); // redirect to List page
        }
    }
}