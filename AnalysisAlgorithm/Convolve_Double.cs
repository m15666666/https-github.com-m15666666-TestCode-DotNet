using System;
// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ����㷨��
    /// </summary>
    public static partial class Convolve
    {
        /// <summary>
        /// ֱ�Ӽ�����������ľ��
        /// </summary>
        /// <param name="xArray">��һ������</param>
        /// <param name="yArray">�ڶ�������</param>
        /// <returns>����������</returns>
        /// <remarks>
        /// ���㹫ʽ����: 
        ///	���: 
        ///		n = x[]��Ԫ�ظ���      m = y[]��Ԫ�ظ��� 
        /// ��ô: 
        ///		              b 
        ///			Cxy[i] =  �� x[k] * y[i-k] 
        ///		             k=a 
        ///	����: 
        ///		if i��[0,m), a = 0, b = i;
        ///     if i��[m,n), a = i - m + 1, b = i;    
        ///     if i��[n,n + m - 1), a = i - m + 1, b = n - 1;         
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
        /// �����ڲ�����ͣ���������
        /// </summary>
        /// <param name="xArray">��һ������</param>
        /// <param name="yArray">�ڶ�������</param>
        /// <param name="outterIndex">�ⲿ��������飩���±�</param>
        /// <param name="innerLBound">�ڲ��±������</param>
        /// <param name="innerHBound">�ڲ��±������</param>
        /// <returns>�ڲ������</returns>
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