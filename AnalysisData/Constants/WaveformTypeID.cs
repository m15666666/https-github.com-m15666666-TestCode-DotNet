namespace AnalysisData.Constants
{
    /// <summary>
    /// 时间波形类型ID
    /// </summary>
    public static class WaveformTypeID
    {
        /// <summary>
        /// 正常时间波形
        /// </summary>
        public const int Normal = 0;

        /// <summary>
        /// 解调时间波形
        /// </summary>
        public const int Demod = 3;

        /// <summary>
        /// 高频
        /// </summary>
        public const int HighFreq = 5;

        /// <summary>
        /// Packs的加速度时间波形
        /// </summary>
        public const int PacksAcceleration = 102;

        /// <summary>
        /// Packs的速度时间波形
        /// </summary>
        public const int PacksVelocity = 101;

        /// <summary>
        /// Packs的位移时间波形s
        /// </summary>
        public const int PacksDisplacement = 103;
    }
}