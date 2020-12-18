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
    public class Person
    {
        [Key]
        [Display(Name = "Person")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]  //Very good to add because the db will limit the string (otherwise it will use default: which is a up to 2gb string)
        [Column("Name")] //Custom Name
        public string Name { get; set; }

        [MaxLength(100)]
        public string PhoneNumber { get; set; }


        /*string _oldCity = "";
        [NotMapped]
        public string City { get { if (TheCity != null) return TheCity.Name; else return _oldCity; } set { _oldCity = value; } }
        */
        [Required]
        public City City { get; set; }


        public Person()
        {

        }
        public Person(string name, string phonenum, City city)
        {
            Name = name;
            PhoneNumber = phonenum;
            City = city;
        }

        public Person(int id, string name, string phonenum, City city) : this(name, phonenum, city)
        {
            Id = id;
        }
    }
}
