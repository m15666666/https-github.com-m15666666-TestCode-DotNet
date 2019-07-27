using System;
using System.Collections.Generic;
using System.Threading;

namespace Moons.Common20.Threadings
{
    /// <summary>
    /// 键对锁对象映射的实用工具类
    /// </summary>
    public static class ThreadLockUtils
    {
        #region 变量和属性

        /// <summary>
        /// 内部锁
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// 键对锁对象的映射
        /// </summary>
        private static readonly Dictionary<string, object> _key2Lock = new Dictionary<string, object>();

        #endregion

        #region 获得或创建锁对象、设置锁对象

        /// <summary>
        /// 获得或创建锁对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>锁对象</returns>
        public static object GetOrCreateLockByKey( string key )
        {
            if( string.IsNullOrEmpty( key ) )
            {
                throw new ArgumentNullException( "key" );
            }

            if( !_key2Lock.ContainsKey( key ) )
            {
                lock( _lock )
                {
                    if( !_key2Lock.ContainsKey( key ) )
                    {
                        SetLock( key, new object() );
                    }
                }
            }
            return _key2Lock[key];
        }

        /// <summary>
        /// 设置锁对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="lockObject">锁对象</param>
        public static void SetLock( string key, object lockObject )
        {
            if( string.IsNullOrEmpty( key ) )
            {
                throw new ArgumentNullException( "key" );
            }

            if( lockObject == null )
            {
                throw new ArgumentNullException( "lockObject" );
            }

            lock( _lock )
            {
                _key2Lock[key] = lockObject;
            }
        }

        #endregion

        #region 锁住对象、解锁对象

        /// <summary>
        /// 对象是否被其他线程锁住
        /// </summary>
        /// <param name="lockObject">锁对象</param>
        /// <returns>true：对象已被其他线程锁住，false：对象未被其他线程锁住</returns>
        public static bool IsLocked( object lockObject )
        {
            if( lockObject == null )
            {
                return false;
            }

            if( TryLock( lockObject ) )
            {
                UnLock( lockObject );
                return false;
            }
            return true;
        }

        /// <summary>
        /// 锁住对象
        /// </summary>
        /// <param name="lockObject">锁对象</param>
        public static void Lock( object lockObject )
        {
            if( lockObject != null )
            {
                Monitor.Enter( lockObject );
            }
        }

        /// <summary>
        /// 试图锁住对象
        /// </summary>
        /// <param name="lockObject">锁对象</param>
        /// <returns>如果当前线程获取该锁，则为 true；否则为 false。</returns>
        public static bool TryLock( object lockObject )
        {
            return lockObject != null && Monitor.TryEnter( lockObject );
        }

        /// <summary>
        /// 解锁对象
        /// </summary>
        /// <param name="lockObject">锁对象</param>
        public static void UnLock( object lockObject )
        {
            if( lockObject != null )
            {
                Monitor.Exit( lockObject );
            }
        }

        #endregion

        #region 通过键锁住对象、通过键解锁对象

        /// <summary>
        /// 对象是否被其他线程锁住
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>true：对象已被其他线程锁住，false：对象未被其他线程锁住</returns>
        public static bool IsLockedByKey( string key )
        {
            return IsLocked( GetOrCreateLockByKey( key ) );
        }

        /// <summary>
        /// 通过键锁住对象
        /// </summary>
        /// <param name="key">键</param>
        public static void LockByKey( string key )
        {
            Lock( GetOrCreateLockByKey( key ) );
        }

        /// <summary>
        /// 试图通过键锁住对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>如果当前线程获取该锁，则为 true；否则为 false。</returns>
        public static bool TryLockByKey( string key )
        {
            return TryLock( GetOrCreateLockByKey( key ) );
        }

        /// <summary>
        /// 通过键解锁对象
        /// </summary>
        /// <param name="key">键</param>
        public static void UnLockByKey( string key )
        {
            UnLock( GetOrCreateLockByKey( key ) );
        }

        #endregion
    }
}