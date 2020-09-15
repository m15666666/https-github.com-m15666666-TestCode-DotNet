using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Utils
{
    public class LineFitUtils
    {
        public static LineFitUtils CreateByYData(double[] yData)
        {
            var xData = new double[yData.Length];
            for (int i = 0; i < xData.Length; i++) xData[i] = i;
            var ret = new LineFitUtils();
            ret.InitByXY(xData, yData);
            return ret;
        }

        public static LineFitUtils CreateByXYData(double[] xData, double[] yData)
        {
            var ret = new LineFitUtils();
            ret.InitByXY(xData, yData);
            return ret;
        }

        public double[] X { get; set; }
        public double[] Y { get; set; }

        /// <summary>
        /// 返回拟和曲线的斜率
        /// </summary>
        public double K => LineCoef != null ? LineCoef.Item2 : 0;

        public Tuple<double, double> LineCoef { get; set; }

        public void InitByXY( double[] x, double[] y)
        {
            X = x;
            Y = y;

            LineCoef = MathNet.Numerics.Fit.Line(X, Y);
        }

        public double CalcY( double x)
        {
            var ret = LineCoef.Item1 + LineCoef.Item2 * x;
            Debug.WriteLine($"LineFitUtils.CalcY:x={x},y={ret}");
            return ret;
        }
    }
}
