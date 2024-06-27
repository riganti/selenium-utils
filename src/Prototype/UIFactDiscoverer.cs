using Xunit.Abstractions;
using Xunit.Sdk;

namespace Riganti.Selenium.Prototype;

public class UIFactDiscoverer : FactDiscoverer
{
    public UIFactDiscoverer(IMessageSink diagnosticMessageSink)
        : base(diagnosticMessageSink)
    {
    }

    protected override IXunitTestCase CreateTestCase(
        ITestFrameworkDiscoveryOptions discoveryOptions,
        ITestMethod testMethod,
        IAttributeInfo factAttribute)
    {
        return new UITestCase
    }
}
