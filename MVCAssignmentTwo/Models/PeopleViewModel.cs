using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    //container for the information you need in yourpeopleview

    public class PeopleViewModel
    {
        public enum SortMode //Maybe as enums?
        {
            [Display(Name = "Standard")] None,
            [Display(Name = "Name ▲")] NameAscending,
            [Display(Name = "Name ▼")] NameDescending,
            [Display(Name = "City ▲")] CityAscending,
            [Display(Name = "City ▼")] CityDescending
        }
        public  SortMode Sort{ get; set; }
        [Display(Name = "Case Sensitive")]
        public bool CaseSensitive { get; set; }
        [Display(Name = "Search")]
        public string SearchQuery { get; set; }
        public List<Person> Persons { get; set; }

        //Since it was a requirement to have all features in one view and it isnt possible to submit a different viewmodel from a view than the input viewmodel
        //Needed to add a createperosnviewmodel here so the view can use this one to submit it to the createperson action.
        //If used partial views then it would be no problem since you could pass the cCreatePersonViewModel to the partial view
        //But for assignment one where all features in a single big view, Maybe there is a better solution?
        //aka i wana use partialviews
        public CreatePersonViewModel CreatePersonViewModel { get; set; } = new CreatePersonViewModel();
    }
}
