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
                        var context = (app.Data.AppContext)services.GetRequiredService<app.Data.AppContext>();
                        app.Data.DbInitializer.Initialize(context);
                        context.Database.Migrate();
                    
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
