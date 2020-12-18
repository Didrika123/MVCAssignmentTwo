using MVCAssignmentTwo.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.ViewModels
{
    public class CountryViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }


        public List<City> Cities;

        public CountryViewModel()
        {

        }
        public CountryViewModel(Country country)
        {
            if (country != null)
            {
                Name = country.Name;
                Cities = country.Cities;
            }
        }
    }
}
