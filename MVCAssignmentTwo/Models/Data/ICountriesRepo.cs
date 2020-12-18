using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public interface ICountriesRepo
    {
        Country Create(string name);
        List<Country> Read();
        Country Read(int id);
        Country LazyRead(int id);
        Country Update(Country country);
        bool Delete(Country country);
    }
}
