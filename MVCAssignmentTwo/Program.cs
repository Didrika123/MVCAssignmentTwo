using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.Database;
using Microsoft.Extensions.DependencyInjection; //Must have this in order to get access to the  "CreateScope" method
namespace MVCAssignmentTwo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Added middlestep: Seed DB
            host = DbInitializer.CreateDatabaseIfNotExisting(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
