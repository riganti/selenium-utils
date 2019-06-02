using System;
using System.Linq;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Validators.Checkers.ElementWrapperCheckers
{
    public class HyperLinkEqualsValidator : IValidator<IElementWrapper>
    {
        private bool strictQueryStringParamsOrder;
        private bool checkQueryStringParams;
        private readonly string url;

        private readonly UrlKind kind;

        private readonly UriComponents finalComponent;

        public HyperLinkEqualsValidator(string url, UrlKind kind, bool strictQueryStringParamsOrder, params UriComponents[] components)
        {
            this.strictQueryStringParamsOrder = strictQueryStringParamsOrder;
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

            if (this.strictQueryStringParamsOrder == false && (finalComponent & UriComponents.Query) > 0)
            {
                checkQueryStringParams = true;
                finalComponent = finalComponent & ~UriComponents.Query;
            }
            else if (this.strictQueryStringParamsOrder == false && (finalComponent & UriComponents.PathAndQuery) > 0)
            {
                checkQueryStringParams = true;
                finalComponent = finalComponent & ~UriComponents.PathAndQuery;
                finalComponent = finalComponent | UriComponents.Path;
            }

        }

        public CheckResult Validate(IElementWrapper wrapper)
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

            // check query string without exact order
            if (checkQueryStringParams)
            {
                var urlQueryParts = providedHref.Query.Trim('?').Split('&');
                isSucceeded = expectedHref.Query.Trim('?').Split('&').All(s => urlQueryParts.Contains(s));
            }

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Link '{wrapper.FullSelector}' provided value '{providedHref}' of attribute href. Provided value does not match with expected value '{url}'.");
        }
    }
}