using System;
using System.Runtime.Serialization;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
{
    /// <summary>
    /// Represents common exception, that is trown when action in browser cannot be executed or received data are incorrect.
    /// </summary>
    [Serializable]
    public class BrowserException : TestExceptionBase
    {
        public BrowserException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BrowserException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or <see langword="null" /> if no inner exception is specified.</param>
        public BrowserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
        /// information about the source or destination.</param>
        protected BrowserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}