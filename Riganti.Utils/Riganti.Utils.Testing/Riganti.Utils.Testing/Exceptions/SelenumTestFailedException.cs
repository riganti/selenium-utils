using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Utils.Testing.SeleniumCore.Exceptions
{
    [Serializable]
    public class SeleniumTestFailedException : WebDriverException
    {
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

        public SeleniumTestFailedException(Exception innerException, string browserName, string screenshotsPath, string currentSubSection) : this($"Test failed in browser '{browserName}'.\r\nTesting Subsection: {currentSubSection}.\r\nScreenshot path: '{screenshotsPath}'. \r\n", innerException)
        {
            this.ScreenshotPath = screenshotsPath;
            this.BrowserName = browserName;
        }

        public string ScreenshotPath { get; set; }
        public string BrowserName { get; set; }

        public override string ToString()
        {
            return $"Test failed in browser '{BrowserName}'. \r\n Screenshot path: '{ScreenshotPath}'.\r\n {base.ToString()}";
        }
    }
}