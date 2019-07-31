using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ��ѧ������
    /// </summary>
    public static class MathConst
    {
        /// <summary>
        /// FFT���ĳ���Ϊ2^30
        /// </summary>
        public const int FFTPowerMax = 30;

        /// <summary>
        /// 2��ƽ����
        /// </summary>
        public const Single SqrtTwo = 1.4142135623730950488016887242097f;

        /// <summary>
        /// ���ڱ����0����СֵΪ10^-6
        /// </summary>
        public const Single epsilon = 1E-6f;

        #region �Ƕ�

        /// <summary>
        /// 2*PI
        /// </summary>
        public const Double TwoPI = 6.2831853071795865f;

        /// <summary>
        /// PI
        /// </summary>
        public const Double PI = 3.14159265359f;

        /// <summary>
        /// 0��
        /// </summary>
        public const int Deg_0 = 0;

        /// <summary>
        /// 90��
        /// </summary>
        public const int Deg_90 = 90;

        /// <summary>
        /// 180��
        /// </summary>
        public const int Deg_180 = 180;

        /// <summary>
        /// 270��
        /// </summary>
        public const int Deg_270 = 270;

        /// <summary>
        /// 360��
        /// </summary>
        public const int Deg_360 = 360;

        #endregion

        #region ʱ��

        /// <summary>
        /// һ����֮�ڵ�������60��
        /// </summary>
        public const int SecondCountOfMinute = 60;

        #endregion

        #region �������������ϵ��

        /// <summary>
        /// �������������ϵ��
        /// </summary>
        public const int LogScale_10 = 10;

        /// <summary>
        /// �������������ϵ��
        /// </summary>
        public const int LogScale_20 = 20;

        #endregion
    }
}