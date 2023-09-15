using Microsoft.EntityFrameworkCore;

namespace api.Data.SeedData
{
    public static class RoleAndAccessRoleSeedData
    {
        public static void Seed(this ModelBuilder builder)
        {
            
            var Admin = new ApplicationRole(){Name = "Admin", NormalizedName = "Admin".ToUpper()};
            var Supervisor = new ApplicationRole(){Name = "Supervisor" ,NormalizedName = "Supervisor".ToUpper() };
            var Employee = new ApplicationRole(){Name = "Employee" , NormalizedName = "Employee".ToUpper() };
            var User = new ApplicationRole(){Name = "User" , NormalizedName = "User".ToUpper() };
            //////////////////////////
            //
            builder.Entity<ApplicationRole>().HasData(Admin, Supervisor, Employee, User);

            builder.Entity<AccessRole>().HasData(new[]
            {
                new AccessRole(){Guid = Guid.NewGuid().ToString(),AccessRoleName = "Admin", ApplicationRoleId = Admin.Id},
                new AccessRole(){ Guid = Guid.NewGuid().ToString(), AccessRoleName = "Admin",ApplicationRoleId = Supervisor.Id},
                new AccessRole(){Guid = Guid.NewGuid().ToString(), AccessRoleName = "Admin", ApplicationRoleId = Employee.Id },
                new AccessRole(){Guid = Guid.NewGuid().ToString(), AccessRoleName = "Admin", ApplicationRoleId = User.Id },
                new AccessRole(){ Guid = Guid.NewGuid().ToString(), AccessRoleName = "Supervisor", ApplicationRoleId = Supervisor.Id},
                new AccessRole(){Guid = Guid.NewGuid().ToString(), AccessRoleName = "Supervisor", ApplicationRoleId = Supervisor.Id}
            });
            
        }
    }
}
