using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Services
{
    public interface ICitiesService
    {
        City Add(CityViewModel person);

        CitiesViewModel All();
        CitiesViewModel All(int page);
        CitiesViewModel FindBy(CitiesViewModel search);
        CitiesViewModel FindBy(CitiesViewModel search, int page);
        City FindBy(int id);
        City LazyFindBy(int id);
        City Edit(int id, CityViewModel cityViewModel);
        bool Remove(int id);
        List<IHasIdAndName> GetCitiesOfCountry(int countryId);
    }
}
