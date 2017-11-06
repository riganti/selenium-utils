using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.FluentApi
{
    public class ElementWrapperCollectionFluetApi : ElementWrapperCollection, IElementWrapperCollectionFluetApi
    {
        public ElementWrapperCollectionFluetApi(IEnumerable<IElementWrapper> collection, string selector, IBrowserWrapper browserWrapper) : base(collection, selector, browserWrapper)
        {
        }

        public ElementWrapperCollectionFluetApi(IEnumerable<IElementWrapper> collection, string selector, ISeleniumWrapper parentElement, IBrowserWrapper browserWrapper) : base(collection, selector, parentElement, browserWrapper)
        {
        }

        public ElementWrapperCollectionFluetApi(IEnumerable<IElementWrapper> collection, string selector, IElementWrapperCollection parentCollection) : base(collection, selector, parentCollection)
        {
        }

        public new IElementWrapperFluentApi this[int index]
        {
            get => (IElementWrapperFluentApi)base[index];
            set => base[index] = value;
        }

        public new IBrowserWrapperFluentApi BrowserWrapper
        {
            get => (IBrowserWrapperFluentApi)base.BrowserWrapper;
            set => base.BrowserWrapper = value;
        }

        public bool Contains(IElementWrapperFluentApi item) => base.Contains(item);

        public new IElementWrapperFluentApi ElementAt(int index) => (IElementWrapperFluentApi)base.ElementAt(index);

        public new IElementWrapperCollectionFluetApi FindElements(string selector, Func<string, By> tmpSelectMethod = null) => (IElementWrapperCollectionFluetApi)base.FindElements(selector, tmpSelectMethod);

        public new IElementWrapperFluentApi First(string selector, Func<string, By> tmpSelectMethod = null) =>
            (IElementWrapperFluentApi)base.First(selector, tmpSelectMethod);

        public new IElementWrapperFluentApi FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null) =>
            (IElementWrapperFluentApi)base.FirstOrDefault(selector, tmpSelectMethod);

        public void ForEach(Action<IElementWrapperFluentApi> action) => base.ForEach(s => action((IElementWrapperFluentApi)s));

        public int IndexOf(IElementWrapperFluentApi item)
            => base.IndexOf(item);

        public new IElementWrapperFluentApi Last()
            => (IElementWrapperFluentApi)base.Last();

        public bool Remove(IElementWrapperFluentApi item)
            => base.Remove(item);

        public IEnumerable<TResult> Select<TResult>(Func<IElementWrapperFluentApi, TResult> selector)
            => base.Select(s => selector((IElementWrapperFluentApi)s));

        public new IElementWrapperFluentApi Single() => (IElementWrapperFluentApi)base.Single();

        public new IElementWrapperFluentApi SingleOrDefault() => (IElementWrapperFluentApi)base.SingleOrDefault();

        public new IElementWrapperCollectionFluetApi ThrowIfDifferentCountThan(int count) =>
            (IElementWrapperCollectionFluetApi)base.ThrowIfDifferentCountThan(count);

        public new IElementWrapperCollectionFluetApi ThrowIfEmptyOrMoreThanOne() => (IElementWrapperCollectionFluetApi)base.ThrowIfEmptyOrMoreThanOne();

        public new IElementWrapperCollectionFluetApi ThrowIfSequenceContainsMoreThanOneElement() => (IElementWrapperCollectionFluetApi)base.ThrowIfSequenceContainsMoreThanOneElement();

        public new IElementWrapperCollectionFluetApi ThrowIfSequenceEmpty() => (IElementWrapperCollectionFluetApi)base.ThrowIfSequenceEmpty();
        IEnumerator<IElementWrapperFluentApi> IElementWrapperCollectionFluetApi.GetEnumerator()
        {
            return GetEnumerator();
        }

        public new IEnumerator<IElementWrapperFluentApi> GetEnumerator()
        {
            return collection.Cast<IElementWrapperFluentApi>().GetEnumerator();
        }




       
    }
}
