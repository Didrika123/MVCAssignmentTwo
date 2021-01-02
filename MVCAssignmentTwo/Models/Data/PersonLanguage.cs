using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class PersonLanguage
    {

        [Required]
        public int PersonId { get; set; }
        [Required]
        public Person Person { get; set; }

        [Required]
        public int LanguageId { get; set; }  //its important that its Named like this, LanguageId for the Class Language. PersonId for the Person class 
        [Required]
        public Language Language { get; set; }
    }
}
