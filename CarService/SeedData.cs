/*@*VenkatReddy Cherkupalli *@
*/

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarService
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var adminAlreadyExists = await roleManager.RoleExistsAsync(Constants.AdminRole);

            if (adminAlreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdminRole));

            var stdUserAlreadyExists = await roleManager.RoleExistsAsync(Constants.StandardUserRole);

            if (stdUserAlreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.StandardUserRole));
        }

        public static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users.Where(x => x.UserName == "dchapman@gmail.com").SingleOrDefaultAsync();

            if (testAdmin != null) return;

            testAdmin = new IdentityUser { UserName = "dchapman@gmail.com", Email = "dchapman@gmail.com" };

            await userManager.CreateAsync(testAdmin, "Password1!");

            await userManager.AddToRoleAsync(testAdmin, Constants.AdminRole);
        }
    }
}
