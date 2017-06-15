using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using System;
using System.Configuration;
using System.Linq;

namespace Riganti.Utils.Testing.Selenium.Core.Comparators
{
    /// <summary>
    /// Represents object that is responsible for correct validation of some value.
    /// </summary>
    public class StringValueComparator
    {
        private readonly string compareValue;
        private string CompareValue => Trim ? compareValue?.Trim() : compareValue;
        public bool Trim { get; set; }

        /// <summary>
        /// Gets or sets the failure message.
        /// </summary>
        /// <value>
        /// The failure message.
        /// </value>
        public string FailureMessage { get; set; }

        /// <summary>
        /// Inits new string comparer.
        /// </summary>
        public StringValueComparator(string compareValue)
        {
            this.compareValue = compareValue;
        }

        /// <summary>
        /// Equalses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        /// <exception cref="UnexpectedElementStateException"></exception>
        public void Equals(string value, StringComparison? comparison = null)
        {
            if (string.Equals(CompareValue, value, comparison ?? SeleniumTestsConfiguration.DefaultStringComparison))
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element compared values differ. Expected value: '{value}', Provided value: '{CompareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        /// <summary>
        /// Nots the equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="comparison">The comparison.</param>
        /// <exception cref="UnexpectedElementStateException"></exception>
        public void NotEquals(string value, StringComparison? comparison = null)
        {
            if (!string.Equals(CompareValue, value, comparison ?? SeleniumTestsConfiguration.DefaultStringComparison))
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element compared values do not differ. Provided value: '{CompareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        /// <summary>
        /// Determines whether [contains] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="comparison">The comparison.</param>
        /// <exception cref="UnexpectedElementStateException"></exception>
        public void Contains(string value, StringComparison? comparison = null)
        {
            if (CompareValue == value || CompareValue != null && CompareValue.IndexOf(value, comparison ?? SeleniumTestsConfiguration.DefaultStringComparison) > -1)
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element value does not contain expected substring. Expected value to contain: '{value}', Provided value: '{CompareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        /// <summary>
        /// Check if provided value by browser is in specified values.
        /// </summary>
        /// <param name="values">Options to compare with.</param>
        public void IsIn(params string[] values)
        {
            IsIn(SeleniumTestsConfiguration.DefaultStringComparison, values);
        }

        /// <summary>
        /// Check if provided value by browser is in specified values.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <param name="values">Options to compare with.</param>
        /// <exception cref="UnexpectedElementStateException"></exception>
        public void IsIn(StringComparison comparison, params string[] values)
        {
            IsIn((x, c) => string.Equals(x, c, comparison), values);
        }

        /// <summary>
        /// Check if provided value by browser is in specified values.
        /// </summary>
        /// <param name="compareFunc">The compare function.</param>
        /// <param name="values">Options to compare with.</param>
        /// <exception cref="UnexpectedElementStateException"></exception>
        public void IsIn(Func<string, string, bool> compareFunc, params string[] values)
        {
            if (values.Any(s => compareFunc(s, CompareValue)))
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element value does not contain any of the expected values. Expected values : '{string.Join(",", values)}', Provided value: '{CompareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        /// <summary>
        /// Conditions the specified function.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <exception cref="UnexpectedElementStateException"></exception>
        public void Condition(Func<string, bool> func)
        {
            try
            {
                if (func(CompareValue))
                {
                    return;
                }
            }
            catch (Exception e)
            {
                throw new UnexpectedElementStateException($"Function executed with errors. Provided value: {CompareValue}. \r\n", e);
            }
            var failureMessage = FailureMessage ?? $"Element compare condition failed. Provided value: '{CompareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        public void StartsWith(string text, StringComparison? ordinalIgnoreCase = null)
        {
            throw new NotImplementedException();
        }

        public void EndsWith(string text, StringComparison? ordinalIgnoreCase = null)
        {
            throw new NotImplementedException();
        }

        public void IndexOf(string text, StringComparison? ordinalIgnoreCase = null)
        {
            throw new NotImplementedException();
        }

        public void Count(string text, StringComparison? ordinalIgnoreCase = null)
        {
            throw new NotImplementedException();
        }

        public void Length(int length)
        {
            throw new NotImplementedException();
        }
    }
}