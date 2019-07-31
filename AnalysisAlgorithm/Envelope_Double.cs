using System;

// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 信号解包络类
    /// </summary>
    public static partial class Envelope
    {
        /// <summary>
        /// 峰值包络检波
        /// </summary>
        /// <param name="xArray">原始数组</param>
        /// <param name="fc">高通截至频率</param>
        /// <param name="fmax">分析带宽</param>
        /// <param name="fs">采样频率</param>
        /// <param name="order">谐波数</param>
        /// <returns>返回包络数组</returns>
        public static _ValueT[] PeakDetection(_ValueT[] xArray, Double fc, Double fmax, Double fs, int order)
        {
            //设置峰值检波参数
            _ValueT tau1 = (fc <= 1000.0f ? 0.0069f : 0.000324f);
            _ValueT tau2 = 2.07f * order / (_ValueT)(MathConst.TwoPI * fmax);
            _ValueT deltaT = 1 / (_ValueT)fs;
            _ValueT factor1 = (_ValueT)Math.Exp(-deltaT / tau1);
            _ValueT factor2 = (_ValueT)Math.Exp(-deltaT / tau2);

            //解包络
            _ValueT peakNegValue = 0;
            _ValueT peak2PeakMaxValue = 0;
            _ValueT[] ret = new _ValueT[xArray.Length];
            for (int index = 0; index < xArray.Length; index++)
            {
                if (xArray[index] < peakNegValue)
                {
                    peakNegValue = xArray[index];
                }
                else
                {
                    if ((xArray[index] - peakNegValue) > peak2PeakMaxValue)
                    {
                        //检查峰值是否还在衰减
                        peak2PeakMaxValue = xArray[index] - peakNegValue;
                    }
                }
                ret[index] = peak2PeakMaxValue;

                peakNegValue *= factor1;
                peak2PeakMaxValue *= factor2;
            }

            return ret;
        }

        /// <summary>
        /// Hilbert包络检波
        /// </summary>
        /// <param name="xArray">原始数组</param>
        /// <returns>返回包络数组</returns>
        public static _ValueT[] HilbertDetection(_ValueT[] xArray)
        {
            int length = xArray.Length;

            //1、进行Hilbert变换
            _ValueT[] reArray = new _ValueT[length];
            _ValueT[] imArray = new _ValueT[length];
            xArray.CopyTo(reArray, 0);
            DSPBasic.HilbertT(reArray, imArray);

            //2、解包络
            _ValueT[] ret = new _ValueT[length];
            for (int index = 0; index < ret.Length; index++)
            {
                Double squareSum = MathBasic.SquareSum(reArray[index], imArray[index], xArray[index]);
                ret[index] = (_ValueT)Math.Sqrt(squareSum);
            }

            return ret;
        }
    }
}
