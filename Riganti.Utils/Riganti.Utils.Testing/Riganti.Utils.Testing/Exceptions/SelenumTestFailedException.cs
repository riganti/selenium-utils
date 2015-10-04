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
    public class SelenumTestFailedException : WebDriverException
    {
        private string screenshotsFolderPath;

        public SelenumTestFailedException()
        {
        }

        public SelenumTestFailedException(string message) : base(message)
        {
        }

        public SelenumTestFailedException(Exception innerException) : base("", innerException)
        {
        }

        public SelenumTestFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SelenumTestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SelenumTestFailedException(Exception innerException, string browserName, string screenshotsFolderPath) : this(innerException)
        {
            this.BrowserName = browserName;
            this.screenshotsFolderPath = screenshotsFolderPath;
        }

        public string ScreenshotPath { get; set; }
        public string BrowserName { get; set; }

        public override string ToString()
        {
            return $"Test failed in browser '{BrowserName}'. \r\n Screenshot path: '{ScreenshotPath}'.\r\n {base.ToString()}";
        }
    }
}