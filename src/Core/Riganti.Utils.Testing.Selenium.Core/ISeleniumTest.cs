using System;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Describes basic structure of tests based on selenium-utils.
    /// </summary>
    public interface ISeleniumTest
    {

        /// <summary>
        /// Represents current scope id.
        /// </summary>
        Guid CurrentScope { get; set; }

        TestSuiteRunner TestSuiteRunner { get;}
    }
}