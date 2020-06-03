using MathNet.Filtering.DataSources;
using Moons.Common20;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnalysisAlgorithm.Tests
{
    /// <summary>
    ///     小波测试类
    /// </summary>
    [TestFixture]
    public class WaveletsTests
    {
        /// <summary>
        ///     测试 正弦波的rms与overall相等
        ///     思路：使用时间波形和频率分别计算
        ///     测试下来：相等
        /// </summary>
        [Test]
        public void Wavelet_DB8_Test()
        {
            /*

            */

            TestWavelet_PanYuNa_Pywavelets();
            TestWavelet_PanYuNa_Matlab();

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


        #region 测试小波算法

        /// <summary>
        /// 测试小波算法，
        /// 测试方法：与matlab的计算结果对比
        /// rsignal_vc.dat是：对信号x=sin(2*pi*1/32*(0:1:1023)); 进行3层分解后得到的数据，
        /// 存储方式是double型的，共4096个数据，其中1~1024为第三层的近似信号，
        /// 1025~2048为第三层的细节信号，2049~3072为第二层细节信号，
        /// 3073~4096为第一层细节信号。和我在matlab里的结果一样。
        /// </summary>
        private static void TestWavelet_PanYuNa_Pywavelets()
        {
            var x = new double[] {
                3, 7, 1, 1, -2, 5, 4, 6,
                3, 7, 1, 1, -2, 5, 4, 6,
            };

            double[][] decomposition = Wavelet_PanYuNa.WaveletDecomposition( x, 2 );

            //MessageBox.Show( "对比测试成功" );
        }

        /// <summary>
        /// 测试小波算法，
        /// 测试方法：与matlab的计算结果对比
        /// rsignal_vc.dat是：对信号x=sin(2*pi*1/32*(0:1:1023)); 进行3层分解后得到的数据，
        /// 存储方式是double型的，共4096个数据，其中1~1024为第三层的近似信号，
        /// 1025~2048为第三层的细节信号，2049~3072为第二层细节信号，
        /// 3073~4096为第一层细节信号。和我在matlab里的结果一样。
        /// </summary>
        private static void TestWavelet_PanYuNa_Matlab()
        {
            var x = new double[1024];
            for( int index = 0; index < x.Length; index++ )
            {
                x[index] = Math.Sin( 2 * Math.PI / 32 * index );
            }

            double[][] decomposition = Wavelet_PanYuNa.WaveletDecomposition( x, 4 );
            double[][] compareDecomposition = ArrayUtils.CreateJaggedArray<double>( 4, 1024 );

            using( var reader = new BinaryReader( File.Open( "rsignal_vc.dat", FileMode.Open ) ) )
            {
                for( int index = 0; index < compareDecomposition.Length; index++ )
                {
                    ReadArray( reader, compareDecomposition[index] );
                }
            }

            for( int index = 0; index < compareDecomposition.Length; index++ )
            {
                if( !ArrayEqual( decomposition[index], compareDecomposition[index] ) )
                {
                    //MessageBox.Show( "对比测试失败" );
                    return;
                }
            }

            //MessageBox.Show( "对比测试成功" );
        }

        /// <summary>
        /// 读取数组
        /// </summary>
        private static void ReadArray( BinaryReader reader, double[] data )
        {
            for( int index = 0; index < data.Length; index++ )
            {
                data[index] = reader.ReadDouble();
            }
        }

        /// <summary>
        /// 比较两个数组是否相等
        /// </summary>
        private static bool ArrayEqual( double[] x, double[] y )
        {
            for( int index = 0; index < x.Length; index++ )
            {
                if( 1e-5 < Math.Abs( x[index] - y[index] ) )
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
