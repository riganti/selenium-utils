using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
