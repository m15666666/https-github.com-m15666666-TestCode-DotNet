namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 排序的基类
    /// </summary>
    public abstract class OrderBase
    {
        /// <summary>
        /// 属于的表对象
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
    }
}