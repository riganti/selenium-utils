using System;

namespace Riganti.Selenium.Core.Factories
{
    public static class WebBrowserFactoryExtensions 
    {

        public static bool GetBooleanOption(this IWebBrowserFactory factory, string optionName, bool defaultValue = false)
        {
            if (factory.Options.TryGetValue(optionName, out var value))
            {
                return Convert.ToBoolean(value);
            }
            else
            {
                return defaultValue;
            }
        }

        public static string GetStringOption(this IWebBrowserFactory factory, string optionName, string defaultValue = "")
        {
            if (factory.Options.TryGetValue(optionName, out var value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public static int GetInt32Option(this IWebBrowserFactory factory, string optionName, int defaultValue = 0)
        {
            if (factory.Options.TryGetValue(optionName, out var value))
            {
                return Convert.ToInt32(value);
            }
            else
            {
                return defaultValue;
            }
        }

    }
}
