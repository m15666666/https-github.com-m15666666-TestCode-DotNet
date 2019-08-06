using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moons.Common20;

namespace Moons.Logging
{
    /// <summary>
    /// 使用 TraceUtils 实现 Microsoft.Extensions.Logging.ILogger
    /// </summary>
    public class Logger : ILogger
    {
        private readonly string name;

        public Logger(string name)
        {
            this.name = name;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:

                    TraceUtils.Debug(this.name + formatter(state, exception), exception);
                    break;
                case LogLevel.Debug:
                    TraceUtils.Debug(this.name + formatter(state, exception), exception);
                    break;
                case LogLevel.Information:
                    TraceUtils.Info(this.name + formatter(state, exception), exception);
                    break;
                case LogLevel.Warning:
                    TraceUtils.Warn(this.name + formatter(state, exception), exception);
                    break;
                case LogLevel.Error:
                    TraceUtils.Error(this.name + formatter(state, exception), exception);
                    break;
                case LogLevel.Critical:
                    TraceUtils.Fatal(this.name + formatter(state, exception), exception);
                    break;
                case LogLevel.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return TraceUtils.Logger.IsDebug;
                case LogLevel.Debug:
                    return TraceUtils.Logger.IsDebug;
                case LogLevel.Information:
                    return TraceUtils.Logger.IsInfo;
                case LogLevel.Warning:
                    return TraceUtils.Logger.IsWarn;
                case LogLevel.Error:
                    return TraceUtils.Logger.IsError;
                case LogLevel.Critical:
                    return TraceUtils.Logger.IsFatal;
                case LogLevel.None:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => NoOpDisposable.Instance;

        sealed class NoOpDisposable : IDisposable
        {
            public static readonly NoOpDisposable Instance = new NoOpDisposable();

            NoOpDisposable()
            {
            }

            public void Dispose()
            {
            }
        }
    }
}
