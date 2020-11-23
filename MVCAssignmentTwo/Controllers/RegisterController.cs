using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCAssignmentTwo.Models;

namespace MVCAssignmentTwo.Controllers
{
    [Route("{controller=Register}/{action=People}/{id?}")]
    public class RegisterController : Controller
    {
        // Your Controller shall use the PeopleService class to handel the interaction of the people data
        // The table should be displayed using an HTML table generated with C# loop
        // The table data should come from a view model, which should have a list of people, and the search phrase if one exists
        readonly IPeopleService _peopleService = new PeopleService();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult People()
        {
            return View(_peopleService.All());
        }

        [HttpPost]
        public IActionResult AddPerson(PeopleViewModel peopleViewModel)
        {
            if (ModelState.IsValid)
            {
                _peopleService.Add(peopleViewModel.CreatePersonViewModel);
            }
            return View("People", _peopleService.All());  //RedirectToAction(nameof(People)); // Set the same viewmodel instead of redirect so that error messages appear 
        }

        [HttpPost]
        public IActionResult Search(PeopleViewModel peopleViewModel)
        {
            return View("People", _peopleService.FindBy(peopleViewModel));
        }

        public IActionResult DeletePerson(int id)
        {
            _peopleService.Remove(id);
            return RedirectToAction(nameof(People));
        }



    }
}
