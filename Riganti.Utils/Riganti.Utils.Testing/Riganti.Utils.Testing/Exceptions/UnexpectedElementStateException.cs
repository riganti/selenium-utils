using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.SeleniumCore.Exceptions
{
    public class UnexpectedElementStateException : WebDriverException
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