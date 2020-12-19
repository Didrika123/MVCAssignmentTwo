using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MVCAssignmentTwo.Models.Data
{
    public class DatabasePeopleRepo : IPeopleRepo
    {
        readonly RegisterDbContext _personDbContext;
        public DatabasePeopleRepo(RegisterDbContext personDbContext)
        {
            _personDbContext = personDbContext;
        }
        public Person Create(string name, string phoneNumber, City city)
        {
            Person person = new Person(name, phoneNumber, city); 
            EntityEntry<Person> entityEntry = _personDbContext.Persons.Add(person);  // When u do something thru DB Context a hook will be created, if info changes without altering the person object inside that hook there will be problem (For example if you do another db call inside some method that might alter info) Like add -> findby
            _personDbContext.SaveChanges();                                          // AKA, EF keeps track of all objects in memory that are READ from the database, So if you Have multiple in memory and try to change one, It wont work ! (So therfore Eager / Lazy is important to know )
            person = entityEntry.Entity;  
            return person;
        }

        public bool Delete(Person person)
        {
            EntityEntry entityEntry = _personDbContext.Persons.Remove(person);
            _personDbContext.SaveChanges();  //Save changes return number of lines in db changed. so u can do a check instead:   if savechange > 0 then sucesful
            return entityEntry.State == EntityState.Deleted;
        }

        public List<Person> Read()
        {
            return _personDbContext.Persons.Include(p => p.City).ThenInclude(c => c.Country).OrderByDescending(p => p.Id).ToList(); //For aesthetic: When getting unfiltered list, make new entries come at the top. (Mabe order by Date (date updated / created)) 
        }

        public Person Read(int id)  
        {
            return _personDbContext.Persons.Include(p => p.City).ThenInclude(c => c.Country).SingleOrDefault(p => p.Id == id);
        }

        public Person Update(Person person)
        {
            EntityEntry<Person> entityEntry = _personDbContext.Persons.Update(person);
             _personDbContext.SaveChanges();
            return entityEntry.Entity;
        }
    }
}
