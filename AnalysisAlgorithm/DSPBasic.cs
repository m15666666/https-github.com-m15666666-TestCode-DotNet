using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ���������źŴ����㷨��
    /// </summary>
    public static partial class DSPBasic
    {
        #region ����Ƶ�ʵõ���������ʱ�䲨�γ��Ȼ���׳���

        /// <summary>
        /// ����Ƶ�ʵķŴ�ϵ��
        /// </summary>
        private const Single SampleFreqScale = 2.56f;

        /// <summary>
        /// ͨ������Ƶ�ʵõ���������
        /// </summary>
        /// <param name="fs">����Ƶ��</param>
        /// <returns>��������</returns>
        public static Double GetFreqBandBySampleFreq( Double fs )
        {
            return fs / SampleFreqScale;
        }

        /// <summary>
        /// ͨ��ʱ�䲨�γ��Ȼ���׳���
        /// </summary>
        /// <param name="timeWaveLength">ʱ�䲨�γ���</param>
        /// <returns>�׳���</returns>
        public static int GetSpectrumLengthByTimeWaveLength( int timeWaveLength )
        {
            return (int)( timeWaveLength / SampleFreqScale + 1 );
        }

        /// <summary>
        /// ���Ƶ�ʷֱ���
        /// </summary>
        /// <param name="fs">����Ƶ��</param>
        /// <param name="timeWaveLength">ʱ�䲨�γ���</param>
        /// <returns>Ƶ�ʷֱ���</returns>
        public static Double GetDeltaFreq( Double fs, int timeWaveLength )
        {
            return fs / timeWaveLength;
        }

        #endregion

        #region ������ת��ת��ΪƵ��(Hz)

        /// <summary>
        /// ������ת��ת��ΪƵ��(Hz)
        /// </summary>
        /// <param name="rpm">����ת��</param>
        /// <returns>Ƶ��(Hz)</returns>
        public static Double RpmtoHz( Double rpm )
        {
            return rpm / MathConst.SecondCountOfMinute;
        }

        /// <summary>
        /// ��Ƶ��(Hz)ת��Ϊ����ת��
        /// </summary>
        /// <param name="hz">Ƶ��(Hz)</param>
        /// <returns>����ת��</returns>
        public static Double HztoRpm( Double hz )
        {
            return hz * MathConst.SecondCountOfMinute;
        }

        #endregion
    }
}