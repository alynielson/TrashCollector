using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using TrashCollectorProject.Models;

[assembly: OwinStartupAttribute(typeof(TrashCollectorProject.Startup))]
namespace TrashCollectorProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists("Employee"))
            {
                CreateEmployeeRole(db, roleManager);
            }
            if (!roleManager.RoleExists("Customer"))
            {
                CreateCustomerRole(db, roleManager);
            }
        }

        private void CreateEmployeeRole(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            var role = new IdentityRole();
            role.Name = "Employee";
            roleManager.Create(role);
        }

        private void CreateCustomerRole(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            var role = new IdentityRole();
            role.Name = "Customer";
            roleManager.Create(role);
        }
    }
}
