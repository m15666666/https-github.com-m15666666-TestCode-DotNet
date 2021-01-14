using System;

// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 统计函数的实用工具类
    /// </summary>
    public static partial class StatisticsUtils
    {
        /// <summary>
        /// 计算数组的均值
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>均值</returns>
        public static _ValueT Mean( Span<_ValueT> array )
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
        /// 计算数组的均值
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>均值</returns>
        public static _ValueT Mean(_ValueT[] array)
        {
            return Mean(array.AsSpan());
        }

        /// <summary>
        /// 计算数组的绝对均值
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>绝对均值</returns>
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
        /// 计算数组的方根幅值
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>方根幅值</returns>
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
        /// 计算数组的皮尔逊相关系数
        /// https://baike.baidu.com/item/皮尔逊相关系数
        /// </summary>
        /// <param name="x">数组1</param>
        /// <param name="y">数组2</param>
        /// <returns>皮尔逊相关系数</returns>
        public static _ValueT PearsonCorrelationCoefficient(_ValueT[] x, _ValueT[] y)
        {
            if (x == null || y == null || x.Length != y.Length) throw new ArgumentNullException(nameof(x));

            _ValueT stdX = StdDeviation(x);
            _ValueT stdY = StdDeviation(y);
            return (_ValueT)(Covariance(x,y) / (stdX * stdY));
        }

        /// <summary>
        /// 计算数组的协方差
        /// https://baike.baidu.com/item/协方差
        /// https://baike.baidu.com/item/标准差
        /// </summary>
        /// <param name="x">数组1</param>
        /// <param name="y">数组2</param>
        /// <returns>协方差</returns>
        public static _ValueT Covariance(_ValueT[] x, _ValueT[] y)
        {
            if (x == null || y == null || x.Length != y.Length) throw new ArgumentNullException(nameof(x));

            int count = x.Length;
            _ValueT averageX = Mean(x);
            _ValueT averageY = Mean(y);
            Double ret = 0;
            for (int i = 0; i < count; i++) ret += (x[i] - averageX) * (y[i] - averageY) / count;
            return (_ValueT)ret;
        }

        /// <summary>
        /// 计算数组的方差
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>方差</returns>
        public static _ValueT Variance(_ValueT[] array)
        {
            return Moment(array, 2);
        }

        /// <summary>
        /// 计算数组的n阶矩
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="order">阶数</param>
        /// <returns>指定阶数的矩</returns>
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
        /// 计算数组的标准偏差
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>标准偏差</returns>
        public static _ValueT StdDeviation(_ValueT[] array)
        {
            return (_ValueT)Math.Sqrt(Variance(array));
        }

        /// <summary>
        /// 计算数组的n阶原点矩
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="order">阶数</param>
        /// <returns>指定阶数的原点矩</returns>
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
