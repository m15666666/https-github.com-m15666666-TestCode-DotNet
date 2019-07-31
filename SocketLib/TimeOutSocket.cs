using System;
using System.Net.Sockets;
using System.Threading;

namespace SocketLib
{
    /// <summary>
    /// 支持超时的Socket Connect方法的类
    /// </summary>
    public class TimeOutSocket : IDisposable
    {
        #region 变量和属性

        /// <summary>
        /// 内部Socket对象
        /// </summary>
        private readonly Socket _innerSocket;

        private readonly ManualResetEvent _timeoutEvent = new ManualResetEvent( false );

        private bool _connectSuccess;
        private Exception _socketException;

        public TimeOutSocket( Socket socket )
        {
            _innerSocket = socket;
        }

        #endregion

        #region 建立连接

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="socket">要建立连接的Socket</param>
        /// <param name="ip">IP</param>
        /// <param name="port">端口</param>
        /// <param name="timeoutMSec">连接超时，以毫秒为单位</param>
        public static void Connect( Socket socket, string ip, int port, int timeoutMSec )
        {
            using( var timeOutSocket = new TimeOutSocket( socket ) )
            {
                timeOutSocket.Connect( ip, port, timeoutMSec );
            }
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">端口</param>
        /// <param name="timeoutMSec">连接超时，以毫秒为单位</param>
        public void Connect( string ip, int port, int timeoutMSec )
        {
            _timeoutEvent.Reset();

            _socketException = null;

            _innerSocket.BeginConnect( ip, port,
                                       ConnectCallBack, _innerSocket );

            if( _timeoutEvent.WaitOne( timeoutMSec, false ) )
            {
                if( !_connectSuccess && _socketException != null )
                {
                    throw _socketException;
                }
            }
            else
            {
                using( _innerSocket )
                {
                }

                throw new TimeoutException( string.Format( "连接“{0}:{1}”超时", ip, port ) );
            }
        }

        /// <summary>
        /// 连接回调函数
        /// </summary>
        /// <param name="asyncResult">IAsyncResult</param>
        private void ConnectCallBack( IAsyncResult asyncResult )
        {
            try
            {
                _connectSuccess = false;
                var socket = asyncResult.AsyncState as Socket;

                if( socket != null )
                {
                    socket.EndConnect( asyncResult );
                    _connectSuccess = true;
                }
            }
            catch( Exception ex )
            {
                _socketException = ex;
            }
            finally
            {
                _timeoutEvent.Set();
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            using( _timeoutEvent )
            {
            }
        }

        #endregion
    }
}