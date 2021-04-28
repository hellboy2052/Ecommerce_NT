using Microsoft.AspNetCore.Identity;
using ServerSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            if (!roleManager.RoleExistsAsync("superadmin").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("superadmin"));
            }
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            //Seed Default User
            var defaultUser = new User
            {
                UserName = "hngtiendng@gmail.com",
                Email = "hngtiendng@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0867537750",
                FullName = "Super Admin",
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.Count(u => u.Email == defaultUser.Email) == 0)
            {
                IdentityResult result = await userManager.CreateAsync(defaultUser, "Aaa!123");
                if (result.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(defaultUser, "admin");
                    
                }
            }
        }
    }
}
