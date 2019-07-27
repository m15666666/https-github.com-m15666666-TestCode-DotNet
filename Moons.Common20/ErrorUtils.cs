using System;
using System.Text;

namespace Moons.Common20
{
    /// <summary>
    /// 处理错误的实用工具类
    /// </summary>
    public static class ErrorUtils
    {
        #region 检查是否出错

        /// <summary>
        /// 检查是否出错
        /// </summary>
        /// <param name="emsg">函数调用返回的详细错误信息</param>
        /// <returns>是否出错</returns>
        public static bool IsError( string emsg )
        {
            return !string.IsNullOrEmpty( emsg );
        }

        /// <summary>
        /// 检查是否成功
        /// </summary>
        /// <param name="emsg">函数调用返回的详细错误信息</param>
        /// <returns>是否成功</returns>
        public static bool IsSuccess( string emsg )
        {
            return string.IsNullOrEmpty( emsg );
        }

        #endregion

        #region 导出错误信息

        /// <summary>
        /// 将异常转化为描述异常的字符串
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>描述异常的字符串</returns>
        public static string Exception2String( Exception ex )
        {
            return ex.ToString();
        }

        /// <summary>
        /// 获得异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常信息</returns>
        public static string GetExceptionMessage( Exception ex )
        {
            return ex.Message;
        }

        /// <summary>
        /// 获得异常的详细信息
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>异常的详细信息</returns>
        public static string GetExceptionDetail( Exception ex )
        {
            var output = new StringBuilder();
            DumpException( ex, output );

            return output.ToString();
        }

        /// <summary>
        /// 导出异常
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="output">StringBuilder，用于输出异常信息</param>
        private static void DumpException( Exception ex, StringBuilder output )
        {
            while( ex != null )
            {
                output.AppendLine( ex.Message ).AppendLine( ex.StackTrace );

                ex = ex.InnerException;
            }

            output.AppendLine( "Environment.StackTrace : " + Environment.StackTrace );
        }

        #endregion

        #region 执行函数

        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="handler">函数</param>
        /// <param name="emsg">消息对象</param>
        /// <returns>true：成功，false：不成功</returns>
        public static bool DoHandler( Action20 handler, ref string emsg )
        {
            try
            {
                handler();

                return true;
            }
            catch( Exception ex )
            {
                emsg = Exception2String( ex );
                return false;
            }
        }

        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="handler">函数</param>
        /// <param name="message">消息</param>
        /// <param name="emsg">详细消息对象</param>
        /// <returns>true：成功，false：不成功</returns>
        public static bool DoHandler(Action20 handler, ref string message, ref string emsg) {
            try {
                handler();

                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                emsg = Exception2String(ex);
                return false;
            }
        }

        #endregion
    }
}