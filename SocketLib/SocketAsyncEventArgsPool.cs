using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Moons.Common20;

namespace SocketLib
{
    /// <summary>
    /// Represents a collection of reusable SocketAsyncEventArgs objects.  
    /// </summary>
    public class SocketAsyncEventArgsPool
    {
        static SocketAsyncEventArgsPool()
        {
            ReadPool = new SocketAsyncEventArgsPool( 10000 );
            AcceptPool = new SocketAsyncEventArgsPool( 10 );
        }

        /// <summary>
        /// Initializes the object pool to the specified size
        /// </summary>
        /// <param name="capacity">初始化容量</param>
        public SocketAsyncEventArgsPool( int capacity )
        {
            _pool = new Stack<SocketAsyncEventExArgs>( capacity );
        }

        #region 变量和属性

        /// <summary>
        /// 内部锁对象
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// 对象集合
        /// </summary>
        private readonly Stack<SocketAsyncEventExArgs> _pool;

        /// <summary>
        /// 用于读取的池
        /// </summary>
        public static SocketAsyncEventArgsPool ReadPool { get; set; }

        /// <summary>
        /// 用于接入的池
        /// </summary>
        public static SocketAsyncEventArgsPool AcceptPool { get; set; }

        /// <summary>
        /// The number of SocketAsyncEventArgs instances in the pool
        /// </summary>
        public int Count
        {
            get { return _pool.Count; }
        }

        /// <summary>
        /// 获取的次数
        /// </summary>
        public ulong GetCount { get; private set; }

        /// <summary>
        /// 返回的次数
        /// </summary>
        public ulong ReturnCount { get; private set; }

        #endregion

        #region 用于接入的SocketAsyncEventExArgs对象

        /// <summary>
        /// 返回用于接入的SocketAsyncEventExArgs对象
        /// </summary>
        /// <param name="item">the SocketAsyncEventArgs instance to add to the pool</param>
        public static void ReturnAccept( SocketAsyncEventExArgs item )
        {
            AcceptPool.Push( item );
        }

        /// <summary>
        /// 获得用于接入的SocketAsyncEventExArgs对象
        /// </summary>
        /// <returns>SocketAsyncEventExArgs对象</returns>
        public static SocketAsyncEventExArgs GetAccept()
        {
            return AcceptPool.Pop();
        }

        #endregion

        #region 操作池

        /// <summary>
        /// Add a SocketAsyncEventArg instance to the pool
        /// </summary>
        /// <param name="item">the SocketAsyncEventArgs instance to add to the pool</param>
        public void Push( SocketAsyncEventExArgs item )
        {
            if( item == null )
            {
                throw new ArgumentNullException( "Items added to a SocketAsyncEventArgsPool cannot be null" );
            }

            item.ClearState();

            lock( _lock )
            {
                if( ReturnCount == ulong.MaxValue )
                {
                    ReturnCount = 0;
                }
                else
                {
                    ReturnCount++;
                }

                _pool.Push( item );
            }
        }

        /// <summary>
        /// Removes a SocketAsyncEventArgs instance from the pool
        /// and returns the object removed from the pool
        /// </summary>
        /// <returns></returns>
        public SocketAsyncEventExArgs Pop()
        {
            lock( _lock )
            {
                if( GetCount == ulong.MaxValue )
                {
                    GetCount = 0;
                }
                else
                {
                    GetCount++;
                }

                if( 0 < Count )
                {
                    return _pool.Pop();
                }
            }

            return new SocketAsyncEventExArgs();
        }

        #endregion

        #region 用于读取的SocketAsyncEventExArgs对象

        /// <summary>
        /// 返回用于读取的SocketAsyncEventExArgs对象
        /// </summary>
        /// <param name="item">the SocketAsyncEventArgs instance to add to the pool</param>
        public static void ReturnRead( SocketAsyncEventExArgs item )
        {
            ReadPool.Push( item );
        }

        /// <summary>
        /// 获得用于读取的SocketAsyncEventExArgs对象
        /// </summary>
        /// <returns>SocketAsyncEventExArgs对象</returns>
        public static SocketAsyncEventExArgs GetRead()
        {
            return ReadPool.Pop();
        }

        #endregion
    }

    /// <summary>
    /// 自定义的SocketAsyncEventArgs类
    /// </summary>
    public class SocketAsyncEventExArgs : SocketAsyncEventArgs
    {
        #region 异步事件

        /// <summary>
        /// 异步接入事件完成时调用的函数
        /// </summary>
        public Action<SocketAsyncEventExArgs> AcceptComplete { get; set; }

        /// <summary>
        /// 异步接收数据事件完成时调用的函数
        /// </summary>
        public Action<SocketAsyncEventExArgs> ReceiveComplete { get; set; }

        #endregion

        public SocketAsyncEventExArgs()
        {
            Completed += SocketAsyncEvent_Completed;
        }

        /// <summary>
        /// 清除状态
        /// </summary>
        public void ClearState()
        {
            RestDataCount = CurrentOffset = 0;

            AcceptSocket = null;
            SetBuffer( null, 0, 0 );

            AcceptComplete = null;
            ReceiveComplete = null;

            SocketError = SocketError.Success;
        }

        /// <summary>
        /// 响应异步事件
        /// </summary>
        private void SocketAsyncEvent_Completed( object sender, SocketAsyncEventArgs e )
        {
            switch( e.LastOperation )
            {
                case SocketAsyncOperation.Accept:
                    EventUtils.FireEvent( AcceptComplete, (SocketAsyncEventExArgs)e );
                    break;

                case SocketAsyncOperation.Receive:
                    EventUtils.FireEvent( ReceiveComplete, (SocketAsyncEventExArgs)e );
                    break;
            }
        }

        #region Buffer相关

        #region 剩下的数据数量相关

        /// <summary>
        /// 剩下的数据数量
        /// </summary>
        public int RestDataCount { get; set; }

        /// <summary>
        /// 当前的偏移量
        /// </summary>
        public int CurrentOffset { get; set; }

        /// <summary>
        /// 移除剩余的数据
        /// </summary>
        /// <param name="removeCount">移除的数量</param>
        public void RemoveRestData( int removeCount )
        {
            if( removeCount < 1 || RestDataCount < removeCount )
            {
                throw new ArgumentException(
                    string.Format( "移除的数量不合法！(removeCount:{0}, {1})", removeCount,
                                   GetRestDataCountInfo() ) );
            }

            RestDataCount -= removeCount;
            CurrentOffset += removeCount;
        }

        /// <summary>
        /// 初始化剩余数据的数量
        /// </summary>
        /// <param name="restCount">剩余数据的数量</param>
        public void InitRestDataCount( int restCount )
        {
            int bytesTransferred = BytesTransferred;
            if( restCount < 1 || bytesTransferred < restCount || 0 < RestDataCount )
            {
                throw new ArgumentException(
                    string.Format( "剩余数据的数量不合法或有剩余数据未移除！(restCount:{0}, {1})", restCount,
                                   GetRestDataCountInfo() ) );
            }

            RestDataCount = restCount;
            CurrentOffset = Offset + ( bytesTransferred - restCount );
        }

        /// <summary>
        /// 获得剩下的数据数量相关的信息
        /// </summary>
        /// <returns>得剩下的数据数量相关的信息</returns>
        private string GetRestDataCountInfo()
        {
            return string.Format( "RestDataCount:{0}, CurrentOffset:{1})", RestDataCount, CurrentOffset );
        }

        #endregion

        #region 初始化缓存

        /// <summary>
        /// 初始化缓存
        /// </summary>
        public void InitBuffer()
        {
            InitBuffer( 1024 );
        }

        /// <summary>
        /// 初始化缓存
        /// </summary>
        /// <param name="byteCount">缓存的字节数</param>
        public void InitBuffer( int byteCount )
        {
            var buffer = new byte[byteCount];
            SetBuffer( buffer, 0, buffer.Length );
        }

        #endregion

        #endregion
    }
}