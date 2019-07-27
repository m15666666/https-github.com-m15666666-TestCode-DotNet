using System;

namespace Moons.Common20.ObjectManagement
{
    /// <summary>
    ///     对象管理器接口
    /// </summary>
    /// <typeparam name="T">内部对象类型</typeparam>
    public interface IObjectManagerWithHandler<T> : IObjectManager<T> where T : class
    {
        /// <summary>
        ///     如果内部不存在对应键的对象，则调用这个代理
        /// </summary>
        Func20<string, T> GetByKeyIfNotExistHandler { get; set; }

        /// <summary>
        ///     清理内部对象的时候，调用这个代理
        /// </summary>
        Action<T> ClearHandler { get; set; }
    }
}