using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCAssignmentTwo.Models.Data;

namespace MVCAssignmentTwo.Models.Data
{
    public class RegisterDbContext : DbContext
    {
        public RegisterDbContext(DbContextOptions<RegisterDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<PersonLanguage> PersonLanguage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // A many-to-many is really two one-to-many relationsships
            modelBuilder.Entity<PersonLanguage>()
                .HasKey(pl => new { pl.PersonId, pl.LanguageId }); //The personId + LanguageId form the composite primary key for the PersonLanguage Objects (So multiple identical personlang cant exist)

            // This creates a one-to-many relationship ebetween PL and person
            modelBuilder.Entity<PersonLanguage>()
                .HasOne(pl => pl.Person)
                .WithMany(p => p.PersonLanguages)
                .HasForeignKey(pl => pl.PersonId);

            // This creates a one-to-many relationship ebetween PL and Language
            modelBuilder.Entity<PersonLanguage>()
                .HasOne(pl => pl.Language)
                .WithMany(l => l.PersonLanguages)
                .HasForeignKey(pl => pl.LanguageId) ;



            // Fun !
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Cities)
                .WithOne(c => c.Country)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Persons)
                .WithOne(p => p.City)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
