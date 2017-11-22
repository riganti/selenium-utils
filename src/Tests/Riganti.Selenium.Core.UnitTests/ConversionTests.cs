using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions;
using Riganti.Selenium.FluentApi;
using Selenium.Core.UnitTests.Mock;

namespace Riganti.Selenium.Core.UnitTests
{
    [TestClass]
    public class ConversionTests
    {
        [TestMethod]
        public void IEnumerableConversionTests()
        {
            var browser = new BrowserWrapperFluentApi(new MockIWebBrowser(new MockIWebDriver()),
                new MockIWebDriver(), new MockITestInstance(), new ScopeOptions());

            var elements = new List<IElementWrapper>()
            {
                new ElementWrapperFluentApi(new MockIWebElement(), browser)
            };
            var collection = new ElementWrapperCollection<IElementWrapperFluentApi, IBrowserWrapperFluentApi>(
                new IElementWrapperFluentApi[] { new ElementWrapperFluentApi(new MockIWebElement(), browser) }, "selector", browser);

            var collection2 = new ElementWrapperCollection<IElementWrapper, IBrowserWrapper>(elements, "selec2", collection.BrowserWrapper, collection);

        }

        //public static IEnumerable<TElement, TBrowser> Cast<TElement, TBrowser>(IEnumerable<TElement, TBrowser> source)
        //{
        //    return CastIterator<TResult>(source);
        //}

        //private static asd<TResult> CastIterator<TResult>(asd<IBrowserWrapper> source)
        //{
        //    foreach (TResult result in source)
        //        yield return result;
        //}
    }

    interface asd<T> : IEnumerable<T>
    {

    }
}
