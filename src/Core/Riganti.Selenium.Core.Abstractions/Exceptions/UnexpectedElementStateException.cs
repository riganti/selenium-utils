using System;
using System.Runtime.Serialization;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
{
    [Serializable]
    public class UnexpectedElementStateException : TestExceptionBase
    {
        public UnexpectedElementStateException()
        {
        }

        public UnexpectedElementStateException(string message) : base(message)
        {
        }

        public UnexpectedElementStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnexpectedElementStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}