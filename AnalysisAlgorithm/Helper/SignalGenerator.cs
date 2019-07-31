using System;

namespace AnalysisAlgorithm.Helper
{
    /// <summary>
    ///     信号发生器类
    /// </summary>
    public class SignalGenerator
    {
        /// <summary>
        ///     创建正弦波
        /// </summary>
        /// <param name="createSinParameter">CreateSinParameter</param>
        /// <returns>正弦波</returns>
        public static Double[] CreateSin( CreateSinParameter createSinParameter )
        {
            var ret = new Double[createSinParameter.DataLength];

            double initPhase = createSinParameter.InitPhaseInDegree * MathConst.PI / MathConst.Deg_180;
            double amplitude = createSinParameter.Amplitude;
            double scale = 2 * Math.PI * createSinParameter.F0 / createSinParameter.Fs;
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = amplitude * Math.Sin( initPhase + scale * index );
            }

            return ret;
        }
    }
}