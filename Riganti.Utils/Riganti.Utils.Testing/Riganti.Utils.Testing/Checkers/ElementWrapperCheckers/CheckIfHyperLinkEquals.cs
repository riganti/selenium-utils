using System;
using System.Linq;
using Riganti.Utils.Testing.Selenium.Core.Checkers;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.ElementWrapperCheckers
{
    public class CheckIfHyperLinkEquals : ICheck<ElementWrapper>
    {
        private readonly string url;

        private readonly UrlKind kind;

        private readonly UriComponents finalComponent;

        public CheckIfHyperLinkEquals(string url, UrlKind kind, params UriComponents[] components)
        {
            this.url = url;
            this.kind = kind;
            if (components.Length == 0)
            {
                components = new UriComponents[1];
                components[0] = kind == UrlKind.Relative ? UriComponents.PathAndQuery : UriComponents.AbsoluteUri;
            }
            UriComponents tempComponent = components[0];
            components.ToList().ForEach(s => tempComponent |= s);
            finalComponent = tempComponent;
        }

        public CheckResult Validate(ElementWrapper wrapper)
        {
            string tempUrl = url;
            var providedHref = new Uri(wrapper.WebElement.GetAttribute("href"));
            if (kind == UrlKind.Relative)
            {
                var host = wrapper.BaseUrl;
                if (string.IsNullOrWhiteSpace(host))
                {
                    host = "http://example.com/";
                }
                else if (!host.EndsWith("/"))
                {
                    host += "/";
                }
                tempUrl = host + (tempUrl.StartsWith("/") ? tempUrl.Substring(1) : tempUrl);
            }
            if (kind == UrlKind.Absolute && tempUrl.StartsWith("//"))
            {
                tempUrl = providedHref.Scheme + ":" + tempUrl;
            }
            var expectedHref = new Uri(tempUrl);
            var isSucceeded = Uri.Compare(providedHref, expectedHref, finalComponent, UriFormat.SafeUnescaped,
                                  StringComparison.OrdinalIgnoreCase) == 0;
            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Link '{wrapper.FullSelector}' provided value '{providedHref}' of attribute href. Provided value does not match with expected value '{url}'.");
        }
    }
}