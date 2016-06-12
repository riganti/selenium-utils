using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Riganti.Utils.Testing.Selenium.Core.Exceptions
{
    [Serializable]
    public class SeleniumTestFailedException : WebDriverException
    {
        private readonly string CurrentSubSection;
        private readonly List<Exception> innerExceptions;

        public SeleniumTestFailedException()
        {
        }

        public SeleniumTestFailedException(string message) : base(message)
        {
        }

        public SeleniumTestFailedException(Exception innerException) : base("", innerException)
        {
        }

        public SeleniumTestFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SeleniumTestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SeleniumTestFailedException(Exception innerException, string browserName, string screenshotsPath) : this($"Test failed in browser '{browserName}'. \r\n Screenshot path: '{screenshotsPath}'.\r\n", innerException)
        {
            this.ScreenshotPath = screenshotsPath;
            this.BrowserName = browserName;
        }
        public SeleniumTestFailedException(List<Exception> innerExceptions, string browserName, string screenshotsPath) : this($"Test failed in browser '{browserName}'. \r\n Screenshot path: '{screenshotsPath}'.\r\n", innerExceptions.FirstOrDefault())
        {
            this.innerExceptions = innerExceptions;
            this.ScreenshotPath = screenshotsPath;
            this.BrowserName = browserName;
        }

        public SeleniumTestFailedException(List<Exception> innerExceptions, string browserName, string screenshotsPath, string currentSubsection) : this($"Test failed in browser '{browserName}'.\r\nTesting Subsection: {currentSubsection}.\r\n Screenshot path: '{screenshotsPath}'.\r\n", innerExceptions.FirstOrDefault())
        {
            this.innerExceptions = innerExceptions;
            this.ScreenshotPath = screenshotsPath;
            this.BrowserName = browserName;
        }

        public SeleniumTestFailedException(Exception innerException, string browserName, string screenshotsPath, string currentSubSection) : this($"Test failed in browser '{browserName}'.\r\nTesting Subsection: {currentSubSection}.\r\nScreenshot path: '{screenshotsPath}'. \r\n", innerException)
        {
            this.CurrentSubSection = currentSubSection;
            this.ScreenshotPath = screenshotsPath;
            this.BrowserName = browserName;
        }

        public string ScreenshotPath { get; set; }
        public string BrowserName { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Test failed in browser '{BrowserName}'.");
            sb.AppendLine($"Screenshot path: '{ScreenshotPath}'.");
            sb.AppendLine();
            sb.Append(base.ToString());
            sb.AppendLine();
            sb.AppendLine();
            // add all exceptions to report
            if (innerExceptions != null && innerExceptions.Count > 1)
            {
                sb.AppendLine("-----------  All Exceptions  -----------");

                for (int i = 0; i < innerExceptions.Count; i++)
                {
                    sb.AppendLine($"// Exception #{i + 1}");
                    sb.AppendLine(innerExceptions[i].ToString());
                    if (i != innerExceptions.Count - 1)
                    {
                        sb.AppendLine();
                        sb.AppendLine();
                    }
                }
            }
            return sb.ToString();
        }
    }
}