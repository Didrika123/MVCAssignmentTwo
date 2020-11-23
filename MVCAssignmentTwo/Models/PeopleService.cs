using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    public class PeopleService : IPeopleService
    {
        readonly IPeopleRepo _peopleRepo = new InMemoryPeopleRepo();
        public Person Add(CreatePersonViewModel person)
        {
            return _peopleRepo.Create(person.Name, person.PhoneNumber, person.City);
        }

        public PeopleViewModel All()
        {
            return new PeopleViewModel() { Persons = _peopleRepo.Read() };
        }

        public Person Edit(int id, Person person)//What to use id for?
        {
            return _peopleRepo.Update(person); 
        }

        public PeopleViewModel FindBy(PeopleViewModel search)
        {
            throw new NotImplementedException();
        }

        public Person FindBy(int id)
        {
            return _peopleRepo.Read(id);
        }

        public bool Remove(int id)
        {
            return _peopleRepo.Delete(_peopleRepo.Read(id));
        }
    }
}
