using app.Models;
using System;
using System.Linq;

namespace app.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppContext context)
        {
            context.Database.EnsureCreated();
            // Look for any students.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var Customers = new Customer[]{
               new Customer{ FirstMidName="CAMARA", LastName="Daouda"},
               new Customer{ FirstMidName="CAMARA", LastName="Ibrahima"},
            };

            foreach(Customer aCustomer in Customers)
            {
                context.Customers.Add(aCustomer);
            }

            context.SaveChanges();

        }
    }
}