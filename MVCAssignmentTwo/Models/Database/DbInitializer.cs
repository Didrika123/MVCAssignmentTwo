using MVCAssignmentTwo.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Database
{
    public static class DbInitializer
    {
        public static void Initialize(RegisterDbContext context)
        {

            //context.Database.EnsureCreated();

            if (context.Persons.Any())
            {
                return;   // DB has been seeded
            }

            // Countries
            var countries = new Country[]
            {
                new Country("Mallorca"),
                new Country("Arabugi"),
                new Country("Finland"),
                new Country("Bulgaria"),
                new Country("Massachusets"),
            };
            foreach (Country country in countries)
            {
                context.Countries.Add(country);
            }
            context.SaveChanges();


            // Cities
            var cities = new City[]
            {
                new City("Akko", countries[0]),
                new City("Manabu", countries[1]),
                new City("Helsinki", countries[2]),
                new City("Poron Käristys", countries[2]),
                new City("Sofia", countries[3]),
                new City("Alagal", countries[3]),
                new City("Mondoi", countries[3]),
            };
            foreach (City city in cities)
            {
                context.Cities.Add(city);
            }
            context.SaveChanges();


            // Languages
            var langs = new Language[]
            {
                new Language("Spanish", "ES"),
                new Language("Swahili", "SW"),
                new Language("Turkish", "TR"),
                new Language("English", "EN"),
                new Language("Chinese", "CH"),
            };
            foreach (Language lang in langs)
            {
                context.Languages.Add(lang);
            }
            context.SaveChanges();



            // Persons
            var persons = new Person[]
            {
                new Person("Carson Alexander", "2010-09-01", cities[0], new List<PersonLanguage>(){ new PersonLanguage() { Language = langs[4] }, new PersonLanguage() { Language = langs[0] },   }),
                new Person("Meredith Alonso","2012-09-01", cities[1], new List<PersonLanguage>(){ new PersonLanguage() { Language = langs[3] }, new PersonLanguage() { Language = langs[2] },   }) ,
                new Person("Arturo Anand", "2013-09-01", cities[3], new List<PersonLanguage>(){ new PersonLanguage() { Language = langs[1] }, new PersonLanguage() { Language = langs[0] },   }) ,
                new Person("Gytis Barzdukas","2012-09-01", cities[0], new List<PersonLanguage>(){ new PersonLanguage() { Language = langs[4] } ,   }) ,
                new Person("Yan Li","2012-09-01", cities[1], new List<PersonLanguage>(){ new PersonLanguage() { Language = langs[0] }, new PersonLanguage() { Language = langs[2] },   }) ,
                new Person("Peggy Justice","2011-09-01", cities[4], new List<PersonLanguage>(){ new PersonLanguage() { Language = langs[3] }, new PersonLanguage() { Language = langs[1] },   }) ,
                new Person("Laura Norman","2013-09-01", cities[2], new List<PersonLanguage>()) ,
                new Person("Nino Olivetto","2005-09-01", cities[2], new List<PersonLanguage>(){ new PersonLanguage() { Language = langs[1] }, new PersonLanguage() { Language = langs[2] },   })
            };

            foreach (Person person in persons)
            {
                context.Persons.Add(person);
            }
            context.SaveChanges();
        }
    }
}
