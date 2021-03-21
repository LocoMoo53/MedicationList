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
    public class UomController : Controller
    {
        private MedicationContext context; // create variable for MedicationContext

        public UomController(MedicationContext ctx)
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
            var uoms = context.Uoms
                .OrderBy(u => u.UomId).ToList(); // create & set variable for UoMs listed alphabetically
            return View(uoms); // return list of UoMs
        }

        [HttpGet]
        public IActionResult Add() // method for adding new UoM
        {
            ViewBag.Action = "Add"; // set to "Add" action
            return View("AddUpdate", new Uom()); // return page for adding new UoM
        }

        [HttpGet]
        public IActionResult Update(int id) // method for updating existing UoM
        {
            ViewBag.Action = "Update"; // set to "Update" action
            var uom = context.Uoms.Find(id); // create & set variable for selected UoM
            return View("AddUpdate", uom); // return page for editing selected UoM
        }

        [HttpPost]
        public IActionResult Update(Uom uom) // method for submitting UoM add/edit
        {
            if (ModelState.IsValid) // check if entries are valid
            {
                if (uom.UomId == 0) // check if new UoM
                {
                    context.Uoms.Add(uom); // add new UoM
                }
                else // if edit to existing UoM
                {
                    context.Uoms.Update(uom); // update selected UoM
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
        public IActionResult Delete(int id) // method for deleting existing UoM
        {
            Uom uom = context.Uoms.Find(id); // create & set variable for selected UoM
            return View(uom); // return page w/ selected UoM
        }

        [HttpPost]
        public IActionResult Delete(Uom uom) // method for submitting UoM deletion
        {
            context.Uoms.Remove(uom); // remove selected UoM
            context.SaveChanges(); // save updated list of UoMs
            return RedirectToAction("List"); // redirect to List page
        }
    }
}