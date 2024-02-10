using Common;
using Entities.Role;
using Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultRolesAsync(RoleManager<Role> roleManager)
        {
            var roles = Enum.GetValues(typeof(RolesEnum)).Cast<RolesEnum>();
            var defaultRoles = roles.Select(role => new Role { Name = role.ToString(), Description = role + " Access" }).ToList();
            foreach (var role in defaultRoles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExist)
                {
                    //create the roles and seed them to the database
                    await roleManager.CreateAsync(new Role
                    {
                        Name = role.Name,
                        Description = role.Description
                    });
                }
            }
        }

        //AQAAAAEAACcQAAAAECO6Iu7R1fqtshraElq+n8emjSAXqcdlsiLPjIla/p8j1L5yi+wxtD+h/3umRZHwvQ==
        public static async Task SeedDefaultUserAsync(UserManager<User> userManager)
        {
            var existUser = await userManager.Users.AnyAsync(u => u.Id == 1);
            if (!existUser)
            {
                var user = new User { UserName = "superadmin1", Email = "superadmin@gmail.com", FullName = "superadmin superadmin", PhoneNumber = "09123456789", IsActive = true };
                var identityResult = await userManager.CreateAsync(user, "superadmin1");
                if (identityResult.Succeeded)
                    await userManager.AddToRoleAsync(user, RolesEnum.SuperAdmin.ToString());
            }
            existUser = await userManager.Users.AnyAsync(u => u.Id == 2);
            if (!existUser)
            {
                var user = new User { UserName = "admin1", Email = "admin1@gmail.com", FullName = "admin1 admin1", PhoneNumber = "09123456789", IsActive = true };
                var identityResult = await userManager.CreateAsync(user, "admin1");
                if (identityResult.Succeeded)
                    await userManager.AddToRoleAsync(user, RolesEnum.Admin.ToString());
                for (var i = 1; i < 6; i++)
                {
                    user = new User { UserName = "doctor" + i, Email = "doctor" + i + "@gmail.com", FullName = "doctor" + i, PhoneNumber = "0912345678" + i, IsActive = true };
                    identityResult = await userManager.CreateAsync(user, "doctor" + i);
                    if (identityResult.Succeeded)
                        await userManager.AddToRoleAsync(user, RolesEnum.Doctor.ToString());
                }
                for (var i = 1; i < 6; i++)
                {
                    user = new User { UserName = "central" + i, Email = "central" + i + "@gmail.com", FullName = "central" + i, PhoneNumber = "0912345678" + i, IsActive = true };
                    identityResult = await userManager.CreateAsync(user, "central" + i);
                    if (identityResult.Succeeded)
                        await userManager.AddToRoleAsync(user, RolesEnum.Central.ToString());
                }
            }
        }
    }
}