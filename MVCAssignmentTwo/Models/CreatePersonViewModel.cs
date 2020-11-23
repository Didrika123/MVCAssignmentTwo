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
        public string PhoneNumber { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
    }
}
