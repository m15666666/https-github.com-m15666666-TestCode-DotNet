using System;
using Moons.Common20.Compare;

namespace Moons.Common20
{
    /// <summary>
    /// 数组操作的实用工具类
    /// </summary>
    public static class ArrayUtils
    {
        #region 基本操作

        /// <summary>
        /// 找到第一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>第一个元素</returns>
        public static T First<T>( T[] array )
        {
            return array[0];
        }

        /// <summary>
        /// 找到最后一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>最后一个元素</returns>
        public static T Last<T>( Span<T> array )
        {
            return array[array.Length - 1];
        }
        /// <summary>
        /// 找到最后一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>最后一个元素</returns>
        public static T Last<T>( T[] array )
        {
            return array[array.Length - 1];
        }

        /// <summary>
        /// 创建数组并赋予默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="length">数组长度</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>数组</returns>
        public static T[] CreateArray<T>( int length, T defaultValue )
        {
            var ret = new T[length];
            SetArrayValue( ret, defaultValue );
            return ret;
        }

        /// <summary>
        /// 设置数组每个元素的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="value">值</param>
        public static void SetArrayValue<T>( T[] array, T value )
        {
            if( array != null )
            {
                for( int index = 0; index < array.Length; index++ )
                {
                    array[index] = value;
                }
            }
        }

        /// <summary>
        /// 创建锯齿数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="rowCount">行数</param>
        /// <param name="columnCount">列数</param>
        /// <returns>锯齿数组</returns>
        public static T[][] CreateJaggedArray<T>( int rowCount, int columnCount )
        {
            var ret = new T[rowCount][];
            for( int index = 0; index < rowCount; index++ )
            {
                ret[index] = new T[columnCount];
            }
            return ret;
        }

        /// <summary>
        /// 所有数组的元素个数
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="arrays">数组的数组</param>
        /// <returns>所有数组的元素个数</returns>
        public static int GetTotalCount<T>( params T[][] arrays )
        {
            int count = 0;
            foreach( var array in arrays )
            {
                count += array.Length;
            }
            return count;
        }

        /// <summary>
        /// 连接所有数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="arrays">数组的数组</param>
        /// <returns>连接后的数组</returns>
        public static T[] JoinArray<T>( params T[][] arrays )
        {
            var ret = new T[GetTotalCount( arrays )];
            int index = 0;
            foreach( var array in arrays )
            {
                array.CopyTo( ret, index );
                index += array.Length;
            }

            return ret;
        }

        /// <summary>
        /// 获得分片数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="startIndex">分片的起始下标</param>
        /// <param name="count">分片的长度</param>
        /// <returns>分片数组</returns>
        public static T[] Slice<T>( T[] array, int startIndex, int count )
        {
            var ret = new T[count];
            Array.Copy( array, startIndex, ret, 0, ret.Length );
            return ret;
        }
        /// <summary>
        /// 获得分片数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="startIndex">分片的起始下标</param>
        /// <param name="count">分片的长度</param>
        /// <returns>分片数组</returns>
        public static Span<T> SliceSpan<T>( T[] array, int startIndex, int count )
        {
            return array.AsSpan(startIndex, count);
        }

        #region 二维数组函数

        /// <summary>
        ///     根据列下标，获得二维数组的分片数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="columnIndex">列下标</param>
        /// <returns>分片数组</returns>
        public static T[] SliceByColumnIndex<T>( T[,] array, int columnIndex )
        {
            var ret = new T[array.GetLength( 0 )];
            for( var index = 0; index < ret.Length; index++ )
            {
                ret[index] = array[index, columnIndex];
            }
            return ret;
        }

        /// <summary>
        ///     根据列下标，设置数组元素的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="columnIndex">列下标</param>
        /// <param name="values">值</param>
        public static void SetArrayValueByColumnIndex<T>( T[,] array, int columnIndex, T[] values )
        {
            for( var index = 0; index < values.Length; index++ )
            {
                array[index, columnIndex] = values[index];
            }
        }

        #endregion

        #endregion

        #region 判断两个数组是否相等

        /// <summary>
        /// 判断两个数组是否相等
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="a">数组1</param>
        /// <param name="b">数组2</param>
        /// <returns>true：相等，false：不相等</returns>
        public static bool Equal<T>( Span<T> a, Span<T> b ) where T : IEquatable<T>
        {
            if( a == null || b == null || a.Length != b.Length )
            {
                return false;
            }

            int count = a.Length;
            for( int index = 0; index < count; index++ )
            {
                if( !a[index].Equals( b[index] ) )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断两个数组是否相等
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="a">数组1</param>
        /// <param name="b">数组2</param>
        /// <returns>true：相等，false：不相等</returns>
        public static bool Equal<T>( T[] a, T[] b ) where T : IEquatable<T>
        {
            if( ReferenceEquals( a, b ) )
            {
                return true;
            }

            if( a == null || b == null || a.Length != b.Length )
            {
                return false;
            }

            return Equal( a, 0, b, 0, a.Length );
        }

        /// <summary>
        /// 判断两个数组是否相等
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="a">数组1</param>
        /// <param name="aOffset">a数组的偏移</param>
        /// <param name="b">数组2</param>
        /// <param name="bOffset">b数组的偏移</param>
        /// <param name="count">比较元素的数量</param>
        /// <returns>true：相等，false：不相等</returns>
        public static bool Equal<T>( T[] a, int aOffset, T[] b, int bOffset, int count ) where T : IEquatable<T>
        {
            if( ReferenceEquals( a, b ) )
            {
                return true;
            }

            if( a == null || b == null )
            {
                return false;
            }

            if( ( a.Length < aOffset + count ) || ( b.Length < bOffset + count ) )
            {
                return false;
            }

            for( int index = 0; index < count; index++ )
            {
                if( !a[aOffset + index].Equals( b[bOffset + index] ) )
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region  计算数值

        #region sum

        /// <summary>
        /// 计算总和
        /// </summary>
        /// <param name="values">输入数组</param>
        /// <returns>总和</returns>
        public static int Sum( int[] values )
        {
            int sum = 0;
            foreach( int value in values )
            {
                sum += value;
            }
            return sum;
        }

        #endregion

        #endregion

        #region 复制数组

        /// <summary>
        /// 复制数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">源数组</param>
        /// <returns>复制的数组</returns>
        public static T[] Clone<T>( T[] source )
        {
            var ret = new T[source.Length];
            source.CopyTo( ret, 0 );
            return ret;
        }

        /// <summary>
        /// 复制数组
        /// </summary>
        /// <param name="sourceArray">源数组</param>
        /// <param name="destinationArray">目标数组</param>
        /// <param name="destinationIndex">目标数组下标</param>
        /// <returns>实际复制的长度</returns>
        public static int Copy( Array sourceArray, Array destinationArray, int destinationIndex )
        {
            return Copy( sourceArray, 0, destinationArray, destinationIndex, sourceArray.Length );
        }

        /// <summary>
        /// 复制数组
        /// </summary>
        /// <param name="sourceArray">源数组</param>
        /// <param name="destinationArray">目标数组</param>
        /// <param name="destinationIndex">目标数组下标</param>
        /// <param name="length">复制的长度</param>
        /// <returns>实际复制的长度</returns>
        public static int Copy( Array sourceArray, Array destinationArray, int destinationIndex, int length )
        {
            return Copy( sourceArray, 0, destinationArray, destinationIndex, length );
        }

        /// <summary>
        /// 复制数组
        /// </summary>
        /// <param name="sourceArray">源数组</param>
        /// <param name="sourceIndex">源数组下标</param>
        /// <param name="destinationArray">目标数组</param>
        /// <param name="destinationIndex">目标数组下标</param>
        /// <param name="length">复制的长度</param>
        /// <returns>实际复制的长度</returns>
        public static int Copy( Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex,
                                int length )
        {
            length = Math.Min( length, destinationArray.Length - destinationIndex );
            if( length <= 0 )
            {
                return 0;
            }

            Array.Copy( sourceArray, sourceIndex, destinationArray, destinationIndex, length );
            return length;
        }

        /// <summary>
        /// 复制字节数组
        /// </summary>
        /// <param name="sourceArray">源数组</param>
        /// <param name="sourceIndex">源数组下标</param>
        /// <param name="destinationArray">目标数组</param>
        /// <param name="destinationIndex">目标数组下标</param>
        /// <param name="length">复制的长度</param>
        public static void CopyBytes( byte[] sourceArray, int sourceIndex, byte[] destinationArray, int destinationIndex,
                                      int length )
        {
            Buffer.BlockCopy( sourceArray, sourceIndex, destinationArray, destinationIndex, length );
        }

        #endregion

        #region 排序

        /// <summary>
        /// 对数组全部元素排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="ascending">true为"升序排列", false为"降序排列"</param>
        /// <returns>排序后的数组</returns>
        public static T[] SortArray<T>( T[] array, bool ascending ) where T : IComparable<T>
        {
            System.Comparison<T> handler = CompareUtils.CreateComparisonHandler<T>( ascending );

            var ret = new T[array.Length];
            array.CopyTo( ret, 0 );
            Array.Sort( ret, handler );
            return ret;
        }

        /// <summary>
        /// 根据数组keys对数组values全部元素排序
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="keys">键数组</param>
        /// <param name="values">值数组</param>
        /// <param name="ascending">true为"升序排列", false为"降序排列"</param>
        /// <returns>排序后的值数组</returns>
        public static TValue[] SortArray<TKey, TValue>( TKey[] keys, TValue[] values, bool ascending )
            where TKey : IComparable<TKey> where TValue : IComparable<TValue>
        {
            Array.Sort( keys, values );

            if( !ascending )
            {
                Array.Reverse( values );
            }

            var ret = new TValue[values.Length];
            values.CopyTo( ret, 0 );
            return ret;
        }

        #endregion

        #region 最值

        /// <summary>
        /// 获得最大值的下标
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>最大值的下标</returns>
        public static int MaxIndex<T>( T[] array ) where T : IComparable<T>
        {
            return MaxIndex( array, 0, array.Length );
        }

        /// <summary>
        /// 获得最大值的下标
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="startIndex">比较的起始下标</param>
        /// <param name="count">比较的数量</param>
        /// <returns>最大值的下标</returns>
        public static int MaxIndex<T>( T[] array, int startIndex, int count ) where T : IComparable<T>
        {
            return MaxMinIndex( array, startIndex, count, CompareUtils.CreateComparisonHandler<T>( true ) );
        }

        /// <summary>
        /// 获得最小值的下标
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>最小值的下标</returns>
        public static int MinIndex<T>( T[] array ) where T : IComparable<T>
        {
            return MinIndex( array, 0, array.Length );
        }

        /// <summary>
        /// 获得最小值的下标
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="startIndex">比较的起始下标</param>
        /// <param name="count">比较的数量</param>
        /// <returns>最小值的下标</returns>
        public static int MinIndex<T>( T[] array, int startIndex, int count ) where T : IComparable<T>
        {
            return MaxMinIndex( array, startIndex, count, CompareUtils.CreateComparisonHandler<T>( false ) );
        }

        /// <summary>
        /// 获得最大值/小值的下标
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="startIndex">比较的起始下标</param>
        /// <param name="count">比较的数量</param>
        /// <param name="handler">Comparison[T]</param>
        /// <returns>最大值/小值的下标</returns>
        private static int MaxMinIndex<T>( T[] array, int startIndex, int count, System.Comparison<T> handler )
            where T : IComparable<T>
        {
            startIndex = Math.Max( startIndex, 0 );
            int endIndex = Math.Min( startIndex + count, array.Length ) - 1;

            int ret = startIndex;
            T compareValue = array[ret];
            for( int index = startIndex + 1; index <= endIndex; index++ )
            {
                if( 0 < handler( array[index], compareValue ) )
                {
                    compareValue = array[index];
                    ret = index;
                }
            }

            return ret;
        }

        /// <summary>
        /// 获得最大值
        /// </summary>
        public static T Max<T>( T[] array ) where T : IComparable<T>
        {
            return array[MaxIndex( array )];
        }

        /// <summary>
        /// 获得最大值
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="startIndex">比较的起始下标</param>
        /// <param name="count">比较的数量</param>
        /// <returns>最大值</returns>
        public static T Max<T>( T[] array, int startIndex, int count ) where T : IComparable<T>
        {
            return array[MaxIndex( array, startIndex, count )];
        }

        /// <summary>
        /// 获得最小值
        /// </summary>
        public static T Min<T>( T[] array ) where T : IComparable<T>
        {
            return array[MinIndex( array )];
        }

        /// <summary>
        /// 获得最小值
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="startIndex">比较的起始下标</param>
        /// <param name="count">比较的数量</param>
        /// <returns>最小值</returns>
        public static T Min<T>( T[] array, int startIndex, int count ) where T : IComparable<T>
        {
            return array[MinIndex( array, startIndex, count )];
        }

        #endregion

        #region 数组类型转换

        /// <summary>
        /// 将double型数组转换为float型数组
        /// </summary>
        /// <param name="array">double型数组</param>
        /// <returns>float型数组</returns>
        public static Single[] Double2Single( Double[] array )
        {
            var ret = new Single[array.Length];
            int index = 0;
            foreach( Double value in array )
            {
                ret[index++] = (Single)value;
            }
            return ret;
        }

        /// <summary>
        /// 将float型数组转换为double型数组
        /// </summary>
        /// <param name="array">float型数组</param>
        /// <returns>double型数组</returns>
        public static Double[] Single2Double( Single[] array )
        {
            var ret = new Double[array.Length];
            array.CopyTo( ret, 0 );
            return ret;
        }

        /// <summary>
        /// 将float型数组转换为double型数组
        /// </summary>
        /// <param name="arrays">float型数组</param>
        /// <returns>double型数组</returns>
        public static Double[][] Single2Double( params Single[][] arrays )
        {
            var ret = new Double[arrays.Length][];
            int index = 0;
            foreach( var array in arrays )
            {
                ret[index++] = Single2Double( array );
            }
            return ret;
        }

        #endregion
    }
}