using System;
using System.Runtime.Serialization;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
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