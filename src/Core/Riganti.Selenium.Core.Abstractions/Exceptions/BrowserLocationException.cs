using System;
using System.Runtime.Serialization;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
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
