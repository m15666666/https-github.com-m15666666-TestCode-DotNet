using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 数学常数类
    /// </summary>
    public static class MathConst
    {
        /// <summary>
        /// FFT最大的长度为2^30
        /// </summary>
        public const int FFTPowerMax = 30;

        /// <summary>
        /// 2的平方根
        /// </summary>
        public const Single SqrtTwo = 1.4142135623730950488016887242097f;

        /// <summary>
        /// 用于避免除0的最小值为10^-6
        /// </summary>
        public const Single epsilon = 1E-6f;

        #region 角度

        /// <summary>
        /// 2*PI
        /// </summary>
        public const Double TwoPI = 6.2831853071795865f;

        /// <summary>
        /// PI
        /// </summary>
        public const Double PI = 3.14159265359f;

        /// <summary>
        /// 0°
        /// </summary>
        public const int Deg_0 = 0;

        /// <summary>
        /// 90°
        /// </summary>
        public const int Deg_90 = 90;

        /// <summary>
        /// 180°
        /// </summary>
        public const int Deg_180 = 180;

        /// <summary>
        /// 270°
        /// </summary>
        public const int Deg_270 = 270;

        /// <summary>
        /// 360°
        /// </summary>
        public const int Deg_360 = 360;

        #endregion

        #region 时间

        /// <summary>
        /// 一分钟之内的秒数（60）
        /// </summary>
        public const int SecondCountOfMinute = 60;

        #endregion

        #region 计算对数的两个系数

        /// <summary>
        /// 计算对数的两个系数
        /// </summary>
        public const int LogScale_10 = 10;

        /// <summary>
        /// 计算对数的两个系数
        /// </summary>
        public const int LogScale_20 = 20;

        #endregion
    }
}