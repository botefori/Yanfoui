using app.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace app.Data
{
    public static class DbInitializer
    {
        public static void Initialize(
            AppDbContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager
        )
        {
            context.Database.EnsureCreated();

            // Look for any Customers.
            if (!context.Customers.Any())
            {
                var Customers = new Customer[]
                {
                    new Customer{ FirstMidName="CAMARA", LastName="Daouda"},
                    new Customer{ FirstMidName="CAMARA", LastName="Ibrahima"},
                    new Customer{ FirstMidName="CAMARA", LastName="Bachir"},
                };

                foreach(Customer aCustomer in Customers)
                {
                    context.Customers.Add(aCustomer);
                }

                context.SaveChanges();
            }

            if (!context.ApplicationRoles.Any())
            {
                var role = new ApplicationRole() { Id = Guid.Parse("4f88811a-f7ab-40c4-9f44-978fa4b561cb"), Name = "Root" };
                var result = roleManager.CreateAsync(role).Result;
            }
            
            if (!context.ApplicationUsers.Any())
            {
                var user = new ApplicationUser()
                {
                    UserName = "Admin",
                    Email = "sample@gmail.com",
                    PhoneNumber = "+60123456789",
                };

                var result = userManager.CreateAsync(user, "Pass@1234").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Root").Wait();
                }
            }
        }
    }
}