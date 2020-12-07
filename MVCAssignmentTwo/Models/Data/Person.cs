using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public Person()
        {

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
