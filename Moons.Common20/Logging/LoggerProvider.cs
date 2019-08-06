using Microsoft.Extensions.Logging;
using Moons.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.Logging
{
    /// <summary>
    /// 使用 TraceUtils 实现 Microsoft.Extensions.Logging.ILoggerProvider
    /// </summary>
    public class LoggerProvider : ILoggerProvider
    {
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName) => new Logger(categoryName);
    }
}
