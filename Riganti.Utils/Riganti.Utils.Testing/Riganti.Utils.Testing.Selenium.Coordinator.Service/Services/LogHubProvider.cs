using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Riganti.Utils.Testing.Selenium.Coordinator.Service.Hubs;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Service.Services
{
    public class LogHubProvider : ILoggerProvider
    {
        private readonly IHubContext<LogHub> hubContext;

        public LogLevel Level { get; set; } = LogLevel.Information;

        public LogHubProvider(IHubContext<LogHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (!categoryName.StartsWith(nameof(Riganti.Utils.Testing.Selenium.Coordinator)))
            {
                return NullLogger.Instance;
            }

            return new LogHubLogger(categoryName, Level, hubContext);
        }

        public void Dispose()
        {
        }
    }
}