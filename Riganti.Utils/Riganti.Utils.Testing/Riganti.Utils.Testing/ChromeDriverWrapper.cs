using OpenQA.Selenium.Chrome;
using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    
    /// <summary>Provides a mechanism to write tests against Chrome</summary>
    public class ChromeDriverWrapper : ChromeDriver, IWebDriverWrapper
    {
        /// <summary>
        /// Gets unique id of instance of this browser.
        /// </summary>
        public Guid DriverId { get; } = Guid.NewGuid();
        public bool Disposed { get; set; }

        public ChromeDriverWrapper()
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - ChromeDriver");
        }

        public ChromeDriverWrapper(ChromeOptions options) : base(options)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - ChromeDriver");
        }

        public ChromeDriverWrapper(ChromeDriverService service) : base(service)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - ChromeDriver");
        }

        public ChromeDriverWrapper(string chromeDriverDirectory) : base(chromeDriverDirectory)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - ChromeDriver");
        }

        public ChromeDriverWrapper(string chromeDriverDirectory, ChromeOptions options) : base(chromeDriverDirectory, options)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - ChromeDriver");
        }

        public ChromeDriverWrapper(string chromeDriverDirectory, ChromeOptions options, TimeSpan commandTimeout) : base(chromeDriverDirectory, options, commandTimeout)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - ChromeDriver");
        }

        public ChromeDriverWrapper(ChromeDriverService service, ChromeOptions options) : base(service, options)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - ChromeDriver");
        }

        public ChromeDriverWrapper(ChromeDriverService service, ChromeOptions options, TimeSpan commandTimeout) : base(service, options, commandTimeout)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - ChromeDriver");
        }
    }
}