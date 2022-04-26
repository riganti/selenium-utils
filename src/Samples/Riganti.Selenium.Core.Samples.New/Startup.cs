namespace Riganti.Selenium.Core.Samples.New
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(builder =>
            {
                builder.MapControllerRoute(
                    name: "test",
                    pattern: "{controller=Test}/{action}/{id?}");

                builder.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

            });
        }
    }
}
