using System;
using System.IO;
using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using DotVVM.Framework.Storage;

namespace Selenium.DotVVM.Samples
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));

            var uploadPath = Path.Combine(applicationPath, "App_Data\\UploadTemp");
            config.ServiceLocator.RegisterSingleton<IUploadedFileStorage>(
                () => new FileSystemUploadedFileStorage(uploadPath, TimeSpan.FromMinutes(30))
            );

            //routing
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
            RegisterRoutes(config.RouteTable);

            RegisterResources(config.RouteTable);

        }

        private void RegisterRoutes(DotvvmRouteTable routeTable)
        {
            routeTable.Add("Default", "", "Views/default.dothtml");
        }

        private void RegisterResources(DotvvmRouteTable routeTable)
        {

        }
    }
}
