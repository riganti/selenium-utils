using System;
using System.Linq;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Exceptions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers
{

    /// <summary>
    /// TODO : Rename to "CurrentUrlValidator"
    /// </summary>
    public class UrlValidator : ICheck<IBrowserWrapper>
    {
        private readonly string failureMessage;
        private readonly Expression<Func<string, bool>> expression;

        public UrlValidator(Expression<Func<string, bool>> expression, string failureMessage = null)
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

    /// <summary>
    /// TODO: Move to separate file. Check whether all validators that validate URL are not duplicated
    /// Ladislav Schumacher
    /// </summary>
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
        /// <param name="currentUrl">Url of currently loaded page.</param>
        /// <param name="urlToCompare">This url is compared with CurrentUrl.</param>
        /// <param name="urlToCompareKind">Determine whether url parameter contains relative or absolute path.</param>
        /// <param name="uriComponents">Determine what parts of urls are compared.</param>
        public bool CompareUrl(string currentUrl, string urlToCompare, UrlKind urlToCompareKind, params UriComponents[] uriComponents)
        {
            var currentUri = new Uri(currentUrl);
            //support relative domain
            //(new Uri() cannot parse the url correctly when the host is missing
            if (urlToCompareKind == UrlKind.Relative)
            {
                urlToCompare = urlToCompare.StartsWith("/") ? $"{currentUri.Scheme}://{currentUri.Host}{urlToCompare}" : $"{currentUri.Scheme}://{currentUri.Host}/{urlToCompare}";
            }

            if (urlToCompareKind == UrlKind.Absolute && urlToCompare.StartsWith("//"))
            {
                if (!string.IsNullOrWhiteSpace(currentUri.Scheme))
                {
                    urlToCompare = currentUri.Scheme + ":" + urlToCompare;
                }
            }

            var expectedUri = new Uri(urlToCompare, UriKind.Absolute);

            if (uriComponents.Length == 0)
            {
                throw new BrowserLocationException($"Function CheckUrlCheckUrl(string, UriKind, params UriComponents) has to have one UriComponents at least.");
            }
            UriComponents finalComponent = uriComponents[0];
            uriComponents.ToList().ForEach(s => finalComponent |= s);

            return Uri.Compare(currentUri, expectedUri, finalComponent, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0;
        }



    }
}