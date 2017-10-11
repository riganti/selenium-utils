using System;
using System.Net;
using Riganti.Utils.Testing.Selenium.Core.Abstractions;

namespace Riganti.Utils.Testing.Selenium.Validators.Checkers.BrowserWrapperCheckers
{
    /// <summary>
    /// TODO: Check if this functionality is available for elements. 
    /// Ladislav Schumacher
    /// </summary>
    public class UrlIsAccessibleValidator : ICheck<IBrowserWrapper>
    {
        private readonly string url;
        private readonly UrlKind urlKind;

        public UrlIsAccessibleValidator(string url, UrlKind urlKind)
        {
            this.url = url;
            this.urlKind = urlKind;
        }

        public CheckResult Validate(IBrowserWrapper wrapper)
        {
            var currentUri = new Uri(wrapper.CurrentUrl);
            var tempUrl = url;

            if (urlKind == UrlKind.Relative)
            {
                tempUrl = wrapper.GetAbsoluteUrl(tempUrl);
            }

            if (urlKind == UrlKind.Absolute && tempUrl.StartsWith("//"))
            {
                if (!string.IsNullOrWhiteSpace(currentUri.Scheme))
                {
                    tempUrl = currentUri.Scheme + ":" + tempUrl;
                }
            }

            HttpWebResponse response = null;
            //($"UrlIsAccessibleValidator: Checking of url: '{tempUrl}'", 10);
            var request = (HttpWebRequest)WebRequest.Create((string) tempUrl);
            request.Method = "HEAD";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                return new CheckResult($"Unable to access {tempUrl}! {e.Status}");
            }
            finally
            {
                response?.Close();
            }

            return CheckResult.Succeeded;
        }
    }
}