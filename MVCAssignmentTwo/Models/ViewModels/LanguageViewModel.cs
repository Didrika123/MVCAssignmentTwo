using MVCAssignmentTwo.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.ViewModels
{
    public class LanguageViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(5)]
        public string Abbreviation { get; set; }


        public List<Person> Persons = new List<Person>();

        public LanguageViewModel()
        {

        }
        public LanguageViewModel(Language language)
        {
            if (language != null)
            {
                Name = language.Name;
                Abbreviation = language.Abbreviation;
            }
        }
    }
}
