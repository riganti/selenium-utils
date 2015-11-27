using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace Riganti.Utils.Testing.SeleniumCore
{
    //TODO: create fast mode base factory 
    internal abstract class FastModeDriverFactoryBase : IWebDriverFactory, IReusableWebDriver
    {
        public IWebDriver CreateNewInstance()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}