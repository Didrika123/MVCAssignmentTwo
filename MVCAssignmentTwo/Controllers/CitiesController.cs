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
        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService; 
        }
        public ActionResult CitySelectListData(int id, int countryId)
        {
            ViewBag.SelectedId = id;
            return PartialView("_SelectListData", _citiesService.GetCitiesOfCountry(countryId));
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
            return View(new CityViewModel());
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
                return RedirectToAction(nameof(Index));
            }
            else return View(cityViewModel);
        }

        // GET: CitiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new CityViewModel(_citiesService.FindBy(id)));
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
            else return View(cityViewModel);
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
