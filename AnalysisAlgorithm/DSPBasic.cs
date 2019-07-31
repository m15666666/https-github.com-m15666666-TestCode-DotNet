using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 基本数字信号处理算法库
    /// </summary>
    public static partial class DSPBasic
    {
        #region 采样频率得到分析带宽、时间波形长度获得谱长度

        /// <summary>
        /// 采样频率的放大系数
        /// </summary>
        private const Single SampleFreqScale = 2.56f;

        /// <summary>
        /// 通过采样频率得到分析带宽
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <returns>分析带宽</returns>
        public static Double GetFreqBandBySampleFreq( Double fs )
        {
            return fs / SampleFreqScale;
        }

        /// <summary>
        /// 通过时间波形长度获得谱长度
        /// </summary>
        /// <param name="timeWaveLength">时间波形长度</param>
        /// <returns>谱长度</returns>
        public static int GetSpectrumLengthByTimeWaveLength( int timeWaveLength )
        {
            return (int)( timeWaveLength / SampleFreqScale + 1 );
        }

        /// <summary>
        /// 获得频率分辨率
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <param name="timeWaveLength">时间波形长度</param>
        /// <returns>频率分辨率</returns>
        public static Double GetDeltaFreq( Double fs, int timeWaveLength )
        {
            return fs / timeWaveLength;
        }

        #endregion

        #region 将分钟转速转换为频率(Hz)

        /// <summary>
        /// 将分钟转速转换为频率(Hz)
        /// </summary>
        /// <param name="rpm">分钟转速</param>
        /// <returns>频率(Hz)</returns>
        public static Double RpmtoHz( Double rpm )
        {
            return rpm / MathConst.SecondCountOfMinute;
        }

        /// <summary>
        /// 将频率(Hz)转换为分钟转速
        /// </summary>
        /// <param name="hz">频率(Hz)</param>
        /// <returns>分钟转速</returns>
        public static Double HztoRpm( Double hz )
        {
            return hz * MathConst.SecondCountOfMinute;
        }

        #endregion
    }
}