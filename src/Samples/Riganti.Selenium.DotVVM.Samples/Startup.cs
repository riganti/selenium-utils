using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Riganti.Selenium.DotVVM.Samples;

public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(s =>
        {
            s.AddConsole();
        });
        services.AddDotVVM<DotvvmStartup>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

        // use DotVVM
        var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

        // use static files
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(env.ContentRootPath)
        });
    }
}
