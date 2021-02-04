using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MVCAssignmentTwo.Models;
using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCAssignmentTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {

        readonly IPeopleService _peopleService;
        readonly ICitiesService _citiesService;
        readonly ICountriesService _countriesService;
        readonly ILanguagesService _languagesService;
        public ReactController(IPeopleService peopleService, ICitiesService citiesService, ILanguagesService languagesService, ICountriesService countriesService)
        {
            _peopleService = peopleService;
            _citiesService = citiesService;
            _languagesService = languagesService;
            _countriesService = countriesService;
        }

        // GET: api/<ReactController>
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _peopleService.All(eager: false).Persons;
        }

        [HttpGet("cities")]
        public IEnumerable<City> GetCities()
        {
            return _citiesService.All(false).Cities;
        }
        [HttpGet("countries")]
        public IEnumerable<Country> GetCountries()
        {
            return _countriesService.All(eager: false).Countries;
        }

        [HttpGet("languages")]
        public IEnumerable<Language> GetLanguages()
        {
            return _languagesService.All().Languages;
        }

        // GET api/<ReactController>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            Person person = _peopleService.FindBy(id);
            if (person == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return MakeDTOish(person);
        }

        // POST api/<ReactController>
        [HttpPost]
        public void Post([FromBody] CreatePersonViewModel createPerson)
        {
            if (ModelState.IsValid)
            {
                if (_peopleService.Add(createPerson) != null)
                    Response.StatusCode = 201;  // Success - Created
                else Response.StatusCode = 500; // Database failed to crate
            }
            else Response.StatusCode = 400;     // Bad request - validation fail
        }

        // For Edit Person
        // PUT api/<ReactController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ReactController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (_peopleService.FindBy(id) != null)
            {
                if (_peopleService.Remove(id))
                    Response.StatusCode = 200;  // Success - removed
                else Response.StatusCode = 500; // Database failed
            }
            else Response.StatusCode = 404;     // Not found
        }

        static Person MakeDTOish(Person person)
        {
            // JSON traverses the object and all references to other objects, then from those objects to their objects. So it will run into infinite loop. 
            // EF handles this on its own, but json not. So have to nullify the back-links.
            // Could make a view model or a data transfer object for person and exclude unwanted stuff (PersonDTO)   (Ex skip the joint table and directly list languages)
            person.City.Persons = null;
            person.City.Country.Cities = null;
            person.PersonLanguages?.ForEach(pl => { pl.Language.PersonLanguages = null; pl.Person = null; });
            return person;
        }
    }
}
