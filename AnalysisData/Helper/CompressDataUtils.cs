using AnalysisAlgorithm;

namespace AnalysisData.Helper
{
    /// <summary>
    /// 压缩数据的实用工具
    /// </summary>
    public static class CompressDataUtils
    {
        #region 波形转换

        /// <summary>
        /// 短整型的最大值
        /// </summary>
        private const short ShortMax = short.MaxValue;

        /// <summary>
        /// 为了保证精度，波形最大幅值的放大系数
        /// </summary>
        private const double WaveAmpInflateScale = 1.005;

        /// <summary>
        /// 获得波形放大后的幅值
        /// </summary>
        /// <param name="absMax">波形绝对值的最大值</param>
        /// <returns>波形放大后的幅值</returns>
        private static double InflateWaveAmp( double absMax )
        {
            // 保证不大于32767
            return absMax * WaveAmpInflateScale;
        }

        /// <summary>
        /// 获得波形的比例系数
        /// </summary>
        /// <param name="absMax">波形绝对值的最大值</param>
        /// <returns>波形的比例系数</returns>
        public static double GetWaveScale( double absMax )
        {
            return InflateWaveAmp( absMax ) / ShortMax;
        }

        /// <summary>
        /// 将double类型的波形转换为short类型的波形
        /// </summary>
        /// <param name="inWave">double类型的波形</param>
        /// <param name="absMax">波形绝对值的最大值</param>
        /// <returns>short类型的波形</returns>
        public static short[] DoubleWave2Short( double[] inWave, double absMax )
        {
            double max = InflateWaveAmp( absMax );

            bool maxIsZero = max <= double.Epsilon;

            var ret = new short[inWave.Length];
            for( int index = 0; index < inWave.Length; index++ )
            {
                ret[index] = (short)( maxIsZero ? 0 : ( inWave[index] / max * ShortMax ) );
            }

            return ret;
        }

        /// <summary>
        /// 将double类型的波形转换为short类型的波形
        /// </summary>
        /// <param name="inWave">double类型的波形</param>
        /// <param name="outWave">short类型的波形</param>
        /// <param name="waveScale">波形的比例系数</param>
        public static void DoubleWave2Short( double[] inWave, out short[] outWave, out double waveScale )
        {
            double absMax = NumbersUtils.AbsMax( inWave );

            waveScale = GetWaveScale( absMax );

            outWave = DoubleWave2Short( inWave, absMax );
        }

        /// <summary>
        /// 将short类型的波形转换为double类型的波形
        /// </summary>
        /// <param name="inWave">short类型的波形</param>
        /// <param name="waveScale">波形的比例系数</param>
        /// <returns>double类型的波形</returns>
        public static double[] ShortWave2Double( short[] inWave, double waveScale )
        {
            if( inWave == null )
            {
                return null;
            }

            var ret = new double[inWave.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = inWave[index] * waveScale;
            }

            return ret;
        }

        #endregion
    }
}