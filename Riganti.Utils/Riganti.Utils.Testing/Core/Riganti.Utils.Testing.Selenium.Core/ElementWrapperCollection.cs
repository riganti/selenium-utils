using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class ElementWrapperCollection : ICollection<IElementWrapper>, IElementWrapperCollection
    {
        public string Selector { get; }
        /// <summary>
        ///         
        /// </summary>
        public ISeleniumWrapper ParentWrapper { get; set; }
        /// <summary>
        /// Wrapper of a current browser. 
        /// </summary>
        public IBrowserWrapper BrowserWrapper { get; set; }
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

        public ElementWrapperCollection(IEnumerable<IElementWrapper> collection, string selector, BrowserWrapper browserWrapper)
        {
            this.collection = new List<IElementWrapper>(collection);
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

        public ElementWrapperCollection(IEnumerable<IElementWrapper> collection, string selector, ISeleniumWrapper parentElement, IBrowserWrapper browserWrapper)
        {
            this.collection = new List<IElementWrapper>(collection);
            SetReferences(selector);
            Selector = selector;
            ParentWrapper = parentElement;
            BrowserWrapper = browserWrapper;
        }

        public ElementWrapperCollection(IEnumerable<IElementWrapper> collection, string selector, IElementWrapperCollection parentCollection)
        {
            this.collection = new List<IElementWrapper>(collection);
            SetReferences(selector);
            Selector = selector;
            ParentWrapper = parentCollection;
        }

        public IElementWrapperCollection ThrowIfSequenceEmpty()
        {
            if (!collection.Any())
            {
                throw new EmptySequenceException($"Sequence contains no elements. Selector: '{FullSelector}'");
            }
            return this;
        }

        public IElementWrapperCollection ThrowIfSequenceContainsMoreThanOneElement()
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
        public IElementWrapperCollection ThrowIfEmptyOrMoreThanOne()
        {
            return ThrowIfSequenceEmpty().ThrowIfSequenceContainsMoreThanOneElement();
        }
        /// <summary>
        /// Throws exception when lenght of sequence is different then specified number.
        /// </summary>
        /// <param name="count">expected sequence lenght</param>
        public IElementWrapperCollection ThrowIfDifferentCountThan(int count)
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
        public IElementWrapper SingleOrDefault()
        {
            return ThrowIfSequenceContainsMoreThanOneElement()?[0];
        }
        /// <summary>
        /// Returns the only web element of a sequence, and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        public IElementWrapper Single()
        {

            return ThrowIfEmptyOrMoreThanOne()?[0];
        }
        /// <summary>
        /// Returns the element at a specified index in a sequence.
        /// </summary>
        /// <param name="index">position of web element to return</param>
        public IElementWrapper ElementAt(int index)
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
        public IElementWrapper Last()
        {
            ThrowIfSequenceEmpty();
            return collection.Last();
        }

        private readonly List<IElementWrapper> collection;

        public IEnumerator<IElementWrapper> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IElementWrapper item)
        {
            throw new NotSupportedException("Collection is readonly.");
        }

        public void Clear()
        {
            throw new NotSupportedException("Collection is readonly.");
        }

        public bool Contains(IElementWrapper item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(IElementWrapper[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IElementWrapper item)
        {
            throw new NotSupportedException("Collection is readonly.");
        }

        public int Count => collection.Count;
        public bool IsReadOnly => true;

        public int IndexOf(IElementWrapper item)
        {
            return collection.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
        }

        public IEnumerable<TResult> Select<TResult>(Func<IElementWrapper, TResult> selector)
        {
            return collection.Select(selector);
        }

        public IElementWrapper this[int index]
        {
            get => ElementAt(index);
            set => collection[index] = value;
        }

        /// <summary>
        /// Performs the specified action on each element of the <see cref="ElementWrapperCollection"/>.
        /// </summary>
        /// <param name="action">The <see cref="T:System.Action`1"/> delegate to perform on each element of the <see cref="T:System.Collections.Generic.List`1"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="action"/> is null.</exception>
        public void ForEach(Action<IElementWrapper> action)
        {
            collection.ForEach(action);
        }

        public IElementWrapperCollection FindElements(string selector, Func<string, By> tmpSelectMethod = null)
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
        public IElementWrapper First(string selector, Func<string, By> tmpSelectMethod = null)
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
        public IElementWrapper FirstOrDefault(string selector, Func<string, By> tmpSelectMethod = null)
        {
            return collection.Select(item => item.FirstOrDefault(selector, tmpSelectMethod)).FirstOrDefault(element => element != null);
        }
    }
}