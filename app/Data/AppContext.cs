
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using app.Models;
namespace app.Data
{
   public class AppContext:DbContext
   {
       public DbSet<Customer> Customers {get; set;}
        public AppContext(DbContextOptions<AppContext> options):base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<Customer>().ToTable("Customer");
        }

   }
}