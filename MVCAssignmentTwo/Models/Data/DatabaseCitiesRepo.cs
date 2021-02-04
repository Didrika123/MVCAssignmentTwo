using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class DatabaseCitiesRepo : ICitiesRepo
    {

        readonly RegisterDbContext _registerDbContext;
        public DatabaseCitiesRepo(RegisterDbContext cityDbContext)
        {
            _registerDbContext = cityDbContext;
        }
        public City Create(string name, Country country)
        {
            City city = new City(name, country);       
            EntityEntry<City> entityEntry = _registerDbContext.Cities.Add(city);
            _registerDbContext.SaveChanges(); 
            city = entityEntry.Entity;  
            return city;
        }

        public bool Delete(City city)
        {
            EntityEntry entityEntry = _registerDbContext.Cities.Remove(city);
            _registerDbContext.SaveChanges();
            return entityEntry.State == EntityState.Deleted;
        }

        public List<City> Read(bool eager)
        {
            if(eager)
                return _registerDbContext.Cities.Include(c => c.Country).Include(c => c.Persons).OrderByDescending(p => p.Id).ToList();
            return _registerDbContext.Cities.ToList();
        }

        public City Read(int id)
        {
            return _registerDbContext.Cities.Include(c => c.Country).Include(c => c.Persons).SingleOrDefault(p => p.Id == id);
        }
        public City LazyRead(int id)
        {
            return _registerDbContext.Cities
                .SingleOrDefault(p => p.Id == id);
        }

        public City Update(City city)
        {
            EntityEntry<City> entityEntry = _registerDbContext.Cities.Update(city);
            _registerDbContext.SaveChanges();
            return entityEntry.Entity;
        }
    }
}
