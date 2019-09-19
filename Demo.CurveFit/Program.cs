using NUnit.Framework;
using System;

namespace Demo.CurveFit
{
    /// <summary>
    /// 演示直线拟合
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CurveFit();
            Console.WriteLine("Demo.CurveFit");
        }

        /// <summary>
        ///     演示直线拟合
        /// </summary>
        [Test]
        public static void CurveFit()
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

            var x = X[X.Length - 1];
            var y = Y[Y.Length - 1];
            Tuple<double, double> lineCoef = MathNet.Numerics.Fit.Line(X, Y);
            var lineY = lineCoef.Item1 + lineCoef.Item2 * x;
            Assert.AreEqual(a, lineCoef.Item1);
            Assert.AreEqual(b, lineCoef.Item2);
            Assert.AreEqual(lineY, y);
        }
    }
}
