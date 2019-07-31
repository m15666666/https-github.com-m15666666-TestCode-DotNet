using System;
using System.Net;
using System.Net.Sockets;
using Moons.Common20;

namespace SocketLib
{
    /// <summary>
    /// Socket的包装器类
    /// </summary>
    public class SocketWrapper : IDisposable
    {
        #region 变量和属性

        #region 发送、接收超时

        /// <summary>
        /// 发送超时
        /// </summary>
        public int SendTimeout
        {
            set { InnerSocket.SendTimeout = value; }
            get { return InnerSocket.SendTimeout; }
        }

        /// <summary>
        /// 接收超时
        /// </summary>
        public int ReceiveTimeout
        {
            set { InnerSocket.ReceiveTimeout = value; }
            get { return InnerSocket.ReceiveTimeout; }
        }

        /// <summary>
        /// 发送、接收超时的信息
        /// </summary>
        public string SendReceiveTimeoutInfo
        {
            get { return string.Format( "SendReceiveTimeout:({0}, {1})", SendTimeout, ReceiveTimeout ); }
        }

        #endregion

        #region 不同版本Socket的特性

        /// <summary>
        /// true：使用Framework3.5的特性（异步），false：使用Framework2.0的特性。默认为true。
        /// </summary>
        public bool UseFramework35Feature { get; set; }

        /// <summary>
        /// 用于接入的参数对象
        /// </summary>
        private SocketAsyncEventExArgs AsyncEventArgs4Accept { get; set; }

        /// <summary>
        /// 用于读取数据的参数对象
        /// </summary>
        private SocketAsyncEventExArgs AsyncEventArgs4Read { get; set; }

        /// <summary>
        /// 释放异步参数
        /// </summary>
        private void ReleaseAsyncEventArgs()
        {
            SocketAsyncEventExArgs args4Read = AsyncEventArgs4Read;
            if( args4Read != null )
            {
                AsyncEventArgs4Read = null;
                SocketAsyncEventArgsPool.ReturnRead( args4Read );
            }

            AsyncEventArgs4Accept = null;
        }

        #endregion

        #region 相关的对象

        /// <summary>
        /// 相关的对象
        /// </summary>
        public object LinkedObject { get; set; }

        /// <summary>
        /// 获得相关的对象
        /// </summary>
        /// <typeparam name="T">相关的对象的类型</typeparam>
        /// <returns>相关的对象</returns>
        public T GetLinkedObject<T>() where T : class
        {
            return LinkedObject as T;
        }

        #endregion

        #region 内部缓冲区

        /// <summary>
        /// 读缓冲区
        /// </summary>
        private readonly ByteBuffer _readBuffer = new ByteBuffer();

        /// <summary>
        /// 用于检测是否处于连接状态
        /// </summary>
        private readonly byte[] _testIsConnected = new byte[1];

        /// <summary>
        /// 读缓冲区
        /// </summary>
        public ByteBuffer ReadByteBuffer
        {
            get { return _readBuffer; }
        }

        /// <summary>
        /// 读缓冲区
        /// </summary>
        public byte[] ReadBuffer
        {
            get { return _readBuffer.Buffer; }
        }

        #endregion

        #region 内部Socket对象

        /// <summary>
        /// 内部Socket对象
        /// </summary>
        private Socket _innerSocket;

        /// <summary>
        /// 内部Socket对象
        /// </summary>
        public Socket InnerSocket
        {
            get { return _innerSocket; }
        }

        /// <summary>
        /// 侦听并接入本身的SocketWrapper对象
        /// </summary>
        public SocketWrapper Listener { get; private set; }

        #endregion

        #region Socket状态

        /// <summary>
        /// 是否处于连接状态
        /// </summary>
        public bool Connected
        {
            get
            {
                if( Disposed )
                {
                    return false;
                }

                Socket socket = _innerSocket;
                return socket != null && socket.Connected;
            }
        }

        /// <summary>
        /// 是否处于断开连接状态
        /// </summary>
        public bool Disconnected
        {
            get { return !Connected; }
        }

        /// <summary>
        /// 通过检测判断是否处于连接状态
        /// 参考：ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.chs/fxref_system/html/cb053393-5e5e-27fb-4f87-32df31f94e44.htm
        /// </summary>
        public bool IsConnectedByDetect
        {
            get
            {
                Socket socket = _innerSocket;
                bool blockingState = socket.Blocking;
                try
                {
                    socket.Blocking = false;
                    socket.Send( _testIsConnected, 0, 0 );
                }
                catch( SocketException ex )
                {
                    // 10035 == WSAEWOULDBLOCK
                    if( ex.NativeErrorCode.Equals( 10035 ) )
                    {
                        // Console.WriteLine( "Still Connected, but the Send would block" );
                    }
                    //else
                    //{
                    //    // Console.WriteLine("Disconnected: error code {0}!", ex.NativeErrorCode);
                    //}
                }
                finally
                {
                    socket.Blocking = blockingState;
                }

                return socket.Connected;
            }
        }

        /// <summary>
        /// true：数据可读，false：不可读
        /// </summary>
        public bool DataAvailable
        {
            get { return Connected && 0 < InnerSocket.Available; }
        }

        /// <summary>
        /// 是否保持SocketWrapper不被Dispose函数销毁，用于客户端发起的、用于发命令的链接
        /// </summary>
        public bool KeepSocketWrapperAlive { get; set; }

        #region 多个线程使用socketwrapper对象

        /// <summary>
        /// socket被多个线程使用的次数
        /// </summary>
        private int _socketUsedCount = 0;

        /// <summary>
        /// socket被多个线程使用的锁
        /// </summary>
        private object _useSocketLock = new object();

        /// <summary>
        /// 使用socket
        /// </summary>
        public void UseSocket()
        {
            System.Threading.Interlocked.Increment( ref _socketUsedCount );
            System.Threading.Monitor.Enter( _useSocketLock );
        }

        /// <summary>
        /// 释放socket
        /// </summary>
        public void ReleaseSocket()
        {
            if (0 < _socketUsedCount)
            {
                System.Threading.Interlocked.Decrement(ref _socketUsedCount);

                try{ System.Threading.Monitor.Exit(_useSocketLock); } catch {}
            }
        }

        #endregion

        #endregion

        #region 关于EndPoint

        /// <summary>
        /// 记录Socket连接的信息
        /// </summary>
        private readonly SocketInfo _socketInfo = new SocketInfo();

        /// <summary>
        /// 记录Socket连接的信息，只用于调试
        /// </summary>
        public SocketInfo SocketInfo
        {
            get { return _socketInfo; }
        }

        /// <summary>
        /// IP和端口的信息
        /// </summary>
        public string IPPortInfo
        {
            get { return _socketInfo.IPPortInfo; }
        }

        /// <summary>
        /// 断开Socket连接时的信息
        /// </summary>
        public string DisconnectMessage
        {
            get { return string.Format( "“{0}”已经关闭", IPPortInfo ); }
        }

        /// <summary>
        /// 初始化端点地址信息
        /// </summary>
        private void InitIPPortInfo()
        {
            _socketInfo.Init( InnerSocket );
        }

        #endregion

        #region 内部异常

        /// <summary>
        /// 内部异常
        /// </summary>
        public Exception InnerException { get; private set; }

        /// <summary>
        /// 内部SocketError
        /// </summary>
        public SocketError SocketError
        {
            get { return SocketUtils.GetSocketError( InnerException ); }
        }

        /// <summary>
        /// 内部Socket错误信息
        /// </summary>
        public string SocketErrorInfo
        {
            get { return string.Format( "{0}, {1}", IPPortInfo, SocketUtils.GetSocketErrorInfo( InnerException ) ); }
        }

        #endregion

        #endregion

        #region 异步事件的代理

        /// <summary>
        /// 异步连接成功时调用的函数
        /// </summary>
        public Action<SocketWrapper> ConnectSuccess { private get; set; }

        /// <summary>
        /// 异步连接失败时调用的函数
        /// </summary>
        public Action<SocketWrapper> ConnectFail { private get; set; }

        /// <summary>
        /// 异步接入Socket成功时调用的函数
        /// </summary>
        public Action<SocketWrapper> AcceptSuccess { private get; set; }

        /// <summary>
        /// 异步接入Socket失败时调用的函数
        /// </summary>
        public Action<SocketWrapper> AcceptFail { private get; set; }

        /// <summary>
        /// 异步读数据成功时调用的函数
        /// </summary>
        public Action<SocketWrapper> ReadSuccess { private get; set; }

        /// <summary>
        /// 异步读数据失败时调用的函数
        /// </summary>
        public Action<SocketWrapper> ReadFail { private get; set; }

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        private void FireEvent( Action<SocketWrapper> handler )
        {
            FireEvent( handler, this );
        }

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="socketWrapper">SocketWrapper</param>
        private static void FireEvent( Action<SocketWrapper> handler, SocketWrapper socketWrapper )
        {
            EventUtils.FireEvent( handler, socketWrapper );
        }

        #endregion

        #region 创建SocketWrapper对象

        private SocketWrapper()
        {
            UseFramework35Feature = true;
        }

        /// <summary>
        /// 创建tcp连接的SocketWrapper对象
        /// </summary>
        /// <returns>tcp连接的SocketWrapper对象</returns>
        public static SocketWrapper CreateTcpSocket()
        {
            return Create( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
        }

        /// <summary>
        /// 创建SocketWrapper对象
        /// </summary>
        /// <param name="addressFamily">AddressFamily</param>
        /// <param name="socketType">SocketType</param>
        /// <param name="protocolType">ProtocolType</param>
        /// <returns>SocketWrapper对象</returns>
        public static SocketWrapper Create( AddressFamily addressFamily, SocketType socketType,
                                            ProtocolType protocolType )
        {
            return new SocketWrapper { _innerSocket = new Socket( addressFamily, socketType, protocolType ) };
        }

        /// <summary>
        /// 创建SocketWrapper对象
        /// </summary>
        /// <param name="socket">内部的Socket对象</param>
        /// <returns>SocketWrapper对象</returns>
        public static SocketWrapper Create( Socket socket )
        {
            return new SocketWrapper { _innerSocket = socket };
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// 是否已经释放了资源
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// 是否已经释放了资源
        /// </summary>
        public bool Disposed
        {
            get { return _disposed; }
        }

        public void Dispose()
        {
            ReleaseSocket();
            if ( KeepSocketWrapperAlive ) { return; }

            if( !_disposed )
            {
                CloseInnerSocket();

                ReleaseAsyncEventArgs();

                _disposed = true;
            }
        }

        /// <summary>
        /// 关闭内存的Socket连接
        /// </summary>
        private void CloseInnerSocket()
        {
            if( _innerSocket != null )
            {
                DisposeUtils.Dispose( ref _innerSocket,
                                      socket => socket.Shutdown( SocketShutdown.Both ),
                                      socket => socket.Close() );
            }
        }

        #endregion

        #region 判断状态

        /// <summary>
        /// 异常是否表示连接已经断开
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>连接是否已经断开</returns>
        public bool IsDisconnect( Exception ex )
        {
            return Disconnected || SocketUtils.IsDisconnectException( ex );
        }

        /// <summary>
        /// 异常是否表示侦听已经关闭
        /// </summary>
        /// <returns>侦听是否已经关闭</returns>
        public bool IsListenClose()
        {
            return IsListenClose( InnerException );
        }

        /// <summary>
        /// 异常是否表示侦听已经关闭
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>侦听是否已经关闭</returns>
        public bool IsListenClose( Exception ex )
        {
            return Disposed || SocketUtils.IsListenClose( ex );
        }

        #endregion

        #region 绑定、侦听端口

        /// <summary>
        /// 绑定端口
        /// </summary>
        /// <param name="port">端口</param>
        public void Bind( int port )
        {
            Bind( new IPEndPoint( IPAddress.Any, port ) );
        }

        /// <summary>
        /// 绑定端口
        /// </summary>
        /// <param name="endPoint">IPEndPoint</param>
        public void Bind( IPEndPoint endPoint )
        {
            Socket socket = InnerSocket;

            socket.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true );

            socket.Bind( endPoint );
        }

        /// <summary>
        /// 开始侦听
        /// </summary>
        /// <param name="backlog">队列数</param>
        public void Listen( int backlog )
        {
            InnerSocket.Listen( backlog );
        }

        #endregion

        #region 接入连接(Accept)

        /// <summary>
        /// Creates a new SocketWrapper for a newly created connection.
        /// </summary>
        /// <returns>SocketWrapper</returns>
        public SocketWrapper Accept()
        {
            return InitAcceptSocket( _innerSocket.Accept() );
        }

        /// <summary>
        /// 初始化接入的Socket对象
        /// </summary>
        /// <param name="argsAccept">SocketAsyncEventExArgs</param>
        /// <returns>SocketWrapper对象</returns>
        private SocketWrapper InitAcceptSocket( SocketAsyncEventExArgs argsAccept )
        {
            SocketWrapper socketWrapper = InitAcceptSocket( argsAccept.AcceptSocket );
            argsAccept.AcceptSocket = null;

            return socketWrapper;
        }

        /// <summary>
        /// 初始化接入的Socket对象
        /// </summary>
        /// <param name="socket">接入的Socket对象</param>
        /// <returns>SocketWrapper对象</returns>
        private SocketWrapper InitAcceptSocket( Socket socket )
        {
            SocketWrapper socketWrapper = Create( socket );

            socketWrapper.Listener = this;
            socketWrapper.InitIPPortInfo();
            return socketWrapper;
        }

        /// <summary>
        /// 异步接入连接的Socket对象
        /// </summary>
        public void BeginAccept()
        {
            if( Disposed )
            {
                return;
            }

            Socket socket = _innerSocket;
            if( UseFramework35Feature )
            {
                SocketAsyncEventExArgs asyncEventArgs = AsyncEventArgs4Accept;
                if( asyncEventArgs == null )
                {
                    asyncEventArgs = AsyncEventArgs4Accept = SocketAsyncEventArgsPool.GetAccept();
                    asyncEventArgs.AcceptComplete = BeginAccept;
                }

                if( !socket.AcceptAsync( asyncEventArgs ) )
                {
                    BeginAccept( asyncEventArgs );
                }
                return;
            }

            socket.BeginAccept( AcceptCallBack, socket );
        }

        /// <summary>
        /// 异步接入连接的Socket对象
        /// </summary>
        private void BeginAccept( SocketAsyncEventExArgs e )
        {
            try
            {
                if( e.SocketError == SocketError.Success )
                {
                    FireEvent( AcceptSuccess, InitAcceptSocket( e ) );
                    return;
                }

                var ex = new Exception( string.Format( "接入({0})Socket失败({1})！", e.RemoteEndPoint, e.SocketError ) );
                OnAcceptFail( ex );
            }
            finally
            {
                BeginAccept();
            }
        }

        /// <summary>
        /// 接入连接回调函数
        /// </summary>
        /// <param name="asyncResult">IAsyncResult</param>
        private void AcceptCallBack( IAsyncResult asyncResult )
        {
            try
            {
                var listenSocket = asyncResult.AsyncState as Socket;
                if( listenSocket != null )
                {
                    Socket newSocket = listenSocket.EndAccept( asyncResult );

                    FireEvent( AcceptSuccess, InitAcceptSocket( newSocket ) );
                }
            }
            catch( Exception ex )
            {
                OnAcceptFail( ex );
            }
            finally
            {
                BeginAccept();
            }
        }

        /// <summary>
        /// 响应异步接入的失败事件
        /// </summary>
        /// <param name="ex">Exception</param>
        private void OnAcceptFail( Exception ex )
        {
            InnerException = ex;
            FireEvent( AcceptFail );
        }

        #endregion

        #region 建立连接

        /// <summary>
        /// 直接建立连接
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        public void DirectConnect( string ip, int port )
        {
            try
            {
                _innerSocket.Connect( ip, port );
            }
            finally
            {
                InitIPPortInfo();
            }

            SetDefaultParameter();
        }

        /// <summary>
        /// 建立阻塞的连接
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="timeout">超时</param>
        public void Connect( string ip, int port, int timeout )
        {
            try
            {
                TimeOutSocket.Connect( _innerSocket, ip, port, timeout );
            }
            finally
            {
                InitIPPortInfo();
            }

            SetDefaultParameter();
        }

        /// <summary>
        /// 建立阻塞的连接
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        public void Connect( string ip, int port )
        {
            Connect( ip, port, 5000 );
        }

        /// <summary>
        /// 异步建立连接
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        public void BeginConnect( string ip, int port )
        {
            Socket socket = _innerSocket;
            socket.BeginConnect( ip, port, ConnectCallBack, socket );
        }

        /// <summary>
        /// 连接回调函数
        /// </summary>
        /// <param name="asyncResult">IAsyncResult</param>
        private void ConnectCallBack( IAsyncResult asyncResult )
        {
            try
            {
                var socket = asyncResult.AsyncState as Socket;
                if( socket != null )
                {
                    socket.EndConnect( asyncResult );

                    SetDefaultParameter();

                    InitIPPortInfo();

                    FireEvent( ConnectSuccess );
                }
            }
            catch( Exception ex )
            {
                InnerException = ex;
                FireEvent( ConnectFail );
            }
        }

        /// <summary>
        /// 设置默认的Socket参数
        /// </summary>
        public void SetDefaultParameter()
        {
            Socket socket = InnerSocket;
            //socket.SendBufferSize = 153600;
            //socket.ReceiveBufferSize = 153600;
            socket.Blocking = true;

            socket.SendTimeout = 20000;
            socket.ReceiveTimeout = 20000;
            //socket.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true );
            //socket.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1 );
        }

        #endregion

        #region 异步读数据

        /// <summary>
        /// 异步接收缓冲区长度的字节数
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        public void BeginReceiveBytes( byte[] buffer )
        {
            BeginReceiveBytes( buffer, 0, buffer.Length );
        }

        /// <summary>
        /// 异步接收指定的字节数，存入接收缓冲区的指定偏移量位置
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">要接收的字节数</param>
        public void BeginReceiveBytes( byte[] buffer, int offset, int count )
        {
            _readBuffer.Init( buffer, offset, count );

            if( UseFramework35Feature )
            {
                SocketAsyncEventExArgs asyncEventArgs = AsyncEventArgs4Read;
                if( asyncEventArgs == null )
                {
                    asyncEventArgs = AsyncEventArgs4Read = SocketAsyncEventArgsPool.GetRead();

                    if( asyncEventArgs.Buffer == null )
                    {
                        asyncEventArgs.InitBuffer();
                    }
                    asyncEventArgs.ReceiveComplete = BeginReceive;
                }
            }

            BeginReceiveBytes();
        }

        /// <summary>
        /// 异步读字节
        /// </summary>
        private void BeginReceiveBytes()
        {
            try
            {
                if( UseFramework35Feature )
                {
                    SocketAsyncEventExArgs asyncEventArgs = AsyncEventArgs4Read;
                    int restDataCount = asyncEventArgs.RestDataCount;
                    if( 0 < restDataCount )
                    {
                        int readCount = Math.Min( _readBuffer.NeedCount, restDataCount );
                        _readBuffer.Read( asyncEventArgs.Buffer, asyncEventArgs.CurrentOffset, readCount );
                        asyncEventArgs.RemoveRestData( readCount );

                        if( TryFireReadSuccess() )
                        {
                            return;
                        }
                    }

                    ReceiveAsync( asyncEventArgs );
                    return;
                }

                _innerSocket.BeginReceive( _readBuffer.Buffer, _readBuffer.WriteOffset, _readBuffer.NeedCount,
                                           SocketFlags.None,
                                           ReceiveCallBack, this );
            }
            catch( Exception ex )
            {
                OnReceiveFail( ex );
            }
        }

        /// <summary>
        /// 异步接收
        /// </summary>
        /// <param name="asyncEventArgs">SocketAsyncEventExArgs</param>
        private void ReceiveAsync( SocketAsyncEventExArgs asyncEventArgs )
        {
            if( !_innerSocket.ReceiveAsync( asyncEventArgs ) )
            {
                BeginReceive( asyncEventArgs );
            }
        }

        /// <summary>
        /// 异步接收数据
        /// </summary>
        private void BeginReceive( SocketAsyncEventExArgs e )
        {
            int bytesTransferred = e.BytesTransferred;
            bool socketClosed = bytesTransferred == 0;
            bool isError = e.SocketError != SocketError.Success;

            try
            {
                if( socketClosed )
                {
                    throw
                        SocketUtils.GetSocketCloseException(
                            SocketUtils.GetSocketCloseMessage( string.Format( "{0},{1}", e.RemoteEndPoint, e.SocketError ) ) );
                }

                if( isError )
                {
                    throw new Exception( string.Format( "读数据({0})失败({1})！", e.RemoteEndPoint, e.SocketError ) );
                }

                SocketLibConfig.OnReceiveBytes( this, e.Buffer, e.Offset, bytesTransferred );

                int readCount = Math.Min( _readBuffer.NeedCount, bytesTransferred );
                _readBuffer.Read( e.Buffer, e.Offset, readCount );

                int restCount = bytesTransferred - readCount;
                if( 0 < restCount )
                {
                    e.InitRestDataCount( restCount );
                }

                if( TryFireReadSuccess() )
                {
                    return;
                }
            }
            catch( Exception ex )
            {
                OnReceiveFail( ex );
                isError = true;
                return;
            }
            finally
            {
                if( socketClosed || isError )
                {
                    Dispose();
                }
            }

            // 未读完，继续读数据
            BeginReceiveBytes();
        }

        /// <summary>
        /// 读数据回调函数
        /// </summary>
        /// <param name="asyncResult">IAsyncResult</param>
        private void ReceiveCallBack( IAsyncResult asyncResult )
        {
            try
            {
                int receiveCount = InnerSocket.EndReceive( asyncResult );
                if( receiveCount == 0 )
                {
                    throw SocketUtils.GetSocketCloseException();
                }

                SocketLibConfig.OnReceiveBytes( this, _readBuffer.Buffer, _readBuffer.WriteOffset, receiveCount );

                _readBuffer.AddReadCount( receiveCount );

                if( TryFireReadSuccess() )
                {
                    return;
                }
            }
            catch( Exception ex )
            {
                OnReceiveFail( ex );
                return;
            }

            // 未读完，继续读数据
            BeginReceiveBytes();
        }

        /// <summary>
        /// 尝试激发异步读数据成功事件
        /// </summary>
        /// <returns>true：已激发，false：未激发</returns>
        private bool TryFireReadSuccess()
        {
            if( _readBuffer.ReadEnough )
            {
                FireEvent( ReadSuccess );
                return true;
            }

            return false;
        }

        /// <summary>
        /// 响应异步读数据的失败事件
        /// </summary>
        /// <param name="ex">Exception</param>
        private void OnReceiveFail( Exception ex )
        {
            InnerException = ex;
            FireEvent( ReadFail );
        }

        #endregion

        #region 发送、接收

        /// <summary>
        /// 将指定字节数的数据发送到已连接的 Socket（从指定的偏移量开始）
        /// </summary>
        /// <param name="buffer">待发送的缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要发送的字节数</param>
        /// <returns>发送的字节数</returns>
        public int Send( byte[] buffer, int offset, int size )
        {
            try
            {
                return CheckReturnCount( InnerSocket.Send( buffer, offset, size, SocketFlags.None ) );
            }
            catch( SocketException ex )
            {
                DisposeUtils.Dispose( this );
                throw new ObjectDisposedException( SocketUtils.GetSocketErrorInfo( ex ), ex );
            }
        }

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
            SocketUtils.SendBytes( this, buffer, offset, size );
        }

        /// <summary>
        /// 从Socket 接收指定的字节数，存入接收缓冲区的指定偏移量位置
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要接收的字节数</param>
        /// <returns>接收的字节数</returns>
        public int Receive( byte[] buffer, int offset, int size )
        {
            Socket socket = InnerSocket;
            try
            {
                return CheckReturnCount( socket.Receive( buffer, offset, size, SocketFlags.None ) );
            }
            catch( SocketException ex )
            {
                //  System.Net.Sockets.Socket类抛出一次超时异常后，
                // 下次再读(如果缓冲区内没有字节的话)可能会出现：“对非阻止性套接字的操作不能立即完成（WouldBlock）”的异常，
                // 而这时Socket.Blocking属性为true（但实际上行为是false）。因此，判断为超时异常后，将Socket.Blocking重置为true，
                // 试验下来效果可行。另一种方式是：每次调用Receive之前，将Socket.Blocking重置为true，也可行。
                if( ex.SocketErrorCode == SocketError.TimedOut )
                {
                    socket.Blocking = true;
                    throw;
                }

                DisposeUtils.Dispose( this );
                throw new ObjectDisposedException( SocketUtils.GetSocketErrorInfo( ex ), ex );
            }
        }

        /// <summary>
        /// 从Socket 接收指定的字节数
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
            SocketUtils.ReceiveBytes( this, buffer, offset, size );
        }

        /// <summary>
        /// 检查返回的字节数
        /// </summary>
        /// <param name="returnCount">返回的字节数</param>
        private int CheckReturnCount( int returnCount )
        {
            if( returnCount == 0 )
            {
                DisposeUtils.Dispose( this );
                throw SocketUtils.GetSocketCloseException( "ReturnCount == 0 " );
            }
            return returnCount;
        }

        #endregion
    }
}