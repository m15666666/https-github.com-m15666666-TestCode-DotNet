using System;
using System.Net;
using System.Net.Sockets;

namespace SocketLib
{
    /// <summary>
    /// Socket的实用工具类
    /// </summary>
    public static class SocketUtils
    {
        #region 常量

        /// <summary>
        /// 最大发送包大小
        /// </summary>
        public const int Socket_MaxPackageSize = 1024;

        #endregion

        #region 发送

        /// <summary>
        /// 将指定字节数的数据发送到已连接的 Socket，使用默认的包的最大尺寸
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">待发送的缓冲区</param>
        public static void SendBytes_PackageSize( SocketWrapper socket, byte[] buffer )
        {
            SendBytes_PackageSize( socket, buffer, Socket_MaxPackageSize );
        }

        /// <summary>
        /// 将指定字节数的数据发送到已连接的 Socket，指定每个包的最大尺寸
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">待发送的缓冲区</param>
        /// <param name="packageSize">每个包的最大尺寸</param>
        public static void SendBytes_PackageSize( SocketWrapper socket, byte[] buffer, int packageSize )
        {
            SendBytes_PackageSize( socket, buffer, 0, buffer.Length, packageSize );
        }

        /// <summary>
        /// 将指定字节数的数据发送到已连接的 Socket，使用默认的包的最大尺寸
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">待发送的缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要发送的字节数</param>
        public static void SendBytes_PackageSize( SocketWrapper socket, byte[] buffer, int offset, int size )
        {
            SendBytes_PackageSize( socket, buffer, offset, size, Socket_MaxPackageSize );
        }

        /// <summary>
        /// 将指定字节数的数据发送到已连接的 Socket，指定每个包的最大尺寸
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">待发送的缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要发送的字节数</param>
        /// <param name="packageSize">每个包的最大尺寸</param>
        public static void SendBytes_PackageSize( SocketWrapper socket, byte[] buffer, int offset, int size,
                                                  int packageSize )
        {
            while( 0 < size )
            {
                int sendSize = Math.Min( packageSize, size );
                SendBytes( socket, buffer, offset, sendSize );

                size -= sendSize;
                offset += sendSize;
            }
        }

        /// <summary>
        /// 将指定字节数的数据发送到已连接的 Socket（从指定的偏移量开始）
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">待发送的缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要发送的字节数</param>
        public static void SendBytes( SocketWrapper socket, byte[] buffer, int offset, int size )
        {
            int currentOffset = offset;
            while( 0 < size )
            {
                int sendCount = socket.Send( buffer, currentOffset, size );
                SocketLibConfig.OnSendBytes( socket, buffer, currentOffset, sendCount );

                size -= sendCount;
                currentOffset += sendCount;
            }
        }

        #endregion

        #region 接收

        /// <summary>
        /// 接收数据，使用默认的包的最大尺寸
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">缓冲区</param>
        public static void ReceiveBytes_PackageSize( SocketWrapper socket, byte[] buffer )
        {
            ReceiveBytes_PackageSize( socket, buffer, 0, buffer.Length, Socket_MaxPackageSize );
        }

        /// <summary>
        /// 接收数据，指定每个包的最大尺寸
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="packageSize">每个包的最大尺寸</param>
        public static void ReceiveBytes_PackageSize( SocketWrapper socket, byte[] buffer, int packageSize )
        {
            ReceiveBytes_PackageSize( socket, buffer, 0, buffer.Length, packageSize );
        }

        /// <summary>
        /// 接收数据，使用默认的包的最大尺寸
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要接收的字节数</param>
        public static void ReceiveBytes_PackageSize( SocketWrapper socket, byte[] buffer, int offset, int size )
        {
            ReceiveBytes_PackageSize( socket, buffer, offset, size, Socket_MaxPackageSize );
        }

        /// <summary>
        /// 接收数据，指定每个包的最大尺寸
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要接收的字节数</param>
        /// <param name="packageSize">每个包的最大尺寸</param>
        public static void ReceiveBytes_PackageSize( SocketWrapper socket, byte[] buffer, int offset, int size,
                                                     int packageSize )
        {
            while( 0 < size )
            {
                int receiveSize = Math.Min( packageSize, size );
                ReceiveBytes( socket, buffer, offset, receiveSize );

                size -= receiveSize;
                offset += receiveSize;
            }
        }

        /// <summary>
        /// 从Socket 接收指定的字节数，存入接收缓冲区的指定偏移量位置
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要接收的字节数</param>
        public static void ReceiveBytes( SocketWrapper socket, byte[] buffer, int offset, int size )
        {
            int currentOffset = offset;
            while( 0 < size )
            {
                int receiveCount = socket.Receive( buffer, currentOffset, size );
                SocketLibConfig.OnReceiveBytes( socket, buffer, currentOffset, receiveCount );

                size -= receiveCount;
                currentOffset += receiveCount;
            }
        }

        #endregion

        #region 获得Socket连接已关闭的异常对象

        /// <summary>
        /// 获得Socket连接已关闭的信息字符串
        /// </summary>
        /// <param name="extraMessage">额外的信息</param>
        /// <returns>Socket连接已关闭的信息字符串</returns>
        public static string GetSocketCloseMessage( string extraMessage )
        {
            const string SocketClose = "Socket已关闭";
            return string.IsNullOrEmpty( extraMessage )
                       ? SocketClose + "！"
                       : string.Format( SocketClose + "({0})！", extraMessage );
        }

        /// <summary>
        /// 获得Socket连接已关闭的异常对象
        /// </summary>
        /// <returns>Socket连接已关闭的异常对象</returns>
        public static ObjectDisposedException GetSocketCloseException()
        {
            return GetSocketCloseException( GetSocketCloseMessage( null ) );
        }

        /// <summary>
        /// 获得Socket连接已关闭的异常对象
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns>Socket连接已关闭的异常对象</returns>
        public static ObjectDisposedException GetSocketCloseException( string message )
        {
            return new ObjectDisposedException( message );
        }

        #endregion

        #region 判断状态

        /// <summary>
        /// 获得Socket错误信息
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Socket错误信息</returns>
        public static string GetSocketErrorInfo( Exception ex )
        {
            var socketEx = ex as SocketException;
            if( socketEx != null )
            {
                return string.Format( "{0},{1},{2}", socketEx.SocketErrorCode, socketEx.ErrorCode,
                                      socketEx.NativeErrorCode );
            }

            return null;
        }

        /// <summary>
        /// 获得SocketError枚举
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>SocketError</returns>
        public static SocketError GetSocketError( Exception ex )
        {
            var socketException = ex as SocketException;
            return socketException != null ? socketException.SocketErrorCode : SocketError.Success;
        }

        /// <summary>
        /// 异常是否表示连接已经断开
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>连接是否已经断开</returns>
        public static bool IsDisconnectException( Exception ex )
        {
            if( ex is ObjectDisposedException )
            {
                return true;
            }

            if( GetSocketError( ex ) == SocketError.ConnectionReset )
            {
                // 即socketException.ErrorCode == 10054
                return true;
            }

            return false;
        }

        /// <summary>
        /// 异常是否表示侦听已经关闭
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>侦听是否已经关闭</returns>
        public static bool IsListenClose( Exception ex )
        {
            switch( GetSocketError( ex ) )
            {
                case SocketError.Interrupted: // 即socketException.ErrorCode == 10004
                case SocketError.OperationAborted: // 由于 Socket 已关闭，重叠的操作被中止。
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 获得SocketException的描述信息
        /// </summary>
        /// <param name="ex">SocketException</param>
        /// <returns>SocketException的描述信息</returns>
        public static string GetSocketExceptionDetail( SocketException ex )
        {
            return string.Format( "Message:{0},SocketErrorCode:{1},ErrorCode:{2}", ex.Message, ex.SocketErrorCode,
                                  ex.ErrorCode );
        }

        #endregion

        #region 获得IPEndPoint(地址)

        /// <summary>
        /// 获得IPEndPoint(地址)
        /// </summary>
        /// <param name="host">ip或主机名，格式为（ip + 端口）："10.1.1.20:1181"。如果不指定端口，则默认端口为：1181</param>
        /// <returns>IPEndPoint(地址)</returns>
        public static IPEndPoint GetEndPoint( string host )
        {
            // 默认端口为:1181
            int port = 1181;
            if( host.Contains( ":" ) )
            {
                string[] split = host.Split( ':' );
                if( !Int32.TryParse( split[1], out port ) )
                {
                    throw new ArgumentException( "Unable to parse host: " + host );
                }

                host = split[0];
            }
            return GetEndPoint( host, port );
        }

        /// <summary>
        /// 获得IPEndPoint(地址)
        /// </summary>
        /// <param name="host">ip或主机名</param>
        /// <param name="port">端口</param>
        /// <returns>IPEndPoint(地址)</returns>
        public static IPEndPoint GetEndPoint( string host, int port )
        {
            IPAddress address;
            if( !IPAddress.TryParse( host, out address ) )
            {
                // 尝试解析主机名
                try
                {
                    address = Dns.GetHostEntry( host ).AddressList[0];
                }
                catch( Exception ex )
                {
                    throw new ArgumentException( "Unable to resolve host: " + host, ex );
                }
            }

            return new IPEndPoint( address, port );
        }

        #endregion
    }
}