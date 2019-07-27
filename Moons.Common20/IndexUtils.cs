using System;
using System.Collections;

namespace Moons.Common20
{
    /// <summary>
    /// 关于下标的实用工具类
    /// </summary>
    public static class IndexUtils
    {
        /// <summary>
        /// 获得指定范围内正确的下标
        /// </summary>
        /// <param name="originIndex">初始下标</param>
        /// <param name="minIndex">下标的最小值</param>
        /// <param name="maxIndex">下标的最大值</param>
        /// <returns>指定范围内正确的下标</returns>
        public static int GetIndexInRange( int originIndex, int minIndex, int maxIndex )
        {
            if( originIndex < minIndex )
            {
                return minIndex;
            }
            if( maxIndex < originIndex )
            {
                return maxIndex;
            }
            return originIndex;
        }

        /// <summary>
        /// 获得指定范围内正确的下标，下标循环移动
        /// </summary>
        /// <param name="originIndex">初始下标</param>
        /// <param name="minIndex">下标的最小值</param>
        /// <param name="maxIndex">下标的最大值</param>
        /// <returns>指定范围内正确的下标</returns>
        public static int GetCircleIndexInRange( int originIndex, int minIndex, int maxIndex )
        {
            if( originIndex < minIndex )
            {
                return maxIndex;
            }
            if( maxIndex < originIndex )
            {
                return minIndex;
            }
            return originIndex;
        }

        /// <summary>
        /// 在集合中查找指定对象，并返回下标，如果找不到则返回-1。
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="find">指定对象</param>
        /// <returns>指定对象下标，如果找不到则返回-1。</returns>
        public static int FindIndex( IEnumerable collection, object find )
        {
            int index = 0;
            foreach( object o in collection )
            {
                if( o == find )
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        #region 查找最接近的下标

        /// <summary>
        /// 在无序排列的数据中查找最接近的下标
        /// </summary>
        /// <param name="randomDatas">数据，无序排列</param>
        /// <param name="value">数值</param>
        /// <returns>最接近的下标</returns>
        public static int FindClosestIndexRandom( double[] randomDatas, double value )
        {
            double minDiff = Double.MaxValue;
            int index = 0;
            int minIndex = 0;
            foreach( double data in randomDatas )
            {
                double diff = Math.Abs( value - data );
                if( diff < minDiff )
                {
                    minDiff = diff;
                    minIndex = index;
                }
                ++index;
            }
            return minIndex;
        }

        /// <summary>
        /// 在有序排列的数据中查找最接近的下标
        /// </summary>
        /// <param name="orderedDatas">数据，有序排列</param>
        /// <param name="value">数值</param>
        /// <returns>最接近的下标</returns>
        public static int FindClosestIndex( double[] orderedDatas, double value )
        {
            int minValueIndex = ArrayUtils.MinIndex( orderedDatas );
            if( value <= orderedDatas[minValueIndex] )
            {
                return minValueIndex;
            }

            int maxValueIndex = ArrayUtils.MaxIndex( orderedDatas );
            if( orderedDatas[maxValueIndex] <= value )
            {
                return maxValueIndex;
            }

            for( int index = 1; index < orderedDatas.Length; index++ )
            {
                int previousIndex = index - 1;
                double previous = orderedDatas[previousIndex];
                double current = orderedDatas[index];
                if( Math.Min( previous, current ) <= value && value <= Math.Max( previous, current ) )
                {
                    return FindClosestIndex( orderedDatas, value, previousIndex, index );
                }
            }

            throw new NotSupportedException( "不应该到达这里" );
        }

        /// <summary>
        /// 在有序排列的数据中查找最接近的下标
        /// </summary>
        /// <param name="orderedDatas">数据，有序排列</param>
        /// <param name="value">数值</param>
        /// <param name="asc">true：orderedData为升序排列，false：orderedData为降序排列</param>
        /// <returns>最接近的下标</returns>
        public static int FindClosestIndex( double[] orderedDatas, double value, bool asc )
        {
            if( orderedDatas == null || orderedDatas.Length == 0 )
            {
                return -1;
            }

            if( orderedDatas.Length == 1 )
            {
                return 0;
            }

            int minValueIndex = asc ? 0 : orderedDatas.Length - 1;
            if( value <= orderedDatas[minValueIndex] )
            {
                return minValueIndex;
            }

            int maxValueIndex = asc ? orderedDatas.Length - 1 : 0;
            if( orderedDatas[maxValueIndex] <= value )
            {
                return maxValueIndex;
            }

            int minIndex = 0;
            int maxIndex = orderedDatas.Length - 1;
            while( true )
            {
                // 两个下标重叠或紧挨着的情况
                if( ( maxIndex - minIndex ) < 2 )
                {
                    return FindClosestIndex( orderedDatas, value, minIndex, maxIndex );
                }

                int midIndex = ( minIndex + maxIndex ) / 2;
                int previousIndex = midIndex - 1;
                double previous = orderedDatas[previousIndex];
                double current = orderedDatas[midIndex];

                // 升序情况
                if( asc )
                {
                    if( value < previous )
                    {
                        maxIndex = previousIndex;
                        continue;
                    }

                    if( current < value )
                    {
                        minIndex = midIndex;
                        continue;
                    }

                    return FindClosestIndex( orderedDatas, value, previousIndex, midIndex );
                }

                // 降序情况
                {
                    if( previous < value )
                    {
                        maxIndex = previousIndex;
                        continue;
                    }

                    if( value < current )
                    {
                        minIndex = midIndex;
                        continue;
                    }

                    return FindClosestIndex( orderedDatas, value, previousIndex, midIndex );
                }
            } // while( true )
        }

        /// <summary>
        /// 查找最接近的下标
        /// </summary>
        /// <param name="orderedDatas">数据，有序排列</param>
        /// <param name="value">数值</param>
        /// <param name="previousIndex">前一个下标</param>
        /// <param name="currentIndex">当前下标</param>
        /// <returns>最接近的下标</returns>
        private static int FindClosestIndex( double[] orderedDatas, double value, int previousIndex, int currentIndex )
        {
            return Math.Abs( orderedDatas[previousIndex] - value ) < Math.Abs( orderedDatas[currentIndex] - value )
                       ? previousIndex
                       : currentIndex;
        }

        #endregion
    }
}