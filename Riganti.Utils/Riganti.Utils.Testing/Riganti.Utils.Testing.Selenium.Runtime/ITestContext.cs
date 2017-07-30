using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Runtime.Drivers;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime
{
    public interface ITestContext
    {
        /// <summary>
        /// Gets the path to the deployment directory.
        /// </summary>
        string DeploymentDirectory { get; }

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
        /// Adds a file name to the list in TestResult.ResultFileNames.
        /// </summary>
        /// <param name="fileName">The file name to add.</param>
        void AddResultFile(string fileName);

        /// <summary>
        /// Gets the web browser object used to run the test.
        /// </summary>
        IWebBrowser CurrentWebBrowser { get; }

        /// <summary>
        /// Gets the factory object which created the web browser object.
        /// </summary>
        IWebBrowserFactory CurrentWebBrowserFactory { get; }

        /// <summary>
        /// Gets the base URL of the application under test.
        /// </summary>
        string BaseUrl { get; }

    }
}
