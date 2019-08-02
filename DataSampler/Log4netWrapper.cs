using log4net;
using Moons.Common20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSampler
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

        public bool IsDebug => Logger.IsDebugEnabled;

        public bool IsError => Logger.IsErrorEnabled;

        public bool IsFatal => Logger.IsFatalEnabled;

        public bool IsInfo => Logger.IsInfoEnabled;

        public bool IsWarn => Logger.IsWarnEnabled;

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            Logger.Error(message, ex);
        }

        public void Fatal(string message)
        {
            Logger.Fatal(message);
        }

        public void Fatal(string message, Exception ex)
        {
            Logger.Fatal(message,ex);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }
    }
}
