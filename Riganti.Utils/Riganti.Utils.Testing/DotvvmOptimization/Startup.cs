using System;
using System.IO;
using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;

//[assembly: OwinStartup(typeof(Selenium.DotVVM.Samples.Startup))]
namespace Selenium.DotVVM.Samples
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;
            var uploadPath = Path.Combine(applicationPhysicalPath, "App_Data\\UploadTemp");
            
            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, options: options =>
            {
                options.Services.TryAddSingleton( typeof(IUploadedFileStorage),serviceProvider => new FileSystemUploadedFileStorage(uploadPath, TimeSpan.FromMinutes(30)));
            });
#if DEBUG
            dotvvmConfiguration.Debug = true;
#endif

            // use static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)

            });
          
        }
    }
}
