using OpenQA.Selenium;
using System.Linq;
using System.Reflection;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public static class ByExtension
    {
        public static string GetSelector(this By by)
        {
            var description = by.GetType().GetRuntimeProperties().First(s => s.Name == "Description").GetValue(by).ToString();
            if (!description.Contains(":"))
                return description;
            return string.Join("", description.Split(':').Skip(1).ToArray());
        }
    }
}