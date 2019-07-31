using System;
// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ���ϲ���ʵ�ù�����
    /// </summary>
    public static partial class CollectionUtils
    {
        #region �������

        /// <summary>
        /// ��������ÿ��Ԫ�س���һ����ֵ
        /// </summary>
        public static void ArrayDivValue( _ValueT[] array, _ValueT value )
        {
            ScaleArray( array, 1 / value );
        }

        /// <summary>
        /// ��������ÿ��Ԫ�ؼ�һ����ֵoffset
        /// </summary>
        public static void OffsetArray( _ValueT[] array, _ValueT offset )
        {
            for( int index = 0; index < array.Length; index++ )
            {
                array[index] += offset;
            }
        }

        /// <summary>
        /// ��������ÿ��Ԫ�س�һ����ֵ
        /// </summary>
        public static void ScaleArray( _ValueT[] array, _ValueT scale )
        {
            for( int index = 0; index < array.Length; index++ )
            {
                array[index] *= scale;
            }
        }

        /// <summary>
        /// ��������ÿ��Ԫ��ȡ����ֵ
        /// </summary>
        public static void AbsArray( _ValueT[] array )
        {
            for( int index = 0; index < array.Length; index++ )
            {
                array[index] = Math.Abs( array[index] );
            }
        }

        /// <summary>
        /// �����������Ԫ����ӣ�������������
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
        /// �����������Ԫ�������������������
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
        /// �����������Ԫ����ˣ�������������
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
        /// �����������Ԫ�������������������
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
        /// ��������ȫ��Ԫ�صĺ�
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
        /// ����������ָ������Ԫ�صĺ�
        /// </summary>
        /// <param name="array">����</param>
        /// <param name="startIndex">��ʼ�±�</param>
        /// <param name="endIndex">��β�±�</param>
        /// <returns>����ĺ�</returns>
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
        /// ��������е����ֵ
        /// </summary>
        /// <param name="array">����</param>
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
        /// ��������е���Сֵ
        /// </summary>
        /// <param name="array">����</param>
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
        /// ����������ֵ�е����ֵ
        /// </summary>
        /// <param name="array">����</param>
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
        /// ����ֵ���ֵ
        /// </summary>
        /// <param name="arrays">����</param>
        /// <returns>����ֵ���ֵ</returns>
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
        /// �������Ԫ��ȡscale * lg(x[i]/x0)
        /// </summary>
        /// <param name="array">��������</param>
        /// <param name="x0">��׼ֵ</param>
        /// <param name="scale">����ֵ</param>
        /// <returns>�ֱ�����(��λ: dB�ֱ�)</returns>
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
        /// �������Ԫ��ȡ20lg(x[i]/x0)
        /// </summary>
        /// <param name="array">��������</param>
        /// <param name="x0">��׼ֵ</param>
        /// <returns>�ֱ�����(��λ: dB�ֱ�)</returns>
        public static _ValueT[] Array20Log10( _ValueT[] array, Double x0 )
        {
            return ArrayLog10( array, x0, MathConst.LogScale_20 );
        }

        /// <summary>
        /// �������Ԫ��ȡ10lg(x[i]/x0)
        /// </summary>
        /// <param name="array">��������</param>
        /// <param name="x0">��׼ֵ</param>
        /// <returns>�ֱ�����(��λ: dB�ֱ�)</returns>
        public static _ValueT[] Array10Log10( _ValueT[] array, Double x0 )
        {
            return ArrayLog10( array, x0, MathConst.LogScale_10 );
        }

        #endregion
    }
}