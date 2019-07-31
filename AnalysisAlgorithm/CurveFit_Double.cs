using System;

// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using Moons.Common20;
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 曲线拟合
    /// </summary>
    public static class CurveFit
    {
        /// <summary>
        /// 直线拟合
        /// </summary>
        public const int PolynomialOrder_Linear = 2;

        /// <summary>
        /// 二次曲线拟合
        /// </summary>
        public const int PolynomialOrder_Parabola = 3;

        /// <summary>
        /// 最小二乘曲线拟合
        /// </summary>
        /// <param name="polynomialOrder">拟合多项式的最高阶数+1; 2:直线拟合, 3:二次曲线拟合</param>
        /// <param name="xData">数据点的x坐标</param>
        /// <param name="yData">数据点的y坐标</param>
        /// <param name="Coef">拟合多项式的系数，输出参数</param>
        /// <param name="dT">拟合偏差，输出参数</param>
        public static void LMS_CurveFit( int polynomialOrder, _ValueT[] xData, _ValueT[] yData, _ValueT[] Coef,
                                         _ValueT[] dT )
        {
            var S = new _ValueT[polynomialOrder + 1];
            var t = new _ValueT[S.Length];

            int length = xData.Length;

            Double xMean = StatisticsUtils.Mean( xData );

            var b = new _ValueT[S.Length];
            b[1] = 1;

            _ValueT[] xDataOffset = ArrayUtils.Clone( xData );
            CollectionUtils.OffsetArray( xDataOffset, -xMean );

            Double xSum = StatisticsUtils.Mean( xDataOffset );

            Coef[1] = StatisticsUtils.Mean( yData ) * b[1];

            t[2] = 1;
            t[1] = -xSum;

            Double D2 = 0;
            Double ySum = 0;
            Double G = 0;

            for( int index = 0; index < length; index++ )
            {
                Double q = xData[index] - xMean - xSum;
                D2 += q * q;
                ySum += yData[index] * q;
                G += ( xData[index] - xMean ) * q * q;
            }

            if( D2 == 0 )
            {
                return;
            }

            ySum /= D2;
            xSum = G / D2;
            Double D1 = length;
            Double Q = D2 / D1;
            D1 = D2;
            Coef[2] = ySum * t[2];
            Coef[1] = ySum * t[1] + Coef[1];

            for( int j = 3; j <= polynomialOrder; j++ )
            {
                S[j] = t[j - 1];
                S[j - 1] = -xSum * t[j - 1] + t[j - 2];

                if( j >= 4 )
                {
                    for( int K = j - 2; 2 <= K; K-- )
                    {
                        S[K] = -xSum * t[K] + t[K - 1] - Q * b[K];
                    }
                }

                S[1] = -xSum * t[1] - Q * b[1];
                D2 = 0;
                ySum = 0;
                G = 0;

                for( int i = 0; i < length; i++ )
                {
                    Q = S[j];

                    for( int K = j - 1; 1 <= K; K-- )
                    {
                        Q = Q * ( xData[i] - xMean ) + S[K];
                    }

                    D2 += Q * Q;
                    ySum += yData[i] * Q;
                    G += ( xData[i] - xMean ) * Q * Q;
                }

                ySum /= D2;
                xSum = G / D2;
                Q = D2 / D1;
                D1 = D2;
                Coef[j] = ySum * S[j];
                t[j] = S[j];

                for( int K = j - 1; 1 <= K; K-- )
                {
                    Coef[K] = ySum * S[K] + Coef[K];
                    b[K] = t[K];
                    t[K] = S[K];
                }
            } // for( int j = 3; j <= polynomialOrder; j++ )

            dT[1] = 0;
            dT[2] = 0;
            dT[3] = 0;

            for( int i = 0; i < length; i++ )
            {
                Q = Coef[polynomialOrder];

                for( int K = polynomialOrder - 1; 1 <= K; K-- )
                {
                    Q = Q * ( xData[i] - xMean ) + Coef[K];
                }

                Double dtt = Q - yData[i];

                if( Math.Abs( dtt ) > dT[3] )
                {
                    dT[3] = Math.Abs( dtt );
                }

                dT[1] += dtt * dtt;
                dT[2] += Math.Abs( dtt );
            }
        }

        /// <summary>
        /// 预测报警时间
        /// </summary>
        /// <param name="polynomialOrder">拟合多项式的最高阶数+1; 2:直线拟合, 3:二次曲线拟合</param>
        /// <param name="Coef">拟合多项式的系数</param>
        /// <param name="threshold">门限值</param>
        /// <returns>报警时间</returns>
        public static Double PredictAlmDate( int polynomialOrder, _ValueT[] Coef, Double threshold )
        {
            Double ret;

            // 最大预测期：三年
            const Double MaxPredict = 1095 * 86400; // 3 * 365 * 24* 60 * 60 秒

            // 近期不会发生报警
            const Double NoRecentAlm = Double.MaxValue;

            switch( polynomialOrder )
            {
                case 2: // Linear
                    if( Coef[2] == 0 )
                    {
                        return NoRecentAlm;
                    }
                    ret = ( threshold - Coef[1] ) / Coef[2];
                    break;

                case 3: // Parabola
                    if( Coef[3] == 0 )
                    {
                        return NoRecentAlm;
                    }
                    Double mid = Math.Abs( Coef[2] * Coef[2] - 4 * ( Coef[1] - threshold ) * Coef[3] );
                    ret = ( Math.Sqrt( mid ) - Coef[2] ) / ( 2 * Coef[3] );
                    break;

                default:
                    return NoRecentAlm;
            }

            return MaxPredict < ret ? NoRecentAlm : ret;
        }
    }
}