using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Describes basic structure of tests based on selenium-utils.
    /// </summary>
    public interface ISeleniumTest
    {
        /// <summary>
        /// Context of a test.
        /// </summary>
        ITestContext Context { get; set; }
        /// <summary>
        /// Writes exception to via all registered loggers.
        /// </summary>
        /// <param name="exception"></param>
        void Log(Exception exception);
        /// <summary>
        /// Represents current scope id.
        /// </summary>
        Guid CurrentScope { get; set; }
    }
}