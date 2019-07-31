// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。

using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 线性插值类
    /// </summary>
    public static class LinearInterpolation
    {
        /// <summary>
        /// 使用线性插值从x值获得y值
        /// </summary>
        /// <param name="x1">x1值</param>
        /// <param name="y1">y1值</param>
        /// <param name="x2">x2值</param>
        /// <param name="y2">y2值</param>
        /// <param name="x">x值</param>
        /// <returns>y值</returns>
        public static _ValueT GetY( _ValueT x1, _ValueT y1, _ValueT x2, _ValueT y2, _ValueT x )
        {
            if( x2 == x1 )
            {
                return y1;
            }
            return y1 + ( y2 - y1 ) * ( x - x1 ) / ( x2 - x1 );
        }
    }
}