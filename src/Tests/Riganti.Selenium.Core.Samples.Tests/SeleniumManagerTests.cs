using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;

[TestClass]
public class SeleniumManagerTests
{
    [TestMethod]
    public void GetBinariesVersion113()
    {
        var data1 = SeleniumManager.BinaryPaths("--browser firefox --driver geckodriver --browser-version 113");
        Console.WriteLine("Firefox binaries: " + JsonConvert.SerializeObject(data1));
        Assert.IsNotNull(data1["browser_path"]);
        Assert.IsNotNull(data1["driver_path"]);

        var data2 = SeleniumManager.BinaryPaths("--browser chrome --driver chromedriver --browser-version 113");
        Console.WriteLine("Chrome binaries: " + JsonConvert.SerializeObject(data2));
        Assert.IsNotNull(data2["browser_path"]);
        Assert.IsNotNull(data2["driver_path"]);
    }

    [TestMethod]
    public void GetBinariesVersionStable()
    {
        var data1 = SeleniumManager.BinaryPaths("--browser firefox --driver geckodriver --browser-version stable");
        Console.WriteLine("Firefox binaries: " + JsonConvert.SerializeObject(data1));
        Assert.IsNotNull(data1["browser_path"]);
        Assert.IsNotNull(data1["driver_path"]);

        var data2 = SeleniumManager.BinaryPaths("--browser chrome --driver chromedriver --browser-version stable");
        Console.WriteLine("Chrome binaries: " + JsonConvert.SerializeObject(data2));
        Assert.IsNotNull(data2["browser_path"]);
        Assert.IsNotNull(data2["driver_path"]);
    }
}