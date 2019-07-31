using System;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 来自VSN实验室的滤波器
    /// </summary>
    public static class VSNFilter
    {
        private static Double Bessel( Double alfa )
        {
            Double Y = alfa / 2, S = 1, D = 1;
            int k = 1;

            Double ret = 1;
            while( ret * 0.0000000001 < S )
            {
                D = D * Y / k;
                S = D * D;
                ret += S;
                k++;
            }

            return ret;
        }

        private static _ValueT[] Convolution( _ValueT[] data, _ValueT[] Coff, int N )
        {
            int Ij = 0, Jj = 0, N22 = N / 2;
            int length = data.Length;

            _ValueT[] ret = new _ValueT[length];
            _ValueT[] midData = new _ValueT[ret.Length + 1];

            for( int i = N22 + 1; i <= length + N22; i++ )
            {
                Ij++;
                midData[Ij] = 0;

                int Lim = i - 1;
                if( i > N )
                {
                    Lim = N - 1;
                }
                if( i > length )
                {
                    Jj = i - length;
                }

                for( int j = Jj; j <= Lim; j++ )
                {
                    midData[Ij] = midData[Ij] + Coff[j] * data[i - j - 1];
                }
            }

            for( int i = 0; i < ret.Length; i++ )
            {
                ret[i] = midData[i + 1];
            }

            return ret;
        }

        private static void WindowPara( Double ripple, out Double alfa, out Double D )
        {
            Double a = -20 * Math.Log10( ripple );
            if( a <= 21 )
            {
                alfa = 0;
                D = 0.9222;
            }
            else if( 21 < a && a <= 50 )
            {
                alfa = 0.5842 * Math.Pow( a - 21, 0.4 ) + 0.07886 * ( a - 21 );
                D = ( a - 7.95 ) / 14.36;
            }
            else // a > 50
            {
                alfa = 0.01102 * ( a - 8.7 );
                D = ( a - 7.95 ) / 14.36;
            }
        }

        /// <summary>
        /// 默认的波纹度
        /// </summary>
        private const Double DefaultRipple = 0.01;

        /// <summary>
        /// 带通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fs">采样频率</param>
        /// <param name="fl">低截止频率</param>
        /// <param name="fh">高截止频率</param>
        /// <param name="ripple">波纹度</param>
        /// <param name="Coff">系数数组，输出</param>
        /// <param name="order">滤波器阶数，输出</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] BandPass( _ValueT[] xArray, Double fs, Double fl, Double fh, Double ripple,
                                          out _ValueT[] Coff, out int order )
        {
            Double alfa;
            Double D;
            WindowPara( ripple, out alfa, out D );

            int N = (int)( D * fs / ( fh - fl ) + 1 );

            order = N;
            Double m = ( N - 1 ) / 2.0;

            Coff = new Double[N];

            Double I1 = Bessel( alfa );
            for( int index = 0; index < N; index++ )
            {
                Double beta = alfa * Math.Sqrt( 1 - Math.Pow( 1 - index / m, 2 ) );
                Double windowFunction = Bessel( beta ) / I1;

                Double pulseFunction;
                if( index == m )
                {
                    pulseFunction = 2 * ( fh - fl ) / fs;
                }
                else
                {
                    pulseFunction =
                        ( Math.Sin( MathConst.TwoPI * fh * ( index - m ) / fs ) -
                          Math.Sin( MathConst.TwoPI * fl * ( index - m ) / fs ) ) / ( index - m ) / MathConst.PI;
                }

                Coff[index] = windowFunction * pulseFunction;
            }

            return Convolution( xArray, Coff, N );
        }

        /// <summary>
        /// 带通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fs">采样频率</param>
        /// <param name="fl">低截止频率</param>
        /// <param name="fh">高截止频率</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] BandPass( _ValueT[] xArray, Double fs, Double fl, Double fh )
        {
            _ValueT[] Coff;
            int order;
            return BandPass( xArray, fs, fl, fh, DefaultRipple, out Coff, out order );
        }

        /// <summary>
        /// 轴心轨迹带通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fs">采样频率</param>
        /// <param name="f0">基频</param>
        /// <param name="fCenters">多个阶次的频率，可能包括基频，升序排列</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] OrbitBandPass( _ValueT[] xArray, Double fs, Double f0, Double[] fCenters )
        {
            if( fCenters == null || fCenters.Length == 0 )
            {
                return xArray;
            }

            Double leftOffset = 0.9 * f0 - f0;
            Double rightOffset = 1.3 * f0 - f0;

            var fls = new Double[fCenters.Length];
            var fhs = new Double[fCenters.Length];

            int index = 0;
            foreach( var fCenter in fCenters )
            {
                fls[index] = Math.Max( 0, fCenter + leftOffset );
                fhs[index] = fCenter + rightOffset;
                index++;
            }

            // 滤波并将波形合成
            var ret = BandPass( xArray, fs, fls[0], fhs[0] );
            for( int freqIndex = 1; freqIndex < fls.Length; freqIndex++ )
            {
                var filteredWave = BandPass( xArray, fs, fls[freqIndex], fhs[freqIndex] );
                for( int waveIndex = 0; waveIndex < ret.Length; waveIndex++ )
                {
                    ret[waveIndex] += filteredWave[waveIndex];
                }
            }

            return ret;
        }

        #region 低通滤波

        /// <summary>
        /// 低通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fs">采样频率</param>
        /// <param name="fh">高截止频率</param>
        /// <param name="ripple">波纹度</param>
        /// <param name="Coff">系数数组，输出</param>
        /// <param name="order">滤波器阶数，输出</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] LowPass( _ValueT[] xArray, Double fs, Double fh, Double ripple, out _ValueT[] Coff,
                                         out int order )
        {
            return BandPass( xArray, fs, 0, fh, ripple, out Coff, out order );
        }

        /// <summary>
        /// 低通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fs">采样频率</param>
        /// <param name="fh">高截止频率</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] LowPass( _ValueT[] xArray, Double fs, Double fh )
        {
            _ValueT[] Coff;
            int order;
            return LowPass( xArray, fs, fh, DefaultRipple, out Coff, out order );
        }

        #endregion

        #region 高通滤波

        /// <summary>
        /// 高通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fs">采样频率</param>
        /// <param name="fl">低截止频率</param>
        /// <param name="ripple">波纹度</param>
        /// <param name="Coff">系数数组，输出</param>
        /// <param name="order">滤波器阶数，输出</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] HighPass( _ValueT[] xArray, Double fs, Double fl, Double ripple, out _ValueT[] Coff,
                                          out int order )
        {
            return BandPass( xArray, fs, fl, fs / 2, ripple, out Coff, out order );
        }

        /// <summary>
        /// 高通滤波
        /// </summary>
        /// <param name="xArray">原始信号数组</param>
        /// <param name="fs">采样频率</param>
        /// <param name="fl">低截止频率</param>
        /// <returns>滤波后数组</returns>
        public static _ValueT[] HighPass( _ValueT[] xArray, Double fs, Double fl )
        {
            _ValueT[] Coff;
            int order;
            return HighPass( xArray, fs, fl, DefaultRipple, out Coff, out order );
        }

        #endregion
    }
}