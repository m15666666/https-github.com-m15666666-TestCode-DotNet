using System;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 数学基本算法类。
    /// </summary>
    public static partial class MathBasic
    {
        /// <summary>
        /// 将两个数相除，防止除0措施,引入极小值。
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>返回z</returns>
        public static _ValueT Div( _ValueT x, _ValueT y )
        {
            return x / ( y == 0 ? MathConst.epsilon : y );
        }

        /// <summary>
        /// 平方值和
        /// </summary>
        /// <param name="values">输入值</param>
        /// <returns>平方值</returns>
        public static _ValueT SquareSum( params _ValueT[] values )
        {
            return SquareSum(values.AsSpan());
        }

        /// <summary>
        /// 平方值和
        /// </summary>
        /// <param name="values">输入值</param>
        /// <returns>平方值</returns>
        public static _ValueT SquareSum( Span<_ValueT> values )
        {
            _ValueT sum = 0;
            foreach( _ValueT value in values ) sum += ( value * value );
            return sum;
        }

        #region integer power

        /// <summary>
        /// 返回指定数字的指定次幂，power必须为正整数
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="power">幂次</param>
        /// <returns>指定数字的指定次幂</returns>
        public static _ValueT PositivePow( _ValueT x, int power )
        {
            if( power <= 0 ) throw new ArgumentOutOfRangeException( String.Format( "power({0}) must be positive.", power ) );

            _ValueT ret = 1;
            while( 0 < power-- ) ret *= x;
            return ret;
        }

        /// <summary>
        /// 返回指定数字的指定次幂
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="power">幂次</param>
        /// <returns>指定数字的指定次幂</returns>
        public static _ValueT IntegerPow( _ValueT x, int power )
        {
            if( x == 0 ) return 0;
            if( power == 0 ) return 1;

            return 0 < power ? PositivePow( x, power ) : PositivePow( 1 / x, -power );
        }

        #endregion
    }
}