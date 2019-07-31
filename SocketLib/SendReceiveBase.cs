using System;
using System.IO;
using System.Net.Sockets;
using Moons.Common20;

namespace SocketLib
{
    /// <summary>
    /// 发送和接收的基类
    /// </summary>
    public abstract class SendReceiveBase
    {
        #region 变量和属性

        /// <summary>
        /// 内部异常
        /// </summary>
        public Exception InnerException { get; set; }

        #region 内部Socket对象

        /// <summary>
        /// 内部SocketWrapper对象
        /// </summary>
        private SocketWrapper _innerSocketWrapper;

        /// <summary>
        /// IP和端口的信息，不会抛出异常
        /// </summary>
        public string SafeIPPortInfo
        {
            get { return _innerSocketWrapper != null ? _innerSocketWrapper.IPPortInfo : string.Empty; }
        }

        /// <summary>
        /// 内部SocketWrapper对象
        /// </summary>
        public SocketWrapper InnerSocketWrapper
        {
            get { return _innerSocketWrapper; }
            set { InitSocket( value ); }
        }

        /// <summary>
        /// 内部Socket
        /// </summary>
        public Socket InnerSocket
        {
            get { return InnerSocketWrapper.InnerSocket; }
        }

        /// <summary>
        /// 通过内部异常表示连接已经断开
        /// </summary>
        /// <returns>true：连接已经断开，false：连接没断开</returns>
        public bool IsDisconnect()
        {
            return InnerSocketWrapper.IsDisconnect( InnerException );
        }

        /// <summary>
        /// 初始化Socket对象
        /// </summary>
        /// <param name="socketWrapper">SocketWrapper</param>
        private void InitSocket( SocketWrapper socketWrapper )
        {
            _innerSocketWrapper = socketWrapper;
        }

        #endregion

        #endregion

        #region 发送字节

        /// <summary>
        /// 将指定字节数的数据发送到已连接的 Socket
        /// </summary>
        /// <param name="buffer">待发送的缓冲区</param>
        public void SendBytes( byte[] buffer )
        {
            SendBytes( buffer, 0, buffer.Length );
        }

        /// <summary>
        /// 将指定字节数的数据发送到已连接的 Socket（从指定的偏移量开始）
        /// </summary>
        /// <param name="buffer">待发送的缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要发送的字节数</param>
        public void SendBytes( byte[] buffer, int offset, int size )
        {
            SocketUtils.SendBytes_PackageSize( InnerSocketWrapper, buffer, offset, size );
        }

        #endregion

        #region 接收字节

        /// <summary>
        /// 从Socket 接收指定的字节数，存入接收缓冲区的指定偏移量位置
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        public void ReceiveBytes( byte[] buffer )
        {
            ReceiveBytes( buffer, 0, buffer.Length );
        }

        /// <summary>
        /// 从Socket 接收指定的字节数，存入接收缓冲区的指定偏移量位置
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要接收的字节数</param>
        public void ReceiveBytes( byte[] buffer, int offset, int size )
        {
            SocketUtils.ReceiveBytes_PackageSize( InnerSocketWrapper, buffer, offset, size );
        }

        #endregion

        #region 文件收发

        /// <summary>
        /// 发送文件的字节数组
        /// </summary>
        /// <param name="path">文件路径</param>
        public void SendFileBytes( string path )
        {
            using( FileStream fs = File.OpenRead( path ) )
            {
                var buffer = new byte[SocketUtils.Socket_MaxPackageSize];
                while( true )
                {
                    int readCount = fs.Read( buffer, 0, buffer.Length );
                    if( readCount == 0 )
                    {
                        break;
                    }

                    SendBytes( buffer, 0, readCount );
                }
            }
        }

        /// <summary>
        /// 接收文件的字节数组，并保存为一个文件
        /// </summary>
        /// <param name="fileLength">文件长度</param>
        /// <param name="path">文件路径</param>
        public void ReceiveFileBytes( long fileLength, string path )
        {
            try
            {
                using( FileStream fs = File.Create( path ) )
                {
                    var buffer = new byte[SocketUtils.Socket_MaxPackageSize];
                    while( 0 < fileLength )
                    {
                        var size = (int)Math.Min( buffer.Length, fileLength );

                        ReceiveBytes( buffer, 0, size );

                        fs.Write( buffer, 0, size );

                        fileLength -= size;
                    }
                }
            }
            catch
            {
                // 删除未传输完全的文件
                File.Delete( path );

                throw;
            }
        }

        #endregion

        #region 异步接收

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="handler">代理</param>
        protected void FireEvent<T>( Action<T> handler ) where T : SendReceiveBase
        {
            EventUtils.FireEvent( handler, (T)this );
        }

        /// <summary>
        /// 异步读字节
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="readSuccess">成功时调用的代理</param>
        /// <param name="readFail">失败时调用的代理</param>
        protected void BeginReceiveBytes( byte[] buffer, Action<SocketWrapper> readSuccess,
                                          Action<SocketWrapper> readFail )
        {
            BeginReceiveBytes( buffer, 0, buffer.Length, readSuccess, readFail );
        }

        /// <summary>
        /// 异步读字节
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要接收的字节数</param>
        /// <param name="readSuccess">成功时调用的代理</param>
        /// <param name="readFail">失败时调用的代理</param>
        protected void BeginReceiveBytes( byte[] buffer, int offset, int size, Action<SocketWrapper> readSuccess,
                                          Action<SocketWrapper> readFail )
        {
            SocketWrapper socket = InnerSocketWrapper;
            socket.ReadSuccess = readSuccess;
            socket.ReadFail = readFail;

            socket.BeginReceiveBytes( buffer, offset, size );
        }

        #endregion
    }
}