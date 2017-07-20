using System;
using System.Linq.Expressions;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.BrowserWrapperCheckers
{
    public class CheckIfUrl : ICheck<BrowserWrapper>
    {
        private readonly string failureMessage;
        private readonly Expression<Func<string, bool>> expression;

        public CheckIfUrl(Expression<Func<string, bool>> expression, string failureMessage = null)
        {
            this.expression = expression;
            this.failureMessage = failureMessage;
        }

        public CheckResult Validate(BrowserWrapper wrapper)
        {
            var isSucceeded = expression.Compile()(wrapper.CurrentUrl);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Current url is not expected. Current url: '{wrapper.CurrentUrl}'. " + (failureMessage ?? ""));
        }
    }
    public class CheckUrl : ICheck<BrowserWrapper>
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

        public CheckResult Validate(BrowserWrapper wrapper)
        {
            var isSucceeded = wrapper.CompareUrl(url, urlKind, components);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Current url is not expected. Current url: '{wrapper.CurrentUrl}'. Expected url: '{url}'");
        }
    }
}