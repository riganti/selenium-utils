using System;

namespace Riganti.Selenium.Core.Abstractions.Reporting
{
    /// <summary>
    /// This is special result that indicates that reporting a result to service failed.
    /// </summary>
    public class FailedTestRunInputResult : TestRunInputResult
    {
        public Exception Exception { get; set; }
    }
}