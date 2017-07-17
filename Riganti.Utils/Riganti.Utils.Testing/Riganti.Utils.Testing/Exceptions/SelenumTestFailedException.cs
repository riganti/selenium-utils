using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Riganti.Utils.Testing.Selenium.Core.Api.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown only from execution block in SeleniumTestBase. Indicates that test failed.
    /// </summary>
    [Serializable]
    public sealed class SeleniumTestFailedException : WebDriverException
    {
        public string CurrentSubSection { get; set; }

        private readonly List<Exception> innerExceptions;

        public CheckResult[] InnerCheckResults { get; }


        /// <inheritdoc />
        public override string Message => ToString();

        /// <summary>
        ///
        /// </summary>
        public string ExceptionMessage { get; set; }

        public string FullStackTrace => base.StackTrace;

        public override string StackTrace => innerExceptions?.FirstOrDefault()?.StackTrace ?? FullStackTrace;

        /// <inheritdoc/>
        internal SeleniumTestFailedException()
        {
        }

        /// <inheritdoc/>

        internal SeleniumTestFailedException(string message) : base(message)
        {
            ExceptionMessage = message;
        }

        /// <inheritdoc/>

        internal SeleniumTestFailedException(Exception innerException) : this("", innerException)
        {
        }

        /// <inheritdoc/>

        internal SeleniumTestFailedException(string message, Exception innerException) : base(message, innerException)
        {
            ExceptionMessage = message;
            this.innerExceptions = new List<Exception>(new[] { innerException });
            InnerCheckResults = CreateCheckResult(this.innerExceptions).ToArray();
        }

        private IEnumerable<CheckResult> CreateCheckResult(IEnumerable<Exception> exceptions)
        {
            return exceptions.OfType<TestExceptionBase>().Select(e => e.CheckResult);
        }

        /// <inheritdoc/>
        private SeleniumTestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc/>
        internal SeleniumTestFailedException(Exception innerException, string browserName, string screenshotsPath) : this($"Test failed in browser '{browserName}'.", innerException)
        {
            this.ScreenshotPath = screenshotsPath;
            this.BrowserName = browserName;
        }


        internal SeleniumTestFailedException(List<Exception> innerExceptions, string browserName, string screenshotsPath = null, string currentSubsection = null) : this($"Test failed in browser '{browserName}'", null)
        {
            CurrentSubSection = currentSubsection;
            ScreenshotPath = screenshotsPath;
            BrowserName = browserName;
            this.innerExceptions = innerExceptions;
            InnerCheckResults = CreateCheckResult(this.innerExceptions).ToArray();
        }

        /// <inheritdoc/>

        internal SeleniumTestFailedException(Exception innerException, string browserName, string screenshotsPath, string currentSubSection) : this($"Test failed in browser '{browserName}'.", innerException)
        {
            this.CurrentSubSection = currentSubSection;
            this.ScreenshotPath = screenshotsPath;
            this.BrowserName = browserName;
        }

        /// <summary>
        /// Path to stored screenshot.
        /// </summary>
        public string ScreenshotPath { get; set; }

        /// <summary>
        /// Name of browser where the test failed.
        /// </summary>
        public string BrowserName { get; set; }

        /// <summary>
        /// Current url of the tested website.
        /// </summary>
        public string Url { get; set; }

        ///<summary>
        /// Returns basic informations to debug exception trown in selenium test.
        /// </summary>
        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();

            RenderMessage(sb);
            RenderInnerCheckResults(sb);
            RenderBrowserName(sb);
            RenderSubsection(sb);
            RenderScreenshotPath(sb);
            RenderUrl(sb);

            sb.AppendLine();

            //render base
            RenderStackTrace(sb, 0, this);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();

            RenderInnerAgregatedExceptions(sb);

            return sb.ToString();
        }

        private void RenderInnerCheckResults(StringBuilder sb)
        {
            RenderCollectionIfNotEmpty(sb, InnerCheckResults, "Inner check results", (index, result) => $"Inner check result #{index}: '{result}'.");
        }

        private void RenderStackTrace(StringBuilder sb, int i, Exception exception)
        {
            if (exception.InnerException != null)
            {
                RenderStackTrace(sb, i + 1, exception.InnerException);
            }

            for (int j = 0; j < i; j++)
            {
                sb.Append("   ");
            }

            if (i != 0)
            {
                //inner
                sb.AppendLine(exception.GetType().FullName + ": " + exception.Message);
                sb.AppendLine(" ---> " + exception.StackTrace);
                sb.AppendLine(" --- End of inner exception stack trace ---");
            }
            else
            {
                //top exception = this
                sb.AppendLine(exception.StackTrace);
            }
        }

        private void RenderCollectionIfNotEmpty<T>(StringBuilder sb, T[] source, string collectionName, Func<int, T, string> createMessage)
        {
            if (source.Any())
            {
                sb.AppendLine($"{collectionName}:");
                for (int i = 0; i < source.Length; i++)
                {
                    var item = source[i];
                    RenderWhenNotNull(sb, item, $"\t{createMessage(i, item)}");
                }
            }
        }

        private void RenderInnerAgregatedExceptions(StringBuilder sb)
        {
            // add all exceptions to report
            if (innerExceptions != null && innerExceptions.Count > 1)
            {
                sb.AppendLine("-----------  Agregated Inner Exceptions  -----------");

                for (int i = 0; i < innerExceptions.Count; i++)
                {
                    RenderInnerException(sb, i);
                }
            }
        }

        private void RenderInnerException(StringBuilder sb, int i)
        {
            sb.AppendLine($"// Exception #{i + 1}");
            sb.AppendLine(innerExceptions[i].ToString());

            //add separator
            if (i != innerExceptions.Count - 1)
            {
                sb.AppendLine();
                sb.AppendLine();
            }
        }

        private void RenderBrowserName(StringBuilder sb)
        {
            RenderWhenNotNull(sb, BrowserName, $"Test failed in browser '{BrowserName}'.");
        }

        private void RenderWhenNotNull<T>(StringBuilder builder, T testedObject, string message)
        {
            if (testedObject != null)
            {
                builder.AppendLine(message);
            }
        }

        private void RenderMessage(StringBuilder sb)
        {
            RenderWhenNotNull(sb, ExceptionMessage, $"Message: {ExceptionMessage}");
        }

        private void RenderSubsection(StringBuilder sb)
        {
            RenderWhenNotNull(sb, CurrentSubSection, $"Current subsection: '{CurrentSubSection}'.");
        }

        private void RenderScreenshotPath(StringBuilder sb)
        {
            RenderWhenNotNull(sb, ScreenshotPath, $"Screenshot path: '{ScreenshotPath}' ");
        }

        private void RenderUrl(StringBuilder sb)
        {
            RenderWhenNotNull(sb, Url, $"Url: {Url} ");
        }
    }
}