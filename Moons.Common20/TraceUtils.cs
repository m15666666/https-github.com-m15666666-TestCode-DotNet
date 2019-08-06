using System;

namespace Moons.Common20
{
    /// <summary>
    ///     跟踪代码执行情况的实用工具类
    /// </summary>
    public static class TraceUtils
    {
        #region 变量和属性

        /// <summary>
        ///     日志对象
        /// </summary>
        public static ILogNet Logger
        {
            get { return EnvironmentUtils.Logger; }
        }

        #endregion

        #region 普通输出

        /// <summary>
        ///     将字节数组使用十六进制输出
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public static void WriteHex( byte[] bytes )
        {
            if( bytes != null && 0 < bytes.Length )
            {
                Info( StringUtils.ToHex( bytes ) );
            }
        }

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void LogDebugInfo( string message )
        {
            if( EnvironmentUtils.IsDebug )
            {
                Info( message );
            }
        }

        #endregion

        #region 输出日志

        /// <summary>
        ///     Debug
        /// </summary>
        /// <param name="message"></param>
        public static void Debug( string message )
        {
            Logger.Debug( message );
        }

        /// <summary>
        ///     Debug
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message, Exception ex)
        {
            Logger.Debug(message);
        }

        /// <summary>
        ///     Error
        /// </summary>
        /// <param name="message"></param>
        public static void Error( string message )
        {
            Logger.Error( message );
        }

        /// <summary>
        ///     Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Error( string message, Exception ex )
        {
            Logger.Error( message, ex );
        }

        /// <summary>
        ///     Fatal
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal( string message )
        {
            Logger.Fatal( message );
        }

        /// <summary>
        ///     Fatal
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Fatal( string message, Exception ex )
        {
            Logger.Fatal( message, ex );
        }

        /// <summary>
        ///     Info
        /// </summary>
        /// <param name="message"></param>
        public static void Info( string message )
        {
            Logger.Info( message );
        }

        /// <summary>
        ///     Info
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message, Exception ex)
        {
            Logger.Info(message);
        }

        /// <summary>
        ///     Warn
        /// </summary>
        /// <param name="message"></param>
        public static void Warn( string message )
        {
            Logger.Warn( message );
        }

        /// <summary>
        ///     Warn
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message, Exception ex)
        {
            Logger.Warn(message);
        }

        #endregion
    }
}