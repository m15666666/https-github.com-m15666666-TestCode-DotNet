using System;
using System.ComponentModel;

namespace Moons.Common20
{
    /// <summary>
    /// 与事件相关的实用工具类
    /// </summary>
    public static class EventUtils
    {
        #region 激发事件

        #region EventHandler

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="sender">激发事件的对象</param>
        /// <param name="e">EventArgs</param>
        public static void FireEvent( EventHandler handler, object sender, EventArgs e )
        {
            if( handler != null )
            {
                handler( sender, e );
            }
        }

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="sender">激发事件的对象</param>
        /// <param name="e">TEventArgs</param>
        public static void FireEvent<TEventArgs>( EventHandler<TEventArgs> handler, object sender, TEventArgs e )
            where TEventArgs : EventArgs
        {
            if( handler != null )
            {
                handler( sender, e );
            }
        }

        #endregion

        #region Action、Action20

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="handler">代理</param>
        /// <param name="data">数据</param>
        public static void FireEvent<T>( Action<T> handler, T data )
        {
            if( handler != null )
            {
                handler( data );
            }
        }

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        public static void FireEvent( Action20 handler )
        {
            if( handler != null )
            {
                handler();
            }
        }

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        public static void FireEvent<TArg1, TArg2>( Action20<TArg1, TArg2> handler, TArg1 arg1, TArg2 arg2 )
        {
            if( handler != null )
            {
                handler( arg1, arg2 );
            }
        }

        #endregion

        #region Func20

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        public static TRet FireEvent<TRet>( Func20<TRet> handler )
        {
            if( handler != null )
            {
                return handler();
            }

            return default( TRet );
        }

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="arg1">参数1</param>
        public static TRet FireEvent<TArg1, TRet>( Func20<TArg1, TRet> handler, TArg1 arg1 )
        {
            if( handler != null )
            {
                return handler( arg1 );
            }

            return default( TRet );
        }

        #endregion

        #region CreateObjectHandler

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        public static TRet FireEvent<TRet>( CreateObjectHandler<TRet> handler )
        {
            if( handler != null )
            {
                return handler();
            }

            return default( TRet );
        }

        #endregion

        #region Predicate

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="arg1">参数1</param>
        public static bool FireEvent<TArg1>( Predicate<TArg1> handler, TArg1 arg1 )
        {
            if( handler != null )
            {
                return handler( arg1 );
            }

            return false;
        }

        #endregion

        #region CollectionChangeEventHandler

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="sender">激发事件的对象</param>
        /// <param name="e">CollectionChangeEventArgs</param>
        public static void FireEvent( CollectionChangeEventHandler handler, object sender,
                                      CollectionChangeEventArgs e )
        {
            if( handler != null )
            {
                handler( sender, e );
            }
        }

        #endregion

        #region PropertyChangedEventHandler

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="sender">激发事件的对象</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        public static void FireEvent( PropertyChangedEventHandler handler, object sender, PropertyChangedEventArgs e )
        {
            if( handler != null )
            {
                handler( sender, e );
            }
        }

        #endregion

        #region ByteBufferHandler(操作字节数组的代理)

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        public static void FireEvent( ByteBufferHandler handler, byte[] buffer, int offset, int size )
        {
            if( handler != null )
            {
                handler( buffer, offset, size );
            }
        }

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <typeparam name="T">数据的来源对象的类型</typeparam>
        /// <param name="handler">代理</param>
        /// <param name="source">数据的来源</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        public static void FireEvent<T>( ByteBufferHandler<T> handler, T source, byte[] buffer, int offset, int size )
        {
            if( handler != null )
            {
                handler( source, buffer, offset, size );
            }
        }

        #endregion

        #endregion
    }
}