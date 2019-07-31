using System;
using AnalysisData.Helper;
using Moons.Common20.Serialization;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 以Int16表示的时间波形
    /// </summary>
    [Serializable]
    public class ShortWaveData
    {
        #region 变量和属性

        /// <summary>
        /// 以double保存的波形
        /// </summary>
        private double[] _doubleWave;

        /// <summary>
        /// 以Int16保存的波形
        /// </summary>
        public short[] ShortWave { get; set; }

        /// <summary>
        /// 波形的比例系数
        /// </summary>
        public double WaveScale { get; set; }

        /// <summary>
        /// 整形数组波形
        /// </summary>
        public VarByteArrayWave IntArrayWave
        {
            set { _doubleWave = value.ToDoubleWave; }
        }

        /// <summary>
        /// 以double保存的波形
        /// </summary>
        public double[] DoubleWave
        {
            get
            {
                if( _doubleWave == null && ShortWave != null )
                {
                    _doubleWave = CompressDataUtils.ShortWave2Double( ShortWave, WaveScale );
                }

                return _doubleWave;
            }
            set { _doubleWave = value; }
        }

        #endregion

        /// <summary>
        /// 初始化以Int16保存的波形、波形的比例系数
        /// </summary>
        public void InitShortWave()
        {
            if( _doubleWave != null )
            {
                short[] shortWave;
                double waveScale;

                CompressDataUtils.DoubleWave2Short( _doubleWave, out shortWave, out waveScale );
                ShortWave = shortWave;
                WaveScale = waveScale;
            }
        }
    }
}