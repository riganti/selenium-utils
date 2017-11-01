using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core.Abstractions
{
    public interface IElementWrapperCollection: ISeleniumWrapper, IReadOnlyCollection<IElementWrapper>
    {
        IElementWrapper  this[int index] { get; set; }

        IBrowserWrapper BrowserWrapper { get; set; }
        int Count { get; }
        bool IsReadOnly { get; }

        bool Contains(IElementWrapper  item);
        IElementWrapper  ElementAt(int index);
        IElementWrapperCollection FindElements(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper  First(string selector, Func<string, By> tmpSelectMethod = null);
        IElementWrapper  FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null);
        void ForEach(Action<IElementWrapper > action);
        int IndexOf(IElementWrapper  item);
        IElementWrapper  Last();
        bool Remove(IElementWrapper  item);
        void RemoveAt(int index);
        IEnumerable<TResult> Select<TResult>(Func<IElementWrapper , TResult> selector);
        IElementWrapper  Single();
        IElementWrapper  SingleOrDefault();
        IElementWrapperCollection ThrowIfDifferentCountThan(int count);
        IElementWrapperCollection ThrowIfEmptyOrMoreThanOne();
        IElementWrapperCollection ThrowIfSequenceContainsMoreThanOneElement();
        IElementWrapperCollection ThrowIfSequenceEmpty();
    }
}