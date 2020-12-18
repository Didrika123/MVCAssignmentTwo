using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class Country : IHasIdAndName
    {
        [Key]
        [Display(Name="Country")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public List<City> Cities { get; set; }

        public Country()
        {
        }
        public Country(string name)
        {
            Name = name;
        }
        public Country(int id, string name) : this(name)
        {
            Id = id;
        }
    }
}
