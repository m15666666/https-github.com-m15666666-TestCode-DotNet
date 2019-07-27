using System;

namespace Moons.Common20.Pool
{
    /// <summary>
    /// 使用ObjectPoolItem对象包装内部对象的对象池的基类
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class PoolItemPoolBase<T> : PoolBase<ObjectPoolItem<T>> where T : class
    {
        #region 变量和属性

        /// <summary>
        /// 创建内部对象的代理
        /// </summary>
        public CreateObjectHandler<T> CreateInnerObjectHandler
        {
            set { CreateObjectHandler = ConvertCreateObjectHandler( value ); }
        }

        /// <summary>
        /// 初始化对象的代理
        /// </summary>
        public Action<T> InitializeInnerObjectHandler
        {
            set { InitializeHandler = ConvertActionHandler( value ); }
        }

        /// <summary>
        /// 取消初始化对象的代理
        /// </summary>
        public Action<T> UnInitializeInnerObjectHandler
        {
            set { UnInitializeHandler = ConvertActionHandler( value ); }
        }

        #endregion

        #region 维护池

        /// <summary>
        /// 转换CreateObjectHandler
        /// </summary>
        /// <param name="handler">创建内部对象的代理</param>
        /// <returns>创建ObjectPoolItem对象的代理</returns>
        private static CreateObjectHandler<ObjectPoolItem<T>> ConvertCreateObjectHandler(
            CreateObjectHandler<T> handler )
        {
            if( handler == null )
            {
                return null;
            }

            return () => new ObjectPoolItem<T> { Data = handler() };
        }

        /// <summary>
        /// 转换Action Handler
        /// </summary>
        /// <param name="handler">Action[T]</param>
        /// <returns>Action[ObjectPoolItem[T]]</returns>
        private static Action<ObjectPoolItem<T>> ConvertActionHandler(
            Action<T> handler )
        {
            if( handler == null )
            {
                return null;
            }

            return data => handler( data.Data );
        }

        #endregion
    }
}