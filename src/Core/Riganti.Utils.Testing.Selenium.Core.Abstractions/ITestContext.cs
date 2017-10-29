namespace Riganti.Utils.Testing.Selenium.Core.Abstractions
{
    public interface ITestContext
    {   /// <summary>
        /// Writes trace messages while the test is running.
        /// </summary>
        /// <param name="format">The string that contains the trace message.</param>
        /// <param name="args">Arguments to add to the trace message.</param>
        void WriteLine(string format, params object[] args);
        string DeploymentDirectory { get; }
        void AddResultFile(string filename);
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



    }
}
