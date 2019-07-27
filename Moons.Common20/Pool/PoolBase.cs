using System;
using System.Collections.Generic;

namespace Moons.Common20.Pool
{
    /// <summary>
    /// 将对象放回池的代理
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <returns>是否成功将对象放回池</returns>
    public delegate bool Put2PoolHandler<T>( T obj );

    /// <summary>
    /// 对象池的接口
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public interface IPool<T> where T : class
    {
        /// <summary>
        /// 池的大小
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// 空闲的对象数量
        /// </summary>
        int AvailableCount { get; }

        /// <summary>
        /// 从池中获得对象
        /// </summary>
        /// <returns>对象</returns>
        T GetFromPool();

        /// <summary>
        /// 将对象放到池中
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否放到了池中，true：放入池中，false：未放入池</returns>
        bool Put2Pool( T obj );

        /// <summary>
        /// 清空池
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// 对象池项的接口
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public interface IObjectPoolItem<T>
    {
        /// <summary>
        /// 是否在池中
        /// </summary>
        bool InPool { get; set; }

        /// <summary>
        /// 释放对象的代理
        /// </summary>
        Put2PoolHandler<T> Put2Pool { get; set; }
    }

    /// <summary>
    /// 对象池项
    /// </summary>
    /// <typeparam name="T">包装的对象类型</typeparam>
    public class ObjectPoolItem<T> : IObjectPoolItem<ObjectPoolItem<T>>, IDisposable
        where T : class
    {
        #region 变量和属性

        /// <summary>
        /// 内部包含的数据
        /// </summary>
        public T Data { get; set; }

        #endregion

        #region Implementation of IObjectPoolItem<ObjectPoolItem<T>>

        /// <summary>
        /// 是否在池中
        /// </summary>
        public bool InPool { get; set; }

        /// <summary>
        /// 释放对象的代理
        /// </summary>
        public Put2PoolHandler<ObjectPoolItem<T>> Put2Pool { get; set; }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            Put2PoolHandler<ObjectPoolItem<T>> put2Pool = Put2Pool;
            if( put2Pool != null )
            {
                // 尝试将对象返回池，未返回池则将对象释放
                put2Pool( this );
            }

            // 如果已经返回池，则不释放资源
            if( !InPool )
            {
                var disposable = Data as IDisposable;
                if( disposable != null )
                {
                    disposable.Dispose();
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 对象池的基类
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class PoolBase<T> : ObjectPoolBase<T> where T : class, IObjectPoolItem<T>
    {
        #region 维护池

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="itemFromPool">从池中取出的对象</param>
        protected override void OnInitialize( T itemFromPool )
        {
            itemFromPool.InPool = false;

            base.OnInitialize( itemFromPool );

            itemFromPool.Put2Pool = Put2Pool;
        }

        /// <summary>
        /// 取消初始化对象
        /// </summary>
        /// <param name="itemToPool">将被放入池中的对象</param>
        protected override void OnUnInitialize( T itemToPool )
        {
            base.OnUnInitialize( itemToPool );

            itemToPool.InPool = true;
        }

        #endregion
    }

    /// <summary>
    /// 对象池的基类
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class ObjectPoolBase<T> : IPool<T> where T : class
    {
        #region 变量和属性

        /// <summary>
        /// 空闲的对象
        /// </summary>
        private readonly Stack<T> _available = new Stack<T>();

        /// <summary>
        /// 表示对象是否已经存在
        /// </summary>
        private readonly Dictionary<int, bool> _hashcode2Exist = new Dictionary<int, bool>( 1024 );

        /// <summary>
        /// 内部锁对象
        /// </summary>
        private readonly object _lock = new object();

        public ObjectPoolBase()
        {
            Size = 10;

            IncrementCount = 0;
        }

        /// <summary>
        /// 创建对象的代理
        /// </summary>
        public CreateObjectHandler<T> CreateObjectHandler { get; set; }

        /// <summary>
        /// 初始化对象的代理
        /// </summary>
        public Action<T> InitializeHandler { get; set; }

        /// <summary>
        /// 取消初始化对象的代理
        /// </summary>
        public Action<T> UnInitializeHandler { get; set; }

        /// <summary>
        /// 递增的数量，默认为零
        /// </summary>
        public int IncrementCount { get; set; }

        /// <summary>
        /// 池的大小，默认为10
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 空闲的对象数量
        /// </summary>
        public int AvailableCount
        {
            get { return _available.Count; }
        }

        #endregion

        #region 维护池

        /// <summary>
        /// 从池中获得对象
        /// </summary>
        /// <returns>对象</returns>
        public T GetFromPool()
        {
            T ret = null;
            lock( _lock )
            {
                if( 0 < _available.Count )
                {
                    ret = _available.Pop();

                    _hashcode2Exist.Remove( ret.GetHashCode() );
                }
            }

            ret = ret ?? CreateObject();

            if( ret != null )
            {
                OnInitialize( ret );
            }

            return ret;
        }

        /// <summary>
        /// 将对象放到池中
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否放到了池中，true：放入池中，false：未放入池</returns>
        public bool Put2Pool( T obj )
        {
            if( obj == null )
            {
                return false;
            }

            lock( _lock )
            {
                if( AlreadyExist( obj ) )
                {
                    return true;
                }

                if( !ReachMaxSize() )
                {
                    OnUnInitialize( obj );

                    _available.Push( obj );

                    _hashcode2Exist[obj.GetHashCode()] = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 清空池
        /// </summary>
        public void Clear()
        {
            lock( _lock )
            {
                _available.Clear();
                _hashcode2Exist.Clear();
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="itemFromPool">从池中取出的对象</param>
        protected virtual void OnInitialize( T itemFromPool )
        {
            EventUtils.FireEvent( InitializeHandler, itemFromPool );
        }

        /// <summary>
        /// 取消初始化对象
        /// </summary>
        /// <param name="itemToPool">将被放入池中的对象</param>
        protected virtual void OnUnInitialize( T itemToPool )
        {
            EventUtils.FireEvent( UnInitializeHandler, itemToPool );
        }

        /// <summary>
        /// 是否达到了池的上限
        /// </summary>
        /// <returns>true：达到了池的上限，false：没达到池的上限</returns>
        private bool ReachMaxSize()
        {
            int availableCount = AvailableCount;
            if( 0 < IncrementCount && Size <= availableCount )
            {
                Size += IncrementCount;
            }

            return Size <= availableCount;
        }

        /// <summary>
        /// 对象在池中是否已经存在
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>true：已存在，false：不存在</returns>
        protected virtual bool AlreadyExist( T obj )
        {
            return _hashcode2Exist.ContainsKey( obj.GetHashCode() );
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>对象</returns>
        private T CreateObject()
        {
            return EventUtils.FireEvent( CreateObjectHandler );
        }

        #endregion
    }
}