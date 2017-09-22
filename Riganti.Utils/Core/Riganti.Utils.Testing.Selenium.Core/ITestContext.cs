using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core.Drivers;
using Riganti.Utils.Testing.Selenium.Core.Factories;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestContext
    {

        /// <summary>
        /// Gets the fully-qualified name of the class that contains the test method that is currently running.
        /// </summary>
        string FullyQualifiedTestClassName { get; }

        /// <summary>
        /// Gets the test name.
        /// </summary>
        string TestName { get; }

        /// <summary>
        /// You can use this property in a TestCleanup method to determine the outcome of a test that has run.
        /// </summary>
        UnitTestResult CurrentTestResult { get; }

        /// <summary>
        /// Writes trace messages while the test is running.
        /// </summary>
        /// <param name="format">The string that contains the trace message.</param>
        /// <param name="args">Arguments to add to the trace message.</param>
        void WriteLine(string format, params object[] args);

        /// <summary>
        /// Gets the web browser object used to run the test.
        /// </summary>
        IWebBrowser CurrentWebBrowser { get; }

        /// <summary>
        /// Gets the base URL of the application under test.
        /// </summary>
        string BaseUrl { get; }

        string DeploymentDirectory { get; }
        void AddResultFile(string filename);
    }
}
