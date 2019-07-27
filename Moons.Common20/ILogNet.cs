using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20
{
    /// <summary>
    /// 日志接口，命名为“ILogNet”，避免与ILog名字冲突
    /// </summary>
    public interface ILogNet
    {
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        void Error(string message, Exception ex);

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);

        void Fatal(string message, Exception ex);

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
    }
}
