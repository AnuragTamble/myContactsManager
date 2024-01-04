using ContactModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ContactManagerData
{
    public class MyContactDbContext:DbContext
    {
        private static IConfigurationRoot _Configuration;
        public MyContactDbContext()
        {
            
        }
        public MyContactDbContext(DbContextOptions<MyContactDbContext> options) : base(options)
        {

        }

        public DbSet<States> States { get; set; }

        public DbSet<Contacts> Contacts { get; set; }


       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _Configuration = builder.Build();

            var cnstr = _Configuration.GetConnectionString("MyContacst");
            optionsBuilder.UseSqlServer(cnstr);
        }

    }
}
