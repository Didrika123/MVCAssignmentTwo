using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string FirstName { get; set; }

        [PersonalData]
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string LastName { get; set; }

        [PersonalData]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}
