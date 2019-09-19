using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnalysisAlgorithm.Tests
{
    /// <summary>
    ///     曲线直线拟合功能 测试类
    /// https://github.com/mathnet/mathnet-numerics
    /// https://numerics.mathdotnet.com/
    /// https://filtering.mathdotnet.com/
    /// </summary>
    [TestFixture]
    public class CurveFitTests
    {
        /// <summary>
        ///     测试“LMS_CurveFit”程序
        ///     思路：与MathNet.Numerics对比测试
        /// </summary>
        [Test]
        public void LMS_CurveFit()
        {
            const int a = 1;
            const int b = 2;
            int len = 1024;
            // y = 1 + 2 * x
            double[] X = new double[len];
            double[] Y = new double[len];
            for (int i = 0; i < len; i++)
            {
                X[i] = i;
                Y[i] = 1 + 2 * i;
            }
            var polynomialOrder = CurveFit.PolynomialOrder_Linear;
            var dT = new double[4];
            var coef = new double[polynomialOrder + 1];
            CurveFit.LMS_CurveFit(polynomialOrder, X, Y,coef, dT);

            var x = X[X.Length - 1];
            var y = Y[Y.Length - 1];
            Tuple<double, double> lineCoef = MathNet.Numerics.Fit.Line(X, Y);
            var lineY = lineCoef.Item1 + lineCoef.Item2 * x;
            polynomialOrder = CurveFit.PolynomialOrder_Parabola;
            var dT2 = new double[4];
            var coef2 = new double[polynomialOrder + 1];
            CurveFit.LMS_CurveFit(polynomialOrder, X, Y, coef2, dT2);
            var polyCoef = MathNet.Numerics.Fit.Polynomial(X, Y, 2);
            var polyY = polyCoef[0] + polyCoef[1] * x + polyCoef[2] * x * x;
            Assert.AreEqual(a, lineCoef.Item1);
            Assert.AreEqual(b, lineCoef.Item2);
            Assert.AreEqual(lineY, y);
            Assert.AreEqual(polyY, y);

            //OutputUtils.ComplexToTxtFile(reArray, imArray, "FftTests-Fft.txt", OutputDir);
        }
    }
}
