using System;
using Xunit;
using Xunit.Sdk;

namespace Riganti.Selenium.Prototype;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
[XunitTestCaseDiscoverer("Riganti.Selenium.Prototype.UIFactDiscoverer", "Riganti.Selenium.Prototype")]
public class UIFactAttribute : FactAttribute
{
    public UIFactAttribute()
    {
    }
}
