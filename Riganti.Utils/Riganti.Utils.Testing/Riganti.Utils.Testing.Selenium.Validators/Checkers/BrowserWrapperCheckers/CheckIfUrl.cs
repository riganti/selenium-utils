using System;
using System.Linq.Expressions;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

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
            var isSucceeded = wrapper.CompareUrl(url, urlKind, components);

            return isSucceeded ? CheckResult.Succeeded : new CheckResult($"Current url is not expected. Current url: '{wrapper.CurrentUrl}'. Expected url: '{url}'");
        }
    }
}