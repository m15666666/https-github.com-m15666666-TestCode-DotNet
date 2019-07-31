using System;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 集合操作实用工具类
    /// </summary>
    public static partial class CollectionUtils
    {
        #region 数组操作

        /// <summary>
        /// 将数组中每个元素除以一个数值
        /// </summary>
        public static void ArrayDivValue( _ValueT[] array, _ValueT value )
        {
            ScaleArray( array, 1 / value );
        }

        /// <summary>
        /// 将数组中每个元素加一个数值offset
        /// </summary>
        public static void OffsetArray( _ValueT[] array, _ValueT offset )
        {
            for( int index = 0; index < array.Length; index++ )
            {
                array[index] += offset;
            }
        }

        /// <summary>
        /// 将数组中每个元素乘一个数值
        /// </summary>
        public static void ScaleArray( _ValueT[] array, _ValueT scale )
        {
            for( int index = 0; index < array.Length; index++ )
            {
                array[index] *= scale;
            }
        }

        /// <summary>
        /// 将数组中每个元素取绝对值
        /// </summary>
        public static void AbsArray( _ValueT[] array )
        {
            for( int index = 0; index < array.Length; index++ )
            {
                array[index] = Math.Abs( array[index] );
            }
        }

        /// <summary>
        /// 将两个数组的元素相加，并返回新数组
        /// </summary>
        public static _ValueT[] AddArray( _ValueT[] xArray, _ValueT[] yArray )
        {
            if( xArray.Length != yArray.Length )
            {
                return null;
            }

            _ValueT[] ret = new _ValueT[xArray.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = xArray[index] + yArray[index];
            }

            return ret;
        }

        /// <summary>
        /// 将两个数组的元素相减，并返回新数组
        /// </summary>
        public static _ValueT[] SubArray( _ValueT[] xArray, _ValueT[] yArray )
        {
            if( xArray.Length != yArray.Length )
            {
                return null;
            }

            _ValueT[] ret = new _ValueT[xArray.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = xArray[index] - yArray[index];
            }

            return ret;
        }

        /// <summary>
        /// 将两个数组的元素相乘，并返回新数组
        /// </summary>
        public static _ValueT[] MulArray( _ValueT[] xArray, _ValueT[] yArray )
        {
            if( xArray.Length != yArray.Length )
            {
                return null;
            }

            _ValueT[] ret = new _ValueT[xArray.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = xArray[index] * yArray[index];
            }

            return ret;
        }

        /// <summary>
        /// 将两个数组的元素相除，并返回新数组
        /// </summary>
        public static _ValueT[] DivArray( _ValueT[] xArray, _ValueT[] yArray )
        {
            if( xArray.Length != yArray.Length )
            {
                return null;
            }

            _ValueT[] ret = new _ValueT[xArray.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = MathBasic.Div( xArray[index], yArray[index] );
            }

            return ret;
        }

        /// <summary>
        /// 计算数组全部元素的和
        /// </summary>
        public static _ValueT SumArray( _ValueT[] array )
        {
            _ValueT sum = 0;
            for( int index = 0; index < array.Length; index++ )
            {
                sum += array[index];
            }
            return sum;
        }

        /// <summary>
        /// 计算数组中指定区间元素的和
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="startIndex">开始下标</param>
        /// <param name="endIndex">结尾下标</param>
        /// <returns>数组的和</returns>
        public static _ValueT SumArray( _ValueT[] array, int startIndex, int endIndex )
        {
            _ValueT sum = 0;
            for( int index = startIndex; index <= endIndex; index++ )
            {
                sum += array[index];
            }
            return sum;
        }

        /// <summary>
        /// 获得数组中的最大值
        /// </summary>
        /// <param name="array">数组</param>
        public static _ValueT Max( _ValueT[] array )
        {
            _ValueT max = _ValueT.MinValue;
            foreach( _ValueT value in array )
            {
                if( max < value )
                {
                    max = value;
                }
            }
            return max;
        }

        /// <summary>
        /// 获得数组中的最小值
        /// </summary>
        /// <param name="array">数组</param>
        public static _ValueT Min( _ValueT[] array )
        {
            _ValueT min = _ValueT.MaxValue;
            foreach( _ValueT value in array )
            {
                if( value < min )
                {
                    min = value;
                }
            }
            return min;
        }

        /// <summary>
        /// 获得数组绝对值中的最大值
        /// </summary>
        /// <param name="array">数组</param>
        public static _ValueT AbsMax( _ValueT[] array )
        {
            _ValueT max = 0;
            foreach( _ValueT value in array )
            {
                _ValueT abs = Math.Abs( value );
                if( abs > max )
                {
                    max = abs;
                }
            }
            return max;
        }

        /// <summary>
        /// 绝对值最大值
        /// </summary>
        /// <param name="arrays">数组</param>
        /// <returns>绝对值最大值</returns>
        public static _ValueT AbsMax( params _ValueT[][] arrays )
        {
            _ValueT max = 0;
            foreach( _ValueT[] array in arrays )
            {
                max = Math.Max( max, AbsMax( array ) );
            }
            return max;
        }

        /// <summary>
        /// 将数组的元素取scale * lg(x[i]/x0)
        /// </summary>
        /// <param name="array">线性数组</param>
        /// <param name="x0">基准值</param>
        /// <param name="scale">比例值</param>
        /// <returns>分贝数组(单位: dB分贝)</returns>
        public static _ValueT[] ArrayLog10( _ValueT[] array, Double x0, Double scale )
        {
            Double x0_dB = Math.Log10( x0 );
            _ValueT[] ret = new _ValueT[array.Length];
            int index = 0;
            // ReSharper disable RedundantCast
            _ValueT minLogValue = (_ValueT)MathBasic.MinLogValue;
            // ReSharper restore RedundantCast
            foreach( _ValueT value in array )
            {
                Double logValue = ( value <= minLogValue ? MathBasic.MinLogValuePower : Math.Log10( value ) );
                // ReSharper disable RedundantCast
                ret[index++] = (_ValueT)( ( logValue - x0_dB ) * scale );
                // ReSharper restore RedundantCast
            }

            return ret;
        }

        /// <summary>
        /// 将数组的元素取20lg(x[i]/x0)
        /// </summary>
        /// <param name="array">线性数组</param>
        /// <param name="x0">基准值</param>
        /// <returns>分贝数组(单位: dB分贝)</returns>
        public static _ValueT[] Array20Log10( _ValueT[] array, Double x0 )
        {
            return ArrayLog10( array, x0, MathConst.LogScale_20 );
        }

        /// <summary>
        /// 将数组的元素取10lg(x[i]/x0)
        /// </summary>
        /// <param name="array">线性数组</param>
        /// <param name="x0">基准值</param>
        /// <returns>分贝数组(单位: dB分贝)</returns>
        public static _ValueT[] Array10Log10( _ValueT[] array, Double x0 )
        {
            return ArrayLog10( array, x0, MathConst.LogScale_10 );
        }

        #endregion
    }
}