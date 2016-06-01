using System;
using System.Runtime.Serialization;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.SeleniumCore.Exceptions
{
    [Serializable]
    public class AlertException : WebDriverException
    {
        public AlertException()
        {
        }

        public AlertException(string message) : base(message)
        {
        }

        public AlertException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}