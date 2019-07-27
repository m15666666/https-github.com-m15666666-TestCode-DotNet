namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 设置QueryInfoBag的接口
    /// </summary>
    public interface ISetQueryInfoBag
    {
        /// <summary>
        /// true：可以调用Set函数，false：不能调用Set函数
        /// </summary>
        bool CanSetQueryInfoBag { get; }

        /// <summary>
        /// 设置QueryInfoBag对象
        /// </summary>
        /// <param name="bag">设置QueryInfoBag对象</param>
        void Set( QueryInfoBag bag );
    }
}