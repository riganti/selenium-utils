
using System;
using System.Runtime.Serialization;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
{
    [Serializable]
    public class BrowserLocationException : TestExceptionBase
    {
        public BrowserLocationException()
        {
        }

        public BrowserLocationException(string message) : base(message)
        {
        }

        public BrowserLocationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BrowserLocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
