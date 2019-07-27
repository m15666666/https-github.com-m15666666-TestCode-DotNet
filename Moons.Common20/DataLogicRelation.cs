namespace Moons.Common20
{
    /// <summary>
    /// 数据的逻辑关系
    /// </summary>
    public enum DataLogicRelation
    {
        /// <summary>
        /// 相等
        /// </summary>
        Equal = 1,

        /// <summary>
        /// 大于
        /// </summary>
        Great = Equal + 1,

        /// <summary>
        /// 小于
        /// </summary>
        Less = Great + 1,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreatEqual = Less + 1,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessEqual = GreatEqual + 1,

        /// <summary>
        /// like，例如："%abc%"
        /// </summary>
        Like = LessEqual + 1,

        /// <summary>
        /// like，例如："%abc"
        /// </summary>
        PrefixLike = Like + 1,

        /// <summary>
        /// like，例如："abc%"
        /// </summary>
        SuffixLike = PrefixLike + 1,

        /// <summary>
        /// 不等
        /// </summary>
        NotEqual = SuffixLike + 1,

        /// <summary>
        /// In
        /// </summary>
        In = NotEqual + 1,

        /// <summary>
        /// NotIn
        /// </summary>
        NotIn = In + 1,
    }
}