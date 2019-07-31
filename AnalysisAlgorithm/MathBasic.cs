using System;
using System.Collections.Generic;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 基本数学操作
    /// </summary>
    public static partial class MathBasic
    {
        static MathBasic()
        {
            // 初始化2的幂次值对幂次的映射
            {
                for( int power = 1, powerValue = 1; power <= MathConst.FFTPowerMax; ++power )
                {
                    powerValue *= 2;
                    _number2PowerOfTwo[powerValue] = power;
                    _powerOfTwor2Number[power] = powerValue;
                }
            }
        }

        #region 与计算对数相关

        /// <summary>
        /// 对数最小值的幂次
        /// </summary>
        public const int MinLogValuePower = -30;

        /// <summary>
        /// 用于计算对数的最小值
        /// </summary>
        public static readonly Double MinLogValue = Math.Pow( 10, MinLogValuePower );

        #endregion

        #region 2的幂次

        /// <summary>
        /// 2的幂次值对幂次的映射
        /// </summary>
        private static readonly Dictionary<int, int> _number2PowerOfTwo = new Dictionary<int, int>();

        /// <summary>
        /// 2的幂次对幂次值的映射
        /// </summary>
        private static readonly Dictionary<int, int> _powerOfTwor2Number = new Dictionary<int, int>();

        /// <summary>
        /// 检查整数是否2的幂次
        /// </summary>
        /// <param name="number">被检查的整数</param>
        /// <return>返回true表示是2的幂次</return>
        public static bool IsPowerOfTwo( int number )
        {
            return _number2PowerOfTwo.ContainsKey( number );
        }

        /// <summary>
        /// 获取2的幂次值
        /// </summary>
        /// <param name="power">2的幂次</param>
        /// <returns>2的幂次值</returns>
        public static int GetPowerValueOfTwo( int power )
        {
            if( _powerOfTwor2Number.ContainsKey( power ) )
            {
                return _powerOfTwor2Number[power];
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// 获取2的幂次
        /// </summary>
        /// <param name="number">被检查的整数</param>
        /// <returns>2的幂次</returns>
        public static int GetPowerOfTwo( int number )
        {
            if( _number2PowerOfTwo.ContainsKey( number ) )
            {
                return _number2PowerOfTwo[number];
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// 获取小于等于指定数的2的幂次
        /// </summary>
        /// <param name="number">被检查的整数</param>
        /// <returns>2的幂次</returns>
        public static int GetFloorPowerOfTwo( int number )
        {
            if( number < 2 )
            {
                throw new ArgumentOutOfRangeException();
            }

            int halfNumber = number / 2;
            for( int power = 1, powerValue = 1; power <= MathConst.FFTPowerMax; power++ )
            {
                powerValue *= 2;
                if( powerValue <= number && halfNumber < powerValue )
                {
                    return power;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// 获取大于等于指定数的2的幂次
        /// </summary>
        /// <param name="number">被检查的整数</param>
        /// <returns>2的幂次</returns>
        public static int GetCeilingPowerOfTwo( int number )
        {
            if( number < 2 )
            {
                throw new ArgumentOutOfRangeException();
            }

            for( int power = 1, powerValue = 1; power <= MathConst.FFTPowerMax; ++power )
            {
                powerValue *= 2;
                if( number <= powerValue )
                {
                    return power;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        #endregion

        /// <summary>
        /// 计算矢量的幅值
        /// </summary>
        /// <param name="re">矢量的实部</param>
        /// <param name="im">矢量的虚部</param>
        /// <returns>返回矢量的幅值</returns>
        public static Double ReIm2Amp( Double re, Double im )
        {
            return Math.Sqrt( SquareSum( re, im ) );
        }

        /// <summary>
        /// 将角度转换为弧度，返回值为-pi~pi.
        /// </summary>
        /// <param name="angle">角度值</param>
        /// <returns>弧度值</returns>
        public static Double AngleToRadians( Double angle )
        {
            return DegreeToAngle180( angle ) * ( MathConst.PI / MathConst.Deg_180 );
        }

        /// <summary>
        /// 将弧度转换为角度，返回值为0度~360度.
        /// </summary>
        /// <param name="radians">弧度值</param>
        /// <returns>角度值</returns>
        public static Double RadiansToAngle360( Double radians )
        {
            return DegreeToAngle360( radians * ( MathConst.Deg_180 / MathConst.PI ) );
        }

        /// <summary>
        /// 将弧度转换为角度，返回值为-180度~180度.
        /// </summary>
        /// <param name="radians">弧度值</param>
        /// <returns>角度值</returns>
        public static Double RadiansToAngle180( Double radians )
        {
            return DegreeToAngle180( radians * ( MathConst.Deg_180 / MathConst.PI ) );
        }

        /// <summary>
        /// 将角度限制到-180度~180度
        /// </summary>
        /// <param name="degree">角度值</param>
        /// <returns>角度值</returns>
        public static Double DegreeToAngle180( Double degree )
        {
            while( MathConst.Deg_180 < degree )
            {
                degree -= MathConst.Deg_360;
            }
            while( degree < -MathConst.Deg_180 )
            {
                degree += MathConst.Deg_360;
            }
            return degree;
        }

        /// <summary>
        /// 将角度限制到0度~360度
        /// </summary>
        /// <param name="degree">角度值</param>
        /// <returns>角度值</returns>
        public static Double DegreeToAngle360( Double degree )
        {
            while( MathConst.Deg_360 < degree )
            {
                degree -= MathConst.Deg_360;
            }
            while( degree < -MathConst.Deg_0 )
            {
                degree += MathConst.Deg_360;
            }
            return degree;
        }

        /// <summary>
        /// 计算矢量的角度, 角度范围为0度~360度，按逆时针方向.
        /// </summary>
        /// <param name="re">矢量的实部</param>
        /// <param name="im">矢量的虚部</param>
        /// <returns>返回矢量的角度(单位：度)</returns>
        public static Double ReIm2Phase360( Double re, Double im )
        {
            return ReIm2Phase360( re, im, false );
        }

        /// <summary>
        /// 计算矢量的角度, 角度范围为0度~360度.
        /// </summary>
        /// <param name="re">矢量的实部</param>
        /// <param name="im">矢量的虚部</param>
        /// <param name="isClockwise">是否按顺时针方向</param>
        /// <returns>返回矢量的角度(单位：度)</returns>
        public static Double ReIm2Phase360( Double re, Double im, bool isClockwise )
        {
            Double phase = RadiansToAngle360( Math.Atan2( im, re ) );
            return isClockwise ? MathConst.Deg_360 - phase : phase;
        }

        /// <summary>
        /// 计算矢量的角度, 角度范围为-180度~180度.
        /// </summary>
        /// <param name="re">矢量的实部</param>
        /// <param name="im">矢量的虚部</param>
        /// <returns>返回矢量的角度(单位：度)</returns>
        public static Double ReIm2Phase180( Double re, Double im )
        {
            return RadiansToAngle180( Math.Atan2( im, re ) );
        }

        /// <summary>
        /// 计算矢量的角度, 角度范围为-90度~90度.
        /// </summary>
        /// <param name="re">矢量的实部</param>
        /// <param name="im">矢量的虚部</param>
        /// <returns>返回矢量的角度(单位：度)</returns>
        public static Double ReIm2Phase90( Double re, Double im )
        {
            return RadiansToAngle180( Math.Atan( im / re ) );
        }

        /// <summary>
        /// 平方值
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>平方值</returns>
        public static Double Square( Double x )
        {
            return x * x;
        }

        /// <summary>
        /// 获得数据所在的象限，(1,2,3,4)之一
        /// </summary>
        /// <param name="re">实部</param>
        /// <param name="im">虚部</param>
        /// <returns>数据所在的象限，(1,2,3,4)之一</returns>
        public static int Quadrant( Double re, Double im )
        {
            if( im >= 0 && re >= 0 )
            {
                return 1; //开始于第1象限
            }
            if( im >= 0 && re < 0 )
            {
                return 2; //开始于第2象限
            }
            if( im < 0 && re < 0 )
            {
                return 3; //开始于第3象限
            }
            if( im < 0 && re >= 0 )
            {
                return 4; //开始于第4象限
            }

            throw new AlgorithmException( "Quadrant不应该到达这里！" );
        }
    }
}