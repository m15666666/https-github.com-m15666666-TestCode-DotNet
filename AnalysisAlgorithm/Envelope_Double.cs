using System;

// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// �źŽ������
    /// </summary>
    public static partial class Envelope
    {
        /// <summary>
        /// ��ֵ����첨
        /// </summary>
        /// <param name="xArray">ԭʼ����</param>
        /// <param name="fc">��ͨ����Ƶ��</param>
        /// <param name="fmax">��������</param>
        /// <param name="fs">����Ƶ��</param>
        /// <param name="order">г����</param>
        /// <returns>���ذ�������</returns>
        public static _ValueT[] PeakDetection(_ValueT[] xArray, Double fc, Double fmax, Double fs, int order)
        {
            //���÷�ֵ�첨����
            _ValueT tau1 = (fc <= 1000.0f ? 0.0069f : 0.000324f);
            _ValueT tau2 = 2.07f * order / (_ValueT)(MathConst.TwoPI * fmax);
            _ValueT deltaT = 1 / (_ValueT)fs;
            _ValueT factor1 = (_ValueT)Math.Exp(-deltaT / tau1);
            _ValueT factor2 = (_ValueT)Math.Exp(-deltaT / tau2);

            //�����
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
                        //����ֵ�Ƿ���˥��
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
        /// Hilbert����첨
        /// </summary>
        /// <param name="xArray">ԭʼ����</param>
        /// <returns>���ذ�������</returns>
        public static _ValueT[] HilbertDetection(_ValueT[] xArray)
        {
            int length = xArray.Length;

            //1������Hilbert�任
            _ValueT[] reArray = new _ValueT[length];
            _ValueT[] imArray = new _ValueT[length];
            xArray.CopyTo(reArray, 0);
            DSPBasic.HilbertT(reArray, imArray);

            //2�������
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
