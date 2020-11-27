using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    public class Person
    {
        public int Id { get; set; }
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
