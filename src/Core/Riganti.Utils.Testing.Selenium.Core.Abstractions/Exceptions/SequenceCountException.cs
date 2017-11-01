using System;
using System.Runtime.Serialization;

namespace Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions
{
    [Serializable]
    public class SequenceCountException : TestExceptionBase
    {
        public SequenceCountException()
        {
        }

        public SequenceCountException(string message) : base(message)
        {
        }

        public SequenceCountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SequenceCountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}