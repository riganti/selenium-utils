using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Riganti.Selenium.Coordinator.Service.Hubs;

namespace Riganti.Selenium.Coordinator.Service.Services
{
    public class LogHubProvider : ILoggerProvider
    {
        private readonly Func<IServiceProvider> serviceProviderAccessor;


        public LogLevel Level { get; set; } = LogLevel.Information;

        public LogHubProvider(Func<IServiceProvider> serviceProviderAccessor)
        {
            this.serviceProviderAccessor = serviceProviderAccessor;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (!categoryName.StartsWith(typeof(Startup).Namespace))
            {
                return NullLogger.Instance;
            }

            return new LogHubLogger(categoryName, Level, serviceProviderAccessor().GetService<IHubContext<LogHub>>());
        }

        public void Dispose()
        {
        }
    }
}