using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Selenium.Core.Samples.FluentApi.Tests
{
    /// <summary>
    /// Attribute that specifies to expect an exception of the specified type
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExpectedSeleniumExceptionAttribute : ExpectedExceptionBaseAttribute
    {
        /// <summary>
        /// Gets a value indicating the Type of the expected exception
        /// </summary>
        public Type[] ExceptionTypes { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow types derived from the type of the expected exception to
        /// qualify as expected
        /// </summary>
        public bool AllowDerivedTypes { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionAttribute" /> class with the expected type
        /// </summary>
        /// <param name="exceptionTypes">Type of the expected exception</param>
        public ExpectedSeleniumExceptionAttribute(params Type[] exceptionTypes)
            : this(string.Empty, exceptionTypes)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionAttribute" /> class with
        /// the expected type and the message to include when no exception is thrown by the test.
        /// </summary>
        /// <param name="exceptionType">Type of the expected exception</param>
        /// <param name="noExceptionMessage">
        /// Message to include in the test result if the test fails due to not throwing an exception
        /// </param>
        public ExpectedSeleniumExceptionAttribute(string noExceptionMessage, params Type[] exceptionTypes)
            : base(noExceptionMessage)
        {
            if (exceptionTypes == null)
                throw new ArgumentNullException("exceptionType");
            foreach (var exceptionType in exceptionTypes)
            {
                if (!typeof(Exception).GetTypeInfo().IsAssignableFrom(exceptionType.GetTypeInfo()))
                    throw new ArgumentException("Given type is not assignable from Exception.");
            }

            this.ExceptionTypes = exceptionTypes;
        }

        /// <summary>
        /// Verifies that the type of the exception thrown by the unit test is expected
        /// </summary>
        /// <param name="exception">The exception thrown by the unit test</param>
        protected override void Verify(Exception exception)
        {
            var seleniumException = exception as SeleniumTestFailedException;
            if (seleniumException != null)
            {
                if (AllowDerivedTypes)
                {
                    if (!seleniumException.InnerExceptions.All(s => ExceptionTypes.Any(n => n.IsInstanceOfType(s))))
                    {
                        RethrowIfAssertException(exception);
                        var exp = seleniumException.InnerExceptions
                            .First(s => ExceptionTypes.Any(n => n.IsInstanceOfType(s)));
                        throw new Exception(string.Format((IFormatProvider)CultureInfo.CurrentCulture, $"Test method threw exception {exp.GetType()}, but exception {string.Join(", ", ExceptionTypes.Select(s => s.FullName))} was expected. Exception message: {exp.Message}"));
                    }
                }

                if (!seleniumException.InnerExceptions.All(s => ExceptionTypes.Any(n => s.GetType() == n)))
                {
                    RethrowIfAssertException(exception);
                    var exp = seleniumException.InnerExceptions.First(s => ExceptionTypes.Any(n => s.GetType() != n));
                    throw new Exception(string.Format((IFormatProvider)CultureInfo.CurrentCulture, $"Test method threw exception {exp.GetType()}, but exception {string.Join(", ", ExceptionTypes.Select(s => s.FullName))} was expected. Exception message: {exp.Message}"));
                }

            }
            else if (seleniumException == null && exception != null)
            {
                RethrowIfAssertException(exception);
                throw new Exception($"Test method threw exception {exception.GetType()}, but exception {string.Join(", ", ExceptionTypes.Select(s => s.FullName))} was expected. Exception message: {exception.Message}", exception);
            }
            else
            {
                RethrowIfAssertException(exception);
                throw new Exception(string.Format((IFormatProvider)CultureInfo.CurrentCulture, NoExceptionMessage ?? "Test method did not throw exception."));
            }

        }
    }
}
