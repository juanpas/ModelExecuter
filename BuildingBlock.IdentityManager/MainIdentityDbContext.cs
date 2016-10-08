using ModelExecuter.IdentityModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ModelExecuter.IdentityManager
{
    public class MainIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MainIdentityDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        static MainIdentityDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<MainIdentityDbContext>(new MainIdentityDatabaseInitializer());
        }

        public static MainIdentityDbContext Create()
        {
            return new MainIdentityDbContext();
        }
    }
}
