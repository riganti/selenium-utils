using OpenQA.Selenium;
using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class ElementWrapperCollection : ICollection<ElementWrapper>, ISeleniumWrapper
    {
        public string Selector { get; }
        public ISeleniumWrapper ParentWrapper { get; set; }
        public BrowserWrapper BrowserWrapper { get; set; }

        public string FullSelector => (ParentWrapper == null ? (Selector ?? "") : $"{ParentWrapper.FullSelector} {Selector ?? ""}").Trim();

        public void ActivateScope()
        {
            if (ParentWrapper == null)
            {
                BrowserWrapper.ActivateScope();
            }
            else
            {
                ParentWrapper.ActivateScope();
            }
        }

        public ElementWrapperCollection(IEnumerable<ElementWrapper> collection, string selector, BrowserWrapper browserWrapper)
        {
            this.collection = new List<ElementWrapper>(collection);
            Selector = selector;
            SetReferences(selector);
            BrowserWrapper = browserWrapper;
        }

        /// <summary>
        /// Sets children reference to Parent wrapper
        /// </summary>
        /// <param name="selector"></param>
        private void SetReferences(string selector)
        {
            foreach (var ew in collection)
            {
                ew.Selector = selector;
                ew.ParentWrapper = this;
            }
        }

        public ElementWrapperCollection(IEnumerable<ElementWrapper> collection, string selector, ISeleniumWrapper parentElement, BrowserWrapper browserWrapper)
        {
            this.collection = new List<ElementWrapper>(collection);
            SetReferences(selector);
            Selector = selector;
            ParentWrapper = parentElement;
            BrowserWrapper = browserWrapper;
        }

        public ElementWrapperCollection(IEnumerable<ElementWrapper> collection, string selector, ElementWrapperCollection parentCollection)
        {
            this.collection = new List<ElementWrapper>(collection);
            SetReferences(selector);
            Selector = selector;
            ParentWrapper = parentCollection;
        }

        public ElementWrapperCollection ThrowIfSequenceEmpty()
        {
            if (!collection.Any())
            {
                throw new EmptySequenceException($"Sequence contains no one element. Selector: '{FullSelector}'");
            }
            return this;
        }

        public ElementWrapperCollection ThrowIfSequenceContainsMoreThanOneElement()
        {
            if (Count > 1)
            {
                throw new MoreElementsInSequenceException($"Sequence containse more Than one element. Selector: '{FullSelector}'");
            }
            return this;
        }

        public ElementWrapperCollection ThrowIfEmptyOrMoreThanOne()
        {
            return ThrowIfSequenceEmpty().ThrowIfSequenceContainsMoreThanOneElement();
        }

        public ElementWrapperCollection ThrowIfDifferentCountThan(int count)
        {
            if (Count != count)
            {
                throw new SequenceCountException($"Count of elements in sequence is different Than expected value. Selector: '{FullSelector}', Expected value: '{count}', Actual value: '{Count}'.");
            }
            return this;
        }

        public ElementWrapper SingleOrDefault()
        {
            return ThrowIfSequenceContainsMoreThanOneElement()?[0];
        }

        public ElementWrapper Single()
        {
            return ThrowIfEmptyOrMoreThanOne()?[0];
        }

        public ElementWrapper ElementAt(int index)
        {
            if (Count <= index || index < 0)
            {
                throw new SequenceCountException($"Index is out of range. Selector: '{FullSelector}', Sequence contains {Count} elements, Current index: '{index}'");
            }
            return collection[index];
        }

        public ElementWrapper Last()
        {
            ThrowIfSequenceEmpty();
            return collection.Last();
        }

        private readonly List<ElementWrapper> collection;

        public IEnumerator<ElementWrapper> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ElementWrapper item)
        {
            throw new NotSupportedException("Collection is readonly.");
        }

        public void Clear()
        {
            throw new NotSupportedException("Collection is readonly.");
        }

        public bool Contains(ElementWrapper item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(ElementWrapper[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ElementWrapper item)
        {
            throw new NotSupportedException("Collection is readonly.");
        }

        public int Count => collection.Count;
        public bool IsReadOnly => true;

        public int IndexOf(ElementWrapper item)
        {
            return collection.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
        }

        public IEnumerable<TResult> Select<TResult>(Func<ElementWrapper, TResult> selector)
        {
            return collection.Select(selector);
        }

        public ElementWrapper this[int index]
        {
            get { return ElementAt(index); }
            set { collection[index] = value; }
        }

        /// <summary>
        /// Performs the specified action on each element of the <see cref="Riganti.Utils.Testing.SeleniumCore.ElementWrapperCollection"/>.
        /// </summary>
        /// <param name="action">The <see cref="T:System.Action`1"/> delegate to perform on each element of the <see cref="T:System.Collections.Generic.List`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="action"/> is null.</exception>
        public void ForEach(Action<ElementWrapper> action)
        {
            collection.ForEach(action);
        }

        public ElementWrapperCollection FindElements(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var results = collection.SelectMany(item => item.FindElements(selector, tmpSelectMethod));

            return new ElementWrapperCollection(results, selector, this);
        }

        /// <summary>
        /// Returns first element from sequence. If sequence contains no element, function throws exception.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="tmpSelectMethod">Defines what type of selector are you want to use only for this query.</param>
        /// <exception cref="EmptySequenceException"></exception>
        public ElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null)
        {
            var element = FirstOrDefault(selector, tmpSelectMethod);
            if (element != null)
            {
                return element;
            }
            throw new EmptySequenceException($"Sequence does not contain element with selector: '{FullSelector}'");
        }

        /// <summary>
        /// Returns first element from sequence. If sequence contains no element, function returns null.
        /// </summary>
        /// <param name="tmpSelectMethod">Defines what type of selector are you want to use only for this query.</param>
        public ElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return collection.Select(item => item.FirstOrDefault(selector, tmpSelectMethod)).FirstOrDefault(element => element != null);
        }
    }
}