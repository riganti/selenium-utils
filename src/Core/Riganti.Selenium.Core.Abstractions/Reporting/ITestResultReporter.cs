using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Riganti.Selenium.Core.Abstractions.Reporting
{
    public interface ITestResultReporter
    {
        Task<TestRunInputResult> ReportTestResult(TestRunInputData data);

        IDictionary<string, string> Options { get; }
        string Name { get; }
        string ReportTestResultUrl { get; set; }
    }
}