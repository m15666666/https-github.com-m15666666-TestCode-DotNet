namespace Moons.Common20.BaseClass
{
    /// <summary>
    /// 获取控件值
    /// </summary>
    public interface IGetControlValue
    {
        /// <summary>
        /// 值
        /// </summary>
        object Value { get; }

        /// <summary>
        /// 控件数据类型
        /// </summary>
        ControlDataType DataType { get; set; }
    }
}