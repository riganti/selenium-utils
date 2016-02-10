using OpenQA.Selenium.Remote;
using System;

namespace Riganti.Utils.Testing.SeleniumCore.BrowserStack
{
    public class CommonBrowserStackBrowserFactory : BrowserStackBrowserFactoryBase
    {
        private readonly Action<DesiredCapabilities> configureCapabilities;
        /// <summary>
        /// Configure browser
        /// </summary>
        public CommonBrowserStackBrowserFactory(Action<DesiredCapabilities> configureCapabilities)
        {
            this.configureCapabilities = configureCapabilities;
        }

        private DesiredCapabilities commonCaps;
        /// <summary>
        /// Configure desktop browser
        /// </summary>
        public CommonBrowserStackBrowserFactory(string browser, string browserVersion, string os, string osVersion, string resolution)
        {
            var caps = new DesiredCapabilities();
            caps.SetCapability(DesiredCapsBrowserKey, browser);
            caps.SetCapability(DesiredCapsBrowserVersionKey, browserVersion);
            caps.SetCapability(DesiredCapsOperatingSystemKey, os);
            caps.SetCapability(DesiredCapsOperationSystemVersionKey, osVersion);
            caps.SetCapability(DesiredCapsResolutionKey, resolution);
            commonCaps = caps;
        }

        /// <summary>
        /// Configure mobile device browser
        /// </summary>
        public CommonBrowserStackBrowserFactory(string browserName, string platform, string device)
        {
            var caps = new DesiredCapabilities();
            caps.SetCapability(DesiredCapsBrowserKey, browserName);
            caps.SetCapability(DesiredCapsMobilePlatformKey, platform);
            caps.SetCapability(DesiredCapsMobileDeviceKey, device);
            commonCaps = caps;
        }

        /// <summary>
        /// Returns new <see cref="DesiredCapabilities"/> with filled capabilities
        /// </summary>
        public override DesiredCapabilities GetDesiredCapabilities()
        {
            DesiredCapabilities caps = commonCaps;
            if (caps == null)
            {
                caps = new DesiredCapabilities();
                configureCapabilities(caps);
            }
            Authenticate(caps);
            return caps;
        }

        /// <summary>
        /// Sets username and access key of browserstack to capabilities
        /// </summary>
        public virtual void Authenticate(DesiredCapabilities caps)
        {
            SeleniumTestsConfiguration.CheckAndSet("selenium:BroserStackUserName", "", s => caps.SetCapability(BrowserStackUsername, s));
            SeleniumTestsConfiguration.CheckAndSet("selenium:BroserStackAccessKey", "", s => caps.SetCapability(BrowserStackAccessKey, s));
        }
    }
}