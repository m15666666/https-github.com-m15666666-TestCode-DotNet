using System;
using Moons.Common20;
using Moons.Common20.Pool;

namespace SocketLib
{
    /// <summary>
    /// SocketLib的配置类
    /// </summary>
    public static class SocketLibConfig
    {
        static SocketLibConfig()
        {
            var byteArrayMemoryManager = new ArrayMemoryManager<byte>();
            byteArrayMemoryManager.Init();

            ByteArrayMemoryManager = byteArrayMemoryManager;
        }

        #region 字节数组内存管理器

        /// <summary>
        /// 字节数组内存管理器
        /// </summary>
        public static ArrayMemoryManager<byte> ByteArrayMemoryManager { get; set; }

        /// <summary>
        /// 获得大于等于给定长度的数组，找不到抛出异常
        /// </summary>
        /// <param name="length">给定长度</param>
        /// <returns>大于等于给定长度的数组</returns>
        public static byte[] GetMoreThanLengthByteArray( int length )
        {
            byte[] ret = ByteArrayMemoryManager.GetMoreThanLength( length );
            if( ret == null )
            {
                throw new ArgumentException( string.Format( "GetMoreThanLengthByteArray 请求长度({0})超出范围！", length ) );
            }

            return ret;
        }

        /// <summary>
        /// 返回数组
        /// </summary>
        /// <param name="data">数组</param>
        public static void ReturnByteArray( byte[] data )
        {
            ByteArrayMemoryManager.Return( data );
        }

        #endregion

        #region 调试、诊断

        /// <summary>
        /// 发送字节时调用的函数
        /// </summary>
        public static event ByteBufferHandler<SocketWrapper> SendBytesEvent;

        /// <summary>
        /// 收到字节时调用的函数
        /// </summary>
        public static event ByteBufferHandler<SocketWrapper> ReceiveBytesEvent;

        /// <summary>
        /// 激发发送字节时调用的函数
        /// </summary>
        /// <param name="socketWrapper">Socket</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        internal static void OnSendBytes( SocketWrapper socketWrapper, byte[] buffer, int offset, int size )
        {
            if( SendBytesEvent != null )
            {
                try
                {
                    EventUtils.FireEvent( SendBytesEvent, socketWrapper, buffer, offset, size );
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 激发发送字节时调用的函数
        /// </summary>
        /// <param name="socketWrapper">Socket</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        internal static void OnReceiveBytes( SocketWrapper socketWrapper, byte[] buffer, int offset, int size )
        {
            if( ReceiveBytesEvent != null )
            {
                try
                {
                    EventUtils.FireEvent( ReceiveBytesEvent, socketWrapper, buffer, offset, size );
                }
                catch
                {
                }
            }
        }

        #endregion
    }
}