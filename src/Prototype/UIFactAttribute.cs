using System;
using OpenQA.Selenium;
using Xunit;

namespace Riganti.Selenium.Prototype;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class UIFactAttribute : FactAttribute
{
    public UIFactAttribute()
    {
    }
    
    public DriverOptions? DriverOptionsOverride { get; set; }
}
