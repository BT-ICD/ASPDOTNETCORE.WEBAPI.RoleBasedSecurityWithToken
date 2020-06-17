using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Data
{
    public class SeedDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreated();

            //UserName: "Ali" Password: "Ali@123"

            //btrivedi -  password@Welcome20
          //  if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "btrivedi1@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "bt12"
                };
                    userManager.CreateAsync(user, "password@Welcome20");
            }
            //To interact with Role Manager 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            //To access List of roles
            //var roles = roleManager.Roles.ToList();
            //var cnt = roles.Count;
            //To create role - Still need to identify - how to create instance of ApplicationUserRole and then insert new record
            //var roleResult= roleManager.CreateAsync(new IdentityRole("Admin"));
            //var roleResult = roleManager.CreateAsync(new IdentityRole("User"));

            //To add User into a role

            //var userRecord = await userManager.FindByEmailAsync ("BTRIVEDI@GMAIL.COM");


        }
    }
}
