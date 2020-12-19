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
        readonly IPeopleService _peopleService;
        public RegisterController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

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
                return Ok();
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
            ModelState.Remove("City.Name"); //Not so Pretty !
            ModelState.Remove("City.Country");
            ModelState.Remove("City.Country.Name");
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
            ModelState.Remove("City.Name");
            ModelState.Remove("City.Country");
            ModelState.Remove("City.Country.Name");
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
            if (ModelState.IsValid)
            {  
                return PartialView("_PersonListPartialView", _peopleService.FindBy(search, search.PageNumber));
            }
            return RedirectToAction(nameof(PersonList));
        }
    }
}
