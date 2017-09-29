using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Attributes;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.PseudoFluentApi.Tests
{
    [TestClass]
    [FullStackTrace]
    public class CoordinatorTests : SeleniumTest
    {
        [TestMethod]
        public void Coordinator_Google()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                browser.CheckIfTitleEquals("Google");
            });
        }

    }
}
