using Riganti.Utils.Testing.SeleniumCore.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class ElementWrapperCollection : ICollection<ElementWrapper>
    {
        public ElementWrapperCollection(IEnumerable<ElementWrapper> collection, string selector)
        {
            this.collection = new List<ElementWrapper>(collection);
            Selector = selector;
        }

        public ElementWrapperCollection(IEnumerable<ElementWrapper> collection, string selector, ElementWrapper parentElement)
        {
            this.collection = new List<ElementWrapper>(collection);
            Selector = selector;
            ParentElement = parentElement;
        }

        public string Selector { get; }
        public ElementWrapper ParentElement { get; set; }

        public ElementWrapperCollection ThrowIfSequenceEmpty()
        {
            if (!collection.Any())
            {
                throw new EmptySequenceException($"Sequence contains no one element. Selector: '{Selector}'");
            }
            return this;
        }

        public ElementWrapperCollection ThrowIfSequenceContainsMoreThanOneElement()
        {
            if (Count > 1)
            {
                throw new MoreElementsInSequenceException($"Sequence containse more Than one element. Selector: '{Selector}'");
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
                throw new SequenceCountException($"Count of elements in sequence is different Than expected value. Selector: '{Selector}', Expected value: '{count}', Actual value: '{Count}'.");
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
                throw new SequenceCountException($"Index is out of range. Selector: '{Selector}', Sequence contains {Count} elements, Current index: '{index}'");
            }
            return this[index];
        }

        public ElementWrapper Last()
        {
            ThrowIfSequenceEmpty();
            return collection.Last();
        }

        private List<ElementWrapper> collection;

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

        public void ForEach(Action<ElementWrapper> action)
        {
            collection.ForEach(action);
        }
    }
}