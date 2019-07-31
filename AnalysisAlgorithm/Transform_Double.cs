using System;
// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// �任�㷨�ࡣ
    /// </summary>
    public static partial class Transform
    {
        /// <summary>
        /// �����л�2���ٸ���Ҷ�任
        /// </summary>
        /// <param name="re">ʵ������</param>
        /// <param name="im">�鲿����</param>
        /// <param name="n">FFT����</param>
        /// <param name="sign">sign=1ʱΪ���任��sign=-1ʱΪ��任</param>
        public static void ComplexFFT( _ValueT[] re, _ValueT[] im, int n, int sign )
        {
            if( !MathBasic.IsPowerOfTwo( n ) )
            {
                return;
            }

            // ����
            {
                // N - 1
                int n_1 = n - 1;

                // highIndex : �Ӹ�λ���λ���±�
                // lowIndex : �ӵ�λ���λ���±�
                for( int highIndex = 0, lowIndex = 0; lowIndex < n_1; lowIndex++ )
                {
                    if( lowIndex < highIndex )
                    {
                        // ��ʱ���������ڽ���������
                        _ValueT realPart = re[highIndex];
                        _ValueT imagPart = im[highIndex];

                        re[highIndex] = re[lowIndex];
                        im[highIndex] = im[lowIndex];

                        re[lowIndex] = realPart;
                        im[lowIndex] = imagPart;
                    }

                    // �����ڸ�λģ���λ�ӷ����ۼ�����ÿ��ֻ��һλ��n / 2��ʾ���������λ��1������λ����0��
                    int highPlus = n / 2;

                    while( highPlus < ( highIndex + 1 ) )
                    {
                        // ��ȥ��һλ����1��Ϊ0
                        highIndex = highIndex - highPlus;

                        // ��1�Ƶ�һλ������>>1������һλ��
                        highPlus = highPlus / 2;
                    }

                    // �ڸ�λģ���λ����һ�μӷ�����ӷ��Ľ�λ��
                    highIndex = highIndex + highPlus;
                }
            } // ����

            // ��������
            {
                int groupOffset = 1;
                int powerOfTwo = MathBasic.GetPowerOfTwo( n );
                for( int levelIndex = 1; levelIndex <= powerOfTwo; levelIndex++ )
                {
                    // ʹ��ͬһ����ת���ӵ�Ԫ������±�֮�
                    groupOffset = 2 * groupOffset;

                    // ��һ����Ҫ����ת���ӵĸ�����Ҳ�ǲ����������Ԫ��֮����±�֮�
                    int twiddleFactorCount = groupOffset / 2;

                    // ÿ�ε�1/n�Ƕȣ����磺��һ����1���ڶ�����1/2����������1/4��
                    _ValueT e = (_ValueT)MathConst.PI / twiddleFactorCount;

                    // ��ʼ��ת���ӵ�ʵ����Ҳ����cos()
                    _ValueT c = 1.0f;

                    // ��ʼ��ת���ӵ��򲿣�Ҳ����sin()
                    _ValueT s = 0.0f;

                    // �������Ǻ����кͲ����ʽ�����еĲ�����cos
                    _ValueT c1 = (_ValueT)Math.Cos( e );

                    // �������Ǻ����кͲ����ʽ�����еĲ�����sin
                    _ValueT s1 = -sign * (_ValueT)Math.Sin( e );

                    for( int twiddleFactorIndex = 0; twiddleFactorIndex < twiddleFactorCount; twiddleFactorIndex++ )
                    {
                        for( int groupIndex = twiddleFactorIndex; groupIndex < n; groupIndex += groupOffset )
                        {
                            // ͬ����һ����������Ԫ�ص��±ꡣ
                            int otherIndex = groupIndex + twiddleFactorCount;

                            // ��ʱ���������ڱ��� W * x[k]��
                            _ValueT realPart = c * re[otherIndex] - s * im[otherIndex];
                            _ValueT imagPart = c * im[otherIndex] + s * re[otherIndex];

                            re[otherIndex] = re[groupIndex] - realPart;
                            im[otherIndex] = im[groupIndex] - imagPart;

                            re[groupIndex] = re[groupIndex] + realPart;
                            im[groupIndex] = im[groupIndex] + imagPart;
                        }

                        // �Ͳ�����㣬��Wn[i]�����Wn[i + 1]
                        {
                            _ValueT lastCos = c;
                            c = c * c1 - s * s1;
                            s = lastCos * s1 + s * c1;
                        }
                    }
                }
            } // ��������

            if( sign == -1 )
            {
                // FFT������ʱ��N��
                for( int index = 0; index < n; index++ )
                {
                    re[index] /= n;
                    im[index] /= n;
                }
            }
        }

        /// <summary>
        /// ʵ���л�2���ٸ���Ҷ�任,�����Ͽ��Լ���һ��ļ�����
        /// </summary>
        /// <param name="xr">ʵ������</param>
        /// <param name="xi">�鲿����</param>
        /// <param name="n">FFT����</param>
        /// <param name="sign">sign=1ʱΪ���任��sign=-1ʱΪ��任</param>
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

            //����������size/2���FFT����size��FFT
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
                // FFT������ʱ��N��
                for( int index = 0; index < n; index++ )
                {
                    xr[index] /= n;
                    xi[index] /= n;
                }
            }
        }

        /// <summary>
        /// ����ϣ�����ر任������ʵ�źţ� re[]=ʵ�źţ�im[]=0.
        /// </summary>
        /// <param name="re">ʵ������</param>
        /// <param name="im">�鲿����</param>
        public static void Hilbert( _ValueT[] re, _ValueT[] im )
        {
            int length = re.Length;

            //˫��FFT�任
            ComplexFFT( re, im, length, 1 );

            int halfLength = length / 2;
            for( int positiveIndex = 0; positiveIndex < halfLength; positiveIndex++ )
            {
                //��Ƶ�ʽ���-90������
                _ValueT reValue = re[positiveIndex];
                _ValueT imValue = im[positiveIndex];
                re[positiveIndex] = imValue;
                im[positiveIndex] = -reValue;

                //��Ƶ�ʽ���90������
                int negativeIndex = positiveIndex + halfLength;
                reValue = re[negativeIndex];
                imValue = im[negativeIndex];
                re[negativeIndex] = -imValue;
                im[negativeIndex] = reValue;
            }

            //˫����FFT�任
            ComplexFFT( re, im, length, -1 );
        }
    }
}