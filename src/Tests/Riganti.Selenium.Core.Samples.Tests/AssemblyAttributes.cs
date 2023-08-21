using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Microsoft.VisualStudio.TestTools.UnitTesting.DoNotParallelize]

[TestClass]
public static class AssemblyAttributes
{
    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
        Trace.WriteLine("Trace test");
        var stdout = Console.OpenStandardOutput();
        using var writer = new StreamWriter(stdout);
        writer.WriteLine("Stdout test");
        writer.Flush();
    }
}
