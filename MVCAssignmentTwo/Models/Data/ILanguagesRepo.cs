using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public interface ILanguagesRepo
    {
        Language Create(string name, string abbreviation);
        List<Language> Read();
        Language Read(int id);
        Language LazyRead(int id);
        Language Update(Language language);
        bool Delete(Language language);
    }
}
