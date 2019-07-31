namespace AnalysisAlgorithm
{
    /// <summary>
    /// 信号窗类型
    /// </summary>
    public enum WindowType
    {
        /// <summary>
        /// 矩形窗
        /// </summary>
        Rectangular,

        /// <summary>
        /// 三角窗
        /// </summary>
        Triangular,

        /// <summary>
        /// 汉宁窗
        /// </summary>
        Hanning,

        /// <summary>
        /// 哈明窗
        /// </summary>
        Hamming,

        /// <summary>
        /// 布莱克曼窗
        /// </summary>
        Blackman,

        /// <summary>
        /// 平顶窗
        /// </summary>
        FlatTop,

        /// <summary>
        /// 指数窗
        /// </summary>
        Exponential,

        /// <summary>
        /// 力窗
        /// </summary>
        Force,
    } ;
}