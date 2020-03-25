using log4net;
using Moons.Common20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moons.Log4net
{
    public class Log4netWrapper : ILogNet
    {
        public Log4netWrapper(ILog logger )
        {
            Logger = logger;
        }

        /// <summary>
        /// 日志对象
        /// </summary>
        public ILog Logger { private get; set; }

        /// <summary>
        /// 是否关闭日志输出
        /// </summary>
        public bool IsTurnOff { get; set; } = false;

        public bool IsDebug => !IsTurnOff && Logger.IsDebugEnabled;

        public bool IsError => !IsTurnOff && Logger.IsErrorEnabled;

        public bool IsFatal => !IsTurnOff && Logger.IsFatalEnabled;

        public bool IsInfo => !IsTurnOff && Logger.IsInfoEnabled;

        public bool IsWarn => !IsTurnOff && Logger.IsWarnEnabled;

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
            Logger.Fatal(message,ex);
        }

        public void Info(string message)
        {
            if (IsTurnOff) return;
            Logger.Info(message);
        }

        public void Warn(string message)
        {
            if (IsTurnOff) return;
            Logger.Warn(message);
        }
    }
}
