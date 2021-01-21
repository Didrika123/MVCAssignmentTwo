using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.ViewModels
{
    public class IdentityCreateViewModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Invalid Name.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Invalid Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select a date.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Birthdate.")]
        [ValidateBirthdate]
        public DateTime Birthdate { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Invalid Username.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage ="Password must be at least 6 characters, contain digit, uppercase, lowercase and symbol.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords doesn't match.")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }



        public class ValidateBirthdate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if(value is DateTime)
                {
                    if (DateTime.Now.AddYears(-18).Date.CompareTo(((DateTime)value).Date) == 0)
                        return new ValidationResult("Happy Birthday !");
                    else if (DateTime.Now.AddYears(-18).Date.CompareTo(((DateTime)value).Date) > 0)
                        return ValidationResult.Success;
                }
                return new ValidationResult("You have to be a minimum age of 18 years to use this service.");
            }
        }
    }
}
