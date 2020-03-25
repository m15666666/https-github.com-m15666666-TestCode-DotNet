using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20
{
    /// <summary>
    /// ILogNet的包装器类
    /// </summary>
    public class LogNetWrapper : ILogNet
    {
        #region 变量和属性

        /// <summary>
        /// 日志对象
        /// </summary>
        public ILogNet Logger { private get; set; }

        /// <summary>
        /// 是否关闭日志输出
        /// </summary>
        public bool IsTurnOff { get; set; } = false;

        public bool IsDebug => !IsTurnOff && Logger.IsDebug;

        public bool IsError => !IsTurnOff && Logger.IsError;

        public bool IsFatal => !IsTurnOff && Logger.IsFatal;

        public bool IsInfo => !IsTurnOff && Logger.IsInfo;

        public bool IsWarn => !IsTurnOff && Logger.IsWarn;

        #endregion

        #region 输出日志

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            if (IsTurnOff) return;
            ILogNet log = Logger;
            if (log != null)
            {
                log.Debug(message);
            }
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            if (IsTurnOff) return;
            ILogNet log = Logger;
            if (log != null)
            {
                log.Error(message);
            }
        }

        public void Error(string message, Exception ex)
        {
            if (IsTurnOff) return;
            ILogNet log = Logger;
            if (log != null)
            {
                log.Error(message, ex);
            }
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string message)
        {
            if (IsTurnOff) return;
            ILogNet log = Logger;
            if (log != null)
            {
                log.Fatal(message);
            }
        }

        public void Fatal(string message, Exception ex)
        {
            if (IsTurnOff) return;
            ILogNet log = Logger;
            if (log != null)
            {
                log.Fatal(message, ex);
            }
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            if (IsTurnOff) return;
            ILogNet log = Logger;
            if (log != null)
            {
                log.Info(message);
            }
        }

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            if (IsTurnOff) return;
            ILogNet log = Logger;
            if (log != null)
            {
                log.Warn(message);
            }
        }

        #endregion
    }
}
