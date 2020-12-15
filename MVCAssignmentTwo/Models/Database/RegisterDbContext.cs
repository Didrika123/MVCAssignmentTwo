using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class RegisterDbContext : DbContext
    {
        public RegisterDbContext(DbContextOptions<RegisterDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
    }
}
