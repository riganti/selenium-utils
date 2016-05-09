using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public static class Extensions
    {
        public static ElementWrapperCollection ToElementsList(this IEnumerable<IWebElement> elements, BrowserWrapper browserWrapper, string selector)
        {
            return new ElementWrapperCollection(elements.Select(s => new ElementWrapper(s, browserWrapper)), selector, browserWrapper);
        }

        public static ElementWrapperCollection ToElementsList(this IEnumerable<IWebElement> elements, BrowserWrapper browserWrapper, string selector, ElementWrapper elementWrapper)
        {
            return new ElementWrapperCollection(elements.Select(s => new ElementWrapper(s, browserWrapper)), selector, elementWrapper, browserWrapper);
        }

   
    }
}