using OpenQA.Selenium;
using System.Linq;
using System.Reflection;

namespace Riganti.Selenium.Core
{
    /// <summary>
    /// Extension methods for class <see cref="By"/>.
    /// </summary>
    public static class ByExtension
    {
        /// <summary>
        /// Tries to get selector type name.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public static string GetSelector(this By by)
        {
            var description = by.GetType().GetRuntimeProperties().First(s => s.Name == "Description").GetValue(by).ToString();
            if (!description.Contains(":"))
                return description;
            return string.Join("", description.Split(':').Skip(1).ToArray());
        }
    }
}