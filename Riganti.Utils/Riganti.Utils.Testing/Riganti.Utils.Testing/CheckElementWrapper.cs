using Riganti.Utils.Testing.Selenium.Core.Comparators;
using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class CheckElementWrapper
    {
        public ElementWrapper ElementWrapper { get; private set; }

        public CheckElementWrapper(ElementWrapper elementWrapper)
        {
            this.ElementWrapper = elementWrapper;
        }

        public CheckElementWrapper Tag(string expectedTagName, string failureMessage = null)
        {
            return Tag(x => x.Equals(expectedTagName), failureMessage);
        }

        public CheckElementWrapper Tag(string[] expectedTagNames, string failureMessage = null)
        {
            return Tag(x => x.AnyFrom(expectedTagNames), failureMessage);
        }

        public CheckElementWrapper Tag(params string[] expectedTagNames)
        {
            return Tag(expectedTagNames, null);
        }

        public CheckElementWrapper Tag(Action<StringValueComparator> action, string failureMessage = null)
        {
            var comparator = new StringValueComparator(ElementWrapper.GetTagName()) { FailureMessage = failureMessage };
            action.Invoke(comparator);

            return this;
        }
    }
}
