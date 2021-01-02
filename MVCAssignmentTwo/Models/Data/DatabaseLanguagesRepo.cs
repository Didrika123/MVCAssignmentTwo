using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class DatabaseLanguagesRepo : ILanguagesRepo
    {
        readonly RegisterDbContext _registerDbContext;
        public DatabaseLanguagesRepo(RegisterDbContext languageDbContext)
        {
            _registerDbContext = languageDbContext;
        }
        public Language Create(string name, string abbreviation)
        {
            Language language = new Language(name, abbreviation);
            EntityEntry<Language> entityEntry = _registerDbContext.Languages.Add(language);
            _registerDbContext.SaveChanges();
            language = entityEntry.Entity;
            return language;
        }

        public bool Delete(Language language)
        {
            EntityEntry entityEntry = _registerDbContext.Languages.Remove(language);
            _registerDbContext.SaveChanges();
            return entityEntry.State == EntityState.Deleted;
        }

        public List<Language> Read()
        {
            return _registerDbContext.Languages
                .OrderByDescending(p => p.Id).ToList();
        }

        public Language Read(int id)
        {
            //where(row => row.name.contains
            return _registerDbContext.Languages
                .SingleOrDefault(p => p.Id == id);
        }

        public Language LazyRead(int id)
        {
            return _registerDbContext.Languages
                .SingleOrDefault(p => p.Id == id);
        }
        public Language Update(Language language)
        {
            EntityEntry<Language> entityEntry = _registerDbContext.Languages.Update(language);
            _registerDbContext.SaveChanges();
            return entityEntry.Entity;
        }
    }
}
