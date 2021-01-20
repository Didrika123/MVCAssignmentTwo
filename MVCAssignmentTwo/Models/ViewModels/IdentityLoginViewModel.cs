using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.ViewModels
{
    public class IdentityLoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Invalid Username.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters, contain digit, uppercase, lowercase and symbol.")]
        public string Password { get; set; }
    }
}
