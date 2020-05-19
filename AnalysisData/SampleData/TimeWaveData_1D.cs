using System;

namespace AnalysisData.SampleData
{
    [Serializable]
    public class TimeWaveData_1D : WaveDataBase
    {
        /// <summary>
        /// 以Int16表示的时间波形
        /// </summary>
        private readonly ShortWaveData _shortWaveData = new ShortWaveData();

        /// <summary>
        /// 以Int16表示的时间波形
        /// </summary>
        public ShortWaveData ShortWaveData
        {
            get { return _shortWaveData; }
        }

        /// <summary>
        /// 时间波形
        /// </summary>
        public double[] Wave
        {
            get { return _shortWaveData.DoubleWave; }
            set { _shortWaveData.DoubleWave = value; }
        }

        /// <summary>
        /// 特征指标
        /// </summary>
        public FeatureData Feature { get; set; }

        #region ToString

        public override string ToString()
        {
            return $"Timewave1: {base.ToString()}";
        }

        #endregion
    }
}