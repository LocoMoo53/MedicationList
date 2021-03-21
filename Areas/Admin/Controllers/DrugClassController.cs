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
    public class DrugClassController : Controller
    {
        private MedicationContext context; // create variable for MedicationContext

        public DrugClassController(MedicationContext ctx)
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
            var drugClasses = context.DrugClasses
                .OrderBy(dc => dc.DrugClassId).ToList(); // create & set variable for drug classes listed alphabetically
            return View(drugClasses); // return list of drug classes
        }

        [HttpGet]
        public IActionResult Add() // method for adding new drug class
        {
            ViewBag.Action = "Add"; // set to "Add" action
            return View("AddUpdate", new DrugClass()); // return page for adding new drug class
        }

        [HttpGet]
        public IActionResult Update(int id) // method for updating existing drug class
        {
            ViewBag.Action = "Update"; // set to "Update" action
            var drugClass = context.DrugClasses.Find(id); // create & set variable for selected drug class
            return View("AddUpdate", drugClass); // return page for editing selected drug class
        }

        [HttpPost]
        public IActionResult Update(DrugClass drugClass) // method for submitting drug class add/edit
        {
            if (ModelState.IsValid) // check if entries are valid
            {
                if (drugClass.DrugClassId == 0) // check if new drug class
                {
                    context.DrugClasses.Add(drugClass); // add new drug class
                }
                else // if edit to existing drug class
                {
                    context.DrugClasses.Update(drugClass); // update selected drug class
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
        public IActionResult Delete(int id) // method for deleting existing drug class
        {
            DrugClass drugClass = context.DrugClasses.Find(id); // create & set variable for selected drug class
            return View(drugClass); // return page w/ selected drug class
        }

        [HttpPost]
        public IActionResult Delete(DrugClass drugClass) // method for submitting drug class deletion
        {
            context.DrugClasses.Remove(drugClass); // remove selected drug class
            context.SaveChanges(); // save updated list of drug classes
            return RedirectToAction("List"); // redirect to List page
        }
    }
}