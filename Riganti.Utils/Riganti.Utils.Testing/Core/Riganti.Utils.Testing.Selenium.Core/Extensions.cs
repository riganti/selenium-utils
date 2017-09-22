using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public static class Extensions
    {
        public static IElementWrapperCollection ToElementsList(this IEnumerable<IWebElement> elements, IBrowserWrapper browserWrapper, string selector)
        {
            return new ElementWrapperCollection(elements.Select(s => new ElementWrapper(s, browserWrapper)), selector, browserWrapper);
        }

        public static IElementWrapperCollection ToElementsList(this IEnumerable<IWebElement> elements, IBrowserWrapper browserWrapper, string selector, IElementWrapper elementWrapper)
        {
            return new ElementWrapperCollection(elements.Select(s => new ElementWrapper(s, browserWrapper)), selector, elementWrapper, browserWrapper);
        }
    }
}