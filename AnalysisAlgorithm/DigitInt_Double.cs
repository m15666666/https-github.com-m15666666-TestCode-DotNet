using System;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using System.Diagnostics;
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 数字积分类
    /// </summary>
    public static partial class DigitInt
    {
        /// <summary>
        /// 积分
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="step">步长</param>
        /// <param name="output">输出</param>
        public static void Int( _ValueT[] input, _ValueT step, _ValueT[] output )
        {
            _ValueT sum = 0;
            output[0] = sum;
            var twoMean = 2 * StatisticsUtils.Mean( input );
            for( int index = 1; index < input.Length; index++ )
            {
                sum += ( input[index - 1] + input[index] - twoMean ) * step / 2;
                output[index] = sum;
            }

            DSPBasic.ACCoupling( output );
        }

        /// <summary>
        /// 积分
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="step">步长</param>
        /// <param name="count">积分次数</param>
        /// <param name="output">输出</param>
        public static void IntN( _ValueT[] input, _ValueT step, int count, _ValueT[] output )
        {
            Debug.Assert( 0 < input.Length && input.Length == output.Length );

            if( count <= 0 || 2 < count )
            {
                input.CopyTo( output, 0 );
                return;
            }

            if( count == 1 )
            {
                Int( input, step, output );
                return;
            }

            var localInput = new _ValueT[input.Length];
            while( 0 < count-- )
            {
                input.CopyTo( localInput, 0 );
                Int( localInput, step, output );
                input = output;
            }
        }

        /// <summary>
        /// 积分
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="count">积分次数</param>
        /// <param name="fl">低截止频率</param>
        /// <param name="fs">采样频率</param>
        /// <param name="output">输出</param>
        public static void IntN( _ValueT[] input, int count, Double fl, Double fs, _ValueT[] output )
        {
            IntN( input, 1 / fs, count, output );

            DigitalIIR.ButterworthHighPass( output, fl, 5, fs ).CopyTo( output, 0 );
        }
    }
}