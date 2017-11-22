using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public static class Extensions
    {
        public static IElementWrapperCollection<TElement, TBrowser> ToElementsList<TElement, TBrowser>(this IEnumerable<IWebElement> elements, TBrowser browserWrapper, string selector, IServiceFactory serviceFactory) where TElement : IElementWrapper where TBrowser : IBrowserWrapper
        {
            Func<IWebElement, IElementWrapper> initElementWrapper = (s) => serviceFactory.Resolve<IElementWrapper>(s, browserWrapper);
            var telements = elements.Select(initElementWrapper).ToList();
            var result = serviceFactory.Resolve<ISeleniumWrapperCollection>(telements, selector, (IBrowserWrapper)browserWrapper);
            return result.Convert<TElement, TBrowser>();
        }

        public static IElementWrapperCollection<TElement, TBrowser> ToElementsList<TElement, TBrowser>(this IEnumerable<IWebElement> elements, TBrowser browserWrapper, string selector, TElement elementWrapper, IServiceFactory serviceFactory) where TElement : IElementWrapper where TBrowser : IBrowserWrapper
        {
            IElementWrapper InitElementWrapper(IWebElement s) => serviceFactory.Resolve<IElementWrapper>(s, browserWrapper);
            var result = serviceFactory.Resolve<ISeleniumWrapperCollection>((IEnumerable<IElementWrapper>)elements.Select(InitElementWrapper).ToList(), selector, (IElementWrapper)elementWrapper, browserWrapper);
            return result.Convert<TElement, TBrowser>();

        }
    }
}