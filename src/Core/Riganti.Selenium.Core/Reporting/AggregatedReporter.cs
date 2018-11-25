using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Selenium.Core.Abstractions.Exceptions;
using Riganti.Selenium.Core.Abstractions.Reporting;

namespace Riganti.Selenium.Core.Reporting
{
    internal class AggregatedReporter
    {
        private readonly IDictionary<string, ITestResultReporter> reporters;
        private readonly IReportingMetadataProvider reportingMetadataProvider;
        private static ConcurrentQueue<TestRunInputData> Queue { get; }

        static AggregatedReporter()
        {
            Queue = new ConcurrentQueue<TestRunInputData>();
        }

        public AggregatedReporter(IDictionary<string, ITestResultReporter> reporters,
                IReportingMetadataProvider reportingMetadataProvider)
        {
            this.reporters = reporters;
            this.reportingMetadataProvider = reportingMetadataProvider;
            Tasks = new List<Task>() {
                new Task(async () => await SendResult()),
                new Task(async () => await SendResult())
            };
        }

        public List<Task> Tasks { get; set; }

        public void ReportFailedTest(Exception exception, string testName, string callerFilePath, int callerLineNumber)
        {
            if (exception == null) return;

            var sExecption = exception as SeleniumTestFailedException;

            var unwrappedTestExceptions = sExecption?.InnerExceptions.OfType<TestExceptionBase>().ToList();
            List<TestRunAttachmentInputData> attachments = GetAttachments(unwrappedTestExceptions);

            var data = new TestRunInputData()
            {
                TestResult = 0,
                Attachments = attachments,
                TestFullName = testName,
                TestOutput = exception.ToString(),
                BuildNumber = reportingMetadataProvider.GetBuildNumber(),
                ProjectName = reportingMetadataProvider.GetProjectName(),
                TestSuiteName = reportingMetadataProvider.GetTestSuiteName(),
            };

            EnqueueResult(data);
        }

        private void EnqueueResult(TestRunInputData data)
        {
            Queue.Enqueue(data);
        }

        public void ReportSuccessfulTest(string testName, string callerFilePath, int callerLineNumber)
        {
            var data = new TestRunInputData()
            {
                TestResult = 1,
                TestFullName = testName,
                BuildNumber = reportingMetadataProvider.GetBuildNumber(),
                ProjectName = reportingMetadataProvider.GetProjectName(),
                TestOutput = "",
                TestSuiteName = reportingMetadataProvider.GetTestSuiteName(),
                Attachments = new List<TestRunAttachmentInputData>()
            };

            EnqueueResult(data);
        }

        public async Task SendResult()
        {
            while (true)
            {
                if (Queue.TryDequeue(out var result) && result != null)
                {
                    var results = await ReportTestResult(result);
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static List<TestRunAttachmentInputData> GetAttachments(List<TestExceptionBase> unwrappedTestExceptions)
        {
            return unwrappedTestExceptions?.Select(s => s.Screenshot).Select(s =>
            {
                try
                {
                    var bytes = File.ReadAllBytes(s);
                    return new TestRunAttachmentInputData() { ContentBase64 = Convert.ToBase64String(bytes), FileName = s };
                }
                catch (Exception e)
                {
                    return null;
                }
            }).Where(s => s != null).ToList();
        }

        private async Task<IList<TestRunInputResult>> ReportTestResult(TestRunInputData data)
        {
            var results = new List<TestRunInputResult>();
            foreach (var testResultReporter in reporters)
            {
                try
                {
                    results.Add(await testResultReporter.Value.ReportTestResult(data));
                }
                catch (Exception e)
                {
                    results.Add(new FailedTestRunInputResult() { Exception = e });
                }
            }
            return results;
        }
    }
}