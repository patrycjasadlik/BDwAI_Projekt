using Microsoft.AspNetCore.Identity;
using SklepInternetowyMVC.Constants;

namespace SklepInternetowyMVC.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)

        {
            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();
            //dodawanie rol do bazy
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
            //tworzenie admina

            var admin = new IdentityUser
            {

                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,

            };
            var UserInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (UserInDb is null)
            {
                await userMgr.CreateAsync(admin, "Admin123@");
                await userMgr.AddToRoleAsync(admin,Roles.Admin.ToString());
            }    
        }
    }
}
