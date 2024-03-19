using System;
using OpenQA.Selenium;

namespace Riganti.Selenium.Prototype;

public sealed record UITestContext
{
    public UITestContext(

        WebDriver webDriver
    )
    {
        WebDriver = webDriver;
    }

    public WebDriver WebDriver { get; }
};
