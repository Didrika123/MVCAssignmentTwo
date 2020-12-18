using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.Services;
using MVCAssignmentTwo.Models.ViewModels;

namespace MVCAssignmentTwo.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountriesService _countriesService;
        private readonly ICitiesService _citiesService;
        public CountriesController(ICountriesService countryService, ICitiesService citiesService)
        {
            _countriesService = countryService;
            _citiesService = citiesService; //Maybe make an action for a partial view with list of cities inside the cities controller  ? 
        }

        public ActionResult CountrySelectListData(int id)
        {
            ViewBag.SelectedId = id;
            return PartialView("_SelectListData", _countriesService.All().Countries.OfType<IHasIdAndName>().ToList());
        }
        // GET: CountriesController
        public ActionResult Index()
        {
            return View(_countriesService.All());
        }

        // GET: CountriesController/Details/5
        public ActionResult Details(int id)
        {
            return View(_countriesService.FindBy(id));
        }

        // GET: CountriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryViewModel countryViewModel)
        {
            if (ModelState.IsValid)
            {
                _countriesService.Add(countryViewModel);
                return RedirectToAction(nameof(Index));
            }
            else return View(countryViewModel);
        }

        // GET: CountriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_countriesService.FindBy(id));
        }

        // POST: CountriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CountryViewModel countryViewModel)
        {

            if (ModelState.IsValid)
            {
                _countriesService.Edit(id, countryViewModel);
                return RedirectToAction(nameof(Index));
            }
            else return View(countryViewModel);
        }

        // GET: CountriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_countriesService.FindBy(id));
        }

        // POST: CountriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CountryViewModel countryViewModel)
        {
            _countriesService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
