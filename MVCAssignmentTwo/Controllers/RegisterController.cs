using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCAssignmentTwo.Models;
using Nancy.Json;

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
            if (_peopleService.Remove(id))
                return Content(""); // RedirectToAction(nameof(People)); //Return a partial view clearing the person div?
            else return NotFound();
        }


        //For assignment 3 below

        [HttpPost]
        public IActionResult CreatePerson(CreatePersonViewModel createPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                _peopleService.Add(createPersonViewModel);
            }
            return RedirectToAction(nameof(People));
        }

        public IActionResult EditPerson(int id)
        {
            Person person = _peopleService.FindBy(id);
            // CreatePersonViewModel createPersonViewModel = new CreatePersonViewModel(person);
            //return PartialView("_EditPersonPartialView", createPersonViewModel);

            if (person == null)
                return NotFound(); //404

            return PartialView("_EditPersonPartialView", person);
        }

        [HttpPost]
        public IActionResult EditPerson(int id, CreatePersonViewModel personViewModel)
        {
            Person person = null;
            if (ModelState.IsValid)
            {
                _peopleService.Edit(id, personViewModel);
            }
            person ??= _peopleService.FindBy(id);
            return PartialView("_PersonPartialView", person);
        }



        [HttpPost]
        public IActionResult EditPerson2(int id, string model)
        {
            Person person = null;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var fromJSON = serializer.Deserialize<CreatePersonViewModel>(model);
            if(fromJSON is CreatePersonViewModel)
                person = _peopleService.Edit(id, fromJSON as CreatePersonViewModel);
            //Json(fromJSON);//
            person ??= _peopleService.FindBy(id) ?? new Person();
            return PartialView("_PersonPartialView", person);
        }




        [HttpPost]
        public IActionResult CreatePerson2(string model)
        {
            Person person = null;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var fromJSON = serializer.Deserialize<CreatePersonViewModel>(model);
            if (fromJSON is CreatePersonViewModel)
                person = _peopleService.Add(fromJSON as CreatePersonViewModel);

            return PartialView("_PersonListPartialView", _peopleService.All().Persons);
        }
    }
}
