namespace Moons.Plugin.Contract
{
    /// <summary>
    /// 插件容器接口
    /// </summary>
    public interface IPlugInContainer
    {
        /// <summary>
        /// 通过键设置值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void SetValueByKey( string key, object value );

        /// <summary>
        /// 通过键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        object GetValueByKey( string key );
    }
}