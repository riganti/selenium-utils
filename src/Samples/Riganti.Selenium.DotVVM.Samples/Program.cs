using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Riganti.Selenium.DotVVM.Samples;

public class Program
{
    public static void Main(string[] args)
    {
        var host = WebHost.CreateDefaultBuilder<Startup>(args)
            .Build();
        host.Run();
    }
}
