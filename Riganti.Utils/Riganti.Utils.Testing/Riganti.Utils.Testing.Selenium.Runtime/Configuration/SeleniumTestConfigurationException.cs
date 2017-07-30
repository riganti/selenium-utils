using System;

namespace Riganti.Utils.Testing.Selenium.Runtime.Configuration
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