using System;
using System.Runtime.Serialization;
using OpenQA.Selenium;

namespace Riganti.Utils.Testing.SeleniumCore.Exceptions
{
    [Serializable]
    public class MoreElementsInSequenceException : WebDriverException
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