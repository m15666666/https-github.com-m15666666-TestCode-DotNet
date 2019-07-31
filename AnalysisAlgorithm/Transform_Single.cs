using System;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 变换算法类。
    /// </summary>
    public static partial class Transform
    {
        /// <summary>
        /// 复序列基2快速傅立叶变换
        /// </summary>
        /// <param name="re">实部数组</param>
        /// <param name="im">虚部数组</param>
        /// <param name="n">FFT点数</param>
        /// <param name="sign">sign=1时为正变换，sign=-1时为逆变换</param>
        public static void ComplexFFT( _ValueT[] re, _ValueT[] im, int n, int sign )
        {
            if( !MathBasic.IsPowerOfTwo( n ) )
            {
                return;
            }

            // 倒序
            {
                // N - 1
                int n_1 = n - 1;

                // highIndex : 从高位向低位的下标
                // lowIndex : 从低位向高位的下标
                for( int highIndex = 0, lowIndex = 0; lowIndex < n_1; lowIndex++ )
                {
                    if( lowIndex < highIndex )
                    {
                        // 临时变量，用于交换复数。
                        _ValueT realPart = re[highIndex];
                        _ValueT imagPart = im[highIndex];

                        re[highIndex] = re[lowIndex];
                        im[highIndex] = im[lowIndex];

                        re[lowIndex] = realPart;
                        im[lowIndex] = imagPart;
                    }

                    // 用于在高位模拟低位加法的累加数，每次只有一位，n / 2表示二进制最高位是1，其余位都是0。
                    int highPlus = n / 2;

                    while( highPlus < ( highIndex + 1 ) )
                    {
                        // 减去这一位，从1变为0
                        highIndex = highIndex - highPlus;

                        // 将1移低一位（即：>>1，左移一位）
                        highPlus = highPlus / 2;
                    }

                    // 在高位模拟低位做了一次加法，或加法的进位。
                    highIndex = highIndex + highPlus;
                }
            } // 倒序

            // 蝶形运算
            {
                int groupOffset = 1;
                int powerOfTwo = MathBasic.GetPowerOfTwo( n );
                for( int levelIndex = 1; levelIndex <= powerOfTwo; levelIndex++ )
                {
                    // 使用同一个旋转因子的元素组的下标之差。
                    groupOffset = 2 * groupOffset;

                    // 这一级需要的旋转因子的个数，也是参与计算两个元素之间的下标之差。
                    int twiddleFactorCount = groupOffset / 2;

                    // 每次的1/n角度，例如：第一次是1，第二次是1/2，第三次是1/4。
                    _ValueT e = (_ValueT)MathConst.PI / twiddleFactorCount;

                    // 初始旋转因子的实部，也就是cos()
                    _ValueT c = 1.0f;

                    // 初始旋转因子的序部，也就是sin()
                    _ValueT s = 0.0f;

                    // 用于三角函数中和差化积公式运算中的参数：cos
                    _ValueT c1 = (_ValueT)Math.Cos( e );

                    // 用于三角函数中和差化积公式运算中的参数：sin
                    _ValueT s1 = -sign * (_ValueT)Math.Sin( e );

                    for( int twiddleFactorIndex = 0; twiddleFactorIndex < twiddleFactorCount; twiddleFactorIndex++ )
                    {
                        for( int groupIndex = twiddleFactorIndex; groupIndex < n; groupIndex += groupOffset )
                        {
                            // 同组另一个参与运算元素的下标。
                            int otherIndex = groupIndex + twiddleFactorCount;

                            // 临时变量，用于保存 W * x[k]。
                            _ValueT realPart = c * re[otherIndex] - s * im[otherIndex];
                            _ValueT imagPart = c * im[otherIndex] + s * re[otherIndex];

                            re[otherIndex] = re[groupIndex] - realPart;
                            im[otherIndex] = im[groupIndex] - imagPart;

                            re[groupIndex] = re[groupIndex] + realPart;
                            im[groupIndex] = im[groupIndex] + imagPart;
                        }

                        // 和差化积运算，从Wn[i]计算出Wn[i + 1]
                        {
                            _ValueT lastCos = c;
                            c = c * c1 - s * s1;
                            s = lastCos * s1 + s * c1;
                        }
                    }
                }
            } // 蝶形运算

            if( sign == -1 )
            {
                // FFT逆运算时除N。
                for( int index = 0; index < n; index++ )
                {
                    re[index] /= n;
                    im[index] /= n;
                }
            }
        }

        /// <summary>
        /// 实序列基2快速傅立叶变换,理论上可以减少一半的计算量
        /// </summary>
        /// <param name="xr">实部数组</param>
        /// <param name="xi">虚部数组</param>
        /// <param name="n">FFT点数</param>
        /// <param name="sign">sign=1时为正变换，sign=-1时为逆变换</param>
        public static void RealFFT( _ValueT[] xr, _ValueT[] xi, int n, int sign )
        {
            int i, j, k, n1, n2, n4, isN, idN, i0, i1, i2, i3;
            _ValueT e, s1, es, a, a3, cc1, ss1, cc3, ss3, r1, r2, s2, s3;
            _ValueT xtr, xti;

            if( !MathBasic.IsPowerOfTwo( n ) )
            {
                return;
            }

            int powerOfTwo = MathBasic.GetPowerOfTwo( n );

            n2 = n * 2;

            // Math.Atan( 1.0 ) * 8.0 == 2 * Math.PI
            es = sign * (_ValueT)( Math.Atan( 1.0 ) * 8.0 );

            for( k = 1; k <= powerOfTwo - 1; k++ )
            {
                n2 = n2 / 2;
                n4 = n2 / 4;
                e = es / n2;
                a = 0;
                for( j = 0; j <= n4 - 1; j++ )
                {
                    a3 = 3 * a;
                    cc1 = (_ValueT)Math.Cos( a );
                    ss1 = (_ValueT)Math.Sin( a );
                    cc3 = (_ValueT)Math.Cos( a3 );
                    ss3 = (_ValueT)Math.Sin( a3 );
                    a = ( j + 1 ) * e;
                    isN = j;
                    idN = 2 * n2;
                    do
                    {
                        for( i0 = isN; i0 <= n - 1; i0 = i0 + idN )
                        {
                            i1 = i0 + n4;
                            i2 = i1 + n4;
                            i3 = i2 + n4;
                            r1 = xr[i0] - xr[i2];
                            s1 = xi[i0] - xi[i2];
                            r2 = xr[i1] - xr[i3];
                            s2 = xi[i1] - xi[i3];
                            xr[i0] = xr[i0] + xr[i2];
                            xi[i0] = xi[i0] + xi[i2];
                            xr[i1] = xr[i1] + xr[i3];
                            xi[i1] = xi[i1] + xi[i3];
                            if( sign == 1 )
                            {
                                s3 = r1 - s2;
                                r1 = r1 + s2;
                                s2 = r2 - s1;
                                r2 = r2 + s1;
                            }
                            else
                            {
                                s3 = r1 + s2;
                                r1 = r1 - s2;
                                s2 = -r2 - s1;
                                r2 = -r2 + s1;
                            }
                            xr[i2] = r1 * cc1 - s2 * ss1;
                            xi[i2] = -s2 * cc1 - r1 * ss1;
                            xr[i3] = s3 * cc3 + r2 * ss3;
                            xi[i3] = r2 * cc3 - s3 * ss3;
                        }
                        isN = 2 * idN - n2 + j;
                        idN = 4 * idN;
                    } while( isN < n - 1 );
                }
            }

            //重新排序，由size/2点的FFT计算size点FFT
            isN = 0;
            idN = 4;

            do
            {
                for( i0 = isN; i0 <= n - 1; i0 = i0 + idN )
                {
                    i1 = i0 + 1;
                    xtr = xr[i0];
                    xti = xi[i0];
                    xr[i0] = xtr + xr[i1];
                    xi[i0] = xti + xi[i1];
                    xr[i1] = xtr - xr[i1];
                    xi[i1] = xti - xi[i1];
                }
                isN = 2 * idN - 2;
                idN = 4 * idN;
            } while( isN < n - 1 );

            j = 1;
            n1 = n - 1;
            for( i = 1; i <= n1; i++ )
            {
                if( i < j )
                {
                    xtr = xr[j - 1];
                    xti = xi[j - 1];
                    xr[j - 1] = xr[i - 1];
                    xi[j - 1] = xi[i - 1];
                    xr[i - 1] = xtr;
                    xi[i - 1] = xti;
                }
                k = n / 2;
                while( k < j )
                {
                    j = j - k;
                    k = k / 2;
                }
                j = j + k;
            }

            if( sign == -1 )
            {
                // FFT逆运算时除N。
                for( int index = 0; index < n; index++ )
                {
                    xr[index] /= n;
                    xi[index] /= n;
                }
            }
        }

        /// <summary>
        /// 计算希尔伯特变换。对于实信号， re[]=实信号，im[]=0.
        /// </summary>
        /// <param name="re">实部数组</param>
        /// <param name="im">虚部数组</param>
        public static void Hilbert( _ValueT[] re, _ValueT[] im )
        {
            int length = re.Length;

            //双边FFT变换
            ComplexFFT( re, im, length, 1 );

            int halfLength = length / 2;
            for( int positiveIndex = 0; positiveIndex < halfLength; positiveIndex++ )
            {
                //正频率进行-90度移相
                _ValueT reValue = re[positiveIndex];
                _ValueT imValue = im[positiveIndex];
                re[positiveIndex] = imValue;
                im[positiveIndex] = -reValue;

                //负频率进行90度移相
                int negativeIndex = positiveIndex + halfLength;
                reValue = re[negativeIndex];
                imValue = im[negativeIndex];
                re[negativeIndex] = -imValue;
                im[negativeIndex] = reValue;
            }

            //双边逆FFT变换
            ComplexFFT( re, im, length, -1 );
        }
    }
}