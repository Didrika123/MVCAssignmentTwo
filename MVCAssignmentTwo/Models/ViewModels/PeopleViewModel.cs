using Microsoft.AspNetCore.Mvc;
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

        public bool IsThereMorePages { get; set; }

        public string FilterString { get; set; }


        [HiddenInput(DisplayValue = false)]
        public int PageNumber { get; set; }
        public CreatePersonViewModel CreatePersonViewModel { get; set; } = new CreatePersonViewModel();
    }
}
