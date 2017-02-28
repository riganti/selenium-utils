using Riganti.Utils.Testing.Selenium.Core.Exceptions;
using System;
using System.Linq;

namespace Riganti.Utils.Testing.Selenium.Core.Comparators
{
    public class StringValueComparator
    {
        private string compareValue;

        public string FailureMessage { get; set; }

        public StringValueComparator(string compareValue)
        {
            this.compareValue = compareValue;
        }

        public void Equals(string value)
        {
            if (compareValue == value)
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element compared values differ. Expected value: '{value}', Provided value: '{compareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        public void NotEquals(string value)
        {
            if (compareValue != value)
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element compared values do not differ. Provided value: '{compareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        public void Contains(string value)
        {
            if (compareValue == value || compareValue != null && compareValue.Contains(value))
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element value does not contain expected substring. Expected value to contain: '{value}', Provided value: '{compareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        public void AnyFrom(string[] values)
        {
            if (values.Any(x => x == compareValue))
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element value does not contain any of the expected values. Expected values : '{string.Join(",", values)}', Provided value: '{compareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }

        public void Condition(Func<string, bool> func)
        {
            if (func(compareValue))
            {
                return;
            }

            var failureMessage = FailureMessage ?? $"Element compare condition failed. Provided value: '{compareValue}' \r\n";
            throw new UnexpectedElementStateException(failureMessage);
        }
    }
}
