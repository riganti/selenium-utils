using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class ElementWrapperCollection : ICollection<ElementWrapper>, ISeleniumWrapper
    {
        public string Selector { get; }
        /// <summary>
        ///         
        /// </summary>
        public ISeleniumWrapper ParentWrapper { get; set; }
        /// <summary>
        /// Wrapper of a current browser. 
        /// </summary>
        public BrowserWrapper BrowserWrapper { get; set; }
        /// <summary>
        /// Selector construced by provided partail selectors in a test. 
        /// </summary>
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
        /// Sets children reference to Parent wrapper.
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
                throw new EmptySequenceException($"Sequence contains no elements. Selector: '{FullSelector}'");
            }
            return this;
        }

        public ElementWrapperCollection ThrowIfSequenceContainsMoreThanOneElement()
        {
            if (Count > 1)
            {
                throw new MoreElementsInSequenceException($"Sequence contains more than one element. Selector: '{FullSelector}'");
            }
            return this;
        }
        /// <summary>
        /// Throws exception when lenght of a sequence is empty or the sequence contains more than one element.
        /// </summary>
        /// <returns></returns>
        public ElementWrapperCollection ThrowIfEmptyOrMoreThanOne()
        {
            return ThrowIfSequenceEmpty().ThrowIfSequenceContainsMoreThanOneElement();
        }
        /// <summary>
        /// Throws exception when lenght of sequence is different then specified number.
        /// </summary>
        /// <param name="count">expected sequence lenght</param>
        public ElementWrapperCollection ThrowIfDifferentCountThan(int count)
        {
            if (Count != count)
            {
                throw new SequenceCountException($"Element count in sequence is different from the expected value. Selector: '{FullSelector}', Expected value: '{count}', Actual value: '{Count}'.");
            }
            return this;
        }
        /// <summary>
        /// Returns a single, specific web element of a sequence, or null value if that element is not found.
        /// </summary>
        public ElementWrapper SingleOrDefault()
        {
            return ThrowIfSequenceContainsMoreThanOneElement()?[0];
        }
        /// <summary>
        /// Returns the only web element of a sequence, and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        public ElementWrapper Single()
        {

            return ThrowIfEmptyOrMoreThanOne()?[0];
        }
        /// <summary>
        /// Returns the element at a specified index in a sequence.
        /// </summary>
        /// <param name="index">position of web element to return</param>
        public ElementWrapper ElementAt(int index)
        {
            if (Count <= index || index < 0)
            {
                throw new SequenceCountException($"Index is out of range. Selector: '{FullSelector}', Sequence contains {Count} elements, Current index: '{index}'");
            }
            return collection[index];
        }
        /// <summary>
        /// Returns last web element of a sequence.
        /// </summary>
        /// <returns></returns>
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
        /// Performs the specified action on each element of the <see cref="ElementWrapperCollection"/>.
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
            throw new EmptySequenceException($"Sequence contains no element with the selector: '{FullSelector}'");
        }

        /// <summary>
        /// Returns first element from sequence. If sequence doesn't contain the element, function returns null.
        /// </summary>
        /// <param name="tmpSelectMethod">Defines what type of selector are you want to use only for this query.</param>
        public ElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return collection.Select(item => item.FirstOrDefault(selector, tmpSelectMethod)).FirstOrDefault(element => element != null);
        }
    }
}