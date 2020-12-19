using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Services
{
    public class CitiesService : ICitiesService
    {
        readonly ICitiesRepo _citiesRepo;
        readonly ICountriesService _countriesService;
        public CitiesService(ICitiesRepo citiesRepo, ICountriesService countriesService)
        {
            _citiesRepo = citiesRepo;
            _countriesService = countriesService;
        }
        public City Add(CityViewModel city)
        {
            city.Country = _countriesService.FindBy(city.Country.Id);
            if (city.Country == null)
                return null;

            return _citiesRepo.Create(city.Name, city.Country);
        }

        public CitiesViewModel All()
        {
            return new CitiesViewModel() { Cities = _citiesRepo.Read() };
        }
        public CitiesViewModel All(int pageNr)
        {
            return new CitiesViewModel() { Cities = _citiesRepo.Read() };
        }


        public City Edit(int id, CityViewModel city)
        {
            city.Country = _countriesService.LazyFindBy(city.Country.Id); //If using eager load it crashes due to city have a duplicate tracked person (in its list of citizens)
            if (city.Country == null)
                return null;
            City editCity = new City(id, city.Name, city.Country);
            return _citiesRepo.Update(editCity);
        }

        public CitiesViewModel FindBy(CitiesViewModel search)
        {
            return null;
        }
        public CitiesViewModel FindBy(CitiesViewModel search, int pageNr)
        {
            return FindBy(search);
        }
        public City FindBy(int id)
        {
            return _citiesRepo.Read(id);
        }
        public City LazyFindBy(int id)
        {
            return _citiesRepo.LazyRead(id);
        }

        public bool Remove(int id)
        {
            return _citiesRepo.Delete(_citiesRepo.Read(id));
        }

        public List<IHasIdAndName> GetCitiesOfCountry(int countryId)
        {
            List<IHasIdAndName> list =
                   _countriesService.FindBy(countryId)?.Cities.OfType<IHasIdAndName>().ToList()                 //Find the country, then return the cities of that country
                ?? _countriesService.All().Countries.FirstOrDefault()?.Cities.OfType<IHasIdAndName>().ToList()  //if not found the country then return the first country's cities.
                ?? new List<IHasIdAndName>();                                                                   //If no countries/cities in database, return empty ist
                //_citiesService.All().Cities.OfType<IHasIdAndName>().ToList();                                 //Return all cities (Not used anymore)
            return list;               
        }
    }
}
