using System;
using OpenQA.Selenium;
using System.Runtime.Serialization;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
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