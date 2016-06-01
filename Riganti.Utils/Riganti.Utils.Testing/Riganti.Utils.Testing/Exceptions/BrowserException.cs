using System;
using System.Runtime.Serialization;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.SeleniumCore.Exceptions
{
    [Serializable]
    public class BrowserException : WebDriverException
    {
        public BrowserException()
        {
        }

        public BrowserException(string message) : base(message)
        {
        }

        public BrowserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BrowserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}