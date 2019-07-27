using System;
using System.Collections.Generic;

namespace Moons.Common20
{
    /// <summary>
    /// 加锁的队列
    /// </summary>
    /// <typeparam name="T">内部元素类型</typeparam>
    public class LockQueue<T> where T : class
    {
        #region 变量和属性

        /// <summary>
        /// 数据缓存
        /// </summary>
        private readonly List<T> _buffer = new List<T>( 1024 );

        /// <summary>
        /// 内部锁对象
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// 内部元素个数
        /// </summary>
        public int Count
        {
            get { return _buffer.Count; }
        }

        /// <summary>
        /// 是否有数据
        /// </summary>
        public bool HasData
        {
            get { return 0 < Count; }
        }

        #endregion

        #region 操作数据

        /// <summary>
        /// 加入队列
        /// </summary>
        /// <param name="data">数据对象</param>
        public void Add( T data )
        {
            lock( _lock )
            {
                _buffer.Add( data );
            }
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
            lock( _lock )
            {
                _buffer.Clear();
            }
        }

        /// <summary>
        /// 把所有元素弹出
        /// </summary>
        /// <returns>所有元素的数组</returns>
        public T[] PopAll()
        {
            if( !HasData )
            {
                return new T[0];
            }

            lock( _lock )
            {
                T[] ret = _buffer.ToArray();
                _buffer.Clear();
                return ret;
            }
        }

        /// <summary>
        /// 弹出count个元素
        /// </summary>
        /// <returns>count个元素的数组</returns>
        public T[] Pop( int count )
        {
            if( count < 1 || !HasData )
            {
                return new T[0];
            }

            lock( _lock )
            {
                count = Math.Min( count, Count );
                if( count == 0 )
                {
                    return new T[0];
                }

                var ret = new T[count];
                _buffer.CopyTo( 0, ret, 0, ret.Length );

                _buffer.RemoveRange( 0, count );

                return ret;
            }
        }

        #endregion
    }

    /// <summary>
    /// 加锁的队列，内部元素类型是object
    /// </summary>
    public class LockQueue : LockQueue<object>
    {
    }
}