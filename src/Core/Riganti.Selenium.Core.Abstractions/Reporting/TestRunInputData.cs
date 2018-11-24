using System.Collections.Generic;

namespace Riganti.Selenium.Core.Abstractions.Reporting
{
    public class TestRunInputData
    {
        public string ProjectName { get; set; }

        public string TestSuiteName { get; set; }

        public string BuildNumber { get; set; }

        public string TestFullName { get; set; }

        ///<summary>
        /// Enum serialization doesn't work in latest SDK for Azure Functions
        /// 0 = Failed, 1 = Success
        /// </summary>
        public int TestResult { get; set; }

        public string TestOutput { get; set; }

        public List<TestRunAttachmentInputData> Attachments { get; set; }
    }
}