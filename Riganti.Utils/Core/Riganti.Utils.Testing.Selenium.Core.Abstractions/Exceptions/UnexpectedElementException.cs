using System;
using System.Runtime.Serialization;

namespace Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions
{
    [Serializable]
    public class UnexpectedElementException : TestExceptionBase
    {
        public UnexpectedElementException()
        {
        }

        public UnexpectedElementException(string message) : base(message)
        {
        }

        public UnexpectedElementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnexpectedElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}