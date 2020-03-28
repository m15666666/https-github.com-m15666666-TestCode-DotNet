namespace Moons.Plugin.Contract
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IPlugIn : IPlugInContainer
    {
        /// <summary>
        /// 插件容器
        /// </summary>
        IPlugInContainer PlugInContainer { get; set; }
    }
}