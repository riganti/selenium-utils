using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Configuration;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ITestInstance
    {
        ISeleniumTest TestClass { get; }
        TestConfiguration TestConfiguration { get; }
    }
}