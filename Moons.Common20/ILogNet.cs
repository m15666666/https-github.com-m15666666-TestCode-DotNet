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
        bool IsDebug { get;  }

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);

        bool IsError { get; }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        void Error(string message, Exception ex);

        bool IsFatal { get; }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);

        void Fatal(string message, Exception ex);

        bool IsInfo { get; }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        bool IsWarn { get; }

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
    }
}
