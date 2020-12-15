using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVCAssignmentTwo.Models
{
    [Table("Person")] //Custom Name
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]  //Very good to add because the db will limit the string (otherwise it will use default: which is a up to 2gb string)
        [Column("Name")] //Custom Name
        public string Name { get; set; }

        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        public Person()
        {

        }
        public Person(string name, string phonenum, string city)
        {
            Name = name;
            PhoneNumber = phonenum;
            City = city;
        }

        public Person(int id, string name, string phonenum, string city)
        {
            Id = id;
            Name = name;
            PhoneNumber = phonenum;
            City = city;
        }
    }
}
