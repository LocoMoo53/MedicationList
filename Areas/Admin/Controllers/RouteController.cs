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
    public class RouteController : Controller
    {
        private MedicationContext context; // create variable for MedicationContext

        public RouteController(MedicationContext ctx)
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
            var routes = context.Routes
                .OrderBy(r => r.RouteId).ToList(); // create & set variable for routes listed alphabetically
            return View(routes); // return list of routes
        }

        [HttpGet]
        public IActionResult Add() // method for adding new route
        {
            ViewBag.Action = "Add"; // set to "Add" action
            return View("AddUpdate", new Route()); // return page for adding new route
        }

        [HttpGet]
        public IActionResult Update(int id) // method for updating existing route
        {
            ViewBag.Action = "Update"; // set to "Update" action
            var route = context.Routes.Find(id); // create & set variable for selected route
            return View("AddUpdate", route); // return page for editing selected route
        }

        [HttpPost]
        public IActionResult Update(Route route) // method for submitting route add/edit
        {
            if (ModelState.IsValid) // check if entries are valid
            {
                if (route.RouteId == 0) // check if new route
                {
                    context.Routes.Add(route); // add new route
                }
                else // if edit to existing route
                {
                    context.Routes.Update(route); // update selected route
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
        public IActionResult Delete(int id) // method for deleting existing route
        {
            Route route = context.Routes.Find(id); // create & set variable for selected route
            return View(route); // return page w/ selected route
        }

        [HttpPost]
        public IActionResult Delete(Route route) // method for submitting route deletion
        {
            context.Routes.Remove(route); // remove selected route
            context.SaveChanges(); // save updated list of routes
            return RedirectToAction("List"); // redirect to List page
        }
    }
}