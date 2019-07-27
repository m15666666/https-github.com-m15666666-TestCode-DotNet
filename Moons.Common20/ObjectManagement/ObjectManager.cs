using System;
using System.Collections.Generic;

namespace Moons.Common20.ObjectManagement
{
    /// <summary>
    ///     对象管理器，IObjectManager的默认实现
    /// </summary>
    public class ObjectManager<T> : IObjectManagerWithHandler<T> where T : class
    {
        /// <summary>
        ///     如果内部不存在对应键的对象，则调用这个代理
        /// </summary>
        public Func20<string, T> GetByKeyIfNotExistHandler { get; set; }

        /// <summary>
        ///     清理内部对象的时候，调用这个代理
        /// </summary>
        public Action<T> ClearHandler { get; set; }

        /// <summary>
        ///     清理内部对象的时候，调用这个函数
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnClear( T item )
        {
            EventUtils.FireEvent( ClearHandler, item );
        }

        /// <summary>
        ///     如果内部不存在对应键的对象，则调用这个函数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual T OnGetByKeyIfNotExist( string key )
        {
            return EventUtils.FireEvent( GetByKeyIfNotExistHandler, key );
        }

        #region 变量和属性

        /// <summary>
        ///     键和对象的映射
        /// </summary>
        private readonly HashDictionary<string, T> _key2Object = new HashDictionary<string, T>();

        /// <summary>
        ///     内部锁
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        ///     对象的拥有者，用于资源释放
        /// </summary>
        public object Owner { get; set; }

        /// <summary>
        ///     对象个数
        /// </summary>
        public int Count
        {
            get { return _key2Object.Count; }
        }

        #endregion

        #region IObjectManager<T> Members

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns>集合</returns>
        public IList<T> GetAll()
        {
            lock( _lock )
            {
                return CollectionUtils.ToList( _key2Object.Values );
            }
        }

        /// <summary>
        ///     是否包含键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>true：包含键，false：不包含</returns>
        public bool ContainKey( string key )
        {
            return _key2Object.ContainsKey( key );
        }

        /// <summary>
        ///     Gets the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>对象</returns>
        public T GetByKey( string key )
        {
            lock( _lock )
            {
                if( ContainKey( key ) )
                {
                    return _key2Object[key];
                }

                var data = OnGetByKeyIfNotExist( key );
                if( data != null )
                {
                    SetByKey( key, data );
                }
                return data;
            }
        }

        /// <summary>
        ///     Gets the by key.
        /// </summary>
        /// <typeparam name="TSubclass">T的子类型</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T对象</returns>
        public TSubclass GetByKey<TSubclass>( string key )
            where TSubclass : class, T
        {
            return GetByKey( key ) as TSubclass;
        }

        /// <summary>
        ///     Sets the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The object.</param>
        public void SetByKey( string key, T data )
        {
            lock( _lock )
            {
                _key2Object[key] = data;
            }
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            lock( _lock )
            {
                var list = GetAll();
                _key2Object.Clear();

                foreach( var item in list )
                {
                    OnClear( item );
                }
            }
        }

        #endregion
    }
}