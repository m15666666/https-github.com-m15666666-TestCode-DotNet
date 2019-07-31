using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    ///     复数类
    /// </summary>
    public class Complex
    {
        /// <summary>
        ///     获得模
        /// </summary>
        public double Modulus
        {
            get { return Math.Sqrt( Real * Real + Imag * Imag ); }
            set
            {
                var angle = RadiansAngle;
                Real = Math.Cos( angle ) * value;
                Imag = Math.Sin( angle ) * value;
            }
        }

        /// <summary>
        ///     设置或返回角度(弧度制)
        /// </summary>
        public double RadiansAngle
        {
            get { return Math.Atan2( Imag, Real ); }
            set
            {
                var modulus = Modulus;
                Real = modulus * Math.Cos( value );
                Imag = modulus * Math.Sin( value );
            }
        }

        /// <summary>
        ///     设置或返回角度(度)
        /// </summary>
        public double DegreeAngle
        {
            get { return MathBasic.RadiansToAngle360( RadiansAngle ); }
            set { RadiansAngle = MathBasic.AngleToRadians( value ); }
        }

        /// <summary>
        ///     获得和设置实部
        /// </summary>
        public double Real { get; set; }

        /// <summary>
        ///     获取和设置虚部
        /// </summary>
        public double Imag { get; set; }

        /// <summary>
        ///     重载加号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Complex operator +( Complex x, Complex y )
        {
            return new Complex( x.Real + y.Real, x.Imag + y.Imag );
        }

        /// <summary>
        ///     重载减号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Complex operator -( Complex x, Complex y )
        {
            return new Complex( x.Real - y.Real, x.Imag - y.Imag );
        }

        /// <summary>
        ///     求得当前复数的相反数
        /// </summary>
        /// <returns></returns>
        public static Complex operator -( Complex x )
        {
            return new Complex( -x.Real, -x.Imag );
        }

        /// <summary>
        ///     重载乘号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Complex operator *( Complex x, Complex y )
        {
            return new Complex( x.Real * y.Real - x.Imag * y.Imag, x.Real * y.Imag + x.Imag * y.Real );
        }

        /// <summary>
        ///     重载除号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Complex operator /( Complex x, Complex y )
        {
            double e, f, s, t;
            if( Math.Abs( y.Real ) >= Math.Abs( y.Imag ) )
            {
                e = y.Imag / y.Real;
                f = y.Real + e * y.Imag;

                s = ( x.Real + x.Imag * e ) / f;
                t = ( x.Imag - x.Real * e ) / f;
            }
            else
            {
                e = y.Real / y.Imag;
                f = y.Imag + e * y.Real;

                s = ( x.Real * e + x.Imag ) / f;
                t = ( x.Imag * e - x.Real ) / f;
            }
            return new Complex( s, t );
        }

        /// <summary>
        ///     求得共轭
        /// </summary>
        public void Conjugate()
        {
            Imag = -Imag;
        }

        /// <summary>
        ///     使用平行四边形法则分解矢量
        /// </summary>
        /// <param name="targetAngle1">分解的矢量1，输入时包含分解的角度1(弧度制)</param>
        /// <param name="targetAngle2">分解的矢量2，输入时包含分解的角度2(弧度制)</param>
        /// <returns>null:如果无法分解，或者包含两个分解的矢量的数组</returns>
        public Complex[] DecomposeVector( double targetAngle1, double targetAngle2 )
        {
            var originAngle = RadiansAngle;

            var diffTarget1 = targetAngle1 - originAngle;
            var diffTarget2 = originAngle - targetAngle2;

            if( Math.Abs( MathBasic.RadiansToAngle180( diffTarget1 ) ) <= 1 ||
                Math.Abs( MathBasic.RadiansToAngle180( diffTarget2 ) ) <= 1 )
            {
                // 角度差小于等于1度，无法分解
                return null;
            }

            var factor = Math.Cos( diffTarget1 ) * Math.Sin( diffTarget2 ) +
                         Math.Sin( diffTarget1 ) * Math.Cos( diffTarget2 );

            if( Math.Abs( factor ) <= double.Epsilon )
            {
                // 除数接近零，无法分解
                return null;
            }

            var modulus1 = Modulus * Math.Sin( diffTarget2 ) / factor;
            var modulus2 = modulus1 * Math.Sin( diffTarget1 ) / Math.Sin( diffTarget2 );

            return new[]
            { CreateByModulusRadians( modulus1, targetAngle1 ), CreateByModulusRadians( modulus2, targetAngle2 ) };
        }

        /// <summary>
        ///     重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Modulus + "@" + RadiansAngle;
        }

        #region ctor

        /// <summary>
        ///     构造函数
        /// </summary>
        public Complex()
        {
            Real = 0;
            Imag = 0;
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="real">实部</param>
        /// <param name="imag">虚部</param>
        public Complex( double real, double imag )
        {
            Real = real;
            Imag = imag;
        }

        /// <summary>
        ///     从模和角度（度）创建复数
        /// </summary>
        /// <param name="modulus">模</param>
        /// <param name="degree">角度（度）</param>
        /// <returns>复数</returns>
        public static Complex CreateByModulusDegree( double modulus, double degree )
        {
            return CreateByModulusRadians( modulus, MathBasic.AngleToRadians( degree ) );
        }

        /// <summary>
        ///     从模和角度（弧度制）创建复数
        /// </summary>
        /// <param name="modulus">模</param>
        /// <param name="radians">角度（弧度制）</param>
        /// <returns>创建复数</returns>
        public static Complex CreateByModulusRadians( double modulus, double radians )
        {
            return new Complex { Real = modulus * Math.Cos( radians ), Imag = modulus * Math.Sin( radians ) };
        }

        #endregion
    }
}