using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    public interface IPeopleService
    {
        Person Add(CreatePersonViewModel person);

        PeopleViewModel All();
        PeopleViewModel All(int page);
        PeopleViewModel FindBy(PeopleViewModel search);
        PeopleViewModel FindBy(PeopleViewModel search, int page);
        Person FindBy(int id);
        Person Edit(int id, CreatePersonViewModel person);
        bool Remove(int id);
    }
}
