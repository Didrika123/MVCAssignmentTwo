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

        [Required]
        public Country Country { get; set; } = new Country(); //Country the city lies in

        public List<Person> Persons { get; set; } //The citizens of the city


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
