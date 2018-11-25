using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Riganti.Selenium.Core.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SkipBrowserAttribute : Attribute
    {
        public string Reason { get; }
        public string BrowserName { get; }

        public SkipBrowserAttribute(string browserName, string reason)
        {
            Reason = reason;
            BrowserName = browserName;
        }

        public static IEnumerable<SkipBrowserAttribute> TryToRetrieveFromStackTrace()
        {
            return new StackTrace()
                       .GetFrames()
                       .Select(f => f.GetMethod().GetCustomAttributes<SkipBrowserAttribute>())
                       .FirstOrDefault(attr => attr.Any())
                   ?? Enumerable.Empty<SkipBrowserAttribute>();
        }
    }
}