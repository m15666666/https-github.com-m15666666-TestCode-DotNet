using System;
using Moons.Common20;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 潘玉娜编写的与小波相关的算法类
    /// </summary>
    public static class Wavelet_PanYuNa
    {
        #region 小波系数

        /// <summary>
        /// db8小波名
        /// </summary>
        public const string WaveName_DB8 = "db8";

        /// <summary>
        /// db8小波基系数
        /// </summary>
        private static readonly Double[] WaveCoef_DB8 = {
                                                            0.03847781105406, 0.221233623576241, 0.477743075214377,
                                                            0.413908266211663,
                                                            -0.01119286766665, -0.200829316391107, 0.000334097046282,
                                                            0.091038178423454,
                                                            -0.012281950523003, -0.031175103325331, 0.009886079648084,
                                                            0.006184422409538,
                                                            -0.003443859628128, -0.000277002274213, 0.000477614855332,
                                                            -0.000083068630599
                                                        };

        /// <summary>
        /// 根据小波名字获取相应的小波基系数，该函数中暂时只给出了一个DB8小波基
        /// </summary>
        /// <param name="wavename"></param>
        private static Double[] GetCoefByName( string wavename )
        {
            switch( wavename )
            {
                    //case WaveName_DB8:
                default:
                    var w = new Double[WaveCoef_DB8.Length];
                    WaveCoef_DB8.CopyTo( w, 0 );
                    return w;
            }
        }

        #endregion

        #region 卷积

        private static T[] Reverse<T>( T[] source )
        {
            var ret = new T[source.Length];
            source.CopyTo( ret, 0 );
            Array.Reverse( ret );
            return ret;
        }

        private static void conv( Double[] a, Double[] b, Double[] c ) //卷积
        {
            Double[] br = Reverse( b );

            for( int i = 0; i < c.Length; i++ )
            {
                Double sum = 0.0; //存放每次卷积的中间值
                for( int j = 0; j < br.Length; j++ )
                {
                    sum += br[j] * a[i + j];
                }
                c[i] = sum;
            }
        }

        private static void conv_full( Double[] a, Double[] b, Double[] c )
        {
            //a前后各补sizeb-1个0
            var a1 = new Double[a.Length + 2 * ( b.Length - 1 )];
            for( int i = 0; i < a1.Length; i++ )
            {
                a1[i] = 0;
            }
            a.CopyTo( a1, b.Length - 1 );

            conv( a1, b, c );
        }

        #endregion

        #region 辅助函数

        /// <summary>
        /// 找出a中小于b的下标，c中的第1个存a中小于b的总个数
        /// </summary>
        private static void getasb( int[] a, int b, int[] c )
        {
            int count = 0;
            foreach( int value in a )
            {
                if( value < b )
                {
                    count++;
                }
            }

            if( 0 < count )
            {
                c[0] = count;
                int cIndex = 1;
                for( int i = 0; i < a.Length; i++ )
                {
                    if( a[i] < b )
                    {
                        c[cIndex++] = i;
                    }
                }
            }
        }

        /// <summary>
        /// 找出a中大于b的下标，c中的第1个存a中大于b的总个数
        /// </summary>
        private static void getamb( int[] a, int b, int[] c )
        {
            int count = 0;
            foreach( int value in a )
            {
                if( value > b )
                {
                    count++;
                }
            }

            if( 0 < count )
            {
                c[0] = count;
                int cIndex = 1;
                for( int i = 0; i < a.Length; i++ )
                {
                    if( a[i] > b )
                    {
                        c[cIndex++] = i;
                    }
                }
            }
        }

        #endregion

        #region 滤波器系数

        /// <summary>
        /// 根据小波基系数获得分解时高、低通滤波器系数
        /// </summary>
        /// <param name="w"></param>
        /// <param name="sizew"></param>
        /// <param name="filter"></param>
        private static void CreateFilterD( Double[] w, int sizew, Double[][] filter )
        {
            Double[] filter0 = filter[0];
            Double[] filter1 = filter[1];

            Double sumw = NumbersUtils.SumArray( w );
            for( int i = 0; i < sizew; i++ )
            {
                //w[i] = w[i] / sumw;
                //filter0[i] = MathConst.SqrtTwo * w[i]; //低通滤波器系数
                var wI = w[i] / sumw;
                filter0[i] = MathConst.SqrtTwo * wI; //低通滤波器系数
            }

            filter0.CopyTo( filter1, 0 );
            Array.Reverse( filter1 );

            for( int i = 1; i < sizew; i = i + 2 )
            {
                filter1[i] = -filter1[i];
            }

            Array.Reverse( filter0 );
            Array.Reverse( filter1 );
        }

        /// <summary>
        /// 根据小波基系数获得重构时的高、低通滤波器系数
        /// </summary>
        /// <param name="w"></param>
        /// <param name="sizew"></param>
        /// <param name="filter"></param>
        private static void CreateFilterR( Double[] w, int sizew, Double[][] filter )
        {
            Double[] filter0 = filter[0];
            Double[] filter1 = filter[1];

            Double sumw = NumbersUtils.SumArray( w );
            for( int i = 0; i < sizew; i++ )
            {
                //w[i] = w[i] / sumw;
                //filter0[i] = MathConst.SqrtTwo * w[i]; //低通滤波器系数
                var wI = w[i] / sumw;
                filter0[i] = MathConst.SqrtTwo * wI; //低通滤波器系数
            }

            filter0.CopyTo( filter1, 0 );
            Array.Reverse( filter1 );

            for( int i = 1; i < sizew; i = i + 2 )
            {
                filter1[i] = -filter1[i];
            }
        }

        #endregion

        #region 小波分解

        /// <summary>
        /// 小波分解
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <param name="decompositionCount">分解的次数</param>
        /// <returns>信号分解后重构的各层信号，
        /// 下标0是倒数第一次分解的近似信号，
        /// 下标1是倒数第一次分解的细节信号，
        /// 下标2是倒数第二次分解的细节信号，
        /// 下标3是倒数第三次分解的细节信号，以此类推，
        /// 下标decompositionCount - 1是第一次分解的细节信号</returns>
        public static Double[][] WaveletDecomposition( Double[] timeWave, int decompositionCount )
        {
            return WaveletDecomposition( timeWave, WaveName_DB8, decompositionCount - 1 );
        }

        /// <summary>
        /// 小波分解
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <param name="waveName">选择的小波基名字</param>
        /// <param name="level">分解的层数</param>
        /// <returns>信号分解后重构的各层信号，
        /// 下标0是倒数第一次分解的近似信号，
        /// 下标1是倒数第一次分解的细节信号，
        /// 下标2是倒数第二次分解的细节信号，
        /// 下标3是倒数第三次分解的细节信号，以此类推，
        /// 下标level是第一次分解的细节信号</returns>
        public static Double[][] WaveletDecomposition( Double[] timeWave, string waveName, int level )
        {
            int sizex = timeWave.Length;

            Double[] w = GetCoefByName( waveName );

            //滤波器长度
            int filterSize = w.Length;

            // 高通滤波器系数
            Double[][] filterD = ArrayUtils.CreateJaggedArray<double>( 2, filterSize );

            // 低通滤波器系数
            Double[][] filterR = ArrayUtils.CreateJaggedArray<double>( 2, filterSize );

            CreateFilterD( w, filterSize, filterD );
            CreateFilterR( w, filterSize, filterR );

            int coefSize = level + 1;
            Double[][] coef = ArrayUtils.CreateJaggedArray<double>( coefSize, sizex );

            int lengthsSize = level + 2;
            var lengths = new int[lengthsSize];
            Decomposition( timeWave, level, filterD, filterSize, coef, lengths );

            Double[][] rsignal = ArrayUtils.CreateJaggedArray<double>( coefSize, sizex );
            WrappLast( coef, lengths, filterR[0], rsignal[0] );

            for( int i = 1; i < coefSize; i++ )
            {
                WrDet( coef, lengths, filterR, rsignal[i], level - i + 1 );
            }

            return rsignal;
        }

        /// <summary>
        /// 小波分解总过程，其结果存放在c和l中，分别为分解得各层系数和每层系数的长度
        /// </summary>
        /// <param name="x"></param>
        /// <param name="level"></param>
        /// <param name="filter_d"></param>
        /// <param name="filterSize"></param>
        /// <param name="c"></param>
        /// <param name="l"></param>
        private static void Decomposition( Double[] x, int level, Double[][] filter_d, int filterSize,
                                           Double[][] c, int[] l )
        {
            int xLength = x.Length;
            l[0] = xLength; //l 的第一个元素为被分解信号的长度
            int sizead = 0; //每层分解后细节和近似信号的长度；
            for( int i = 0; i < level; i++ )
            {
                sizead = ( xLength + filterSize - 1 ) / 2;

                //存放分解后近似和细节系数，第1行为近似系数
                Double[][] ad = ArrayUtils.CreateJaggedArray<double>( 2, sizead );

                Dwt( x, xLength, filter_d[0], filter_d[1], filterSize, ad );

                xLength = sizead; //下层分解的信号程度
                l[i + 1] = sizead; //每次分解得到的近似（细节）系数的长度

                for( int j = 0; j < sizead; j++ )
                {
                    c[i][j] = ad[1][j]; //将新得到的细节系数放入c中，因此，c中第一行存放第一层的细节系数
                    x[j] = ad[0][j]; //将得到的近似系数作为下层分解的信号；
                }
            }
            l[level + 1] = sizead; //最后的细节系数的长度存在l的最后一个元素中

            // 最后一层分解的近似系数
            for( int j = 0; j < sizead; j++ )
            {
                c[level][j] = x[j];
            }
        }

        /// <summary>
        /// 每次小波分解
        /// sizef是滤波器的长度，ad前半部分存近似系数，后半部分存细节系数
        /// ad的前半部分x的前sizex个元素，进入下层分解使用
        /// sizeapp=(last-first)/2+1; 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="xSize">必须传入，因为xSize与x.Length不一定相等</param>
        /// <param name="Lo_D"></param>
        /// <param name="Hi_D"></param>
        /// <param name="filterSize"></param>
        /// <param name="ad"></param>
        private static void Dwt( Double[] x, int xSize, Double[] Lo_D, Double[] Hi_D, int filterSize, Double[][] ad )
        {
            int first = 1; //matlab中first=2，因为在c++中下标是从0开始的
            int lenEXT = filterSize - 1;
            int last = xSize + filterSize - 2; //matlab中last=sizex+sizef-1，因为在c++中下标是从0开始的
            int sizeI = xSize + 2 * lenEXT;

            var I = new int[sizeI];
            GetSymIndices( xSize, lenEXT, I );

            var y = new Double[sizeI];
            for( int i = 0; i < sizeI; i++ )
            {
                y[i] = x[I[i] - 1]; //以上是按matlab中的下标来标定的，在c++中要减1
            } //相当于matlab中的wextend 函数

            int sizez = sizeI - filterSize + 1; //卷积（matlab中‘valid’时的）后矩阵长度
            var z = new Double[sizez];

            conv( y, Lo_D, z );

            int j = 0;
            for( int i = first; i <= last; i = i + 2 )
            {
                ad[0][j++] = z[i];
            }

            conv( y, Hi_D, z );

            j = 0;
            for( int i = first; i <= last; i = i + 2 )
            {
                ad[1][j++] = z[i];
            }
        }

        private static void GetSymIndices( int xLength, int extLength, int[] I )
        {
            for( int i = 0; i < I.Length; i++ )
            {
                if( i < extLength )
                {
                    I[i] = extLength - i;
                }
                else if( i >= extLength && i < extLength + xLength )
                {
                    I[i] = 1 + i - extLength;
                }
                else if( i >= extLength + xLength )
                {
                    I[i] = xLength - ( i - extLength - xLength );
                }
            }

            if( xLength < extLength )
            {
                var K = new int[I.Length];
                var J = new int[I.Length];

                J[0] = 0;
                getasb( I, 1, K );

                for( int i = 1; i <= I[0]; i++ )
                {
                    I[K[i]] = 1 - I[K[i]];
                }

                getamb( I, xLength, J );
                if( J[0] != 0 )
                {
                    for( int i = 1; i <= J[0]; i++ )
                    {
                        I[J[i]] = 2 * xLength + 1 - I[J[i]];
                    }

                    getasb( I, 1, K );
                    for( int i = 1; i <= I[0]; i++ )
                    {
                        I[K[i]] = 1 - I[K[i]];
                    }
                }
            }
        }

        #endregion

        #region 重构小波

        private static void Upsconv( Double[] x, int xLength, Double[] f, Double[] y )
        {
            int sizez1 = 2 * xLength - 1;
            int sizez2 = sizez1 + f.Length - 1; //卷积后的长度

            var z1 = new Double[sizez1];
            ZeroPad( x, xLength, z1 ); //奇数坐标插0

            var z2 = new Double[sizez2];
            conv_full( z1, f, z2 );

            Wkeep( z2, y );
        }

        /// <summary>
        /// 取给定向量的中心部分长度为y.Length的向量
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void Wkeep( Double[] x, Double[] y )
        {
            int first = ( x.Length - y.Length ) / 2;
            for( int index = 0; index < y.Length; index++ )
            {
                y[index] = x[first + index];
            }
        }

        /// <summary>
        /// 奇数下标补零
        /// </summary>
        /// <param name="x"></param>
        /// <param name="xLength"></param>
        /// <param name="y"></param>
        private static void ZeroPad( Double[] x, int xLength, Double[] y )
        {
            for( int i = 0; i < xLength; i++ )
            {
                y[2 * i] = x[i];
                if( i < xLength - 1 )
                {
                    y[2 * i + 1] = 0;
                }
            }
        }

        /// <summary>
        /// 重构最后一层的近似信号
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <param name="Lo_R"></param>
        /// <param name="app"></param>
        private static void WrappLast( Double[][] c, int[] l, Double[] Lo_R, Double[] app )
        {
            int level = l.Length - 2; //分解的层数
            int sizeapp = l[l.Length - 1]; //最后一层细节系数的长度存在l的最后一个元素里
            for( int i = 0; i < sizeapp; i++ )
            {
                app[i] = c[level][i]; //c共有level+1行，最后一行为最后一层分解的近似系数
            }

            for( int i = 1; i <= level; i++ )
            {
                int sizeb = l[level - i];
                var b = new Double[sizeb];
                Upsconv( app, sizeapp, Lo_R, b );
                for( int j = 0; j < sizeb; j++ )
                {
                    app[j] = b[j];
                }
                sizeapp = sizeb;
            }
        }

        /// <summary>
        /// 重构levelth层的细节信号
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <param name="filter_r"></param>
        /// <param name="det"></param>
        /// <param name="levelth"></param>
        private static void WrDet( Double[][] c, int[] l, Double[][] filter_r, Double[] det,
                                   int levelth )
        {
            int sizedet = l[levelth]; //第levelth细节系数的长度的长度
            for( int i = 0; i < sizedet; i++ )
            {
                det[i] = c[levelth - 1][i]; //取第levelth层的细节系数
            }

            int sizeb = l[levelth - 1];
            var b = new Double[sizeb];
            Upsconv( det, sizedet, filter_r[1], b );
            for( int j = 0; j < sizeb; j++ )
            {
                det[j] = b[j];
            }

            sizedet = sizeb;
            for( int i = 2; i <= levelth; i++ )
            {
                sizeb = l[levelth - i]; //上一层的长度
                b = new Double[sizeb];
                Upsconv( det, sizedet, filter_r[0], b );
                for( int j = 0; j < sizeb; j++ )
                {
                    det[j] = b[j];
                }
                sizedet = sizeb;
            }
        }

        #endregion
    }
}