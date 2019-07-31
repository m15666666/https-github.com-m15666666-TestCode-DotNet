using System;
// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ��ѧ�����㷨�ࡣ
    /// </summary>
    public static partial class MathBasic
    {
        /// <summary>
        /// ���������������ֹ��0��ʩ,���뼫Сֵ��
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>����z</returns>
        public static _ValueT Div( _ValueT x, _ValueT y )
        {
            return x / ( y == 0 ? MathConst.epsilon : y );
        }

        /// <summary>
        /// ƽ��ֵ��
        /// </summary>
        /// <param name="values">����ֵ</param>
        /// <returns>ƽ��ֵ</returns>
        public static _ValueT SquareSum( params _ValueT[] values )
        {
            _ValueT sum = 0;
            foreach( _ValueT value in values )
            {
                sum += ( value * value );
            }
            return sum;
        }

        #region integer power

        /// <summary>
        /// ����ָ�����ֵ�ָ�����ݣ�power����Ϊ������
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="power">�ݴ�</param>
        /// <returns>ָ�����ֵ�ָ������</returns>
        public static _ValueT PositivePow( _ValueT x, int power )
        {
            if( power <= 0 )
            {
                throw new ArgumentOutOfRangeException( String.Format( "power({0})��������������", power ) );
            }

            _ValueT ret = 1;
            while( 0 < power-- )
            {
                ret *= x;
            }
            return ret;
        }

        /// <summary>
        /// ����ָ�����ֵ�ָ������
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="power">�ݴ�</param>
        /// <returns>ָ�����ֵ�ָ������</returns>
        public static _ValueT IntegerPow( _ValueT x, int power )
        {
            if( power == 0 )
            {
                return 1;
            }

            if( 0 < power )
            {
                return PositivePow( x, power );
            }

            if( x == 0 )
            {
                return 0;
            }

            return PositivePow( 1 / x, -power );
        }

        #endregion
    }
}