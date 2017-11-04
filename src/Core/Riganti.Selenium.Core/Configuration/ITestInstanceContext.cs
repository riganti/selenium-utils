using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.Core.Drivers;

namespace Riganti.Selenium.Core.Configuration
{
    public interface ITestInstanceContext : ITestContext
    {
        /// <summary>
        /// Gets the web browser object used to run the test.
        /// </summary>
        IWebBrowser CurrentWebBrowser { get; }

        /// <summary>
        /// Gets the base URL of the application under test.
        /// </summary>
        string BaseUrl { get; }

    }
}