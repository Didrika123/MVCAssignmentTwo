using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class DatabaseCountriesRepo : ICountriesRepo
    {
        readonly RegisterDbContext _registerDbContext;
        public DatabaseCountriesRepo(RegisterDbContext countryDbContext)
        {
            _registerDbContext = countryDbContext;
        }
        public Country Create(string name)
        {
            Country country = new Country(name);
            EntityEntry<Country> entityEntry = _registerDbContext.Countries.Add(country);
            _registerDbContext.SaveChanges();
            country = entityEntry.Entity;
            return country;
        }

        public bool Delete(Country country)
        {
            EntityEntry entityEntry = _registerDbContext.Countries.Remove(country);
            _registerDbContext.SaveChanges();
            return entityEntry.State == EntityState.Deleted;
        }

        public List<Country> Read()
        {
            return _registerDbContext.Countries
                .Include(c => c.Cities)
                .ThenInclude(c => c.Persons)
                .OrderByDescending(p => p.Id).ToList();
        }

        public Country Read(int id)
        {
            //where(row => row.name.contains
            return _registerDbContext.Countries
                .Include(c => c.Cities)
                .ThenInclude(c => c.Persons)
                .SingleOrDefault(p => p.Id == id);
        }

        public Country LazyRead(int id)
        {
            return _registerDbContext.Countries
                .SingleOrDefault(p => p.Id == id);
        }
        public Country Update(Country country)
        {
            EntityEntry<Country> entityEntry = _registerDbContext.Countries.Update(country);
            _registerDbContext.SaveChanges();
            return entityEntry.Entity;
        }
    }
}
