using Riganti.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Selenium.Core.Comparators
{
    public class PresenceValidator

    {
        private bool Value;

        public PresenceValidator(bool value)
        {
            Value = value;
        }

        public string FailureMessage { get; set; }

        public void Has(string failureMessage = null)
        {
            if (!Value)
            {
                throw new UnexpectedElementStateException(failureMessage ?? this.FailureMessage ?? "The element has not something.");
            }
        }

        public void HasNot(string failureMessage = null)
        {
            if (Value)
            {
                throw new UnexpectedElementStateException(failureMessage ?? this.FailureMessage ?? "The element has something and should not.");
            }
        }
    }
}