using System;
using Riganti.Utils.Testing.Selenium.Core.Api;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.BrowserWrapperCheckers
{
    public class CheckIfHyperLinkEquals : ICheck<BrowserWrapper>
    {
        private readonly string selector;
        private readonly string url;
        private readonly UrlKind kind;
        private readonly UriComponents[] components;

        public CheckIfHyperLinkEquals(string selector, string url, UrlKind kind, params UriComponents[] components)
        {
            this.selector = selector;
            this.url = url;
            this.kind = kind;
            this.components = components;
        }

        public CheckResult Validate(BrowserWrapper wrapper)
        {
            wrapper.ForEach(selector, element =>
            {
                AssertUI.CheckIfHyperLinkEquals(element, url, kind, components);
            });
            return CheckResult.Succeeded;
        }
    }
}