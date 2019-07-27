namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 两个条件组的基类
    /// </summary>
    public abstract class BinaryConditionGroupBase : ConditionGroupBase
    {
        /// <summary>
        /// 左面的条件
        /// </summary>
        public QueryConditionBase Left { get; set; }

        /// <summary>
        /// 右面的条件
        /// </summary>
        public QueryConditionBase Right { get; set; }
    }
}