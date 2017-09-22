using OpenQA.Selenium;
using System;
using System.Runtime.Serialization;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
{
    [Serializable]
    public class EmptySequenceException : TestExceptionBase
    {
        public EmptySequenceException()
        {
        }

        public EmptySequenceException(string message) : base(message)
        {
        }

        public EmptySequenceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptySequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}