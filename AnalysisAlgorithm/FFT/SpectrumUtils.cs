using Moons.Common20;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AnalysisAlgorithm.FFT
{
    /// <summary>
    /// 频谱计算实用工具类
    /// </summary>
    public class SpectrumUtils
    {
        /// <summary>
        /// 时间波形
        /// </summary>
        public double[] Timewave { get; set; }

        /// <summary>
        /// fft 幅值谱或者有效值 单边
        /// </summary>
        public double[] FFTAmp { get; set; }

        /// <summary>
        /// 是否是有效值谱
        /// </summary>
        public bool IsRmsFFT { get; set; }

        /// <summary>
        /// 采样频率
        /// </summary>
        public double Fs { get; set; }

        /// <summary>
        /// 基频
        /// </summary>
        public double F0 { get; set; }

        /// <summary>
        /// 时间波形长度
        /// </summary>
        public int TimeWaveLength => Timewave != null? Timewave.Length : 0;

        public void InitByTimewave(double fs, double f0, double[] timewave, bool isRmsFFT)
        {
            Timewave = timewave;

            IsRmsFFT = isRmsFFT;

            FFTAmp = SpectrumBasic.AmpSpectrum(timewave);
            if(IsRmsFFT) NumbersUtils.ScaleArray(FFTAmp, 1f/ MathConst.SqrtTwo);

            int timeWaveLength = timewave.Length;

            Fs = fs;

            F0 = f0;

            XFFT = GetXFFTBySpectrum(fs, f0, f0, timeWaveLength, FFTAmp);
            XHalfFFT = GetXFFTBySpectrum(fs, 0.5 * f0, f0, timeWaveLength, FFTAmp);

            //Hz100 = GetFFTAmpBySpectrum(fs, 100, timeWaveLength, FFTAmp);
            XHz100 = GetXFFTBySpectrum(fs, 100, 100, timeWaveLength, FFTAmp);

            Overall = SpectrumBasic.Overall_RmsSpectrum(FFTAmp);
            HighestPeak = NumbersUtils.Max(FFTAmp);
        }

        /// <summary>
        /// 获得对应频率的幅值
        /// </summary>
        /// <param name="freq"></param>
        /// <returns></returns>
        public double GetFFTAmp(double freq)
        {
            return GetFFTAmpBySpectrum(Fs, freq, TimeWaveLength, FFTAmp);
        }

        /// <summary>
        /// 获得一系列倍频的幅值
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="startFreq"></param>
        /// <param name="timeWaveLength"></param>
        /// <param name="spectrum"></param>
        /// <param name="peakOffset"></param>
        /// <returns></returns>
        private static double[] GetXFFTBySpectrum(double fs, double startFreq, double offsetFreq, int timeWaveLength, double[] spectrum, int peakOffset = 1)
        {
            double upperFreq = fs / 2.56f;
            
            List<double> ret = new List<double>();
            for(double f = startFreq; f <= upperFreq; f += offsetFreq)
            {
                var v = GetFFTAmpBySpectrum(fs, f, timeWaveLength, spectrum, peakOffset);
                ret.Add(v);
            }
            return ret.ToArray();
        }

        /// <summary>
        /// 获得某个点频谱的值
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="freq"></param>
        /// <param name="timeWaveLength"></param>
        /// <param name="spectrum"></param>
        /// <param name="peakOffset"></param>
        /// <returns></returns>
        private static double GetFFTAmpBySpectrum(double fs, double freq, int timeWaveLength, double[] spectrum, int peakOffset = 1)
        {
            int centerIndex = SpectrumBasic.GetFreqIndex( fs, freq, timeWaveLength );
            if (spectrum.Length <= centerIndex) return 0;

            int SearchOffset = peakOffset;
            int SearchCount = 2 * SearchOffset + 1;
            var index = ArrayUtils.MaxIndex(spectrum, centerIndex - SearchOffset, SearchCount);
            return spectrum[index];
        }

        /// <summary>
        /// 幅值谱或有效值谱中的整数分频，下标为0对应1X
        /// </summary>
        public double[] XFFT { get; set; }

        /// <summary>
        /// 幅值谱或有效值谱中的0.5分频及其间隔为1X的频率，下标为0对应0.5X
        /// </summary>
        public double[] XHalfFFT { get; set; }

        /// <summary>
        /// 幅值谱或有效值谱中的100hz整数分频，下标为0对应100hz
        /// </summary>
        public double[] XHz100 { get; set; }

        /// <summary>
        /// 100Hz分量
        /// </summary>
        public double Hz100 => XHz100 != null && 0 < XHz100.Length ? XHz100[0] : 0;

        /// <summary>
        /// 总值 overvall rms
        /// </summary>
        public double Overall { get; set; }

        /// <summary>
        /// 最大峰值（真峰值或者有效峰值的最大值）
        /// </summary>
        public double HighestPeak { get; set; }

        /// <summary>
        /// 0.5分频及其间隔为1X的频率
        /// </summary>
        public double X0_5 => XHalfFFT != null && 0 < XHalfFFT.Length ? XHalfFFT[0] : 0;
        public double X1_5  => XHalfFFT != null && 1 < XHalfFFT.Length ? XHalfFFT[1] : 0;
        public double X2_5  => XHalfFFT != null && 2 < XHalfFFT.Length ? XHalfFFT[2] : 0;
        public double X3_5  => XHalfFFT != null && 3 < XHalfFFT.Length ? XHalfFFT[3] : 0;
        public double X4_5  => XHalfFFT != null && 4 < XHalfFFT.Length ? XHalfFFT[4] : 0;
        public double X5_5  => XHalfFFT != null && 5 < XHalfFFT.Length ? XHalfFFT[5] : 0;

        /// <summary>
        /// 基频及倍频
        /// </summary>
        public double X1 => XFFT != null && 0 < XFFT.Length ? XFFT[0] : 0;
        public double X2  => XFFT != null && 1 < XFFT.Length ? XFFT[1] : 0;
        public double X3  => XFFT != null && 2 < XFFT.Length ? XFFT[2] : 0;
        public double X4  => XFFT != null && 3 < XFFT.Length ? XFFT[3] : 0;
        public double X5  => XFFT != null && 4 < XFFT.Length ? XFFT[4] : 0;
        public double X6  => XFFT != null && 5 < XFFT.Length ? XFFT[5] : 0;
    }
}
