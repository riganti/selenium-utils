using System;
using System.Runtime.Serialization;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
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