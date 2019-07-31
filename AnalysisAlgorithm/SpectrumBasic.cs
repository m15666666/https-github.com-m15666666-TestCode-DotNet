using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 与频谱相关的基本算法类
    /// </summary>
    public static partial class SpectrumBasic
    {
        /// <summary>
        /// 获得指定频率处的下标
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <param name="freq">指定频率</param>
        /// <param name="timeWaveLength">时间波形长度</param>
        /// <returns>指定频率处的下标</returns>
        public static int GetFreqIndex( Double fs, Double freq, int timeWaveLength )
        {
            return (int)( freq / DSPBasic.GetDeltaFreq( fs, timeWaveLength ) );
        }

        /// <summary>
        /// 根据下标获得频率值
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <param name="index">下标</param>
        /// <param name="timeWaveLength">时间波形长度</param>
        /// <returns>指定下标的频率值</returns>
        public static Double GetFreqByIndex( Double fs, int index, int timeWaveLength )
        {
            return DSPBasic.GetDeltaFreq( fs, timeWaveLength ) * index;
        }
    }
}