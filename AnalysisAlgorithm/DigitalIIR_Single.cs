using System;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 数字IIR滤波器
    /// </summary>
    public static partial class DigitalIIR
    {
        /// <summary>
        /// Butterworth带通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fl">低截止频率</param>
        /// <param name="fh">高截止频率</param>
        /// <param name="order">滤波器阶数</param>
        /// <param name="fs">采样频率</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] ButterworthBandPass( _ValueT[] xArray, Double fl, Double fh, int order, Double fs )
        {
            int length = xArray.Length;
            //计算滤波器系数
            _ValueT t = (_ValueT)( 1 / fs );
            _ValueT wl = (_ValueT)( Math.Sin( fl * MathConst.PI * t ) / Math.Cos( fl * MathConst.PI * t ) );
            _ValueT wh = (_ValueT)( Math.Sin( fh * MathConst.PI * t ) / Math.Cos( fh * MathConst.PI * t ) );
            _ValueT wc = wh - wl;
            _ValueT q = wc * wc + 2.0f * wl * wh;
            _ValueT s = wl * wl * wh * wh;
            _ValueT[] FilterA = new _ValueT[order];
            _ValueT[] FilterB = new _ValueT[order];
            _ValueT[] FilterC = new _ValueT[order];
            _ValueT[] FilterD = new _ValueT[order];
            _ValueT[] FilterE = new _ValueT[order];
            _ValueT[] ret = new _ValueT[length];

            for( int index = 0; index < order; index++ )
            {
                _ValueT cs = (_ValueT)Math.Cos( ( 2.0 * ( index + 1.0 + order ) - 1.0 ) * MathConst.PI / 4.0 / order );
                _ValueT p = -2.0f * wc * cs;
                _ValueT r = p * wl * wh;
                _ValueT x = 1 + p + q + r + s;
                FilterA[index] = wc * wc / x;
                FilterB[index] = ( -4 - 2 * p + 2 * r + 4 * s ) / x;
                FilterC[index] = ( 6 - 2 * q + 6 * s ) / x;
                FilterD[index] = ( -4 + 2 * p - 2 * r + 4 * s ) / x;
                FilterE[index] = ( 1 - p + q - r + s ) / x;
            }

            // 开始滤波
            for( int index = 0; index < length; index++ )
            {
                ret[index] = xArray[index];
            }

            _ValueT[] MidVar = new _ValueT[length];
            for( int orderIndex = 0; orderIndex < order; orderIndex++ )
            {
                MidVar[0] = FilterA[orderIndex] * ret[0];
                MidVar[1] = FilterA[orderIndex] * ret[1] - FilterB[orderIndex] * MidVar[0];
                MidVar[2] = FilterA[orderIndex] * ( ret[2] - 2 * ret[0] ) - FilterB[orderIndex] * MidVar[1] -
                            FilterC[orderIndex] * MidVar[0];
                MidVar[3] = FilterA[orderIndex] * ( ret[3] - 2 * ret[1] ) - FilterB[orderIndex] * MidVar[2] -
                            FilterC[orderIndex] * MidVar[1] - FilterD[orderIndex] * MidVar[0];
                for( int index = 4; index < length; index++ )
                {
                    MidVar[index] = FilterA[orderIndex] * ( ret[index] - 2 * ret[index - 2] + ret[index - 4] ) -
                                    FilterB[orderIndex] * MidVar[index - 1] -
                                    FilterC[orderIndex] * MidVar[index - 2] - FilterD[orderIndex] * MidVar[index - 3] -
                                    FilterE[orderIndex] * MidVar[index - 4];
                }
                for( int index = 0; index < length; index++ )
                {
                    ret[index] = MidVar[index];
                }
            }

            return ret;
        }

        /// <summary>
        /// Butterworth高通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fl">低截止频率</param>
        /// <param name="order">滤波器阶数</param>
        /// <param name="fs">采样频率</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] ButterworthHighPass( _ValueT[] xArray, Double fl, int order, Double fs )
        {
            return ButterworthBandPass( xArray, fl, fs / 2, order, fs );
        }
    }
}