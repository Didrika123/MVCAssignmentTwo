using MVCAssignmentTwo.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.ViewModels
{
    public class CityViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }


        public Country Country { get; set; } = new Country();
        public List<Country> Countries { get; set; }

        public List<Person> Persons { get; set; }


        public CityViewModel()
        {

        }
        public CityViewModel(City city)
        {
            if (city != null)
            {
                Name = city.Name;
                Persons = city.Persons;
                Country = city.Country;
            }
        }
    }
}
