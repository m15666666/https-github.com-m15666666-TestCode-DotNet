using System;
using System.Runtime.InteropServices;
using Moons.Common20;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 与小波相关的算法类
    /// </summary>
    public static class Wavelet
    {
        #region 包装WVLET.DLL动态库

        /// <summary>
        /// 小波算法DLL
        /// </summary>
        private const string DLL_WVLET = "WVLET.DLL";

        [DllImport( DLL_WVLET, EntryPoint = "Get_h_mallat" )]
        private static extern int Get_h_mallat( float[] LowFreqArray, float[] HighFreqArray );

        [DllImport( DLL_WVLET, EntryPoint = "Decomposition" )]
        private static extern int Decomposition( int length, float[] dataIn, float[] dataOut, int startIndex,
                                                 int endIndex, float[] hg );

        private static void Decomposition( float[] dataIn, float[] dataOut, int startIndex, int endIndex, float[] hg )
        {
            Decomposition( dataIn.Length, dataIn, dataOut, startIndex, endIndex, hg );
        }

        #endregion

        /// <summary>
        /// 小波分解
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <returns>分解的结果</returns>
        private static Single[][] WaveletDecomposition( Single[] timeWave )
        {
            // H() filter
            Single[] H = new float[100];

            // G() filter
            Single[] L = new float[100];

            int length = timeWave.Length;

            Single[] L1FreqData = new float[length / 2];
            Single[] H1FreqData = new float[L1FreqData.Length];

            Single[] L2FreqData = new float[length / 4];
            Single[] H2FreqData = new float[L2FreqData.Length];
            Single[] HL1FreqData = new float[L2FreqData.Length];
            Single[] HH1FreqData = new float[L2FreqData.Length];

            Single[] L3FreqData = new float[length / 8];
            Single[] H3FreqData = new float[L3FreqData.Length];
            Single[] H33FreqData = new float[L3FreqData.Length];
            Single[] H34FreqData = new float[L3FreqData.Length];
            Single[] H35FreqData = new float[L3FreqData.Length];
            Single[] H36FreqData = new float[L3FreqData.Length];
            Single[] H37FreqData = new float[L3FreqData.Length];
            Single[] H38FreqData = new float[L3FreqData.Length];

            int h_number = Get_h_mallat( L, H );
            int Index = ( h_number - 1 ) / 2;

            // 一次分解
            Decomposition( timeWave, L1FreqData, -Index, Index, L );
            Decomposition( timeWave, H1FreqData, -Index + 1, Index + 1, H );


            // 二次分解
            Decomposition( L1FreqData, L2FreqData, -Index, Index, L );
            Decomposition( L1FreqData, H2FreqData, -Index + 1, Index + 1, H );

            Decomposition( H1FreqData, HL1FreqData, -Index, Index, L );
            Decomposition( H1FreqData, HH1FreqData, -Index + 1, Index + 1, H );


            // 三次分解
            Decomposition( L2FreqData, L3FreqData, -Index, Index, L );
            Decomposition( L2FreqData, H3FreqData, -Index + 1, Index + 1, H );

            Decomposition( H2FreqData, H33FreqData, -Index, Index, L );
            Decomposition( H2FreqData, H34FreqData, -Index + 1, Index + 1, H );

            Decomposition( HL1FreqData, H35FreqData, -Index, Index, L );
            Decomposition( HL1FreqData, H36FreqData, -Index + 1, Index + 1, H );

            Decomposition( HH1FreqData, H37FreqData, -Index, Index, L );
            Decomposition( HH1FreqData, H38FreqData, -Index + 1, Index + 1, H );

            Single[] one = ArrayUtils.JoinArray( L1FreqData, H1FreqData );

            Single[] two = ArrayUtils.JoinArray( L2FreqData, H2FreqData, HL1FreqData, HH1FreqData );

            Single[] three = ArrayUtils.JoinArray( L3FreqData, H3FreqData, H33FreqData, H34FreqData, H35FreqData,
                                                        H36FreqData, H37FreqData, H38FreqData );

            return new Single[][] { one, two, three };
        }

        /// <summary>
        /// 小波分解
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <param name="decompositionCount">分解的次数，目前只支持1，2，3，其他数值返回null</param>
        /// <returns>分解的结果</returns>
        public static Single[][] WaveletDecomposition( Single[] timeWave, int decompositionCount )
        {
            switch( decompositionCount )
            {
                case 1:
                case 2:
                case 3:
                    Single[][] decomposition = WaveletDecomposition( timeWave );
                    Single[][] ret = new Single[decompositionCount][];
                    for( int index = 0; index < decompositionCount; index++ )
                    {
                        ret[index] = decomposition[index];
                    }

                    return ret;
            }

            return null;
        }

        /// <summary>
        /// 小波分解
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <param name="decompositionCount">分解的次数，目前只支持1，2，3，其他数值返回null</param>
        /// <returns>分解的结果</returns>
        public static Double[][] WaveletDecomposition( Double[] timeWave, int decompositionCount )
        {
            Single[][] decomposition = WaveletDecomposition( ArrayUtils.Double2Single( timeWave ),
                                                             decompositionCount );
            if( decomposition != null )
            {
                return ArrayUtils.Single2Double( decomposition );
            }

            return null;
        }
    }
}
