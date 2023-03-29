using Microsoft.Extensions.Logging;
using System;

namespace Sat.Recruitment.Test
{
    public class FakeLogger<T> : ILogger, ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state) => new LoggingScope();

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            // Do nothing
        }
    }

    public class LoggingScope : IDisposable
    {
        public void Dispose()
        {
            // Do nothing
        }
    }
}
