using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MathNet;
using MathNet.Filtering.DataSources;

namespace AnalysisAlgorithm.Tests
{

    /// <summary>
    /// 滤波器测试
    /// </summary>
    [TestFixture]
    public class FilterTests
    {
        /// <summary>
        ///     测试 MathNet.Filtering 带通滤波 程序
        ///     思路：scilab fft、plot(abs(fft(sinwave))) 画图观察 是否滤掉 一根谱线 
        /// </summary>
        [Test]
        public void BandPassTest()
        {
            int dataLength = 1000;
            double samplerate = 1000;
            double[] sinWave1 = SignalGenerator.Sine(samplerate, 101,0,10,dataLength);
            double[] sinWave2 = SignalGenerator.Sine(samplerate, 201, 0, 10, dataLength);
            double[] sinWave = new double[sinWave1.Length];
            for (int i = 0; i < sinWave.Length; i++)
                sinWave[i] = sinWave1[i] + sinWave2[i];

            // 使用scilab 的变量浏览器直接粘贴多行数据
            OutputUtils.ToTxtFile(sinWave, "sinwave.txt");

            var filter = MathNet.Filtering.OnlineFilter.CreateBandpass(MathNet.Filtering.ImpulseResponse.Finite, samplerate, 90, 120);

            OutputUtils.ToTxtFile(filter.ProcessSamples(sinWave), "sinwave-filter.txt");
        }
    }
}
