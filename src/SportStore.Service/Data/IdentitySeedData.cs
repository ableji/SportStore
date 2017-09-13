using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Data
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string AdminPassword = "12345";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<AppUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();


            string[] roles = new string[] { "Administrator", "User" };

            foreach (string r in roles)
            {
                IdentityRole role = await roleManager.FindByNameAsync(r);

                if (role == null) await roleManager.CreateAsync(new IdentityRole { Name = r });

            }

            AppUser user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new AppUser { UserName = adminUser, Email = "Admin@Sportstore.com" };
                IdentityResult result = await userManager.CreateAsync(user, AdminPassword);

                if (result.Succeeded)
                {
                    var res = await userManager.AddToRoleAsync(user, "Administrator");  
                }
            }
            

        }
    }
}
