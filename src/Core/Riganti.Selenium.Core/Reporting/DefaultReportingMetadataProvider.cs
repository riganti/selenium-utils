using System;

using System.Reflection;

namespace Riganti.Selenium.Core.Reporting
{
    public class DefaultReportingMetadataProvider : IReportingMetadataProvider
    {
        private readonly ITestContextProvider testContextProvider;
        private static DateTime Date = DateTime.Now;

        public DefaultReportingMetadataProvider(ITestContextProvider testContextProvider)
        {
            this.testContextProvider = testContextProvider;
        }

        public string GetBuildNumber()
        {
            return $"{Date:yyyyMMdd}-{Date:hhmmss}";
        }

        public string GetProjectName()
        {
            try
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public string GetTestSuiteName()
        {
            return Type.GetType(testContextProvider.GetGlobalScopeTestContext().FullyQualifiedTestClassName)?.Name ?? "Test Suite";
        }
    }
}