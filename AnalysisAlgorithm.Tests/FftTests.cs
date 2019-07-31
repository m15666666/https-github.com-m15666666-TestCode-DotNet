using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnalysisAlgorithm.Tests
{
    /// <summary>
    ///     fft功能 测试类
    /// https://baike.baidu.com/item/%E5%92%8C%E8%A7%92%E5%85%AC%E5%BC%8F
    /// https://baike.baidu.com/item/%E7%A7%AF%E5%8C%96%E5%92%8C%E5%B7%AE
    /// https://baike.baidu.com/item/%E5%92%8C%E5%B7%AE%E5%8C%96%E7%A7%AF
    /// </summary>
    [TestFixture]
    public class FftTests
    {
        /// <summary>
        /// 输出目录
        /// </summary>
        private static string OutputDir = @"D:\temp2";

        /// <summary>
        ///     测试“fft”程序
        ///     思路：与scilab对比测试
        /// </summary>
        [Test]
        public void Fft()
        {
            /*

                        scilab:
                        --> fft([1 2 3 1 2 3 1 2]) / 8
             ans  =
                        column 1 to 5
                1.875  
                -0.125 - 0.0732233i  
                -0.125 - 0.25i  
                -0.125 + 0.4267767i  
                -0.125

                        column 6 to 8
                -0.125 - 0.4267767i  
                -0.125 + 0.25i  
                -0.125 + 0.0732233i

                        DSPBasic.BiReImSpectrum(xArray, out reArray, out imArray) ，DSPBasic.CxFFT(reArray, imArray) 运行结果:
                1.875
                -0.125 - 0.073i
                -0.125 - 0.25i
                -0.125 + 0.427i
                -0.125

                -0.125 - 0.427i
                -0.125 + 0.25i
                -0.125 + 0.073i

            */
            double[] reArray = new double[] { 1, 2, 3, 1, 2, 3, 1, 2 };
            double[] imArray = OutputUtils.CreateArray<double>(reArray.Length, 0);
            DSPBasic.CxFFT(reArray, imArray);

            double scale = 1.0f / reArray.Length;
            for( int i = 0; i < reArray.Length; i++)
            {
                reArray[i] *= scale;
                imArray[i] *= scale;
            }

            OutputUtils.ComplexToTxtFile(reArray, imArray, "FftTests-Fft.txt", OutputDir);
        }

        /// <summary>
        ///     测试“ifft”程序，与fft程序结果相比：只是虚部的符号变了(角度变为负的)，多除个N。
        ///     思路：与scilab对比测试
        /// </summary>
        [Test]
        public void IFft()
        {
            /*
                        scilab:
                        --> ifft([1 2 3 1 2 3 1 2])
             ans  =
                1.875  
                -0.125 + 0.0732233i  
                -0.125 + 0.25i  
                -0.125 - 0.4267767i  
                -0.125  

                -0.125 + 0.4267767i
                -0.125 - 0.25i  
                -0.125 - 0.0732233i

                        DSPBasic.CxInvFFT( reArray, imArray ) 运行结果:
                1.875
                -0.125 + 0.073i
                -0.125 + 0.25i
                -0.125 - 0.427i
                -0.125

                -0.125 + 0.427i
                -0.125 - 0.25i
                -0.125 - 0.073i
            */
            double[] reArray = new double[] { 1, 2, 3, 1, 2, 3, 1, 2 };
            double[] imArray = OutputUtils.CreateArray<double>(reArray.Length, 0);
            DSPBasic.CxInvFFT( reArray, imArray );

            OutputUtils.ComplexToTxtFile(reArray, imArray, "FftTests-iFft.txt", OutputDir);
        }

        /// <summary>
        ///     测试“fft、ifft”程序
        ///     思路：与原始对比测试
        /// </summary>
        [Test]
        public void FftAndIFft()
        {
            double[] reArray = new double[] { 1, 2, 3, 1, 2, 3, 1, 2 };
            double[] imArray = OutputUtils.CreateArray<double>(reArray.Length, 0);
            DSPBasic.CxFFT(reArray, imArray);
            DSPBasic.CxInvFFT(reArray, imArray);

            OutputUtils.ComplexToTxtFile(reArray, imArray, "FftTests-FftAndIFft.txt", OutputDir);
        }

        /// <summary>
        ///     测试“HilbertT”程序，与fft程序结果相比：只是虚部的符号变了(角度变为负的)，多除个N。
        ///     思路：与scilab对比测试
        /// </summary>
        [Test]
        public void HilbertT()
        {
            /*
                        scilab:
                        --> hilbert([1 2 3 1 2 3 1 2])
            ans  =
                1. + 0.2071068i   
                2. - 1.3106602i   
                3. + 0.5i   
                1. + 0.6035534i   
                2. - 1.2071068i

                3. + 0.8106602i   
                1. + 0.5i   
                2. - 0.1035534i

                        DSPBasic.HilbertT( reArray, imArray ) 运行结果:
                0.207 - 2i
                -1.311 - 1.75i
                0.5 - 2i
                0.604 - 1.75i
                -1.207 - 2i

                0.811 - 1.75i
                0.5 - 2i
                -0.104 - 1.75i

            */
            double[] reArray = new double[] { 1, 2, 3, 1, 2, 3, 1, 2 };
            double[] imArray = OutputUtils.CreateArray<double>(reArray.Length, 0);
            DSPBasic.HilbertT(reArray, imArray);

            //DSPBasic.CxFFT(reArray, imArray);

            //double scale = 1.0f / reArray.Length;
            //for (int i = 0; i < reArray.Length; i++)
            //{
            //    reArray[i] *= scale;
            //    imArray[i] *= scale;
            //}
            for (int i = 0; i < reArray.Length; i++)
            {
                reArray[i] = Math.Sqrt(reArray[i] * reArray[i] + imArray[i] * imArray[i] );
                //imArray[i] *= scale;
            }

            OutputUtils.ComplexToTxtFile(reArray, imArray, "FftTests-HilbertT.txt", OutputDir);
        }
    }
}
