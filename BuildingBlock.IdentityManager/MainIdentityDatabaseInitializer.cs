using BuildingBlock.Defs;
using BuildingBlock.IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data;
using System.Data.Entity;
using System.Web;

namespace BuildingBlock.IdentityManager
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<MainIdentityDbContext>
    // This example shows you how to create a new database if the Model changes
    public class MainIdentityDatabaseInitializer : DropCreateDatabaseIfModelChanges<MainIdentityDbContext>
    {

        protected override void Seed(MainIdentityDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(MainIdentityDbContext db)
        {
            string excelFileName = HttpContext.Current.Server.MapPath("/bin/SampleData/") + "DataInitializer.xlsx";

            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            AddRoles(excelFileName, roleManager);
            AddUsers(excelFileName, roleManager, userManager);
        }

        private static void AddRoles(string excelFileName, ApplicationRoleManager roleManager)
        {
            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "Role");

            foreach (DataRow dr in dt.Rows)
            {
                string roleName = dr["Name"].ToString();

                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    var roleresult = roleManager.Create(role);
                }

            }
        }

        private static void AddUsers(string excelFileName, ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "User");

            foreach (DataRow dr in dt.Rows)
            {
                string email = dr["Email"].ToString();
                var user = userManager.FindByEmail(email);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = email, Email = email, FullName = dr["Name"].ToString() };
                    var result = userManager.Create(user, dr["Password"].ToString());
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                string roleName = dr["Role"].ToString();
                if (!string.IsNullOrEmpty(roleName))
                {
                    var role = roleManager.FindByName(roleName);
                    var rolesForUser = userManager.GetRoles(user.Id);
                    if (!rolesForUser.Contains(roleName))
                    {
                        var result = userManager.AddToRole(user.Id, roleName);
                    }
                }
            }
        }



    }

}
