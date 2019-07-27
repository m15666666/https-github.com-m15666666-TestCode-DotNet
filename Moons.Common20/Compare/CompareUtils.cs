using System;

namespace Moons.Common20.Compare
{
    /// <summary>
    /// 比较的实用工具类
    /// </summary>
    public static class CompareUtils
    {
        static CompareUtils()
        {
            FloatValueWithin = 1e-6;
        }

        #region 是否相等

        /// <summary>
        /// 浮点数允许的误差
        /// </summary>
        public static double FloatValueWithin { get; set; }

        /// <summary>
        /// 比较两个值是否相等
        /// </summary>
        /// <param name="expected">期望值</param>
        /// <param name="actual">实际值</param>
        /// <returns>true：相等，false：不等</returns>
        public static bool AreEqual( double expected, double actual )
        {
            return Math.Abs( expected - actual ) <= FloatValueWithin;
        }

        #endregion

        #region 基于IComparable接口对Comparison代理类型的实现

        /// <summary>
        /// 基于IComparable接口对Comparison代理类型的实现，升序
        /// </summary>
        /// <typeparam name="T">实现了IComparable接口的类型</typeparam>
        /// <param name="x">比较的第一项</param>
        /// <param name="y">比较的第二项</param>
        /// <returns>1：大于，0：等于，-1：小于</returns>
        private static int AscComparison<T>( T x, T y ) where T : IComparable<T>
        {
            return x.CompareTo( y );
        }

        /// <summary>
        /// 基于IComparable接口对Comparison代理类型的实现，降序
        /// </summary>
        /// <typeparam name="T">实现了IComparable接口的类型</typeparam>
        /// <param name="x">比较的第一项</param>
        /// <param name="y">比较的第二项</param>
        /// <returns>1：大于，0：等于，-1：小于</returns>
        private static int DescComparison<T>( T x, T y ) where T : IComparable<T>
        {
            return y.CompareTo( x );
        }

        /// <summary>
        /// 创建基于IComparable接口对Comparison代理类型的实现
        /// </summary>
        /// <typeparam name="T">实现了IComparable接口的类型</typeparam>
        /// <param name="ascending">true为"升序排列", false为"降序排列"</param>
        /// <returns>基于IComparable接口对Comparison代理类型的实现</returns>
        public static System.Comparison<T> CreateComparisonHandler<T>( bool ascending ) where T : IComparable<T>
        {
            return ascending ? new System.Comparison<T>( AscComparison ) : DescComparison;
        }

        #endregion
    }
}