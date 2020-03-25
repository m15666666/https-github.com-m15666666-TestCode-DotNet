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
        private readonly string _name;
        private readonly ILogNet _logNet;

        public Logger(string name, ILogNet logNet = null)
        {
            _name = name;
            _logNet = logNet ?? EnvironmentUtils.Logger;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _logNet.Debug(this._name + formatter(state, exception));
                    break;

                case LogLevel.Debug:
                    _logNet.Debug(this._name + formatter(state, exception));
                    break;

                case LogLevel.Information:
                    _logNet.Info(this._name + formatter(state, exception));
                    break;

                case LogLevel.Warning:
                    _logNet.Warn(this._name + formatter(state, exception));
                    break;

                case LogLevel.Error:
                    _logNet.Error(this._name + formatter(state, exception), exception);
                    break;

                case LogLevel.Critical:
                    _logNet.Fatal(this._name + formatter(state, exception), exception);
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
                    return _logNet.IsDebug;
                case LogLevel.Debug:
                    return _logNet.IsDebug;
                case LogLevel.Information:
                    return _logNet.IsInfo;
                case LogLevel.Warning:
                    return _logNet.IsWarn;
                case LogLevel.Error:
                    return _logNet.IsError;
                case LogLevel.Critical:
                    return _logNet.IsFatal;
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
