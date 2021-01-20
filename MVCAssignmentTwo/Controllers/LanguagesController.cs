using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.Services;
using MVCAssignmentTwo.Models.ViewModels;

namespace MVCAssignmentTwo.Controllers
{
    [Authorize(Roles = "Peach,Banana")]
    public class LanguagesController : Controller
    {
        private readonly ILanguagesService _languagesService;
        public LanguagesController(ILanguagesService languagesService)
        {
            _languagesService = languagesService;
        }
        [AllowAnonymous]
        public ActionResult LanguageSelectListData(string selectedIds)
        {
            if (User.IsInRole("Peach") || User.IsInRole("Banana") || User.IsInRole("Apple"))
            {
                if (!String.IsNullOrEmpty(selectedIds))
                    ViewBag.SelectedIds = selectedIds.Split(',');

                return PartialView("_SelectListData", _languagesService.All().Languages.OfType<IHasIdAndName>().ToList());
            }
            return NotFound();
        }
        // GET: LanguagesController
        public ActionResult Index()
        {
            return View(_languagesService.All());
        }

        // GET: LanguagesController/Details/5
        public ActionResult Details(int id)
        {
            return View(_languagesService.FindBy(id));
        }

        // GET: LanguagesController/Create
        public ActionResult Create()
        {
            return View(new LanguageViewModel());
        }

        // POST: LanguagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LanguageViewModel createLanguage)
        {
            if (ModelState.IsValid)
            {
                _languagesService.Add(createLanguage);
                return RedirectToAction(nameof(Index));
            }
            else return View(createLanguage);
        }

        // GET: LanguagesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_languagesService.FindBy(id));
        }

        // POST: LanguagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LanguageViewModel editLanguage)
        {
            if (ModelState.IsValid)
            {
                _languagesService.Edit(id, editLanguage);
                return RedirectToAction(nameof(Index));
            }
            else return View(editLanguage);
        }

        // GET: LanguagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_languagesService.FindBy(id));
        }

        // POST: LanguagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, LanguageViewModel createLanguage)
        {
            _languagesService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
