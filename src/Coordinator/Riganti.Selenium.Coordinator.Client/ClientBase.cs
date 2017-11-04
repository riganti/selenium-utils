using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Riganti.Selenium.Coordinator.Client
{
    public abstract class ClientBase
    {
        public string ApiUrl { get; }

        public ClientBase(string apiUrl)
        {
            ApiUrl = apiUrl;
        }


        protected virtual HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ApiUrl)
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        protected async Task Call(string method, string url, object body, IDictionary<string, string> queryString = null)
        {
            await Call<object>(method, url, body, queryString);
        }

        protected async Task<TResponse> Call<TResponse>(string method, string url, object body, IDictionary<string, string> queryString = null)
        {
            using (var client = CreateHttpClient())
            {
                var requestJson = JsonConvert.SerializeObject(new object());
                var requestUrl = ComposeUrl(url, queryString);
                var request = new HttpRequestMessage(new HttpMethod(method), requestUrl)
                {
                    Content = new StringContent(requestJson, Encoding.UTF8)
                };

                var result = await client.SendAsync(request);

                result.EnsureSuccessStatusCode();
                var responseJson = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(responseJson);
            }
        }

        protected virtual string ComposeUrl(string url, IDictionary<string, string> queryString)
        {
            var result = new StringBuilder(url);

            if (queryString != null)
            {
                foreach (var entry in queryString)
                {
                    result.Append(!url.Contains("?") ? "?" : "&");
                    result.Append(WebUtility.UrlEncode(entry.Key));
                    result.Append("=");
                    result.Append(WebUtility.UrlEncode(entry.Value ?? ""));
                }
            }

            return result.ToString();
        }

    }
}