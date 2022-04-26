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
