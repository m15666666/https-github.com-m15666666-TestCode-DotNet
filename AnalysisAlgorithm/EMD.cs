using System;
using Moons.Common20;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 与EMD（经验模态分解）相关的算法类
    /// </summary>
    public static class EMD
    {
        /// <summary>
        /// EMD分解
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <returns>分解的结果</returns>
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

        #region EMD的实现类

        /// <summary>
        /// EMD的实现类
        /// </summary>
        private class EMDImp
        {
            private int MaxNumber, MinNumber;

            /// <summary>
            /// 数据长度
            /// </summary>
            private readonly int DataLength;

            /// <summary>
            /// 生成包络过程中保存最大值，最小值
            /// </summary>
            private readonly Double[][] maxdata;

            /// <summary>
            /// 生成包络过程中保存最大值，最小值
            /// </summary>
            private readonly Double[][] mindata;

            /// <summary>
            /// X轴数组
            /// </summary>
            private readonly Double[] time;

            /// <summary>
            /// 用于传入生成包络的系数
            /// </summary>
            private readonly Double[] x0;

            /// <summary>
            /// 用于传入生成包络的系数
            /// </summary>
            private readonly Double[] y0;

            /// <summary>
            /// X轴数组，同time，直接复制time的数据
            /// </summary>
            private readonly Double[] x;

            /// <summary>
            /// 包络结果的输出
            /// </summary>
            private readonly Double[] y;

            /// <summary>
            /// 第一行是时间波形，其他4行保存中间结果
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

                // 将时间数值赋予x(t)数组,供后面的例程调用
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
            /// 如果上下极值点的数目小于3, 则出错
            /// </summary>
            private bool IsMaxMinNumberError()
            {
                return ( MaxNumber < 3 ) || ( MinNumber < 3 );
            }

            public Double[][] EMDDecomposition()
            {
                // 初始化数据
                Double[][] ret = ArrayUtils.CreateJaggedArray<double>( 4, DataLength );

                // 保存data中上一个下标的数据
                Double[] dataBackup = new Double[DataLength];

                // 用于第二次计算
                Double[] work2 = new Double[DataLength];

                //Double[] vTmpData = new Double[DataLength];

                Double[] max_0 = maxdata[0];
                Double[] max_1 = maxdata[1];

                Double[] min_0 = mindata[0];
                Double[] min_1 = mindata[1];

                const int IMFNumber = 4;
                for( int imfNumber = 1; imfNumber <= IMFNumber; imfNumber++ )
                {
                    // 当前imfNumber下标的data
                    Double[] imfData = data[imfNumber];

                    // 上一个imfNumber下标的data
                    Double[] imfDataLast = data[imfNumber - 1];

                    // 备份下标为imfNumber - 1的data数组
                    imfDataLast.CopyTo( dataBackup, 0 );

                    // 选取余量最大数值的阈值
                    Double threshold = NumbersUtils.AbsMax( imfDataLast ) / 10;

                    // 至少执行一次
                    while( true )
                    {
                        // 计算局部极值
                        CalcMaxMinData( imfDataLast );

                        // 如果上下极值点的数目小于3, 则停止执行
                        if( IsMaxMinNumberError() )
                        {
                            throw GetIMFNUException( imfNumber - 1 );
                        }

                        // 开始计算上包络线
                        for( int index = 0; index < MaxNumber; index++ )
                        {
                            x0[index] = max_0[index];
                            y0[index] = max_1[index];
                        }

                        splnsub( MaxNumber, DataLength );

                        y.CopyTo( imfData, 0 );

                        // 以上：上包络线计算完毕


                        // 开始计算下包络线
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

                        // 以上：下包络线和上包络线的中心线计算完毕,数值存放在 (data(imfnumber, k),k=0..DataLength-1)


                        // 开始计算IMF
                        for( int index = 0; index < DataLength; index++ )
                        {
                            imfData[index] = imfDataLast[index] - imfData[index];
                        }

                        // 验证是否为真实的IMF,重新计算上下包络线
                        CalcMaxMinData( imfData );

                        // 如果上下极值点的数目小于3, 则停止执行
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

                        // 准备下一次循环
                        imfData.CopyTo( imfDataLast, 0 );

                        // 计算中心线的平均时，需要考虑边界的影响
                        Double mean = 0;
                        for( int index = 100; index < DataLength - 100; index++ )
                        {
                            Double abs = Math.Abs( work2[index] );
                            if( mean < abs )
                            {
                                mean = abs;
                            }
                        }

                        // 退出循环的条件
                        if( mean <= threshold )
                        {
                            break;
                        }
                    } // while (true)


                    // 重新赋予原来的真实数值
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
