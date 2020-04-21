using System;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 基本数字信号处理算法库
    /// </summary>
    public static partial class DSPBasic
    {
        #region 频谱算法

        #region 基于转速将秒转化为转数(周期).

        /// <summary>
        /// 基于转速将秒转化为转数(周期).
        /// </summary>
        /// <param name="second">阶次</param>
        /// <param name="rpm">分钟转速</param>
        /// <returns>转数(周期)</returns>
        public static Double SecondtoCycle( _ValueT second, Double rpm )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( second * RpmtoHz( rpm ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 基于转速将转数(周期)转化为秒.
        /// </summary>
        /// <param name="cycle">阶次</param>
        /// <param name="rpm">分钟转速</param>
        /// <returns>转数(周期)</returns>
        public static Double CycletoSecond( _ValueT cycle, Double rpm )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( cycle * MathConst.SecondCountOfMinute / rpm );
            // ReSharper restore RedundantCast
        }

        #endregion

        #region Hz与阶次转换

        /// <summary>
        /// 基于转速将X转化为Hz.
        /// </summary>
        /// <param name="order">阶次</param>
        /// <param name="rpm">分钟转速</param>
        /// <returns>频率值</returns>
        public static Double XtoHz( _ValueT order, Double rpm )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( order * RpmtoHz( rpm ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 基于转速将Hz转化为X.
        /// </summary>
        /// <param name="hz">频率值</param>
        /// <param name="rpm">分钟转速</param>
        /// <returns>阶次</returns>
        public static Double HztoX( _ValueT hz, Double rpm )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( hz * MathConst.SecondCountOfMinute / rpm );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 基于转速将Hz转化为X.
        /// </summary>
        /// <param name="xArray">输入Hz数组，输出阶次数组</param>
        /// <param name="rpm">分钟转速</param>
        public static void HztoX( _ValueT[] xArray, Double rpm )
        {
            // ReSharper disable RedundantCast
            NumbersUtils.ScaleArray( xArray, (_ValueT)( MathConst.SecondCountOfMinute / rpm ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 基于转速将X转化为Hz.
        /// </summary>
        /// <param name="xArray">输入阶次数组，输出Hz数组</param>
        /// <param name="rpm">分钟转速</param>
        public static void XtoHz( _ValueT[] xArray, Double rpm )
        {
            // ReSharper disable RedundantCast
            NumbersUtils.ScaleArray( xArray, (_ValueT)( RpmtoHz( rpm ) ) );
            // ReSharper restore RedundantCast
        }

        #endregion

        /// <summary>
        /// 计算复序列基2快速傅立叶正变换(双边), x(t) => X(f)
        /// </summary>
        /// <param name="reArray">输入复信号实部Re(x)，返回FFT实部</param>
        /// <param name="imArray">输入复信号虚部Im(x)，返回FFT虚部</param>
        /// <remarks>
        /// 按照FFT标准公式计算，所有返回值均未除N. 
        /// </remarks>
        public static void CxFFT( _ValueT[] reArray, _ValueT[] imArray )
        {
            #region 输入参数合理性检查

            //检查实部与虚部数组的个数必须相等
            MathError.CheckLengthEqual( reArray, imArray );

            //检查实部与虚部数组的个数必须大于0
            MathError.CheckLengthGTZero( reArray );

            //检查数组长度是否2的幂次
            MathError.CheckPowerOfTwo( reArray.Length );

            #endregion

            Transform.ComplexFFT( reArray, imArray, reArray.Length, 1 );
        }

        /// <summary>
        /// 计算复序列基2快速傅立叶逆变换(双边), X(f) => x(t)
        /// </summary>
        /// <param name="reArray">输入FFT实部，返回复信号实部</param>
        /// <param name="imArray">输入FFT虚部，返回复信号虚部</param>
        /// <remarks>
        /// 返回值均除以N，即：reArray[i]=Re{x[i]/N}, imArray[i]=Im{x[i]/N}. 
        /// </remarks>
        public static void CxInvFFT( _ValueT[] reArray, _ValueT[] imArray )
        {
            #region 输入参数合理性检查

            //检查实部与虚部数组的个数必须相等
            MathError.CheckLengthEqual( reArray, imArray );

            //检查实部与虚部数组的个数必须大于0
            MathError.CheckLengthGTZero( reArray );

            //检查数组长度是否2的幂次
            MathError.CheckPowerOfTwo( reArray.Length );

            #endregion

            Transform.ComplexFFT( reArray, imArray, reArray.Length, -1 );
        }


        /// <summary>
        /// 计算实序列基2快速傅立叶正变换(双边)
        /// </summary>
        /// <param name="reArray">输入复信号实部数组，返回FFT实部数组</param>
        /// <param name="imArray">输入复信号虚部数组，返回FFT虚部数组</param>
        /// <remarks>
        /// 按照FFT标准公式计算，所有返回值均未除N. 
        /// </remarks>
        public static void ReFFT( _ValueT[] reArray, out _ValueT[] imArray )
        {
            #region 输入参数合理性检查

            //检查实部与虚部数组的个数必须大于0
            MathError.CheckLengthGTZero( reArray );

            //检查数组长度是否2的幂次
            MathError.CheckPowerOfTwo( reArray.Length );

            #endregion

            imArray = new _ValueT[reArray.Length];

            Transform.RealFFT( reArray, imArray, reArray.Length, 1 );
        }

        /// <summary>
        /// 从频谱的实部、虚部得到双边幅值谱
        /// </summary>
        /// <param name="reArray">频谱的实部</param>
        /// <param name="imArray">频谱的虚部</param>
        /// <param name="divLength">是否除数据的长度，主要为了兼容两种FFT的计算方法</param>
        /// <returns>双边幅值谱</returns>
        public static _ValueT[] ReIm2AmpSpectrum( _ValueT[] reArray, _ValueT[] imArray, bool divLength )
        {
            _ValueT[] ret = new _ValueT[reArray.Length];
            int length = divLength ? reArray.Length : 1;
            for( int index = 0; index < ret.Length; index++ )
            {
                Double squareSum = MathBasic.SquareSum( reArray[index], imArray[index] );
                // ReSharper disable RedundantCast
                ret[index] = (_ValueT)( Math.Sqrt( squareSum ) / length );
                // ReSharper restore RedundantCast
            }
            return ret;
        }

        /// <summary>
        /// 从频谱的实部、虚部得到相位谱，相位谱为0到360度
        /// </summary>
        /// <param name="reArray">频谱的实部</param>
        /// <param name="imArray">频谱的虚部</param>
        /// <returns>相位谱</returns>
        public static _ValueT[] ReIm2PhaseSpectrum( _ValueT[] reArray, _ValueT[] imArray )
        {
            _ValueT[] ret = new _ValueT[reArray.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                // ReSharper disable RedundantCast
                ret[index] = (_ValueT)MathBasic.ReIm2Phase180( reArray[index], imArray[index] );
                // ReSharper restore RedundantCast
            }
            return ret;
        }

        /// <summary>
        /// 计算双边频谱的实部和虚部，已经除N。
        /// </summary>
        /// <param name="xArray">输入信号</param>
        /// <param name="reArray">频谱的实部</param>
        /// <param name="imArray">频谱的虚部</param>
        public static void BiReImSpectrum( _ValueT[] xArray, out _ValueT[] reArray, out _ValueT[] imArray )
        {
            #region 输入参数合理性检查

            //检查实部与虚部数组的个数必须大于0
            MathError.CheckLengthGTZero( xArray );

            //检查数组长度是否2的幂次
            MathError.CheckPowerOfTwo( xArray.Length );

            #endregion

            int length = xArray.Length;
            reArray = new _ValueT[length];
            imArray = new _ValueT[length];
            xArray.CopyTo( reArray, 0 );
            CxFFT( reArray, imArray );

            for( int index = 0; index < reArray.Length; index++ )
            {
                reArray[index] /= length;
                imArray[index] /= length;
            }
        }

        /// <summary>
        /// 计算单边频谱的实部和虚部( 谱线数 = 输入信号长度 / 2)，已经除N。
        /// </summary>
        /// <param name="xArray">输入信号</param>
        /// <param name="reArray">频谱的实部</param>
        /// <param name="imArray">频谱的虚部</param>
        public static void ReImSpectrum( _ValueT[] xArray, out _ValueT[] reArray, out _ValueT[] imArray )
        {
            _ValueT[] re, im;
            BiReImSpectrum( xArray, out re, out im );

            reArray = new _ValueT[xArray.Length / 2];
            imArray = new _ValueT[reArray.Length];

            reArray[0] = re[0];
            imArray[0] = im[0];
            for( int index = 1; index < reArray.Length; index++ )
            {
                reArray[index] = 2 * re[index];
                imArray[index] = 2 * im[index];
            }
        }

        /// <summary>
        /// 计算双边幅值相位谱.
        /// </summary>
        /// <param name="xArray">输入信号</param>
        /// <param name="ampSpectrum">返回双边幅值谱</param>
        /// <param name="phaseSpectrum">返回双边相位谱(单位：度)，-180° ~ 180°</param>
        //	计算公式如下: 
        //				AmpSpectrum= |FFT{x}| / N
        public static void BiAmpPhaseSpectrum( _ValueT[] xArray, out _ValueT[] ampSpectrum, out _ValueT[] phaseSpectrum )
        {
            #region 输入参数合理性检查

            //检查实部与虚部数组的个数必须大于0
            MathError.CheckLengthGTZero( xArray );

            //检查数组长度是否2的幂次
            MathError.CheckPowerOfTwo( xArray.Length );

            #endregion

            int length = xArray.Length;
            _ValueT[] reArray = new _ValueT[length];
            _ValueT[] imArray = new _ValueT[length];
            xArray.CopyTo( reArray, 0 );
            CxFFT( reArray, imArray );

            ampSpectrum = ReIm2AmpSpectrum( reArray, imArray, true );
            phaseSpectrum = ReIm2PhaseSpectrum( reArray, imArray );
        }

        /// <summary>
        /// 计算单边幅值相位谱( 谱线数 = 输入信号长度 / 2)
        /// </summary>
        /// <param name="xArray">输入信号x[i]</param>
        /// <param name="ampSpectrum">返回单边幅值谱</param>
        /// <param name="phaseSpectrum">返回相位谱(单位：度)</param>
        /// <remarks>
        /// 可以直接显示， 无须乘或除任何系数。例如，1024点的时间波形得到512点谱。
        /// </remarks>
        public static void AmpPhaseSpectrum( _ValueT[] xArray, out _ValueT[] ampSpectrum, out _ValueT[] phaseSpectrum )
        {
            _ValueT[] amplitude, phase;
            BiAmpPhaseSpectrum( xArray, out amplitude, out phase );

            int lineNum = xArray.Length / 2;
            ampSpectrum = new _ValueT[lineNum];
            phaseSpectrum = new _ValueT[lineNum];

            for( int index = 0; index < ampSpectrum.Length; index++ )
            {
                ampSpectrum[index] = 2 * amplitude[index];
                phaseSpectrum[index] = phase[index];
            }
            ampSpectrum[0] /= 2;
        }

        /// <summary>
        /// 计算双边功率谱.
        /// </summary>
        /// <param name="xArray">原始信号</param>
        /// <param name="powerSpectrum">返回双边功率谱</param>
        /// <param name="phaseSpectrum">返回双边相位谱(单位：度)</param>
        //	计算公式如下: 
        //				Gs(f) = |FFT{x}|^2 / N^2 = Amp{x}^2
        public static void BiPowerSpectrum( _ValueT[] xArray, out _ValueT[] powerSpectrum, out _ValueT[] phaseSpectrum )
        {
            _ValueT[] ampSpectrum;
            BiAmpPhaseSpectrum( xArray, out ampSpectrum, out phaseSpectrum );

            powerSpectrum = new _ValueT[ampSpectrum.Length];
            for( int index = 0; index < powerSpectrum.Length; index++ )
            {
                powerSpectrum[index] = ampSpectrum[index] * ampSpectrum[index];
            }
        }

        /// <summary>
        /// 计算单边功率谱.( 谱线数 = 原始信号长度 / 2)
        /// </summary>
        /// <param name="xArray">原始信号</param>
        /// <returns>单边功率谱</returns>
        /// <remarks>
        /// 无须乘或除任何系数。例如：1024点的时间波形得到512点谱。
        /// </remarks>
        //	计算公式如下: 
        //				Gs(f) = 2 * |FFT{x}|^2 / N^2 = 2 * Amp{x}^2
        public static _ValueT[] PowerSpectrum( _ValueT[] xArray )
        {
            _ValueT[] ampSpectrum, phaseSpectrum;
            BiAmpPhaseSpectrum( xArray, out ampSpectrum, out phaseSpectrum );

            _ValueT[] powerSpectrum = new _ValueT[ampSpectrum.Length / 2];
            for( int index = 0; index < powerSpectrum.Length; index++ )
            {
                powerSpectrum[index] = 2 * ampSpectrum[index] * ampSpectrum[index];
            }
            powerSpectrum[0] /= 2;

            return powerSpectrum;
        }

        /// <summary>
        /// 计算希尔伯特变换。对于实信号， re[]=实信号，im[]=0。
        /// </summary>
        /// <param name="reArray">输入信号的实部数组，输出变换后的实部数组</param>
        /// <param name="imArray">输入信号的虚部数组，输出变换后的虚部数组</param>
        public static void HilbertT( _ValueT[] reArray, _ValueT[] imArray )
        {
            #region 输入参数合理性检查

            //检查实部与虚部数组的个数必须相等
            MathError.CheckLengthEqual( reArray, imArray );

            //检查实部与虚部数组的个数必须大于0
            MathError.CheckLengthGTZero( reArray );

            //检查数组长度是否2的幂次
            MathError.CheckPowerOfTwo( reArray.Length );

            #endregion

            Transform.Hilbert( reArray, imArray );
        }

        #endregion

        #region 时域信号处理

        /// <summary>
        /// 计算两个数组的直接卷积Cxy
        /// </summary>
        /// <param name="xArray">第一个数组[n]</param>
        /// <param name="yArray">第二个数组[m]</param>
        /// <returns>卷积结果数组[n+m-1]</returns>
        public static _ValueT[] DirectConvolve( _ValueT[] xArray, _ValueT[] yArray )
        {
            #region 输入参数合理性检查

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            //系统默认x数组长度 >= y数组长度
            if( xArray.Length >= yArray.Length )
            {
                return Convolve.DirectConvolve( xArray, yArray );
            }

            //如果x数组长度 < y数组长度,交换x和y数组
            return Convolve.DirectConvolve( yArray, xArray );
        }

        /// <summary>
        /// 进行Butterworth带通滤波
        /// </summary>
        /// <param name="xArray">输入信号数组</param>
        /// <param name="order">滤波器阶数</param>
        /// <param name="fl">低截止频率</param>
        /// <param name="fh">高截止频率</param>
        /// <param name="fs">采样频率</param>
        /// <returns>返回滤波后的数据</returns>
        public static _ValueT[] ButterworthBandPass( _ValueT[] xArray, int order, Double fl, Double fh,
                                                     Double fs )
        {
            #region 输入参数合理性检查

            MathError.CheckLengthGTZero( xArray );

            //检查阶数必须大于0
            if( order <= 0 )
            {
                throw new AlgorithmException( MathError.MathErrorOrderGTZero );
            }

            //检查截止频率必须大于0
            if( fl < 0 )
            {
                fl = 0;
            }
            Double freqBand = GetFreqBandBySampleFreq( fs );
            if( fh <= 0 )
            {
                fh = freqBand;
            }
            if( fh > freqBand )
            {
                fh = freqBand;
            }

            //检查截止频率上下限关系
            if( fl > fh )
            {
                Double mid = fl;
                fl = fh;
                fh = mid;
            }

            #endregion

            return DigitalIIR.ButterworthBandPass( xArray, fl, fh, order, fs );
        }

        /// <summary>
        /// 计算两个数组的直接相关Rxy(已做端点效应修正)
        /// </summary>
        /// <param name="xArray">输入第一个数组x[n]</param>
        /// <param name="yArray">输入第二个数组y[m]</param>
        /// <param name="dt">输入采样时间间隔</param>
        /// <param name="rxyArray">返回相关结果数组</param>
        /// <param name="tauArray">返回时间延迟数组</param>
        /// <remarks>
        /// 如果时间延迟tau[i]大于0, 表示信号y[m]超前x[n];如果时间延迟tau[i]小于0, 表示信号y[m]滞后x[n].
        /// </remarks>
        public static void DirectCorrelate( _ValueT[] xArray, _ValueT[] yArray, _ValueT dt,
                                            out _ValueT[] rxyArray, out _ValueT[] tauArray )
        {
            #region 输入参数合理性检查

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            Correlate.DirectCorrelate( xArray, yArray, dt, out rxyArray, out tauArray );
        }


        /// <summary>
        /// 计算两个数组的快速相关Rxy(已做端点效应修正)
        /// </summary>
        /// <param name="xArray">输入第一个数组x[n]</param>
        /// <param name="yArray">输入第二个数组y[m]</param>
        /// <param name="dt">输入采样时间间隔</param>
        /// <param name="rxyArray">返回相关结果数组</param>
        /// <param name="tauArray">返回时间延迟数组</param>
        /// <remarks>
        /// 如果时间延迟tau[i]大于0, 表示信号y[m]超前x[n];如果时间延迟tau[i]小于0, 表示信号y[m]滞后x[n].
        /// </remarks>
        public static void FastCorrelate( _ValueT[] xArray, _ValueT[] yArray, _ValueT dt,
                                          out _ValueT[] rxyArray, out _ValueT[] tauArray )
        {
            #region 输入参数合理性检查

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            Correlate.FastCorrelate( xArray, yArray, dt, out rxyArray, out tauArray );
        }

        /// <summary>
        /// 去除直流分量
        /// </summary>
        /// <param name="xArray">输入原始信号，返回AC后的数组</param>
        public static void ACCoupling( _ValueT[] xArray )
        {
            if( 0 < xArray.Length )
            {
                _ValueT average = StatisticsUtils.Mean( xArray );
                for( int index = 0; index < xArray.Length; index++ )
                {
                    xArray[index] -= average;
                }
            }
        }

        #endregion

        #region 时域有量纲指标

        /// <summary>
        /// 真峰峰值. 反映了信号瞬时作用强度, 反映了幅值变化范围,以及偏离中心情况. 根据理论公式计算, 常作为位移信号的数字特征.
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>真峰峰值</returns>
        //计算公式： max(x[])-min(x[])
        public static _ValueT TruePeak2Peak( _ValueT[] xArray )
        {
            if( 0 < xArray.Length )
            {
                _ValueT min = xArray[0];
                _ValueT max = min;

                foreach( _ValueT value in xArray )
                {
                    if( value > max )
                    {
                        max = value;
                    }
                    if( value < min )
                    {
                        min = value;
                    }
                }
                return max - min;
            }
            return 0;
        }

        /// <summary>
        /// 峰峰值. 反映了信号瞬时作用强度. 根据RMS值计算,常作为工程应用中位移信号的数字特征.
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>峰峰值</returns>
        //计算公式： 2×sqrt(2)×RMS
        public static _ValueT Peak2Peak( _ValueT[] xArray )
        {
            return 2 * Peak( xArray );
        }

        /// <summary>
        /// 真峰值. 反映了信号瞬时作用最大强度. 根据理论公式计算,常作为加速度信号的数字特征.
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>真峰值</returns>
        //	使用CollectionUtils.AbsMax()方法
        public static _ValueT TruePeak( _ValueT[] xArray )
        {
            return NumbersUtils.AbsMax( xArray );
        }

        /// <summary>
        /// 峰值. 反映了信号瞬时作用最大强度. 根据RMS值计算,常作为工程应用中加速度信号的数字特征.
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>峰值</returns>
        //计算公式： sqrt(2)×RMS
        public static _ValueT Peak( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( MathConst.SqrtTwo * RMS( xArray ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 总值. 反映了信号的平均功率. 根据RMS值计算,常作为工程应用中位移、速度和加速度信号的数字特征.
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>总值</returns>
        //	计算公式： RMS^2
        public static _ValueT Overall( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)MathBasic.Square( RMS( xArray ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 有效值(又叫均方根值). 反映了信号作用强度. 常作为速度信号的数字特征.
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>有效值</returns>
        //	使用StatisticsUtils.OriginMoment算法
        //	计算公式： sqrt(sum(x[t]^2/N))
        public static _ValueT RMS( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)Math.Sqrt( StatisticsUtils.OriginMoment( xArray, 2 ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 均方值. 反映了信号作用强度. 常作为速度信号的数字特征.
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>均方值</returns>
        //计算公式： RMS^2
        public static _ValueT MeanSquare( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)MathBasic.Square( RMS( xArray ) );
            // ReSharper restore RedundantCast
        }

        #endregion

        #region 时域无量纲指标

        /// <summary>
        /// 波形指标.(无量纲指标)
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>波形指标</returns>
        /// <remarks>
        /// 对信号的幅值和频率变化不敏感（与机器工作条件关系不大），而对故障足够敏感。
        /// </remarks>
        //	使用MathBasic.Div()方法
        public static _ValueT ShapeFactor( _ValueT[] xArray )
        {
            return MathBasic.Div( RMS( xArray ), StatisticsUtils.AbsMean( xArray ) );
        }

        /// <summary>
        /// 脉冲指标. (无量纲指标)
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>脉冲指标</returns>
        /// <remarks>
        /// 对信号的幅值和频率变化不敏感（与机器工作条件关系不大），而对故障足够敏感。
        /// </remarks>
        //	使用CollectionUtils.AbsMax()方法替换CollectionUtils.Max()
        //	使用MathBasic.Div()方法和CollectionUtils.AbsMax()
        public static _ValueT ImpulseFactor( _ValueT[] xArray )
        {
            return MathBasic.Div( NumbersUtils.AbsMax( xArray ), StatisticsUtils.AbsMean( xArray ) );
        }

        /// <summary>
        /// 峰值指标. (无量纲指标)
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>峰值指标</returns>
        /// <remarks>
        /// 对信号的幅值和频率变化不敏感（与机器工作条件关系不大），而对故障足够敏感。
        /// </remarks>
        //	使用CollectionUtils.AbsMax()方法替换CollectionUtils.Max()
        //	使用MathBasic.Div()方法和CollectionUtils.AbsMax()
        public static _ValueT CrestFactor( _ValueT[] xArray )
        {
            return MathBasic.Div( NumbersUtils.AbsMax( xArray ), RMS( xArray ) );
        }

        /// <summary>
        /// 裕度指标. (无量纲指标)
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>裕度指标</returns>
        /// <remarks>
        /// 对信号的幅值和频率变化不敏感（与机器工作条件关系不大），而对故障足够敏感。
        /// </remarks>
        //	使用CollectionUtils.AbsMax()方法替换CollectionUtils.Max()
        //	使用MathBasic.Div()方法和CollectionUtils.AbsMax()
        public static _ValueT ClearanceFactor( _ValueT[] xArray )
        {
            return MathBasic.Div( NumbersUtils.AbsMax( xArray ), StatisticsUtils.SMR( xArray ) );
        }

        /// <summary>
        /// 歪度指标. (无量纲指标)
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>歪度指标</returns>
        /// <remarks>
        /// 对信号的幅值和频率变化不敏感（与机器工作条件关系不大），而对故障足够敏感。
        /// </remarks>
        public static _ValueT SkewFactor( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)MathBasic.Div( StatisticsUtils.Moment( xArray, 3 ),
                                           MathBasic.PositivePow( StatisticsUtils.StdDeviation( xArray ), 3 ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 峭度指标. (无量纲指标)
        /// </summary>
        /// <param name="xArray">输入原始信号</param>
        /// <returns>峭度指标</returns>
        /// <remarks>
        /// 对信号的幅值和频率变化不敏感（与机器工作条件关系不大），而对故障足够敏感。
        /// </remarks>
        public static _ValueT KurtoFactor( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)MathBasic.Div( StatisticsUtils.Moment( xArray, 4 ),
                                           MathBasic.PositivePow( StatisticsUtils.StdDeviation( xArray ), 4 ) );
            // ReSharper restore RedundantCast
        }

        #endregion

        #region 信号加窗算法

        /// <summary>
        /// 对信号应用一个三角窗
        /// </summary>
        /// <param name="xArray">一维输入信号</param>
        /// <returns>返回加窗后的信号</returns>
        public static _ValueT[] TriangleWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenTriWindow( xArray.Length );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// 对信号应用一个汉宁窗
        /// </summary>
        /// <param name="xArray">一维输入信号</param>
        /// <returns>返回加窗后的信号</returns>
        public static _ValueT[] HanningWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenCosWindows( WindowType.Hanning, xArray.Length );
            return MaskWindow( xArray, win, Window.AmpScale_Hanning );
        }

        /// <summary>
        /// 对信号应用一个哈明窗
        /// </summary>
        /// <param name="xArray">一维输入信号</param>
        /// <returns>返回加窗后的信号</returns>
        public static _ValueT[] HammingWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenCosWindows( WindowType.Hamming, xArray.Length );
            return MaskWindow( xArray, win, Window.AmpScale_Hamming );
        }

        /// <summary>
        /// 对信号应用一个布莱克曼窗
        /// </summary>
        /// <param name="xArray">一维输入信号</param>
        /// <returns>返回加窗后的信号</returns>
        public static _ValueT[] BlackmanWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenCosWindows( WindowType.Blackman, xArray.Length );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// 对信号应用一个平顶窗
        /// </summary>
        /// <param name="xArray">一维输入信号</param>
        /// <returns>返回加窗后的信号</returns>
        public static _ValueT[] FlatTopWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenCosWindows( WindowType.FlatTop, xArray.Length );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// 对信号应用一个平顶窗
        /// </summary>
        /// <param name="xArray">一维输入信号</param>
        /// <param name="final">指数窗终值(推荐final=0.01)</param>
        /// <returns>返回加窗后的信号</returns>
        public static _ValueT[] ExpWindow( _ValueT[] xArray, Single final )
        {
            Single[] win = Window.GenExpWindow( xArray.Length, final );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// 对信号应用一个力窗
        /// </summary>
        /// <param name="xArray">一维输入信号</param>
        /// <param name="duty">延迟的百分比(推荐duty=50)</param>
        /// <returns>返回加窗后的信号</returns>
        public static _ValueT[] ForceWindow( _ValueT[] xArray, Single duty )
        {
            Single[] win = Window.GenForceWindow( xArray.Length, duty );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// 给信号加窗
        /// </summary>
        /// <param name="xArray">信号</param>
        /// <param name="window">窗系数</param>
        private static _ValueT[] MaskWindow( _ValueT[] xArray, Single[] window )
        {
            return MaskWindow( xArray, window, Window.AmpScale_Rectangle );
        }

        /// <summary>
        /// 给信号加窗
        /// </summary>
        /// <param name="xArray">信号</param>
        /// <param name="window">窗系数</param>
        /// <param name="ampScale">幅值相等恢复系数</param>
        private static _ValueT[] MaskWindow( _ValueT[] xArray, Single[] window, Single ampScale )
        {
            _ValueT[] ret = new _ValueT[xArray.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = xArray[index] * window[index] * ampScale;
            }
            return ret;
        }

        #endregion
    }
}