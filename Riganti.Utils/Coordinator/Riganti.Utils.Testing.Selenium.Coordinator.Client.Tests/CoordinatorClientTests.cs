using System;
using System.Net.Http;
using System.Threading;
using Riganti.Utils.Testing.Selenium.Core.Abstractions.Attributes;
using Xunit;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Client.Tests
{
    public class CoordinatorClientTests
    {
        [Fact]
        public void SimpleClientTest()
        {
            var client = new CoordinatorClient("http://localhost:62242/");

            // test lease acquired
            var lease = client.AcquireLease("chrome").Result;
            Thread.Sleep(2000);

            // test lease renewed
            var lease2 = client.RenewLease(lease.LeaseId).Result;
            Assert.True(lease.ExpirationDateUtc < lease2.ExpirationDateUtc);
            Thread.Sleep(2000);

            // test that the container is running
            var httpClient = new HttpClient();
            httpClient.GetAsync(lease.Url).Wait();

            // test lease drop
            client.DropLease(lease.LeaseId).Wait();
            Thread.Sleep(2000);
            Assert.ThrowsAsync<Exception>(() => httpClient.GetAsync(lease.Url)).Wait();
        }
    }


}
