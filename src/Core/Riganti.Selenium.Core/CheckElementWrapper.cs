using System;
using Riganti.Selenium.Core.Comparators;

namespace Riganti.Selenium.Core
{
    /// <summary>
    /// Represents tool to set validation rules on some element.
    /// </summary>
    public class CheckElementWrapper
    {
        /// <summary>
        /// Element to validate.
        /// </summary>
        public ElementWrapper ElementWrapper { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckElementWrapper"/> class.
        /// </summary>
        /// <param name="elementWrapper">The element wrapper.</param>
        public CheckElementWrapper(ElementWrapper elementWrapper)
        {
            this.ElementWrapper = elementWrapper;
        }

        /// <summary>
        /// Checks the tag name.
        /// </summary>
        /// <param name="action">Validation rule</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <returns></returns>
        public CheckElementWrapper Tag(Action<StringValueComparator> action, string failureMessage = null)
        {
            var comparator = new StringValueComparator(ElementWrapper.GetTagName()) { FailureMessage = failureMessage };
            action.Invoke(comparator);
            return this;
        }

        /// <summary>
        /// Checks the inner text of provided element.
        /// </summary>
        /// <param name="action">Validation rule</param>
        /// <param name="failureMessage">The failure message.</param>
        public CheckElementWrapper InnerText(Action<StringValueComparator> action, string failureMessage = null)
        {
            var comparator = new StringValueComparator(ElementWrapper.GetInnerText()) { FailureMessage = failureMessage };
            action.Invoke(comparator);
            return this;
        }

        /// <summary>
        /// Checks the attribute value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="action">The action.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <returns></returns>
        public CheckElementWrapper Attribute(string name, Action<StringValueComparator> action, string failureMessage = null)
        {
            var comparator = new StringValueComparator(ElementWrapper.GetAttribute(name)) { FailureMessage = failureMessage };
            action.Invoke(comparator);
            return this;
        }

        /// <summary>
        /// Checks the attribute value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="action">The action.</param>
        /// <param name="failureMessage">The failure message.</param>
        /// <returns></returns>
        public CheckElementWrapper CssClass(string name, Action<PresenceValidator> action, string failureMessage = null)
        {
            var comparator = new PresenceValidator(ElementWrapper.HasCssClass(name)) { FailureMessage = failureMessage };
            action.Invoke(comparator);
            return this;
        }

        public CheckElementWrapper Value(Action<StringValueComparator> action, string failureMessage = null)
        {
            var comparator = new StringValueComparator(ElementWrapper.GetValue()) { FailureMessage = failureMessage };
            action.Invoke(comparator);
            return this;
        }
    }
}