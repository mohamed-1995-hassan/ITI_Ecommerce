using ITI_Ecommerce.Constants;
using Microsoft.AspNetCore.Identity;

namespace ITI_Ecommerce.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };
            IdentityUser? identityUser = await userManager.FindByEmailAsync(admin.Email);
            if(identityUser is null)
            {
                await userManager.CreateAsync(admin,"Admin@123");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
            
        }
    }
}
