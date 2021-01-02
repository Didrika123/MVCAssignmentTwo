using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCAssignmentTwo.Models.Data;

namespace MVCAssignmentTwo.Models
{
    [Table("Person")] //Custom Name
    public class Person : IHasIdAndName
    {
        [Key]
        [Display(Name = "Person")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]  //Very good to add because the db will limit the string (otherwise it will use default: which is a up to 2gb string)
        [Column("Name")] 
        public string Name { get; set; }

        [MaxLength(100)]
        public string PhoneNumber { get; set; }


        /* [NotMapped] //If you want to exclude properties from db
        public string City  */

        [Required]
        public City City { get; set; }

        public List<PersonLanguage> PersonLanguages { get; set; }


        public Person()
        {

        }
        public Person(string name, string phonenum, City city, List<PersonLanguage> personLanguages)
        {
            Name = name;
            PhoneNumber = phonenum;
            City = city;
            PersonLanguages = personLanguages;
        }

        public Person(int id, string name, string phonenum, City city, List<PersonLanguage> personLanguages) : this(name, phonenum, city, personLanguages)
        {
            Id = id;
        }
    }
}
