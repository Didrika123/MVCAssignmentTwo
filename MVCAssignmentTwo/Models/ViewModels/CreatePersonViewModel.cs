using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    // Use to prevent overposting and to use data annotations to validate inputs when creating new person
    public class CreatePersonViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Phone number")]
        [StringLength(20, MinimumLength = 6)]
        public string PhoneNumber { get; set; }

        [Required]
        public City City { get; set; } = new City() { Country = new Country() };

        public List<City> Cities { get; set; }

        //I added a special viewmodel for languageselection because it wasnt possible to have an "EditorTemplate/EditorFor" for a list
        public LanguageSelectionViewModel LanguageSelectionViewModel { get; set; } = new LanguageSelectionViewModel();

        public CreatePersonViewModel()
        {

        }
        public CreatePersonViewModel(Person person)
        {
            if (person != null)
            {
                Name = person.Name;
                PhoneNumber = person.PhoneNumber;
                City = person.City ?? new City();
                LanguageSelectionViewModel.LanguageIds = person.PersonLanguages.Select(pl => pl.LanguageId).ToList<int>();
            }
        }
    }
}
