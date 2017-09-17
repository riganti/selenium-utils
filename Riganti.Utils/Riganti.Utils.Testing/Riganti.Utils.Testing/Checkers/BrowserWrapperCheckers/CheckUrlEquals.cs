using System;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.BrowserWrapperCheckers
{
    public class CheckUrlEquals : ICheck<BrowserWrapper>
    {
        private readonly string url;

        public CheckUrlEquals(string url)
        {
            this.url = url;
        }

        public CheckResult Validate(BrowserWrapper wrapper)
        {
            var uri1 = new Uri(wrapper.CurrentUrl, UriKind.Absolute);
            var uri2 = new Uri(url, UriKind.RelativeOrAbsolute);

            var isSucceeded = uri1 == uri2;

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Current url is not expected. Current url: '{wrapper.CurrentUrl}', Expected url: '{url}'.");
        }
    }
}