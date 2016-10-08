using ModelExecuter.Model;
using ModelExecuter.Repository.Configuration;
using ModelExecuter.Repository.SampleData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Repository
{
    public class MainDbContext : DbContext
    {

        public MainDbContext()
            : 
            base(nameOrConnectionString: "DefaultConnection") { }

        public DbSet<File> Files { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<ParameterCategory> ParameterCategories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<TextResource> TextResources { get; set; }


        static MainDbContext()
        {
            Database.SetInitializer(new MainDatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new TextResourceConfiguration());


        }
    }
}
