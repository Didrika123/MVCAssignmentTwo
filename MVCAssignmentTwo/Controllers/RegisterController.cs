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

        public IActionResult People()
        {
            return View(_peopleService.All(0));
        }

        public IActionResult GetPerson(int id)
        {
            Person person = _peopleService.FindBy(id);
            if (person != null)
            {
                return PartialView("_PersonPartialView", person);
            }
            else return NotFound(); //404
        }
        public IActionResult DeletePerson(int id)
        {
            if (_peopleService.Remove(id))
                //return RedirectToAction(nameof(PersonList));  //return Content(""); // RedirectToAction(nameof(People)); //Return a partial view clearing the person div?
                return Content("");
            else return NotFound();
        }

        [HttpGet]
        public IActionResult EditPerson(int id)
        {
            Person person = _peopleService.FindBy(id);
            if(person != null)
            {
                CreatePersonViewModel createPersonViewModel = new CreatePersonViewModel(person);
                return PartialView("_EditPersonPartialView", createPersonViewModel);
            }
            else return NotFound(); //404
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPerson(int id, CreatePersonViewModel createPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                Person person = _peopleService.Edit(id, createPersonViewModel);
                if (person != null)
                {
                    //SUCCESFUL
                    return PartialView("_PersonPartialView", person);
                }
            }
            Response.StatusCode = 400;
            return PartialView("_EditPersonPartialView", createPersonViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePerson(CreatePersonViewModel createPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                Person person = _peopleService.Add(createPersonViewModel);
                if (person != null)
                {
                    //SUCCESFUL
                    return RedirectToAction(nameof(PersonList));
                }
            }
            Response.StatusCode = 400; 
            return PartialView("_CreatePersonPartialView", createPersonViewModel);
        }

        [HttpGet]
        public IActionResult PersonList(int id)
        {
            return PartialView("_PersonListPartialView", _peopleService.All(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonList(PeopleViewModel search)
        {
           // if (ModelState.IsValid) //think it failes because CreatePersonViewModel is null (which have reqs in it), maybe dont have to be so harsh for a search ? or maybe should create a special viewmodel for search or maybe use the Route ids
            {  
                return PartialView("_PersonListPartialView", _peopleService.FindBy(search, search.PageNumber));
            }
            //return RedirectToAction(nameof(PersonList));
        }
    }
}
