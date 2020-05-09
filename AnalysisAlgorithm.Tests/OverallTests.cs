using MathNet.Filtering.DataSources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnalysisAlgorithm.Tests
{
    /// <summary>
    ///     频谱的overall和rms 测试类
    /// </summary>
    [TestFixture]
    public class OverallTests
    {
        /// <summary>
        ///     测试 正弦波的rms与overall相等
        ///     思路：使用时间波形和频率分别计算
        ///     测试下来：相等
        /// </summary>
        [Test]
        public void Overall_RMSTest()
        {
            /*

            */

            int dataLength = 2048;
            double samplerate = 2560;
            double f0 = 160;
            double initPhase = 45; // 度

            // 两种生成正弦波算法一致，只是初始角度的地方需要换算一下
            double[] sinWave1 = SignalGenerator.Sine(samplerate, f0, initPhase / 180f * Math.PI, 1, dataLength);

            //double[] sinWave1 =
            //    AnalysisAlgorithm.Helper.SignalGenerator.CreateSin(new AnalysisAlgorithm.Helper.CreateSinParameter
            //    {
            //        DataLength = dataLength,
            //        F0 = f0,
            //        Fs = samplerate,
            //        InitPhaseInDegree = initPhase
            //    });

            var timewave = sinWave1;
            var rms = DSPBasic.RMS(timewave);

            var fftAmp = SpectrumBasic.AmpSpectrum(timewave);
            var overall = SpectrumBasic.Overall_AmpSpectrum(fftAmp);
            Assert.AreEqual(rms, overall, 0.000001f);

            NumbersUtils.ScaleArray(fftAmp, 1f/ MathConst.SqrtTwo);
            overall = SpectrumBasic.Overall_RmsSpectrum(fftAmp);
            Assert.AreEqual(rms, overall, 0.000001f);
        }
    }
}
