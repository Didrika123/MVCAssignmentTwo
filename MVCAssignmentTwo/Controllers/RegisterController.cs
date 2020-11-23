using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCAssignmentTwo.Controllers
{
    [Route("{controller=Register}/{action=Index}")]
    public class RegisterController : Controller
    {
        // Your Controller shall use the PeopleService class to handel the interaction of the people data
        // The table should be displayed using an HTML table generated with C# loop
        // The table data should come from a view model, which should have a list of people, and the search phrase if one exists
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult People()
        {
            return View();
        }
    }
}
