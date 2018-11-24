using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Riganti.Selenium.Core.Abstractions.Reporting;

namespace Riganti.Selenium.Core.Reporting
{
    /// <summary>
    /// Reporter for Azure DevOps test runner
    /// </summary>
    public class AzureDevOpsReporter : ITestResultReporter
    {
        public HttpClient Client { get; set; }

        public AzureDevOpsReporter()
        {
            Client = new HttpClient();
        }

        public Task<TestRunInputResult> ReportTestResult(TestRunInputData data)
        {
            RewriteMetadata(data);
            return SendReport(data);
        }

        protected virtual async Task<TestRunInputResult> SendReport(TestRunInputData data)
        {
            var response = await Client.PostAsync(new Uri(ReportTestResultUrl), new StringContent(JsonConvert.SerializeObject(data)));
            return JsonConvert.DeserializeObject<TestRunInputResult>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Rewrite or add metadata before the result is sent to the reporting app.
        /// </summary>
        /// <param name="data"></param>
        protected virtual void RewriteMetadata(TestRunInputData data)
        {
            // rewrite metadata
            string buildId = Environment.GetEnvironmentVariable("BUILD_BUILDID");
            if (!string.IsNullOrWhiteSpace(buildId))
            {
                data.BuildNumber = buildId;
                data.ProjectName =
                    Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT") + "|" +
                    Environment.GetEnvironmentVariable("BUILD_BUILDNUMBER") + "|" +
                    Environment.GetEnvironmentVariable("BUILD_DEFINITIONNAME");
            }
        }

        public IDictionary<string, string> Options { get; } = new Dictionary<string, string>();

        /// <inheritdoc />
        public string Name { get; } = "azureDevOps";

        /// <inheritdoc />
        public string ReportTestResultUrl { get; set; }
    }
}