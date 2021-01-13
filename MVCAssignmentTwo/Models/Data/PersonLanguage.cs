using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class PersonLanguage // Association table or join table  (Only required of EF to understand relationships for database, will not be required in asp.net 5.0)
    {
         // You could add an Id here for the PersonLanguages ( For some databases its even required, but not sqlserver )
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
