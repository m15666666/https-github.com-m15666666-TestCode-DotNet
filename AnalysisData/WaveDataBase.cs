namespace AnalysisData
{
    public abstract class WaveDataBase : TrendData
    {
        /// <summary>
        /// 采样频率
        /// </summary>
        public double SampleFreq { get; set; }

        /// <summary>
        /// 倍频系数
        /// </summary>
        public int MultiFreq { get; set; }
    }
}
