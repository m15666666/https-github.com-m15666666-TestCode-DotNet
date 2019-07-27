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

        #endregion

        #region 输出日志

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
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
            ILogNet log = Logger;
            if (log != null)
            {
                log.Error(message);
            }
        }

        public void Error(string message, Exception ex)
        {
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
            ILogNet log = Logger;
            if (log != null)
            {
                log.Fatal(message);
            }
        }

        public void Fatal(string message, Exception ex)
        {
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
            ILogNet log = Logger;
            if (log != null)
            {
                log.Warn(message);
            }
        }

        #endregion
    }
}
