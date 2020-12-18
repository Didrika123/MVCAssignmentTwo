using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCAssignmentTwo.Models;
using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.Services;
using MVCAssignmentTwo.Models.ViewModels;

namespace MVCAssignmentTwo.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICitiesService _citiesService;
        private readonly IPeopleService _peopleService;
        private readonly ICountriesService _countriesService;
        public CitiesController(ICitiesService citiesService, ICountriesService countriesService, IPeopleService peopleService)
        {
            _peopleService = peopleService;
            _citiesService = citiesService; //Maybe make an action for a partial view with list of cities inside the cities controller  ? 
            _countriesService = countriesService;
        }
        public ActionResult CitySelectListData(int id, int countryId)
        {
            ViewBag.SelectedId = id;
            //Todo Move To SerVice
         //   if (id > 0 && countryId == 0)
           //     countryId = _citiesService.FindBy(id).Country.Id;
           // Country c = _countriesService.FindBy(countryId);
            List<IHasIdAndName> list = 
                   _countriesService.FindBy(countryId)?.Cities.OfType<IHasIdAndName>().ToList() 
                ?? _countriesService.All().Countries.FirstOrDefault()?.Cities.OfType<IHasIdAndName>().ToList() 
                ?? new List<IHasIdAndName>(); 
                //_citiesService.All().Cities.OfType<IHasIdAndName>().ToList();
            return PartialView("_SelectListData", list );
        }

        // GET: CitiesController
        public ActionResult Index()
        {
            return View(_citiesService.All());
        }

        // GET: CitiesController/Details/5
        public ActionResult Details(int id)
        {
            return View(_citiesService.FindBy(id));
        }

        // GET: CitiesController/Create
        public ActionResult Create()
        {
            CityViewModel cityViewModel = new CityViewModel();
            cityViewModel.Countries = _countriesService.All().Countries;
            //cityViewModel.Persons = _peopleService.All().Persons;
            return View(cityViewModel);
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityViewModel cityViewModel)
        {
            ModelState.Remove("Country.Name");
            if (ModelState.IsValid)
            {
                City newCity = _citiesService.Add(cityViewModel);
                //Update the cities for peopol
                /*List<Person> people = _peopleService.All().Persons;
                people = cityViewModel.PersonIds.Select(i => people.Find(p => p.Id == i)).ToList();
                people.ForEach(p => { p.TheCity = newCity; _peopleService.Edit(p); }); */
                

                return RedirectToAction(nameof(Index));
            }
            else return View(cityViewModel);
        }

        // GET: CitiesController/Edit/5
        public ActionResult Edit(int id)
        {
            CityViewModel cityViewModel = new CityViewModel(_citiesService.FindBy(id));
            cityViewModel.Countries = _countriesService.All().Countries;
            return View(cityViewModel);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CityViewModel cityViewModel)
        {

            ModelState.Remove("Country.Name");
            if (ModelState.IsValid)
            {
                _citiesService.Edit(id, cityViewModel);
                return RedirectToAction(nameof(Index));
            }
            else return View(cityViewModel); //will crash cuz  it doesnt have the list of countries
        }

        // GET: CitiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_citiesService.FindBy(id));
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CityViewModel cityViewModel)
        {
            _citiesService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
