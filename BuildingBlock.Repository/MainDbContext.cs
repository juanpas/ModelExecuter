using BuildingBlock.Model;
using BuildingBlock.Repository.Configuration;
using BuildingBlock.Repository.SampleData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Repository
{
    public class MainDbContext : DbContext
    {

        public MainDbContext()
            : 
            base(nameOrConnectionString: "DefaultConnection") { }

        public DbSet<File> Files { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<ParameterCategory> ParameterCategories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<TextResource> TextResources { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ExtendedProduct> ExtendedProducts { get; set; }
        public DbSet<ProductSetting> ProductSettings { get; set; }
        public DbSet<ProductSeller> ProductSellers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductProvider> ProductProviders { get; set; }
        public DbSet<ProductQuotation> ProductQuotations { get; set; }
        public DbSet<ProductQuotationActivity> ProductQuotationsActivities { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadCategory> LeadCategories { get; set; }


        static MainDbContext()
        {
            Database.SetInitializer(new MainDatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new ModuleConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new TextResourceConfiguration());
            modelBuilder.Configurations.Add(new ServiceConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductSellerConfiguration());
            modelBuilder.Configurations.Add(new ProductSettingConfiguration());
            modelBuilder.Configurations.Add(new ProductQuotationConfiguration());
            modelBuilder.Configurations.Add(new ProductQuotationActivityConfiguration());
            modelBuilder.Configurations.Add(new ExtendedProductConfiguration());
            modelBuilder.Configurations.Add(new LeadConfiguration());


        }
    }
}
