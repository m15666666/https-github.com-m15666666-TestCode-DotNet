using System;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 卷积算法类
    /// </summary>
    public static partial class Convolve
    {
        /// <summary>
        /// 直接计算两个数组的卷积
        /// </summary>
        /// <param name="xArray">第一个数组</param>
        /// <param name="yArray">第二个数组</param>
        /// <returns>卷积结果数组</returns>
        /// <remarks>
        /// 计算公式如下: 
        ///	如果: 
        ///		n = x[]的元素个数      m = y[]的元素个数 
        /// 那么: 
        ///		              b 
        ///			Cxy[i] =  Σ x[k] * y[i-k] 
        ///		             k=a 
        ///	其中: 
        ///		if i∈[0,m), a = 0, b = i;
        ///     if i∈[m,n), a = i - m + 1, b = i;    
        ///     if i∈[n,n + m - 1), a = i - m + 1, b = n - 1;         
        /// </remarks>  
        public static _ValueT[] DirectConvolve( _ValueT[] xArray, _ValueT[] yArray )
        {
            int xLength = xArray.Length;
            int yLength = yArray.Length;
            _ValueT[] ret = new _ValueT[xLength + yLength - 1];

            for( int outterIndex = 0; outterIndex < yLength; outterIndex++ )
            {
                int innerLBound = 0, innerHBound = outterIndex;
                ret[outterIndex] = InnerConvolveSum( xArray, yArray, outterIndex, innerLBound, innerHBound );
            }

            for( int outterIndex = yLength; outterIndex < xLength; outterIndex++ )
            {
                int innerLBound = outterIndex - yLength + 1, innerHBound = outterIndex;
                ret[outterIndex] = InnerConvolveSum( xArray, yArray, outterIndex, innerLBound, innerHBound );
            }

            for( int outterIndex = xLength; outterIndex < xLength + yLength - 1; outterIndex++ )
            {
                int innerLBound = outterIndex - yLength + 1, innerHBound = xLength - 1;
                ret[outterIndex] = InnerConvolveSum( xArray, yArray, outterIndex, innerLBound, innerHBound );
            }

            return ret;
        }

        /// <summary>
        /// 计算内部卷积和，辅助函数
        /// </summary>
        /// <param name="xArray">第一个数组</param>
        /// <param name="yArray">第二个数组</param>
        /// <param name="outterIndex">外部（结果数组）的下标</param>
        /// <param name="innerLBound">内部下标的下限</param>
        /// <param name="innerHBound">内部下标的上限</param>
        /// <returns>内部卷积和</returns>
        private static _ValueT InnerConvolveSum( _ValueT[] xArray, _ValueT[] yArray, int outterIndex, int innerLBound,
                                                 int innerHBound )
        {
            _ValueT sum = 0;
            for( int innerIndex = innerLBound; innerIndex <= innerHBound; innerIndex++ )
            {
                sum += xArray[innerIndex] * yArray[outterIndex - innerIndex];
            }
            return sum;
        }
    }
}