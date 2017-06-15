using System;
using System.Collections;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestContext
    {
        /// <summary>When overridden in a derived class, gets the test properties.</summary>
        /// <returns>An <see cref="T:System.Collections.IDictionary" /> object that contains key/value pairs that represent the test properties.</returns>
        IDictionary Properties { get; }

        /// <summary>Gets the top-level directory for the test run that contains deployed files and result files.</summary>
        /// <returns>The top-level directory for the test run that contains deployed files and result files.</returns>
        /// <exception cref="T:System.InvalidCastException">An invalid value type is associated with the <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestDir" />  property of the test context.</exception>
        string TestRunDirectory { get; }

        /// <summary>Gets the directory for files deployed for the test run. This property typically contains a subdirectory of <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestRunDirectory" />.</summary>
        /// <returns>Returns the directory for files deployed for the test run.</returns>
        /// <exception cref="T:System.InvalidCastException">An invalid value type is associated with the <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestDeploymentDir" /> property of the test context.</exception>
        string DeploymentDirectory { get; }

        /// <summary>Gets the top-level directory that contains test results and test result directories for the test run. This is typically a subdirectory of <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestRunDirectory" />.</summary>
        /// <returns>The top-level directory that contains test results and test result directories for the test run.</returns>
        string ResultsDirectory { get; }

        /// <summary>Gets the top-level directory for the test run result files. This property typically contains a subdirectory of <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.ResultsDirectory" />.</summary>
        /// <returns>The top-level directory for the test run result files.</returns>
        /// <exception cref="T:System.InvalidCastException">An invalid value type is associated with the <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestLogsDir" />   property of the test context.</exception>
        string TestRunResultsDirectory { get; }

        /// <summary>Gets the directory for the test result files.</summary>
        /// <returns>The directory for the test result files.</returns>
        string TestResultsDirectory { get; }

        /// <summary>Gets the path to the test directory. Deprecated. Use <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestRunDirectory" /> instead.</summary>
        /// <returns>The path to the test directory.</returns>
        /// <exception cref="T:System.InvalidCastException">An invalid value type is associated with the <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestDir" />  property of the test context.</exception>
        string TestDir { get; }

        /// <summary>Gets the path to the test deployment directory. Deprecated. Use <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.DeploymentDirectory" /> instead.</summary>
        /// <returns>The path to the test deployment directory.</returns>
        /// <exception cref="T:System.InvalidCastException">An invalid value type is associated with the <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestDeploymentDir" /> property of the test context.</exception>
        string TestDeploymentDir { get; }

        /// <summary>Gets the path to the test log directory. Deprecated. Use <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestRunResultsDirectory" /> instead.</summary>
        /// <returns>The path to the test log directory.</returns>
        /// <exception cref="T:System.InvalidCastException">An invalid value type is associated with the <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestLogsDir" />   property of the test context.</exception>
        string TestLogsDir { get; }

        /// <summary>Gets the fully-qualified name of the class that contains the test method that is currently running.</summary>
        /// <returns>The fully-qualified name of the class that contains the test method that is currently running.</returns>
        string FullyQualifiedTestClassName { get; }

        /// <summary>Gets the test name.</summary>
        /// <returns>The test name.</returns>
        /// <exception cref="T:System.InvalidCastException">An invalid value type is associated with the <see cref="P:Microsoft.VisualStudio.TestTools.UnitTesting.TestContext.TestName" />    property of the test context.</exception>
        string TestName { get; }

        /// <summary>You can use this property in a TestCleanup method to determine the outcome of a test that has run. </summary>
        /// <returns>A <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.UnitTestOutcome" /> that states the outcome of a test that has run.</returns>
        UnitTestResult CurrentTestResult { get; }

        /// <summary>When overridden in a derived class, used to write trace messages while the test is running.</summary>
        /// <param name="format">The string that contains the trace message.</param>
        /// <param name="args">Arguments to add to the trace message.</param>
        void WriteLine(string format, params object[] args);

        /// <summary>When overridden in a derived class, adds a file name to the list in TestResult.ResultFileNames.</summary>
        /// <param name="fileName">The file name to add.</param>
        void AddResultFile(string fileName);

    }

 
    public enum UnitTestResult
    {
        Failed,
        Inconclusive,
        Passed,
        InProgress,
        Error,
        Timeout,
        Aborted,
        Unknown,
    }
}