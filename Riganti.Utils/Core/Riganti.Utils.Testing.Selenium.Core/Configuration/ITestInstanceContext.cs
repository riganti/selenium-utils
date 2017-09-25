using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Drivers;

namespace Riganti.Utils.Testing.Selenium.Core.Configuration
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