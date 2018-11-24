using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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