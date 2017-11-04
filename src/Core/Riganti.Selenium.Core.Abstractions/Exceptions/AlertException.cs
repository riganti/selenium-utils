using System;
using System.Runtime.Serialization;

namespace Riganti.Selenium.Core.Abstractions.Exceptions
{
    /// <summary>
    /// Exception that is trown by browser's alert.
    /// </summary>
    /// <seealso cref="OpenQA.Selenium.WebDriverException" />
    [Serializable]
    public class AlertException : TestExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlertException"/> class.
        /// </summary>
        public AlertException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AlertException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or <see langword="null" /> if no inner exception is specified.</param>
        public AlertException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
        /// information about the source or destination.</param>
        protected AlertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}