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
            // Look for any Customers.
            if (context.Customers.Any())
            {
                return;
            }

            var Customers = new Customer[]{
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
    }
}