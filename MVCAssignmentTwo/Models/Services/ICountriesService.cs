using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Services
{
    public interface ICountriesService
    {
        Country Add(CountryViewModel createCountry);

        CountriesViewModel All();
        CountriesViewModel All(int page);
        CountriesViewModel FindBy(CountriesViewModel search);
        CountriesViewModel FindBy(CountriesViewModel search, int page);
        Country FindBy(int id);
        Country LazyFindBy(int id);
        Country Edit(int id, CountryViewModel createCountry);
        bool Remove(int id);
    }
}
