using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Services
{
    public class CountriesService : ICountriesService
    {
        readonly ICountriesRepo _countriesRepo;
        public CountriesService(ICountriesRepo countriesRepo)
        {
            _countriesRepo = countriesRepo;
        }
        public Country Add(CountryViewModel country)
        {
            return _countriesRepo.Create(country.Name);
        }

        public CountriesViewModel All(bool eager)
        {
            return new CountriesViewModel() { Countries = _countriesRepo.Read(eager) };
        }
        public CountriesViewModel All(int pageNr)
        {
            return new CountriesViewModel() { Countries = _countriesRepo.Read() };
        }


        public Country Edit(int id, CountryViewModel country)
        {
            Country editCountry = new Country(id, country.Name);
            return _countriesRepo.Update(editCountry);
        }

        public CountriesViewModel FindBy(CountriesViewModel search)
        {
            return null;
        }
        public CountriesViewModel FindBy(CountriesViewModel search, int pageNr)
        {
            return FindBy(search);
        }
        public Country FindBy(int id)
        {
            return _countriesRepo.Read(id);
        }

        public Country LazyFindBy(int id)
        {
            return _countriesRepo.LazyRead(id);
        }
        public bool Remove(int id)
        {
            return _countriesRepo.Delete(_countriesRepo.Read(id));
        }
    }
}
