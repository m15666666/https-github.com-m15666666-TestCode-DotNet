namespace Moons.Common20.QueryData
{
    /// <summary>
    /// Like条件
    /// </summary>
    public class LikeCondition : QueryConditionBase
    {
        public LikeCondition()
        {
            AppendPrefixChar  = false;
            AppendSuffixChar = true;
        }

        /// <summary>
        ///  是否追加前缀字符
        /// </summary>
        public bool AppendPrefixChar { get; set; }

        /// <summary>
        ///  是否追加后缀字符
        /// </summary>
        public bool AppendSuffixChar { get; set; }
    }
}