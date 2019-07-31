using System;
using Moons.Common20;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ��EMD������ģ̬�ֽ⣩��ص��㷨��
    /// </summary>
    public static class EMD
    {
        /// <summary>
        /// EMD�ֽ�
        /// </summary>
        /// <param name="timeWave">ʱ�䲨��</param>
        /// <returns>�ֽ�Ľ��</returns>
        public static Double[][] EMDDecomposition( Double[] timeWave )
        {
            Double[][] decomposition = new EMDImp( timeWave ).EMDDecomposition();
            if( decomposition != null )
            {
                Double[][] ret = ArrayUtils.CreateJaggedArray<double>(4, timeWave.Length / 2);
                for( int rowIndex = 0; rowIndex < ret.Length; rowIndex++ )
                {
                    Double[] target = ret[rowIndex];
                    Double[] source = decomposition[rowIndex];
                    for( int index = 0; index < target.Length; index++ )
                    {
                        target[index] = source[index];
                    }
                }
                return ret;
            }
            return null;
        }

        #region EMD��ʵ����

        /// <summary>
        /// EMD��ʵ����
        /// </summary>
        private class EMDImp
        {
            private int MaxNumber, MinNumber;

            /// <summary>
            /// ���ݳ���
            /// </summary>
            private readonly int DataLength;

            /// <summary>
            /// ���ɰ�������б������ֵ����Сֵ
            /// </summary>
            private readonly Double[][] maxdata;

            /// <summary>
            /// ���ɰ�������б������ֵ����Сֵ
            /// </summary>
            private readonly Double[][] mindata;

            /// <summary>
            /// X������
            /// </summary>
            private readonly Double[] time;

            /// <summary>
            /// ���ڴ������ɰ����ϵ��
            /// </summary>
            private readonly Double[] x0;

            /// <summary>
            /// ���ڴ������ɰ����ϵ��
            /// </summary>
            private readonly Double[] y0;

            /// <summary>
            /// X�����飬ͬtime��ֱ�Ӹ���time������
            /// </summary>
            private readonly Double[] x;

            /// <summary>
            /// �����������
            /// </summary>
            private readonly Double[] y;

            /// <summary>
            /// ��һ����ʱ�䲨�Σ�����4�б����м���
            /// </summary>
            private readonly Double[][] data;

            public EMDImp( Double[] timeWave )
            {
                DataLength = timeWave.Length;

                data = ArrayUtils.CreateJaggedArray<double>( 5, DataLength );
                timeWave.CopyTo( data[0], 0 );

                time = new Double[DataLength];
                for( int index = 0; index < time.Length; index++ )
                {
                    time[index] = index;
                }

                mindata = ArrayUtils.CreateJaggedArray<double>( 3, DataLength );
                maxdata = ArrayUtils.CreateJaggedArray<double>( 3, DataLength );

                x0 = new Double[DataLength];
                y0 = new Double[DataLength];

                // ��ʱ����ֵ����x(t)����,����������̵���
                x = new Double[DataLength];
                time.CopyTo( x, 0 );

                y = new Double[DataLength];
            }

            private void CalcMaxMinData( Double[] imfData )
            {
                int flag = -1;
                if( imfData[0] > imfData[1] )
                {
                    flag = 1;
                }

                MaxNumber = MinNumber = 0;

                Double[] max_0 = maxdata[0];
                Double[] max_1 = maxdata[1];

                Double[] min_0 = mindata[0];
                Double[] min_1 = mindata[1];

                for( int index = 1; index < DataLength - 1; index++ )
                {
                    if( flag > 0 )
                    {
                        Double localMax = imfData[index];
                        if( localMax > imfData[index + 1] )
                        {
                            max_1[MaxNumber] = imfData[index];
                            max_0[MaxNumber] = time[index];
                            MaxNumber++;
                            flag = -1;
                        }
                    }

                    if( flag < 0 )
                    {
                        Double localMin = imfData[index];
                        if( localMin < imfData[index + 1] )
                        {
                            min_1[MinNumber] = imfData[index];
                            min_0[MinNumber] = time[index];
                            MinNumber++;
                            flag = 1;
                        }
                    }
                }

                max_1[MaxNumber] = max_1[MaxNumber - 1];
                max_0[MaxNumber] = time[DataLength - 1];
                min_1[MinNumber] = min_1[MinNumber - 1];
                min_0[MinNumber] = time[DataLength - 1];
                MaxNumber++;
                MinNumber++;
            }

            private void splnsub( int n, int M )
            {
                Double[] work1 = new Double[M + 1];
                Double[] work2 = new Double[work1.Length];
                Double[] work3 = new Double[work1.Length];
                Double[] work4 = new Double[work1.Length];
                Double[] work5 = new Double[work1.Length];

                int n1 = n - 1;
                int n2 = n - 2;

                for( int index = 0; index < n1; index++ )
                {
                    work2[index] = x0[index + 1] - x0[index];
                    work3[index] = ( y0[index + 1] - y0[index] ) / work2[index];
                }

                work1[0] = work1[n - 1] = 0;
                for( int index = 1; index < n1; index++ )
                {
                    work1[index] = 6 * ( work3[index] - work3[index - 1] );
                }

                Double work5InitScale = 0.5 / ( work2[0] + work2[1] );
                work5[0] = -work2[1] * work5InitScale;
                work4[0] = work1[1] * work5InitScale;

                for( int index = 1; index < n2; index++ )
                {
                    Double work5Scale = 1 /
                                        ( 2 * ( work2[index] + work2[index + 1] ) + work2[index] * work5[index - 1] );
                    work5[index] = -work2[index + 1] * work5Scale;
                    work4[index] = ( work1[index + 1] - work2[index] * work4[index - 1] ) * work5Scale;
                }

                work1[n1 - 1] = work4[n2 - 1];
                for( int n2Index = 1; n2Index < n2; n2Index++ )
                {
                    int index = n2 - n2Index;
                    work1[index] = work5[index - 1] * work1[index + 1] + work4[index - 1];
                }

                for( int index = 0; index < n1; index++ )
                {
                    work5[index] = ( work1[index + 1] - work1[index] ) / work2[index];
                }

                int i = 1;
                int k = 0;
                for( int index = 0; index < M; index++ )
                {
                    while( x[index] > x0[i] )
                    {
                        k = i;
                        i++;
                    }

                    Double temp1 = x[index] - x0[k];
                    Double temp2 = x[index] - x0[i];
                    Double temp3 = temp1 * temp2;
                    Double temp4 = work1[k] + temp1 * work5[k];
                    Double temp = ( work1[i] + work1[k] + temp4 ) / 6;
                    y[index] = y0[k] + temp1 * work3[k] + temp3 * temp;
                }
            }

            private static ImfNumberException GetIMFNUException(int imfNumber)
            {
                return new ImfNumberException(imfNumber);
            }

            /// <summary>
            /// ������¼�ֵ�����ĿС��3, �����
            /// </summary>
            private bool IsMaxMinNumberError()
            {
                return ( MaxNumber < 3 ) || ( MinNumber < 3 );
            }

            public Double[][] EMDDecomposition()
            {
                // ��ʼ������
                Double[][] ret = ArrayUtils.CreateJaggedArray<double>( 4, DataLength );

                // ����data����һ���±������
                Double[] dataBackup = new Double[DataLength];

                // ���ڵڶ��μ���
                Double[] work2 = new Double[DataLength];

                //Double[] vTmpData = new Double[DataLength];

                Double[] max_0 = maxdata[0];
                Double[] max_1 = maxdata[1];

                Double[] min_0 = mindata[0];
                Double[] min_1 = mindata[1];

                const int IMFNumber = 4;
                for( int imfNumber = 1; imfNumber <= IMFNumber; imfNumber++ )
                {
                    // ��ǰimfNumber�±��data
                    Double[] imfData = data[imfNumber];

                    // ��һ��imfNumber�±��data
                    Double[] imfDataLast = data[imfNumber - 1];

                    // �����±�ΪimfNumber - 1��data����
                    imfDataLast.CopyTo( dataBackup, 0 );

                    // ѡȡ���������ֵ����ֵ
                    Double threshold = CollectionUtils.AbsMax( imfDataLast ) / 10;

                    // ����ִ��һ��
                    while( true )
                    {
                        // ����ֲ���ֵ
                        CalcMaxMinData( imfDataLast );

                        // ������¼�ֵ�����ĿС��3, ��ִֹͣ��
                        if( IsMaxMinNumberError() )
                        {
                            throw GetIMFNUException( imfNumber - 1 );
                        }

                        // ��ʼ�����ϰ�����
                        for( int index = 0; index < MaxNumber; index++ )
                        {
                            x0[index] = max_0[index];
                            y0[index] = max_1[index];
                        }

                        splnsub( MaxNumber, DataLength );

                        y.CopyTo( imfData, 0 );

                        // ���ϣ��ϰ����߼������


                        // ��ʼ�����°�����
                        for( int index = 0; index < MinNumber; index++ )
                        {
                            x0[index] = min_0[index];
                            y0[index] = min_1[index];
                        }

                        splnsub( MinNumber, DataLength );

                        for( int index = 0; index < DataLength; index++ )
                        {
                            imfData[index] = ( imfData[index] + y[index] ) / 2;
                        }

                        // ���ϣ��°����ߺ��ϰ����ߵ������߼������,��ֵ����� (data(imfnumber, k),k=0..DataLength-1)


                        // ��ʼ����IMF
                        for( int index = 0; index < DataLength; index++ )
                        {
                            imfData[index] = imfDataLast[index] - imfData[index];
                        }

                        // ��֤�Ƿ�Ϊ��ʵ��IMF,���¼������°�����
                        CalcMaxMinData( imfData );

                        // ������¼�ֵ�����ĿС��3, ��ִֹͣ��
                        if( IsMaxMinNumberError() )
                        {
                            throw GetIMFNUException( imfNumber - 1 );
                        }

                        for( int index = 0; index < MaxNumber; index++ )
                        {
                            x0[index] = max_0[index];
                            y0[index] = max_1[index];
                        }

                        splnsub( MaxNumber, DataLength );

                        y.CopyTo( work2, 0 );

                        for( int index = 0; index < MinNumber; index++ )
                        {
                            x0[index] = min_0[index];
                            y0[index] = min_1[index];
                        }

                        splnsub( MinNumber, DataLength );

                        for( int index = 0; index < DataLength; index++ )
                        {
                            work2[index] = ( work2[index] + y[index] ) / 2;
                        }

                        // ׼����һ��ѭ��
                        imfData.CopyTo( imfDataLast, 0 );

                        // ���������ߵ�ƽ��ʱ����Ҫ���Ǳ߽��Ӱ��
                        Double mean = 0;
                        for( int index = 100; index < DataLength - 100; index++ )
                        {
                            Double abs = Math.Abs( work2[index] );
                            if( mean < abs )
                            {
                                mean = abs;
                            }
                        }

                        // �˳�ѭ��������
                        if( mean <= threshold )
                        {
                            break;
                        }
                    } // while (true)


                    // ���¸���ԭ������ʵ��ֵ
                    dataBackup.CopyTo( imfDataLast, 0 );

                    //Double max = Double.MinValue;
                    //Double min = Double.MaxValue;
                    //for( int index = 0; index < DataLength; index++ )
                    //{
                    //    vTmpData[index] = imfDataLast[index];

                    //    Double value = imfData[index];
                    //    if (max < value)
                    //    {
                    //        max = value;
                    //    }

                    //    if (min > value)
                    //    {
                    //        min = value;
                    //    }
                    //}

                    for( int index = 0; index < DataLength; index++ )
                    {
                        imfData[index] = imfDataLast[index] - imfData[index];
                    }

                    dataBackup.CopyTo( ret[imfNumber - 1], 0 );
                } // for( int imfnumber = 1; imfnumber <= IMFNU; imfnumber++ )

                return ret;
            }
        }

        #endregion
    }
}
