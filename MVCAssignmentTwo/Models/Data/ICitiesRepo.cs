using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public interface ICitiesRepo
    {
        City Create(string name, Country country);
        List<City> Read();
        City Read(int id);
        City LazyRead(int id); //Needed to remove entity tracking dependency when updating
        City Update(City city);
        bool Delete(City city);
    }
}
