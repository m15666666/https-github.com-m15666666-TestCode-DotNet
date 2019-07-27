using System;
using System.Collections.Generic;

namespace Moons.Common20
{
    /// <summary>
    /// 释放资源的实用工具类
    /// </summary>
    public static class DisposeUtils
    {
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposable">IDisposable</param>
        public static void Dispose( ref IDisposable disposable )
        {
            if( disposable != null )
            {
                Dispose( disposable );
                disposable = null;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposable">IDisposable</param>
        public static void Dispose( IDisposable disposable )
        {
            if( disposable != null )
            {
                TryCatchUtils.Catch( disposable.Dispose );
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposables">IDisposable</param>
        public static void Dispose( IEnumerable<IDisposable> disposables )
        {
            if( disposables != null )
            {
                foreach( IDisposable disposable in disposables )
                {
                    Dispose( disposable );
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="data">资源对象</param>
        /// <param name="disposeHandlers">释放资源的函数代理</param>
        public static void Dispose<T>( ref T data, params Action<T>[] disposeHandlers ) where T : class
        {
            if( data != null )
            {
                Dispose( data, disposeHandlers );
                data = default( T );
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="data">资源对象</param>
        /// <param name="disposeHandlers">释放资源的函数代理</param>
        public static void Dispose<T>( T data, params Action<T>[] disposeHandlers ) where T : class
        {
            if( data == null )
            {
                return;
            }

            ForUtils.ForEach( disposeHandlers, handler => TryCatchUtils.Catch( handler, data ) );

            var disposable = data as IDisposable;
            if( disposable != null )
            {
                Dispose( disposable );
            }
        }
    }
}