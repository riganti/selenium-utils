namespace Riganti.Selenium.Core
{
    public interface IReportingMetadataProvider
    {
        string GetBuildNumber();

        string GetProjectName();

        string GetTestSuiteName();
    }
}