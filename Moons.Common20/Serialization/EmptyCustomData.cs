namespace Moons.Common20.Serialization
{
    /// <summary>
    ///     空的自定义数据
    /// </summary>
    public class EmptyCustomData : CustomDataBase
    {
    }

    /// <summary>
    ///     json格式的自定义数据
    /// </summary>
    public class JsonCustomData : CustomDataBase
    {
        /// <summary>
        /// json格式的字符串
        /// </summary>
        public string Text { get; set; }
    }
}