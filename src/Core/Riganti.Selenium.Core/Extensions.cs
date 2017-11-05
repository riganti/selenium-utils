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
            Func<IWebElement, IElementWrapper> initElementWrapper = new Func<IWebElement, IElementWrapper>((s) =>
            {
                var type = serviceFactory.Resolve<IElementWrapper>();
                return (IElementWrapper)Activator.CreateInstance(type, s, browserWrapper);
            });

            return new ElementWrapperCollection(elements.Select(initElementWrapper), selector, browserWrapper);
        }

        public static IElementWrapperCollection ToElementsList(this IEnumerable<IWebElement> elements, IBrowserWrapper browserWrapper, string selector, IElementWrapper elementWrapper, IServiceFactory serviceFactory)
        {
            Func<IWebElement,IElementWrapper> initElementWrapper = new Func<IWebElement, IElementWrapper>((s) =>
            {
                var type = serviceFactory.Resolve<IElementWrapper>();
                return (IElementWrapper) Activator.CreateInstance(type, s, browserWrapper);
            });

            return new ElementWrapperCollection(elements.Select(initElementWrapper), selector, elementWrapper, browserWrapper);
        }
    }
}