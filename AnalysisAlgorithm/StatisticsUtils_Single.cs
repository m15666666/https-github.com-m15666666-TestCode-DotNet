using System;

// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ͳ�ƺ�����ʵ�ù�����
    /// </summary>
    public static partial class StatisticsUtils
    {
        /// <summary>
        /// ��������ľ�ֵ
        /// </summary>
        /// <param name="array">����</param>
        /// <returns>��ֵ</returns>
        public static _ValueT Mean(_ValueT[] array)
        {
            int count = array.Length;
            _ValueT averageValue = 0;
            foreach (_ValueT value in array)
            {
                averageValue += (value / count);
            }
            return averageValue;
        }

        /// <summary>
        /// ��������ľ��Ծ�ֵ
        /// </summary>
        /// <param name="array">����</param>
        /// <returns>���Ծ�ֵ</returns>
        public static _ValueT AbsMean(_ValueT[] array)
        {
            int count = array.Length;
            _ValueT averageValue = 0;
            foreach (_ValueT value in array)
            {
                averageValue += (Math.Abs(value) / count);
            }
            return averageValue;
        }

        /// <summary>
        /// ��������ķ�����ֵ
        /// </summary>
        /// <param name="array">����</param>
        /// <returns>������ֵ</returns>
        public static _ValueT SMR(_ValueT[] array)
        {
            int count = array.Length;
            Double averageValue = 0;
            foreach (_ValueT value in array)
            {
                averageValue += (Math.Sqrt(Math.Abs(value)) / count);
            }
            return (_ValueT)MathBasic.Square(averageValue);
        }

        /// <summary>
        /// ��������ķ���
        /// </summary>
        /// <param name="array">����</param>
        /// <returns>����</returns>
        public static _ValueT Variance(_ValueT[] array)
        {
            return Moment(array, 2);
        }

        /// <summary>
        /// ���������n�׾�
        /// </summary>
        /// <param name="array">����</param>
        /// <param name="order">����</param>
        /// <returns>ָ�������ľ�</returns>
        public static _ValueT Moment(_ValueT[] array, int order)
        {
            int count = array.Length;
            _ValueT average = Mean(array);
            Double ret = 0;
            foreach (_ValueT value in array)
            {
                ret += (MathBasic.IntegerPow(value - average, order) / count);
            }
            return (_ValueT)ret;
        }

        /// <summary>
        /// ��������ı�׼ƫ��
        /// </summary>
        /// <param name="array">����</param>
        /// <returns>��׼ƫ��</returns>
        public static _ValueT StdDeviation(_ValueT[] array)
        {
            return (_ValueT)Math.Sqrt(Variance(array));
        }

        /// <summary>
        /// ���������n��ԭ���
        /// </summary>
        /// <param name="array">����</param>
        /// <param name="order">����</param>
        /// <returns>ָ��������ԭ���</returns>
        public static _ValueT OriginMoment(_ValueT[] array, int order)
        {
            int count = array.Length;
            Double ret = 0;
            foreach (_ValueT value in array)
            {
                ret += (MathBasic.IntegerPow(value, order) / count);
            }
            return (_ValueT)ret;
        }
    }
}
