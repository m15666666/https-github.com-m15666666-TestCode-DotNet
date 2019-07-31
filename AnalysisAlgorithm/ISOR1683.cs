using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ISOR1683对数级基准值()
    /// </summary>
    /// <remarks>
    /// 参考: 佟德纯,《振动监测与诊断》,1997.
    /// </remarks>
    internal static class ISOR1683
    {
        /// <summary>
        /// 振动力基准值(单位：N)
        /// </summary>
        public const Double f0 = 1.0E-6;
        /// <summary>
        /// 振动位移基准值(单位：m)
        /// </summary>
        public const Double d0 = 1.0E-12;
        /// <summary>
        /// 振动速度基准值(单位：m/s)
        /// </summary>
        public const Double v0 = 1.0E-5;
        /// <summary>
        /// 振动加速度基准值(单位：m/s^2)
        /// </summary>
        public const Double a0 = 1.0E-6;
    }
}
