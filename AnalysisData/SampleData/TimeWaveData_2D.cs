using System;

namespace AnalysisData.SampleData
{
    [Serializable]
    public class TimeWaveData_2D : WaveDataBase
    {
        /// <summary>
        /// 以Int16表示的时间波形
        /// </summary>
        private readonly ShortWaveData _shortWaveData_1D = new ShortWaveData();

        /// <summary>
        /// 以Int16表示的时间波形
        /// </summary>
        private readonly ShortWaveData _shortWaveData_2D = new ShortWaveData();

        /// <summary>
        /// 通道一以Int16表示的时间波形
        /// </summary>
        public ShortWaveData ShortWaveData_1D
        {
            get { return _shortWaveData_1D; }
        }

        /// <summary>
        /// 通道二以Int16表示的时间波形
        /// </summary>
        public ShortWaveData ShortWaveData_2D
        {
            get { return _shortWaveData_2D; }
        }

        /// <summary>
        /// 通道一时间波形
        /// </summary>
        public double[] Wave_1D
        {
            get { return _shortWaveData_1D.DoubleWave; }
            set { _shortWaveData_1D.DoubleWave = value; }
        }

        /// <summary>
        /// 通道二时间波形
        /// </summary>
        public double[] Wave_2D
        {
            get { return _shortWaveData_2D.DoubleWave; }
            set { _shortWaveData_2D.DoubleWave = value; }
        }

        /// <summary>
        /// 通道一特征指标
        /// </summary>
        public FeatureData Feature_1D { get; set; }

        /// <summary>
        /// 通道一特征指标
        /// </summary>
        public FeatureData Feature_2D { get; set; }
    }
}