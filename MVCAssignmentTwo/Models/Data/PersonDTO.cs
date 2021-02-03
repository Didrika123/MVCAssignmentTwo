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
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public City City { get; set; }

        public List<Language> Languages { get; set; }


        public PersonDTO(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            PhoneNumber = person.PhoneNumber;
            City = person.City;
            Languages = person.PersonLanguages.Select(pl => pl.Language).ToList();
            City.Persons = null;
            City.Country.Cities = null;
            foreach (var item in Languages)
                item.PersonLanguages = null;

            // JSON traverses the object and all references to other objects, then from those objects to their objects. So it will run into infinite loop. 
            // EF handles this on its own, but json not. So have to nullify the back-links.
            // Could make a view model or a data transfer object for person and exclude unwanted stuff (PersonDTO)   (Ex skip the joint table and directly list languages)
            //person.City.Persons = null;
            //person.City.Country.Cities = null;
            //person.PersonLanguages?.ForEach(pl => { pl.Language.PersonLanguages = null; pl.Person = null; });
        }
    }
}
