using System;

namespace SocketLib
{
    /// <summary>
    /// 通过Socket发命令的错误信息数据类
    /// </summary>
    [Serializable]
    public class ErrorData
    {
        /// <summary>
        /// 错误信息，为null或空字符串表示没有错误
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 详细错误信息
        /// </summary>
        public string ErrorDetail { get; set; }

        /// <summary>
        /// 检查是否出错
        /// </summary>
        public bool IsError
        {
            get { return ErrorUtils.IsError( ErrorMsg ); }
        }

        /// <summary>
        /// 检查是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return ErrorUtils.IsSuccess( ErrorMsg ); }
        }

        /// <summary>
        /// 设置异常
        /// </summary>
        /// <param name="ex">Exception</param>
        public void SetException( Exception ex )
        {
            ErrorMsg = ErrorUtils.GetExceptionMessage( ex );
            ErrorDetail = ErrorUtils.GetExceptionDetail( ex );
        }

        /// <summary>
        /// 转化为异常对象
        /// </summary>
        /// <returns>异常对象</returns>
        public Exception ToException()
        {
            return new Exception( ErrorMsg + Environment.NewLine + ErrorDetail );
        }
    }
}