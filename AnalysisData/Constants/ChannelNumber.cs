namespace AnalysisData.Constants
{
    /// <summary>
    /// 通道号
    /// </summary>
    public static class ChannelNumber
    {
        /// <summary>
        /// 通道一编号
        /// </summary>
        public const byte One = 1;

        /// <summary>
        /// 通道二编号
        /// </summary>
        public const byte Two = 2;

        /// <summary>
        /// 二维通道号数组
        /// </summary>
        public static byte[] ChannelNumbers_2D
        {
            get { return _ChannelNumbers_2D; }
        }

        /// <summary>
        /// 二维通道号数组
        /// </summary>
        private static readonly byte[] _ChannelNumbers_2D = new[] { One, Two };
    }
}
