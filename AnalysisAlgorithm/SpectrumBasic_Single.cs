using System;
using System.Collections.Generic;
using Moons.Common20;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 与频谱相关的基本算法类
    /// </summary>
    public static partial class SpectrumBasic
    {
        #region 获得工频处的信息

        /// <summary>
        /// 获得工频处的单边频谱的实部和虚部
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <param name="f0">工频</param>
        /// <param name="timeWave">时间波形</param>
        /// <param name="re">工频处的单边频谱的实部</param>
        /// <param name="im">工频处的单边频谱的虚部</param>
        public static void GetBaseFreqReIm(Double fs, Double f0, _ValueT[] timeWave, out _ValueT re, out _ValueT im)
        {
            int baseFreqIndex = GetBaseFreqIndex(fs, f0, timeWave);
            _ValueT[] reArray, imArray;
            DSPBasic.ReImSpectrum(timeWave, out reArray, out imArray);

            re = reArray[baseFreqIndex];
            im = imArray[baseFreqIndex];
        }

        /// <summary>
        /// 获得工频处的下标
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <param name="f0">工频</param>
        /// <param name="timeWave">时间波形</param>
        /// <returns>工频处的下标</returns>
        public static int GetBaseFreqIndex(Double fs, Double f0, _ValueT[] timeWave)
        {
            int centerIndex = GetFreqIndex(fs, f0, timeWave.Length);

            const int SearchOffset = 2;
            const int SearchCount = 2 * SearchOffset + 1;
            _ValueT[] ampSpectrum = AmpSpectrum(timeWave);
            return ArrayUtils.MaxIndex(ampSpectrum, centerIndex - SearchOffset, SearchCount);
        }

        /// <summary>
        /// 获得工频处的下标
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <param name="beginFrequency">频率范围起始</param>
        /// <param name="endFrequency">频率范围截至</param>
        /// <param name="timeWave">时间波形</param>
        /// <param name="amplitudeOfF0">频谱的工频幅值</param>
        /// <returns>工频处的下标</returns>
        public static int GetBaseFreqIndex(Double fs, Double beginFrequency, Double endFrequency, _ValueT[] timeWave, out Double amplitudeOfF0)
        {
            int beginIndex = GetFreqIndex(fs, Math.Min(beginFrequency, endFrequency), timeWave.Length);
            int endIndex = GetFreqIndex(fs, Math.Max(beginFrequency, endFrequency), timeWave.Length);

            _ValueT[] ampSpectrum = AmpSpectrum(timeWave);
            var indexOfF0 = ArrayUtils.MaxIndex(ampSpectrum, beginIndex, endIndex - beginIndex + 1);

            amplitudeOfF0 = ampSpectrum[indexOfF0];

            return indexOfF0;
        }

        /// <summary>
        /// 获得工频
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <param name="beginFreq">频率范围起始</param>
        /// <param name="endFreq">频率范围截至</param>
        /// <param name="timeWave">时间波形</param>
        /// <param name="amplitudeOfF0">频谱的工频幅值</param>
        /// <returns>工频</returns>
        public static Double GetBaseFreq(Double fs, Double beginFreq, Double endFreq, _ValueT[] timeWave, out Double amplitudeOfF0)
        {
            int index = GetBaseFreqIndex(fs, beginFreq, endFreq, timeWave, out amplitudeOfF0);
            return GetFreqByIndex(fs, index, timeWave.Length);
        }

        #endregion

        #region 获得总值

        /// <summary>
        /// 总值. 反映了信号的平均功率. 根据频谱计算,常作为工程应用中位移、速度和加速度信号的数字特征.
        /// </summary>
        /// <param name="xArray">时间波形</param>
        /// <returns>总值</returns>
        public static _ValueT Overall(_ValueT[] xArray)
        {
            return Overall_AmpSpectrum(AmpSpectrum(xArray));
        }

        /// <summary>
        /// 通过单边幅值谱获得总值
        /// </summary>
        /// <param name="ampSpectrum">幅值谱</param>
        /// <returns>总值</returns>
        public static _ValueT Overall_AmpSpectrum(_ValueT[] ampSpectrum)
        {
            return Overall_AmpSpectrum(ampSpectrum, 0, ampSpectrum.Length, WindowType.Rectangular);
        }
        /// <summary>
        /// 通过有效值单边谱获得总值
        /// </summary>
        /// <param name="rmsSpectrum">有效值谱</param>
        /// <returns>总值</returns>
        public static _ValueT Overall_RmsSpectrum(_ValueT[] rmsSpectrum)
        {
            return Overall_RmsSpectrum(rmsSpectrum, 0, rmsSpectrum.Length, WindowType.Rectangular);
        }

        /// <summary>
        /// 通过单边幅值谱获得总值
        /// </summary>
        /// <param name="ampSpectrum">幅值谱</param>
        /// <param name="startIndex">起始下标</param>
        /// <param name="count">个数</param>
        /// <param name="windowType">信号窗类型</param>
        /// <returns>总值</returns>
        public static _ValueT Overall_AmpSpectrum(_ValueT[] ampSpectrum, int startIndex, int count,
                                                   WindowType windowType)
        {
            var ret = Overall_RmsSpectrum(ampSpectrum, startIndex, count, windowType);
            ret /= MathConst.SqrtTwo;
            // ReSharper disable RedundantCast
            return (_ValueT)ret;
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// 通过有效值单边谱获得总值
        /// </summary>
        /// <param name="rmsSpectrum">有效值谱</param>
        /// <param name="startIndex">起始下标</param>
        /// <param name="count">个数</param>
        /// <param name="windowType">信号窗类型</param>
        /// <returns>总值</returns>
        public static _ValueT Overall_RmsSpectrum(_ValueT[] rmsSpectrum, int startIndex, int count,
                                                   WindowType windowType = WindowType.Rectangular)
        {
            if (CollectionUtils.IsNullOrEmptyG(rmsSpectrum) || count < 1 || rmsSpectrum.Length <= startIndex)
            {
                return 0;
            }

            startIndex = IndexUtils.GetIndexInRange(startIndex, 0, rmsSpectrum.Length - 1);
            count = Math.Min(count, rmsSpectrum.Length - startIndex);
            Double ret = MathBasic.SquareSum(ArrayUtils.SliceSpan(rmsSpectrum, startIndex, count));
            if (windowType == WindowType.Hanning)
            {
                ret *= Window.PowerScale_Hanning;
            }
            // ReSharper disable RedundantCast
            return (_ValueT)Math.Sqrt(ret);
            // ReSharper restore RedundantCast
        }
        #endregion

        /// <summary>
        /// 计算单边幅值谱( 谱线数 = 输入信号长度 / 2.56 + 1)
        /// </summary>
        /// <param name="xArray">输入信号x[i]</param>
        /// <returns>单边谱</returns>
        /// <remarks>
        /// 可以直接显示， 无须乘或除任何系数。例如，1024点的时间波形得到401点谱。
        /// </remarks>
        public static _ValueT[] AmpSpectrum(_ValueT[] xArray)
        {
            _ValueT[] biSideSpectrum, phaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum(xArray, out biSideSpectrum, out phaseSpectrum);

            int lineCount = DSPBasic.GetSpectrumLengthByTimeWaveLength(xArray.Length);
            _ValueT[] ampSpectrum = new _ValueT[lineCount];
            for (int index = 0; index < lineCount; index++)
            {
                ampSpectrum[index] = 2 * biSideSpectrum[index];
            }
            ampSpectrum[0] /= 2;

            return ampSpectrum;
        }

        /// <summary>
        /// 计算相位谱( 谱线数 = 输入信号长度 / 2.56 + 1)，-180° ~ 180°
        /// </summary>
        /// <param name="xArray">输入信号x[i]</param>
        /// <returns>相位谱，-180° ~ 180°</returns>
        public static _ValueT[] PhaseSpectrum(_ValueT[] xArray)
        {
            _ValueT[] biSideSpectrum, phaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum(xArray, out biSideSpectrum, out phaseSpectrum);

            int lineCount = DSPBasic.GetSpectrumLengthByTimeWaveLength(xArray.Length);
            _ValueT[] ret = new _ValueT[lineCount];
            ArrayUtils.Copy(phaseSpectrum, ret, 0, ret.Length);

            return ret;
        }

        /// <summary>
        /// 返回频谱X轴的数据，用于显示
        /// </summary>
        /// <param name="fs">采样频率，如果是等角度采样则传入倍频系数</param>
        /// <param name="dataLength">采样的数据长度</param>
        /// <param name="retLength">希望返回的数组的长度</param>
        /// <returns>频谱X轴的数据，用于显示</returns>
        public static _ValueT[] SpectrumXAxisTicks(_ValueT fs, int dataLength, int retLength)
        {
            _ValueT delta = fs / dataLength;
            _ValueT value = 0;
            _ValueT[] ret = new _ValueT[retLength];
            for (int index = 0; index < ret.Length; index++)
            {
                ret[index] = value;
                value += delta;
            }
            return ret;
        }

        /// <summary>
        /// 返回时间波形X轴的数据，用于显示，等时间采样时单位是毫秒
        /// </summary>
        /// <param name="fs">采样频率</param>
        /// <param name="isOrder">是否是等角度采样</param>
        /// <param name="dataLength">采样的数据长度</param>
        /// <returns>时间波形X轴的数据，用于显示</returns>
        public static _ValueT[] TimeWaveXAxisTicks(_ValueT fs, bool isOrder, int dataLength)
        {
            _ValueT[] ret = new _ValueT[dataLength];

            if (isOrder)
            {
                for (int index = 0; index < ret.Length; index++)
                {
                    ret[index] = index + 1;
                }
                return ret;
            }

            _ValueT deltaT = 1f / fs * 1000;
            _ValueT currentTime = 0;
            for (int index = 0; index < ret.Length; index++)
            {
                currentTime += deltaT;
                ret[index] = currentTime;
            }
            return ret;
        }

        /// <summary>
        /// 计算比谱和差谱.
        /// </summary>
        /// <param name="waveType">输入信号的数据类型</param>
        /// <param name="xArray">输入信号x[i]</param>
        /// <param name="yArray">输入信号y[i]</param>
        /// <param name="diffSpectrum">输出差谱</param>
        /// <param name="ratioSpectrum">输出比谱</param>
        public static void DiffRatioSpectrum(WaveType waveType, _ValueT[] xArray, _ValueT[] yArray,
                                              out _ValueT[] diffSpectrum, out _ValueT[] ratioSpectrum)
        {
            #region 输入参数合理性检查

            MathError.CheckLengthEqual(xArray, yArray);

            #endregion

            bool isTimeWave = (WaveType.TimeWave == waveType);
            _ValueT[] xAmpArray = isTimeWave ? AmpSpectrum(xArray) : xArray;
            _ValueT[] yAmpArray = isTimeWave ? AmpSpectrum(yArray) : yArray;
            diffSpectrum = NumbersUtils.SubArray(xAmpArray, yAmpArray);
            ratioSpectrum = NumbersUtils.DivArray(xAmpArray, yAmpArray);
        }

        /// <summary>
        /// 获得波形数据中的主要峰值的序号(从大到小排列).
        /// </summary>
        /// <param name="xArray">波形数据</param>
        /// <param name="includeTwoSide">是否包含两端的峰值</param>
        /// <returns>主要峰值的下标，如果未找到任何峰值则返回null</returns>
        public static int[] MaxPeaksIndex(_ValueT[] xArray, bool includeTwoSide)
        {
            switch (xArray.Length)
            {
                case 1:
                    return includeTwoSide ? new[] { 0 } : null;

                case 0:
                    return null;
            }

            List<int> peakIndexList = new List<int>();
            int lineCount = xArray.Length;

            // 包括两端的峰值
            if (includeTwoSide)
            {
                if (xArray[0] > xArray[1])
                {
                    peakIndexList.Add(0);
                }
                if (xArray[lineCount - 1] > xArray[lineCount - 2])
                {
                    peakIndexList.Add(lineCount - 1);
                }
            }


            for (int index = 1; index < lineCount - 1; index++)
            {
                if (
                    (xArray[index - 1] < xArray[index]) &&
                    (xArray[index] > xArray[index + 1])
                    )
                {
                    peakIndexList.Add(index);
                }
            }

            if (peakIndexList.Count == 0)
            {
                return null;
            }

            int[] peakIndexes = peakIndexList.ToArray();
            _ValueT[] keys = new _ValueT[peakIndexes.Length];
            int keyIndex = 0;
            foreach (int peakIndex in peakIndexes)
            {
                keys[keyIndex++] = xArray[peakIndex];
            }
            return ArrayUtils.SortArray(keys, peakIndexes, false);
        }

        /// <summary>
        /// 获得波形数据中最大的n个主要峰值(从大到小排列).
        /// </summary>
        /// <param name="xArray">波形数据</param>
        /// <param name="includeTwoSide">是否包含两端的峰值</param>
        /// <param name="maxNum">最大峰值个数</param>
        /// <returns>n个主要峰值，如果未找到任何峰值则返回null</returns>
        public static _ValueT[] MaxPeaks(_ValueT[] xArray, bool includeTwoSide, int maxNum)
        {
            if (maxNum <= 0)
            {
                return null;
            }

            int[] peakIndex = MaxPeaksIndex(xArray, includeTwoSide);
            if (peakIndex == null)
            {
                return null;
            }

            _ValueT[] maxPeaks = new _ValueT[Math.Min(maxNum, peakIndex.Length)];
            for (int index = 0; index < maxPeaks.Length; index++)
            {
                maxPeaks[index] = xArray[peakIndex[index]];
            }

            return maxPeaks;
        }

        /// <summary>
        /// 将线性谱转化为分贝谱.
        /// </summary>
        /// <param name="linearSpectrum">线性谱</param>
        /// <param name="vibQtyType">振动量类型</param>
        /// <param name="isAmpSpectrum">是否幅值谱(true为幅值谱, false为功率谱)</param>
        /// <returns>分贝谱</returns>
        //	将功率谱的基准值取平方
        private static _ValueT[] LinearToDBSpectrum(_ValueT[] linearSpectrum, VibQtyType vibQtyType, bool isAmpSpectrum)
        {
            // 基准值
            Double x0 = 1;

            switch (vibQtyType)
            {
                case VibQtyType.Force:
                    x0 = ISOR1683.f0;
                    break;

                case VibQtyType.Displacement:
                    x0 = ISOR1683.d0;
                    break;

                case VibQtyType.Velocity:
                    x0 = ISOR1683.v0;
                    break;

                case VibQtyType.Acceleration:
                    x0 = ISOR1683.a0;
                    break;
            }

            if (isAmpSpectrum)
            {
                return NumbersUtils.Array20Log10(linearSpectrum, x0);
            }
            return NumbersUtils.Array10Log10(linearSpectrum, MathBasic.Square(x0));
        }

        /// <summary>
        /// 将线性谱转化为分贝谱，基准值为1，系数为10
        /// </summary>
        /// <param name="linearSpectrum">线性谱</param>
        /// <returns>分贝谱</returns>
        public static _ValueT[] LinearToDBSpectrum(_ValueT[] linearSpectrum)
        {
            return LinearToDBSpectrum(linearSpectrum, VibQtyType.Generic, false);
        }

        /// <summary>
        /// 计算实倒谱( 谱线数 = 输入信号长度 / 2)
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <returns>倒谱波形</returns>
        // 基于功率谱计算实倒谱。
        // 计算公式：  C(q) = Real{IFFT{log10[Gx(f)]}}
        // 参考文献：1990-沈玉娣-《FORTRAN源程序汇编》
        public static _ValueT[] Cepstrum(_ValueT[] timeWave)
        {
            //原始信号去除均值, 1990-沈玉娣-《FORTRAN源程序汇编》
            DSPBasic.ACCoupling(timeWave);

            // 先得到双边对数功率谱
            _ValueT[] biAmpSpectrum, phaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum(timeWave, out biAmpSpectrum, out phaseSpectrum);
            int lineCount = biAmpSpectrum.Length;
            _ValueT[] powerSpectrum = new _ValueT[lineCount];

            for (int index = 0; index < lineCount; index++)
            {
                Double powerSpectrumValue = biAmpSpectrum[index] * biAmpSpectrum[index];
                const int MinPower = -20;
                const Double MinValue = 1.0E-20;
                powerSpectrum[index] = (powerSpectrumValue <= MinValue
                                             ? MinPower
                                             // ReSharper disable RedundantCast
                                             : (_ValueT)Math.Log10(powerSpectrumValue));
                // ReSharper restore RedundantCast
            }

            //双边FFT逆变换
            _ValueT[] imArray = new _ValueT[lineCount];
            DSPBasic.CxInvFFT(powerSpectrum, imArray);
            //去掉倒谱中t=0处幅值
            powerSpectrum[0] = 0;

            _ValueT[] cepstrum = new _ValueT[lineCount / 2];
            for (int index = 0; index < cepstrum.Length; index++)
            {
                cepstrum[index] = Math.Abs(powerSpectrum[index]);
            }

            return cepstrum;
        }

        /// <summary>
        /// 包络解调
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <param name="fl">低截止频率</param>
        /// <param name="fh">高截止频率</param>
        /// <param name="fs">采样频率</param>
        /// <returns>包络解调波形</returns>
        public static _ValueT[] DeModSpectrum(_ValueT[] timeWave, Double fl, Double fh,
                                               Double fs)
        {
            #region 输入参数合理性检查

            //检查实数数组的个数必须大于0
            MathError.CheckLengthGTZero(timeWave);

            //检查数组长度是否2的幂次
            MathError.CheckPowerOfTwo(timeWave.Length);

            #endregion

            return DeModSpectrum(DSPBasic.ButterworthBandPass(timeWave, 5, fl, fh, fs));
        }

        /// <summary>
        /// 包络解调
        /// </summary>
        /// <param name="timeWave">时间波形</param>
        /// <returns>包络解调波形</returns>
        public static _ValueT[] DeModSpectrum(_ValueT[] timeWave)
        {
            #region 输入参数合理性检查

            //检查实数数组的个数必须大于0
            MathError.CheckLengthGTZero(timeWave);

            //检查数组长度是否2的幂次
            MathError.CheckPowerOfTwo(timeWave.Length);

            #endregion

            //进行基于Hilbert变换的包络解调
            _ValueT[] envArray = Envelope.HilbertDetection(timeWave);

            //去除支流分量
            DSPBasic.ACCoupling(envArray);

            //计算包络幅值谱
            _ValueT[] ampSpectrum, phaseSpectrum;
            DSPBasic.AmpPhaseSpectrum(envArray, out ampSpectrum, out phaseSpectrum);

            return ampSpectrum;
        }

        /// <summary>
        /// 获得波德图的相位数组
        /// </summary>
        /// <param name="reArray">频谱的实部</param>
        /// <param name="imArray">频谱的虚部</param>
        /// <param name="revs">分钟转速数组</param>
        /// <returns>波德图的相位数组</returns>
        public static _ValueT[] BodePhase(_ValueT[] reArray, _ValueT[] imArray, int[] revs)
        {
            return BodePhase(reArray, imArray, revs[0] < revs[revs.Length - 1]);
        }

        /// <summary>
        /// 获得波德图的相位数组
        /// </summary>
        /// <param name="reArray">频谱的实部</param>
        /// <param name="imArray">频谱的虚部</param>
        /// <param name="isClockWise">true：起机顺时针；false：停机相位逐渐增大，逆时针过程</param>
        /// <returns>波德图的相位数组</returns>
        private static _ValueT[] BodePhase(_ValueT[] reArray, _ValueT[] imArray, bool isClockWise)
        {
            // 上一个相位
            Double lastPhase = 0;

            _ValueT[] ret = new _ValueT[reArray.Length];
            for (int index = 0; index < reArray.Length; index++)
            {
                _ValueT re = reArray[index];
                _ValueT im = imArray[index];

                Double phase = MathBasic.ReIm2Phase180(re, im);

                if (index == 0)
                {
                    lastPhase = phase;
                }
                else
                {
                    Double offset = phase - lastPhase;
                    if (Math.Abs(offset) <= MathConst.Deg_180)
                    {
                        // 相位差满足要求，保存这个相位。
                    }
                    else if (Math.Abs(offset + MathConst.Deg_360) <= MathConst.Deg_180)
                    {
                        phase += MathConst.Deg_360;
                    }
                    else
                    {
                        phase -= MathConst.Deg_360;
                    }

                    lastPhase = phase;
                }

                // ReSharper disable RedundantCast
                ret[index] = (_ValueT)phase;
                // ReSharper restore RedundantCast
            } // for( int index = 0; index < reArray.Length; index++ )

            return ret;
        }
    }
}