using OpenQA.Selenium.Edge;
using Riganti.Selenium.Core.Factories;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public class EdgeHelpers
    {
        public static EdgeDriver CreateEdgeDriver(LocalWebBrowserFactory factory)
        {
            var options = new EdgeOptions()
            {
            };

            return new EdgeDriver(options);
        }
    }
}
