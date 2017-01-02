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
           
            //routing
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
            RegisterRoutes(config.RouteTable);

            RegisterResources(config.RouteTable);

        }

        private void RegisterRoutes(DotvvmRouteTable routeTable)
        {
            routeTable.Add("DefaultRoute", "", "Views/default.dothtml");
        }

        private void RegisterResources(DotvvmRouteTable routeTable)
        {

        }
    }
}
