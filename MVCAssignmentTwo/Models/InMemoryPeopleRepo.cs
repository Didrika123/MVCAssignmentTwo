using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    public class InMemoryPeopleRepo : IPeopleRepo
    {
        private static readonly List<Person> _persons = new List<Person>() { 
            new Person() { Id = _idCounter++, Name = "Kalle", PhoneNumber =  "556-6622", City = "Storstan" } ,
            new Person() { Id = _idCounter++, Name = "Morairy", PhoneNumber = "+266 2555", City = "London" },
            new Person() { Id = _idCounter++, Name = "Kammy", PhoneNumber = "+7733 5", City = "Shire" },
        };
        private static int _idCounter;

        public Person Create(string name, string phoneNumber, string city)
        {
            Person newPerson = new Person()
            {
                Name = name,
                PhoneNumber = phoneNumber,
                City = city,
                Id = _idCounter++
            };
            _persons.Add(newPerson);
            return newPerson; //Maybe do some checkkkkkkkKKKKKK, but should it be necessary since we have viewmodel validation? Maybe a check for person alrdy exsists?
        }

        public bool Delete(Person person)
        {
            return _persons.Remove(person);
        }

        public List<Person> Read()
        {
            return _persons;
        }

        public Person Read(int id)
        {
            return _persons.Find(n => n.Id == id);
        }

        public Person Update(Person person)
        {
            int oldPersonIndex = _persons.FindIndex(n => n.Id == person.Id);
            if(oldPersonIndex >= 0)
                _persons[oldPersonIndex] = person;
            return person; // ? hmm
        }
    }
}
