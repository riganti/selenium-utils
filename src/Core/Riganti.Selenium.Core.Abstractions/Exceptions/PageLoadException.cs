using System;
using System.Runtime.Serialization;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
{
    [Serializable]
    public class PageLoadException : TestExceptionBase
    {
        public PageLoadException()
        {
        }

        public PageLoadException(string message) : base(message)
        {
        }

        public PageLoadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PageLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}