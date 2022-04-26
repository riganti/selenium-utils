using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Riganti.Selenium.Core.Samples.New
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }


        public static WebApplication BuildWebHost(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            
            app.UseRouting();
            app.MapControllerRoute(
                   name: "default",
                    pattern: "{controller=Home}/{action=Index}");

            app.MapControllerRoute(
                        name: "test",
                        pattern: "{controller=Test}/{action}/{id?}");

            return app;
        }

    }
}
