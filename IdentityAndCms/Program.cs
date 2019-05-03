using System;
using IdentityAndCms.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityAndCms
{
    public class Program
    {
        public static DbInitializer DbInitializer { get; private set; }

        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    RoleManager<IdentityRole> roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
                    UserManager<User> userManager = service.GetRequiredService<UserManager<User>>();

                    CmsDbContext context = service.GetRequiredService<CmsDbContext>();

                    DbInitializer.Initialize(context, roleManager, userManager);
                }
                catch (Exception ex)
                {
                    var logger = service.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
