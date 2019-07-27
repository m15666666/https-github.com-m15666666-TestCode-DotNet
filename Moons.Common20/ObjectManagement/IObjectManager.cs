using System.Collections.Generic;

namespace Moons.Common20.ObjectManagement
{
    /// <summary>
    ///     对象管理器接口
    /// </summary>
    /// <typeparam name="T">内部对象类型</typeparam>
    public interface IObjectManager<T> where T : class
    {
        /// <summary>
        ///     IObjectManager的拥有者，用于资源释放
        /// </summary>
        object Owner { get; set; }

        /// <summary>
        ///     对象个数
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns>T集合</returns>
        IList<T> GetAll();

        /// <summary>
        ///     是否包含键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>true：包含键，false：不包含</returns>
        bool ContainKey( string key );

        /// <summary>
        ///     Gets the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>T</returns>
        T GetByKey( string key );

        /// <summary>
        ///     Gets the by key.
        /// </summary>
        /// <typeparam name="TSubclass">T的子类型</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T对象</returns>
        TSubclass GetByKey<TSubclass>( string key ) where TSubclass : class, T;

        /// <summary>
        ///     Sets the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The object.</param>
        void SetByKey( string key, T data );

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        void Clear();
    }
}