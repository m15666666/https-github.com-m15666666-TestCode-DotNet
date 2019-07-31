using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 信号窗类
    /// </summary>
    public static class Window
    {
        #region 恢复系数

        /// <summary>
        /// 矩形窗幅值相等恢复系数
        /// </summary>
        public const float AmpScale_Rectangle = 1f;

        /// <summary>
        /// 矩形窗功率相等恢复系数
        /// </summary>
        public const float PowerScale_Rectangle = 1f;

        /// <summary>
        /// 汉宁窗幅值相等恢复系数
        /// </summary>
        public const float AmpScale_Hanning = 2f;

        /// <summary>
        /// 汉宁窗功率相等恢复系数
        /// </summary>
        public const float PowerScale_Hanning = 1.633f;

        /// <summary>
        /// 哈明窗幅值相等恢复系数
        /// </summary>
        public const float AmpScale_Hamming = 1.852f;

        /// <summary>
        /// 哈明窗功率相等恢复系数
        /// </summary>
        public const float PowerScale_Hamming = 1.586f;

        /// <summary>
        /// 获得窗幅值相等恢复系数
        /// </summary>
        /// <param name="windowType">窗类型</param>
        /// <returns>窗幅值相等恢复系数</returns>
        public static float GetAmpScale( WindowType windowType )
        {
            switch( windowType )
            {
                case WindowType.Hanning:
                    return AmpScale_Hanning;

                case WindowType.Hamming:
                    return AmpScale_Hamming;
            }
            return AmpScale_Rectangle;
        }

        /// <summary>
        /// 获得窗功率相等恢复系数
        /// </summary>
        /// <param name="windowType">窗类型</param>
        /// <returns>窗功率相等恢复系数</returns>
        public static float GetPowerScale( WindowType windowType )
        {
            switch( windowType )
            {
                case WindowType.Hanning:
                    return PowerScale_Hanning;

                case WindowType.Hamming:
                    return PowerScale_Hamming;
            }
            return PowerScale_Rectangle;
        }

        #endregion

        #region 生成信号窗

        /// <summary>
        /// 生成一个余弦型的信号窗
        /// </summary>
        /// <param name="winType">信号窗类型</param>
        /// <param name="length">信号窗长度</param>
        /// <returns>返回指定窗的一维数组</returns>
        /// <remarks>
        /// 参考: MathWorks, gencoswin.m, Matlab 7.0.0, 1988-2004.
        /// http://zh.wikipedia.org/wiki/%E7%AA%97%E5%87%BD%E6%95%B0
        /// </remarks>
        public static Single[] GenCosWindows( WindowType winType, int length )
        {
            Double a0 = 0, a1 = 0, a2 = 0, a3 = 0, a4 = 0;

            switch( winType )
            {
                case WindowType.Hanning:
                    // Hann window
                    // w[i] = 0.5 * (1 - cos(2*pi*i/n)); 
                    a0 = 0.5;
                    a1 = 0.5;
                    a2 = 0;
                    a3 = 0;
                    a4 = 0;
                    break;
                case WindowType.Hamming:
                    // Hamming window
                    // w[i] = (54 - 46*cos(2*pi*i/n))/100;
                    a0 = 0.54;
                    a1 = 0.46;
                    a2 = 0;
                    a3 = 0;
                    a4 = 0;
                    break;
                case WindowType.Blackman:
                    // Blackman window
                    // w[i] = (42 - 50*cos(2*pi*i/n) + 8*cos(4*pi*i/n))/100;
                    a0 = 0.42;
                    a1 = 0.5;
                    a2 = 0.08;
                    a3 = 0;
                    a4 = 0;
                    break;
                case WindowType.FlatTop:
                    // Flattop window
                    // Original coefficients as defined in the reference (see flattopwin.m);
                    // a0 = 1;
                    // a1 = 1.93;
                    // a2 = 1.29;
                    // a3 = 0.388;
                    // a4 = 0.032;
                    // Scaled by (a0+a1+a2+a3+a4)
                    a0 = 0.2156;
                    a1 = 0.4160;
                    a2 = 0.2781;
                    a3 = 0.0836;
                    a4 = 0.0069;
                    break;
            }

            // N - 1
            int n_1 = length - 1;

            var win = new Single[length];
            for( int index = 0; index < length; index++ )
            {
                Double scale = MathConst.PI * index / n_1;
                win[index] = (Single)
                             ( a0 -
                               a1 * Math.Cos( 2 * scale ) +
                               a2 * Math.Cos( 4 * scale ) -
                               a3 * Math.Cos( 6 * scale ) +
                               a4 * Math.Cos( 8 * scale )
                             );
            }
            return win;
        }

        /// <summary>
        /// 生成一个三角窗
        /// </summary>
        /// <param name="length">信号窗长度</param>
        /// <returns>返回三角窗的一维数组</returns>
        /// <remarks>
        /// 参考: NI, Windowing Functions部分, ComponentWorks 1.0 帮助.
        /// </remarks>
        //	计算公式如下: 
        //				w[i] = 1 - abs(2*i-n)/n); 
        public static Single[] GenTriWindow( int length )
        {
            var win = new Single[length];
            for( int index = 0; index < length; index++ )
            {
                win[index] = 1 - Math.Abs( 2 * index - length ) / length;
            }
            return win;
        }

        /// <summary>
        /// 生成一个指数窗
        /// </summary>
        /// <param name="length">信号窗长度</param>
        /// <param name="final">指数窗终值(推荐final=0.01)</param>
        /// <returns>返回指数窗的一维数组</returns>
        /// <remarks>
        /// 参考: NI, Windowing Functions部分, ComponentWorks 1.0 帮助.
        /// </remarks>
        //	计算公式如下: 
        //				w[i] = exp(i*ln(FinalValue/(n-1))); 
        public static Single[] GenExpWindow( int length, Single final )
        {
            var win = new Single[length];
            for( int index = 0; index < length; index++ )
            {
                win[index] = (Single)Math.Exp( index * Math.Log( final / ( length - 1 ) ) );
            }
            return win;
        }

        /// <summary>
        /// 生成一个力窗
        /// </summary>
        /// <param name="length">信号窗长度</param>
        /// <param name="duty">延迟的百分比(推荐duty=50)</param>
        /// <returns>返回力窗的一维数组</returns>
        /// <remarks>
        /// 参考: NI, Windowing Functions部分, ComponentWorks 1.0 帮助.
        /// </remarks>
        //	计算公式如下: 
        //	n = number of elements in the input signal     
        //  m = int((duty / 100) * n) 
        //
        //	For 0 <= i <= m, w[i] = 1; 
        //	For all other i in 0,1,...,n-1, w[i] = 0; 
        public static Single[] GenForceWindow( int length, Single duty )
        {
            var win = new Single[length];
            Single m = (int)( ( duty / 100 ) * length );
            for( int index = 0; index < length; index++ )
            {
                win[index] = ( index <= m ? 1 : 0 );
            }
            return win;
        }

        #endregion

        #region 应用窗函数

        /// <summary>
        /// 应用窗函数
        /// </summary>
        /// <param name="wave">输入(波形)，输出(应用了窗函数的波形)</param>
        /// <param name="win">窗系数</param>
        public static void ApplyWindow( Double[] wave, Single[] win )
        {
            for( int index = 0; index < wave.Length; index++ )
            {
                wave[index] *= win[index];
            }
        }

        /// <summary>
        /// 应用窗函数
        /// </summary>
        /// <param name="wave">输入(波形)，输出(应用了窗函数的波形)</param>
        /// <param name="win">窗系数</param>
        public static void ApplyWindow( Single[] wave, Single[] win )
        {
            for( int index = 0; index < wave.Length; index++ )
            {
                wave[index] *= win[index];
            }
        }

        /// <summary>
        /// 应用窗函数
        /// </summary>
        /// <param name="wave">输入(波形)，输出(应用了窗函数的波形)</param>
        /// <param name="win">窗系数</param>
        public static void ApplyWindow( Int32[] wave, Single[] win )
        {
            for( int index = 0; index < wave.Length; index++ )
            {
                wave[index] = (Int32)( wave[index] * win[index] );
            }
        }

        #endregion
    }
}