using Moons.Common20;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Serilogs
{
    /// <summary>
    /// 使用Serilog实现ILogNet
    /// https://serilog.net/
    /// </summary>
    public class SerilogWrapper : ILogNet
    {
        public SerilogWrapper(Serilog.ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// 日志对象
        /// </summary>
        public Serilog.ILogger Logger { private get; set; }

        /// <summary>
        /// 是否关闭日志输出
        /// </summary>
        public bool IsTurnOff { get; set; } = false;

        public bool IsDebug => !IsTurnOff && Logger.IsEnabled(Serilog.Events.LogEventLevel.Debug);

        public bool IsError => !IsTurnOff && Logger.IsEnabled(Serilog.Events.LogEventLevel.Error);

        public bool IsFatal => !IsTurnOff && Logger.IsEnabled(Serilog.Events.LogEventLevel.Fatal);

        public bool IsInfo => !IsTurnOff && Logger.IsEnabled(Serilog.Events.LogEventLevel.Information);

        public bool IsWarn => !IsTurnOff && Logger.IsEnabled(Serilog.Events.LogEventLevel.Warning);

        public void Debug(string message)
        {
            if (IsTurnOff) return;
            Logger.Debug(message);
        }

        public void Error(string message)
        {
            if (IsTurnOff) return;
            Logger.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            if (IsTurnOff) return;
            Logger.Error(message, ex);
        }

        public void Fatal(string message)
        {
            if (IsTurnOff) return;
            Logger.Fatal(message);
        }

        public void Fatal(string message, Exception ex)
        {
            if (IsTurnOff) return;
            Logger.Fatal(message, ex);
        }

        public void Info(string message)
        {
            if (IsTurnOff) return;
            Logger.Information(message);
        }

        public void Warn(string message)
        {
            if (IsTurnOff) return;
            Logger.Warning(message);
        }
    }
}
