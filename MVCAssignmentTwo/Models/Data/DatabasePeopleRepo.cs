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
        public Person Create(string name, string phoneNumber, string city)
        {
           // Person person = new Person(_personDbContext.Persons.Last().Id + 1, name, phoneNumber, city); //Hmmm, Id ?, we want to make sure its unique and incrementing correctly !
            Person person = new Person(name, phoneNumber, city); //Hmmm, Actually let db do ID handling !
                                                                    // person = _personDbContext.Persons.Add(person).Entity; //I hoped that this would give a new Id, but it didnt 
            EntityEntry<Person> entityEntry = _personDbContext.Persons.Add(person);
            _personDbContext.SaveChanges();  //I guess when u save changes it will be given an id, But how do i get it here ? 
            person = entityEntry.Entity;  //NICE it worke !
            return person;
        }

        public bool Delete(Person person)
        {
            EntityEntry entityEntry = _personDbContext.Persons.Remove(person);
            _personDbContext.SaveChanges();
            return entityEntry.State == EntityState.Deleted;
        }

        public List<Person> Read()
        {
            return _personDbContext.Persons.OrderByDescending(p => p.Id).ToList(); //For aesthetic: When getting unfiltered list, make new entries come at the top. (Mabe order by Date (date updated / created)) 
        }

        public Person Read(int id)  
        {
            //where(row => row.name.contains
            return _personDbContext.Persons.SingleOrDefault(p => p.Id == id);
        }

        public Person Update(Person person)
        {
            EntityEntry<Person> entityEntry = _personDbContext.Persons.Update(person);
            _personDbContext.SaveChanges();
            return entityEntry.Entity;
        }
    }
}
