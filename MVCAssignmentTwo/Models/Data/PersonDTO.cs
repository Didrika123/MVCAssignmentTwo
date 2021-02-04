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
    public class PersonDTO : IHasIdAndName
    {
        // Felt it was a bit overkill with DTO
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public City City { get; set; }

        public List<Language> Languages { get; set; }


        public PersonDTO(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            PhoneNumber = person.PhoneNumber;
            //City = person.City;
            Languages = person.PersonLanguages.Select(pl => pl.Language).ToList();
            //City.Persons = null;
            //City.Country.Cities = null;
            foreach (var item in Languages)
                item.PersonLanguages = null;
             //person.City.Persons = null;
            //person.City.Country.Cities = null;
            //person.PersonLanguages?.ForEach(pl => { pl.Language.PersonLanguages = null; pl.Person = null; });
        }
        public Person ToPerson()
        {
            return new Person();
        }
    }
}
