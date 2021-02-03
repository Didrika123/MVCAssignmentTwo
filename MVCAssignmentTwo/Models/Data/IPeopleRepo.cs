using MVCAssignmentTwo.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    public interface IPeopleRepo
    {
        Person Create(string name, string phoneNumber, City city, List<PersonLanguage> personLanguages);
        List<Person> Read(bool eager = true);
        Person Read(int id);
        Person Update(Person person);
        bool Delete(Person person);
    }
}
