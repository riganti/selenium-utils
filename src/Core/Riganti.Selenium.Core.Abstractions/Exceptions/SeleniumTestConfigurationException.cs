using System;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
{
    public class SeleniumTestConfigurationException : Exception
    {
        public SeleniumTestConfigurationException(string message) : base(message)
        {
        }

        public SeleniumTestConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}