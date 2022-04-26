using System;
using System.Runtime.Serialization;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
{
    [Serializable]
    public class AssertationMisusageException : Exception
    {
        public AssertationMisusageException() { }
        public AssertationMisusageException(string message) : base(message) { }
        public AssertationMisusageException(string message, Exception inner) : base(message, inner) { }
        protected AssertationMisusageException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}