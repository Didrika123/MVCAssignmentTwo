using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    interface IPeopleRepo
    {
        Person Create(string name, string phoneNumber, string city);//“parameters needed to create Person excluding id”
        List<Person> Read();
        Person Read(int id);
        Person Update(Person person);
        bool Delete(Person person);
    }
}
