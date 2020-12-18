using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class City : IHasIdAndName
    {
        [Key]
        [Display(Name = "City")]
        [Range(1, int.MaxValue, ErrorMessage = "You need to choose a City.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public Country Country { get; set; }

        public List<Person> Persons { get; set; }

        public City()
        {

        }
        public City(string name, Country country)
            : this(0, name, country)
        {
        }
        public City(int id, string name, Country country)
        {
            Id = id;
            Name = name;
            Country = country;
        }
    }
}
