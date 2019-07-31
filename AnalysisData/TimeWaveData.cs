using System;

namespace AnalysisData
{
    /// <summary>
    /// 时间波形数据
    /// </summary>
    [Serializable]
    public class TimeWaveData : WaveDataBase
    {
        /// <summary>
        /// 时间波形
        /// </summary>
        public double[] Wave;
    }
}
