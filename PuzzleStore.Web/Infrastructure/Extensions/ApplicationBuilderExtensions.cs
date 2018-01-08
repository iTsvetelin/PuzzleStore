using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PuzzleStore.Data;
using PuzzleStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static  IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PuzzleStoreDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                

                Task.
                     Run( async () =>
                     {
                         var roleName = GlobalConstants.AdministratorRole;

                         var roles = new[]
                         {
                             GlobalConstants.AdministratorRole,
                             GlobalConstants.ModeratorRole
                         };

                         foreach(var role in roles)
                         {
                             var roleExists = await roleManager.RoleExistsAsync(role);

                             if (!roleExists)
                             {
                                 await roleManager.CreateAsync(new IdentityRole
                                 {
                                     Name = role
                                 });
                             }
                         }
                         
                         var adminUser = await userManager.FindByNameAsync(roleName);

                         if(adminUser == null)
                         {

                             adminUser = new User
                                 {
                                     Email = "admin@mysite.com",
                                     UserName = "admin@mysite.com"
                                 };

                             await userManager.CreateAsync(adminUser,"admin12");

                             await userManager.AddToRoleAsync(adminUser, roleName);
                         }

                     })
                     .Wait();
            }

            return app;
        }
    }
}
