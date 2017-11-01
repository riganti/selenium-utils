using System;
using System.Runtime.Serialization;

namespace Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions
{
    [Serializable]
    public class MoreElementsInSequenceException : TestExceptionBase
    {
        public MoreElementsInSequenceException()
        {
        }

        public MoreElementsInSequenceException(string message) : base(message)
        {
        }

        public MoreElementsInSequenceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MoreElementsInSequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}