using System;
using System.Net;

namespace Riganti.Utils.Testing.Selenium.Core.Checkers.BrowserWrapperCheckers
{
    public class CheckIfUrlIsAccessible : ICheck<BrowserWrapper>
    {
        private readonly string url;
        private readonly UrlKind urlKind;

        public CheckIfUrlIsAccessible(string url, UrlKind urlKind)
        {
            this.url = url;
            this.urlKind = urlKind;
        }

        public CheckResult Validate(BrowserWrapper wrapper)
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
            SeleniumTestBase.Log($"CheckIfUrlIsAccessible: Checking of url: '{tempUrl}'", 10);
            var request = (HttpWebRequest)WebRequest.Create(tempUrl);
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