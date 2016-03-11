using OpenQA.Selenium.IE;
using System;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class InternetExplorerDriverWrapper : InternetExplorerDriver, IWebDriverWrapper
    {
        public InternetExplorerDriverWrapper()
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - InternetExplorerDriver");
        }

        public InternetExplorerDriverWrapper(InternetExplorerOptions options) : base(options)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - InternetExplorerDriver");
        }

        public InternetExplorerDriverWrapper(InternetExplorerDriverService service) : base(service)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - InternetExplorerDriver");
        }

        public InternetExplorerDriverWrapper(string internetExplorerDriverServerDirectory) : base(internetExplorerDriverServerDirectory)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - InternetExplorerDriver");
        }

        public InternetExplorerDriverWrapper(string internetExplorerDriverServerDirectory, InternetExplorerOptions options) : base(internetExplorerDriverServerDirectory, options)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - InternetExplorerDriver");
        }

        public InternetExplorerDriverWrapper(string internetExplorerDriverServerDirectory, InternetExplorerOptions options, TimeSpan commandTimeout) : base(internetExplorerDriverServerDirectory, options, commandTimeout)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - InternetExplorerDriver");
        }

        public InternetExplorerDriverWrapper(InternetExplorerDriverService service, InternetExplorerOptions options) : base(service, options)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - InternetExplorerDriver");
        }

        public InternetExplorerDriverWrapper(InternetExplorerDriverService service, InternetExplorerOptions options, TimeSpan commandTimeout) : base(service, options, commandTimeout)
        {
            SeleniumTestBase.LogDriverId(this, "CTOR - InternetExplorerDriver");
        }

        public Guid DriverId { get; } = Guid.NewGuid();
        public bool Disposed { get; set; }

    }
}