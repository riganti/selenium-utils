using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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

        /// <inheritdoc />
        public override string Message => ToString();
        /// <summary>
        /// 
        /// </summary>
        public string ExceptionMessage { get; set; }
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

        internal SeleniumTestFailedException(Exception innerException) : base("", innerException)
        {
        }
        /// <inheritdoc/>

        internal SeleniumTestFailedException(string message, Exception innerException) : base(message, innerException)
        {
            ExceptionMessage = message;
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
        /// <inheritdoc/>
        internal SeleniumTestFailedException(List<Exception> innerExceptions, string browserName) : this($"Test failed in browser '{browserName}'.", null)
        {
            this.innerExceptions = innerExceptions;
            this.BrowserName = browserName;
        }
        /// <inheritdoc/>

        internal SeleniumTestFailedException(List<Exception> innerExceptions, string browserName, string screenshotsPath) : this($"Test failed in browser '{browserName}'.", null)
        {
            this.innerExceptions = innerExceptions;
            this.ScreenshotPath = screenshotsPath;
            this.BrowserName = browserName;
        }
        /// <inheritdoc/>

        internal SeleniumTestFailedException(List<Exception> innerExceptions, string browserName, string screenshotsPath, string currentSubsection) : this($"Test failed in browser '{browserName}'", null)
        {
            this.innerExceptions = innerExceptions;
            CurrentSubSection = currentSubsection;
            ScreenshotPath = screenshotsPath;
            BrowserName = browserName;
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

        private void RenderWhenNotNull(StringBuilder builder, object testedObject, string message)
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