using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MVCAssignmentTwo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCAssignmentTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {

        readonly IPeopleService _peopleService;
        public ReactController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        // GET: api/<ReactController>
        [HttpGet]
        //[DisableCors]
        public IEnumerable<Person> Get()
        {
            // Dont need this anymore, cause added cors polecy
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");  // Allow calls coming from outside the Host
            return _peopleService.All(eager: false).Persons;
        }


        // GET api/<ReactController>/5
        [HttpGet("{id}")]
        public PersonDTO Get(int id)
        {
            Person person = _peopleService.FindBy(id);
            if (person == null)
            {
                Response.StatusCode = 404;
                return null;
            }
         
            return new PersonDTO(person);
        }

        // POST api/<ReactController>
        [HttpPost]
        public void Post([FromBody] CreatePersonViewModel createPerson)
        {
            //createPerson = new CreatePersonViewModel(person)
            // Todo add check for cities, languages exist
            if (ModelState.IsValid)
            {
                if (_peopleService.Add(createPerson) != null)
                    Response.StatusCode = 201;  // Success - Created
                else Response.StatusCode = 500; // Database failed to crate
            }
            else Response.StatusCode = 400;     // Bad request - validation fail
        }
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
    }
}
