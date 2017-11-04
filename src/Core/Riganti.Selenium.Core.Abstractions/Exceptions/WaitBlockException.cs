using System;
using System.Runtime.Serialization;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
{
    [Serializable]
    public class WaitBlockException : TestExceptionBase
    {
        public WaitBlockException()
        {
        }

        public WaitBlockException(string message) : base(message)
        {
        }

        public WaitBlockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WaitBlockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}