using OpenQA.Selenium.Internal;
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

        public string FullSelector => (ParentWrapper == null ? (Selector ?? "") : $"{ParentWrapper.FullSelector} {Selector ?? ""}").Trim();

        public ElementWrapperCollection(IEnumerable<ElementWrapper> collection, string selector)
        {
            this.collection = new List<ElementWrapper>(collection);
            Selector = selector;
            SetRereferences(selector);
        }
        /// <summary>
        /// Sets children reference to Parent wrapper
        /// </summary>
        /// <param name="selector"></param>
        private void SetRereferences(string selector)
        {
            foreach (var ew in collection)
            {
                ew.Selector = selector;
                ew.ParentWrapper = this;
            }
        }

        public ElementWrapperCollection(IEnumerable<ElementWrapper> collection, string selector, ElementWrapper parentElement)
        {
            this.collection = new List<ElementWrapper>(collection);
            SetRereferences(selector);
            Selector = selector;
            ParentWrapper = parentElement;
        }
        public ElementWrapperCollection(IEnumerable<ElementWrapper> collection, string selector, ElementWrapperCollection parentCollection)
        {
            this.collection = new List<ElementWrapper>(collection);
            SetRereferences(selector);
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
            return this[index];
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
            get { return collection[index]; }
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

        public ElementWrapperCollection FindElements(string selector)
        {
            var results = collection.SelectMany(item => item.FindElements(selector));

            return new ElementWrapperCollection(results, selector, this);
        }

        public ElementWrapper First(string selector)
        {
            var element = FirstOrDefault(selector);
            if (element != null)
            {
                return element;
            }
            throw new EmptySequenceException($"Sequence does not contain element with selector: '{FullSelector}'");
        }

        public ElementWrapper FirstOrDefault(string selector)
        {
            return collection.Select(item => item.FirstOrDefault(selector)).FirstOrDefault(element => element != null);
        }

    }
}