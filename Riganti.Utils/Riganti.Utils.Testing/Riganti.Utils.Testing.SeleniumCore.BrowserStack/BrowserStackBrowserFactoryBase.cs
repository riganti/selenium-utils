using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Riganti.Utils.Testing.SeleniumCore.BrowserStack
{
    public abstract class BrowserStackBrowserFactoryBase : IBrowserStackDriverFactory
    {
      

        public virtual string BrowserStackHubUrl => "http://hub.browserstack.com/wd/hub/";

        public IWebDriver CreateNewInstance()
        {
            return new RemoteWebDriver(new Uri(BrowserStackHubUrl), GetDesiredCapabilities());
        }

        public abstract DesiredCapabilities GetDesiredCapabilities();

        public const string BrowserStackUsername = "browserstack.user";
        public const string BrowserStackAccessKey = "browserstack.key";
        public const string DesiredCapsBrowserKey = "browser";
        public const string DesiredCapsBrowserVersionKey = "browser_version";
        public const string DesiredCapsOperatingSystemKey = "os";
        public const string DesiredCapsOperationSystemVersionKey = "os_version";
        public const string DesiredCapsResolutionKey = "resolution";
        public const string DesiredCapsMobilePlatformKey = "platform";
        public const string DesiredCapsMobileBrowserNameKey = "browserName";
        public const string DesiredCapsMobileDeviceKey = "device";
        public const string DesiredCapsBrowserstackLocalKey = "browserstack.local";
        public const string DesiredCapsBrowserstackLocalIdentifierKey = "browserstack.localIdentifier";
        public const string DesiredCapsAcceptSslCertsKey = "acceptSslCerts";
        public const string DesiredCapsIEPopupsKey = "browserstack.ie.enablePopups";
        public const string DesiredCapsBrowserstackDebugKey = "browserstack.debug";
    }
}