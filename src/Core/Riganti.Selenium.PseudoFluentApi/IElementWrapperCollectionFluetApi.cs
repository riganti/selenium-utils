using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public interface IElementWrapperCollectionFluetApi : IElementWrapperCollection
    {
        new IElementWrapperFluentApi this[int index] { get; set; }

        new IBrowserWrapperFluentApi BrowserWrapper { get; set; }
        bool Contains(IElementWrapperFluentApi item);
        new IElementWrapperFluentApi ElementAt(int index);
        new IElementWrapperCollectionFluetApi FindElements(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapperFluentApi First();
        new IElementWrapperFluentApi First(string selector, Func<string, By> tmpSelectMethod = null);
        new IElementWrapperFluentApi FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        void ForEach(Action<IElementWrapperFluentApi> action);
        int IndexOf(IElementWrapperFluentApi item);
        new IElementWrapperFluentApi Last();
        bool Remove(IElementWrapperFluentApi item);
        IEnumerable<TResult> Select<TResult>(Func<IElementWrapperFluentApi, TResult> selector);
        new IElementWrapperFluentApi Single();
        new IElementWrapperFluentApi SingleOrDefault();
        new IElementWrapperCollectionFluetApi ThrowIfDifferentCountThan(int count);
        new IElementWrapperCollectionFluetApi ThrowIfEmptyOrMoreThanOne();
        new IElementWrapperCollectionFluetApi ThrowIfSequenceContainsMoreThanOneElement();
        new IElementWrapperCollectionFluetApi ThrowIfSequenceEmpty();
        new IEnumerator<IElementWrapperFluentApi> GetEnumerator();


    }
}