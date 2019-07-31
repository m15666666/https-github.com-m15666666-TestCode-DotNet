using System;

namespace SocketLib
{
    /// <summary>
    /// 字节包结构异常
    /// </summary>
    public class PackageStructException : Exception
    {
        public PackageStructException( string message ) : base( message )
        {
        }

        public PackageStructException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        #region 变量和属性

        /// <summary>
        /// 没有错误
        /// </summary>
        private PackageStructError _structError = PackageStructError.None;

        /// <summary>
        /// 字节包结构错误码
        /// </summary>
        public PackageStructError StructError
        {
            get { return _structError; }
            set { _structError = value; }
        }

        #endregion
    }

    /// <summary>
    /// 字节包结构错误码
    /// </summary>
    public enum PackageStructError
    {
        /// <summary>
        /// 没有错误
        /// </summary>
        None = 0xA5,

        /// <summary>
        /// 包头分界符错误
        /// </summary>
        PackageHead = 0x00,

        /// <summary>
        /// 包头版本号错误
        /// </summary>
        PackageHeadVersion = 0x07,

        /// <summary>
        /// 包头crc错误
        /// </summary>
        PackageHeadCrc = 0x02,

        /// <summary>
        /// 包尾crc错误
        /// </summary>
        PackageTailCrc = 0x03,

        /// <summary>
        /// 包尾分界符错误
        /// </summary>
        PackageTail = 0x01,
    }
}