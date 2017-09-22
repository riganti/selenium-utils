using System;
using System.Linq;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    public class CheckIfUrl : ICheck<IBrowserWrapper>
    {
        private readonly string failureMessage;
        private readonly Expression<Func<string, bool>> expression;

        public CheckIfUrl(Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            this.expression = expression;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var isSucceeded = expression.Compile()(wrapper.CurrentUrl);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Current url is not expected. Current url: '{wrapper.CurrentUrl}'. " + (failureMessage ?? ""));
        }
    }
    public class CheckUrl : ICheck<IBrowserWrapper>
    {
        private readonly string url;
        private readonly UrlKind urlKind;
        private readonly UriComponents[] components;

        public CheckUrl(string url, UrlKind urlKind, params UriComponents[] components)
        {
            this.url = url;
            this.urlKind = urlKind;
            this.components = components;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var isSucceeded = CompareUrl(wrapper.CurrentUrl, url, urlKind, components);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Current url is not expected. Current url: '{wrapper.CurrentUrl}'. Expected url: '{url}'");
        }

        /// <summary>
        /// Compates current Url and given url.
        /// </summary>
        /// <param name="url">This url is compared with CurrentUrl.</param>
        /// <param name="urlKind">Determine whether url parameter contains relative or absolute path.</param>
        /// <param name="components">Determine what parts of urls are compared.</param>
        public bool CompareUrl(string currentUrl, string url, UrlKind urlKind, params UriComponents[] components)
        {
            var currentUri = new Uri(currentUrl);
            //support relative domain
            //(new Uri() cannot parse the url correctly when the host is missing
            if (urlKind == UrlKind.Relative)
            {
                url = url.StartsWith("/") ? $"{currentUri.Scheme}://{currentUri.Host}{url}" : $"{currentUri.Scheme}://{currentUri.Host}/{url}";
            }

            if (urlKind == UrlKind.Absolute && url.StartsWith("//"))
            {
                if (!string.IsNullOrWhiteSpace(currentUri.Scheme))
                {
                    url = currentUri.Scheme + ":" + url;
                }
            }

            var expectedUri = new Uri(url, UriKind.Absolute);

            if (components.Length == 0)
            {
                throw new BrowserLocationException($"Function CheckUrlCheckUrl(string, UriKind, params UriComponents) has to have one UriComponents at least.");
            }
            UriComponents finalComponent = components[0];
            components.ToList().ForEach(s => finalComponent |= s);

            return Uri.Compare(currentUri, expectedUri, finalComponent, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0;
        }



    }
}