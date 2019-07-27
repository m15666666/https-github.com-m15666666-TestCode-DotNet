namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 返回的字段
    /// </summary>
    public class ReturnField
    {
        /// <summary>
        /// 属于的表对象
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
    }
}