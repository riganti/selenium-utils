using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public static class Extensions
    {
        public static IElementWrapperCollection ToElementsList(this IEnumerable<IWebElement> elements, IBrowserWrapper browserWrapper, string selector, IServiceFactory serviceFactory)
        {
            Func<IWebElement, IElementWrapper> initElementWrapper = (s) => serviceFactory.Resolve<IElementWrapper>(s, browserWrapper);
            return serviceFactory.Resolve<IElementWrapperCollection>(elements.Select(initElementWrapper), selector, browserWrapper);
        }

        public static IElementWrapperCollection ToElementsList(this IEnumerable<IWebElement> elements, IBrowserWrapper browserWrapper, string selector, IElementWrapper elementWrapper, IServiceFactory serviceFactory)
        {
            IElementWrapper InitElementWrapper(IWebElement s) => serviceFactory.Resolve<IElementWrapper>(s, browserWrapper);
            return serviceFactory.Resolve<IElementWrapperCollection>(elements.Select(InitElementWrapper), selector, elementWrapper, browserWrapper);
        }
    }
}