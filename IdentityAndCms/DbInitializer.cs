using System.Collections.Generic;
using IdentityAndCms.CMS;
using IdentityAndCms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace IdentityAndCms
{
    public class DbInitializer
    {
        internal static void Initialize(CmsDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            context.Database.EnsureCreated();

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole("Admin");

                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("Normal User").Result)
            {
                IdentityRole role = new IdentityRole("Normal User");

                roleManager.CreateAsync(role).Wait();
            }

            //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*//

            if (userManager.FindByNameAsync("Abdullah").Result == null)
            {
                User user = new User();
                user.Email = "AboAliSweden@admin.se";
                user.UserName = "AboAliSweden";

                var result = userManager.CreateAsync(user, "Abduallah1996-").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("Reem").Result == null)
            {
                User user = new User();
                user.Email = "Reem@Gmail.com";
                user.UserName = "Reem";

                var result = userManager.CreateAsync(user, "Abduallah1996-").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Normal User").Wait();
                }
            }

            //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*//

            if (!context.Countries.Any())
            {
                var countries = new List<Country>();
                {
                    countries.Add(new Country() { Name = "Sweden" });
                    countries.Add(new Country() { Name = "Sweden" });
                }
                context.Countries.AddRange(countries);

                context.SaveChanges();
            }

            


        }
    }
}