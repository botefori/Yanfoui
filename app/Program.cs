using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using app.Data;
using Microsoft.AspNetCore.Identity;
using app.Models;

namespace app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                        .UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseUrls("http://*:5000")
                        .UseIISIntegration()
                        .UseStartup<Startup>()
                        .Build();
        

                using(var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try{
                        var context = services.GetRequiredService<AppDbContext>();
                        context.Database.Migrate();

                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                        app.Data.DbInitializer.Initialize(context, userManager, roleManager);
                        
                    }catch(Exception ex)
                    {
                      ILogger logger = (ILogger)host.Services.GetService(typeof(ILogger<Program>));
                      logger.LogError(ex, "An error occurred while seeding the database."+ex.Message);
                    }
                }
          

           host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
