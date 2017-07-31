using System;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
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