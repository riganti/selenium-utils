using System;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
namespace Riganti.Selenium.Core.UnitTests;

[TestClass]
public class SeleniumManagerTests
{
    [TestMethod]
    public void GetBinaries()
    {
        var data1 = SeleniumManager.BinaryPaths("--browser firefox --driver geckodriver --browser-version 104");
        Console.WriteLine("Firefox binaries: " + JsonSerializer.Serialize(data1));
        Assert.IsNotNull(data1["browser_path"]);
        Assert.IsNotNull(data1["driver_path"]);

        var data2 = SeleniumManager.BinaryPaths("--browser chrome --driver chromedriver --browser-version 104");
        Console.WriteLine("Chrome binaries: " + JsonSerializer.Serialize(data2));
        Assert.IsNotNull(data2["browser_path"]);
        Assert.IsNotNull(data2["driver_path"]);
    }
}