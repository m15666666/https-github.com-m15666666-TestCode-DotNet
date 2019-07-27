using System;

namespace Moons.Common20.Pool
{
    /// <summary>
    /// 操作池的包装器类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PoolWrapper<T> : IDisposable where T : class
    {
        /// <summary>
        /// 释放对象的代理，用于返回池不成功的情况下释放对象
        /// </summary>
        public Action<T> ReleaseHandler { get; set; }

        /// <summary>
        /// 池的引用
        /// </summary>
        public IPool<T> Pool { get; set; }

        /// <summary>
        /// 内部锁
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// 内部数据
        /// </summary>
        private T _innerData;

        /// <summary>
        /// 内部数据
        /// </summary>
        public T InnerData
        {
            get
            {
                if( _innerData == null )
                {
                    InitInnerData();
                }
                return _innerData;
            }
        }

        /// <summary>
        /// 初始化内部数据
        /// </summary>
        private void InitInnerData()
        {
            lock( _lock )
            {
                if( _innerData == null )
                {
                    _innerData = Pool.GetFromPool();
                }
            }
        }

        /// <summary>
        /// 重载隐式操作符
        /// </summary>
        /// <param name="wrapper">操作池的包装器类</param>
        /// <returns>池中的对象</returns>
        public static implicit operator T( PoolWrapper<T> wrapper )
        {
            return wrapper.InnerData;
        }

        public void Dispose()
        {
            T innerData = _innerData;
            if( innerData != null )
            {
                _innerData = null;

                // 首先尝试放入池，如果不成功，则尝试释放对象
                if( !Pool.Put2Pool( innerData ) )
                {
                    Action<T> release = ReleaseHandler;
                    if( release != null )
                    {
                        release( innerData );
                    }
                }
            }
        }
    }
}