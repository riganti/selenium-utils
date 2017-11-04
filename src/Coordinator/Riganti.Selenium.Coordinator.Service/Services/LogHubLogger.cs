using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Riganti.Selenium.Coordinator.Service.Hubs;

namespace Riganti.Selenium.Coordinator.Service.Services
{
    public class LogHubLogger : ILogger
    {
        private readonly string categoryName;
        private readonly LogLevel minLogLevel;
        private readonly IHubContext<LogHub> hubContext;

        public LogHubLogger(string categoryName, LogLevel minLogLevel, IHubContext<LogHub> hubContext)
        {
            this.categoryName = categoryName;
            this.minLogLevel = minLogLevel;
            this.hubContext = hubContext;
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= minLogLevel;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            LogHub.AddMessage(hubContext, message, logLevel == LogLevel.Error || logLevel == LogLevel.Critical);
        }

        private string DefaultFormatter<TState>(TState state, Exception exception)
        {
            var result = "";

            if (state != null)
            {
                result += state.ToString();
            }
            if (exception != null)
            {
                result += Environment.NewLine + exception + Environment.NewLine;
            }

            return result;
        }


        public IDisposable BeginScope<TState>(TState state)
        {
            Log(LogLevel.Debug, new EventId(-1), $"Operation ({state}) stated.", null, DefaultFormatter);
            return new LogScope<TState>(state, () =>
            {
                Log(LogLevel.Debug, new EventId(-1), $"Operation ({state}) finished.", null, DefaultFormatter);
            });
        }



        class LogScope<TState> : IDisposable
        {
            private readonly Action disposeAction;

            public LogScope(TState state, Action disposeAction = null)
            {
                this.disposeAction = disposeAction;
            }

            public void Dispose()
            {
                disposeAction?.Invoke();
            }
        }
    }
}