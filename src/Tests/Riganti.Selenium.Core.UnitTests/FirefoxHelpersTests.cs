using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Drivers.Implementation;

namespace Selenium.Core.UnitTests
{
    [TestClass]
    public class FirefoxHelpersTests
    {
        [TestMethod]
        public void GetFirefoxOptions_UsesBlankPageAtStartup()
        {
            var options = FirefoxHelpers.GetFirefoxOptions(new Dictionary<string, string>());
            var firefoxOptions = (Dictionary<string, object>)options.ToCapabilities().GetCapability("moz:firefoxOptions");
            var preferences = (Dictionary<string, object>)firefoxOptions["prefs"];

            Assert.AreEqual(0, preferences["browser.startup.page"]);
        }
    }
}
