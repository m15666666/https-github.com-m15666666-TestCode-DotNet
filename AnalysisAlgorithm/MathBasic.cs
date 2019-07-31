using System;
using System.Collections.Generic;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ������ѧ����
    /// </summary>
    public static partial class MathBasic
    {
        static MathBasic()
        {
            // ��ʼ��2���ݴ�ֵ���ݴε�ӳ��
            {
                for( int power = 1, powerValue = 1; power <= MathConst.FFTPowerMax; ++power )
                {
                    powerValue *= 2;
                    _number2PowerOfTwo[powerValue] = power;
                    _powerOfTwor2Number[power] = powerValue;
                }
            }
        }

        #region �����������

        /// <summary>
        /// ������Сֵ���ݴ�
        /// </summary>
        public const int MinLogValuePower = -30;

        /// <summary>
        /// ���ڼ����������Сֵ
        /// </summary>
        public static readonly Double MinLogValue = Math.Pow( 10, MinLogValuePower );

        #endregion

        #region 2���ݴ�

        /// <summary>
        /// 2���ݴ�ֵ���ݴε�ӳ��
        /// </summary>
        private static readonly Dictionary<int, int> _number2PowerOfTwo = new Dictionary<int, int>();

        /// <summary>
        /// 2���ݴζ��ݴ�ֵ��ӳ��
        /// </summary>
        private static readonly Dictionary<int, int> _powerOfTwor2Number = new Dictionary<int, int>();

        /// <summary>
        /// ��������Ƿ�2���ݴ�
        /// </summary>
        /// <param name="number">����������</param>
        /// <return>����true��ʾ��2���ݴ�</return>
        public static bool IsPowerOfTwo( int number )
        {
            return _number2PowerOfTwo.ContainsKey( number );
        }

        /// <summary>
        /// ��ȡ2���ݴ�ֵ
        /// </summary>
        /// <param name="power">2���ݴ�</param>
        /// <returns>2���ݴ�ֵ</returns>
        public static int GetPowerValueOfTwo( int power )
        {
            if( _powerOfTwor2Number.ContainsKey( power ) )
            {
                return _powerOfTwor2Number[power];
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// ��ȡ2���ݴ�
        /// </summary>
        /// <param name="number">����������</param>
        /// <returns>2���ݴ�</returns>
        public static int GetPowerOfTwo( int number )
        {
            if( _number2PowerOfTwo.ContainsKey( number ) )
            {
                return _number2PowerOfTwo[number];
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// ��ȡС�ڵ���ָ������2���ݴ�
        /// </summary>
        /// <param name="number">����������</param>
        /// <returns>2���ݴ�</returns>
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
        /// ��ȡ���ڵ���ָ������2���ݴ�
        /// </summary>
        /// <param name="number">����������</param>
        /// <returns>2���ݴ�</returns>
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
        /// ����ʸ���ķ�ֵ
        /// </summary>
        /// <param name="re">ʸ����ʵ��</param>
        /// <param name="im">ʸ�����鲿</param>
        /// <returns>����ʸ���ķ�ֵ</returns>
        public static Double ReIm2Amp( Double re, Double im )
        {
            return Math.Sqrt( SquareSum( re, im ) );
        }

        /// <summary>
        /// ���Ƕ�ת��Ϊ���ȣ�����ֵΪ-pi~pi.
        /// </summary>
        /// <param name="angle">�Ƕ�ֵ</param>
        /// <returns>����ֵ</returns>
        public static Double AngleToRadians( Double angle )
        {
            return DegreeToAngle180( angle ) * ( MathConst.PI / MathConst.Deg_180 );
        }

        /// <summary>
        /// ������ת��Ϊ�Ƕȣ�����ֵΪ0��~360��.
        /// </summary>
        /// <param name="radians">����ֵ</param>
        /// <returns>�Ƕ�ֵ</returns>
        public static Double RadiansToAngle360( Double radians )
        {
            return DegreeToAngle360( radians * ( MathConst.Deg_180 / MathConst.PI ) );
        }

        /// <summary>
        /// ������ת��Ϊ�Ƕȣ�����ֵΪ-180��~180��.
        /// </summary>
        /// <param name="radians">����ֵ</param>
        /// <returns>�Ƕ�ֵ</returns>
        public static Double RadiansToAngle180( Double radians )
        {
            return DegreeToAngle180( radians * ( MathConst.Deg_180 / MathConst.PI ) );
        }

        /// <summary>
        /// ���Ƕ����Ƶ�-180��~180��
        /// </summary>
        /// <param name="degree">�Ƕ�ֵ</param>
        /// <returns>�Ƕ�ֵ</returns>
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
        /// ���Ƕ����Ƶ�0��~360��
        /// </summary>
        /// <param name="degree">�Ƕ�ֵ</param>
        /// <returns>�Ƕ�ֵ</returns>
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
        /// ����ʸ���ĽǶ�, �Ƕȷ�ΧΪ0��~360�ȣ�����ʱ�뷽��.
        /// </summary>
        /// <param name="re">ʸ����ʵ��</param>
        /// <param name="im">ʸ�����鲿</param>
        /// <returns>����ʸ���ĽǶ�(��λ����)</returns>
        public static Double ReIm2Phase360( Double re, Double im )
        {
            return ReIm2Phase360( re, im, false );
        }

        /// <summary>
        /// ����ʸ���ĽǶ�, �Ƕȷ�ΧΪ0��~360��.
        /// </summary>
        /// <param name="re">ʸ����ʵ��</param>
        /// <param name="im">ʸ�����鲿</param>
        /// <param name="isClockwise">�Ƿ�˳ʱ�뷽��</param>
        /// <returns>����ʸ���ĽǶ�(��λ����)</returns>
        public static Double ReIm2Phase360( Double re, Double im, bool isClockwise )
        {
            Double phase = RadiansToAngle360( Math.Atan2( im, re ) );
            return isClockwise ? MathConst.Deg_360 - phase : phase;
        }

        /// <summary>
        /// ����ʸ���ĽǶ�, �Ƕȷ�ΧΪ-180��~180��.
        /// </summary>
        /// <param name="re">ʸ����ʵ��</param>
        /// <param name="im">ʸ�����鲿</param>
        /// <returns>����ʸ���ĽǶ�(��λ����)</returns>
        public static Double ReIm2Phase180( Double re, Double im )
        {
            return RadiansToAngle180( Math.Atan2( im, re ) );
        }

        /// <summary>
        /// ����ʸ���ĽǶ�, �Ƕȷ�ΧΪ-90��~90��.
        /// </summary>
        /// <param name="re">ʸ����ʵ��</param>
        /// <param name="im">ʸ�����鲿</param>
        /// <returns>����ʸ���ĽǶ�(��λ����)</returns>
        public static Double ReIm2Phase90( Double re, Double im )
        {
            return RadiansToAngle180( Math.Atan( im / re ) );
        }

        /// <summary>
        /// ƽ��ֵ
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>ƽ��ֵ</returns>
        public static Double Square( Double x )
        {
            return x * x;
        }

        /// <summary>
        /// ����������ڵ����ޣ�(1,2,3,4)֮һ
        /// </summary>
        /// <param name="re">ʵ��</param>
        /// <param name="im">�鲿</param>
        /// <returns>�������ڵ����ޣ�(1,2,3,4)֮һ</returns>
        public static int Quadrant( Double re, Double im )
        {
            if( im >= 0 && re >= 0 )
            {
                return 1; //��ʼ�ڵ�1����
            }
            if( im >= 0 && re < 0 )
            {
                return 2; //��ʼ�ڵ�2����
            }
            if( im < 0 && re < 0 )
            {
                return 3; //��ʼ�ڵ�3����
            }
            if( im < 0 && re >= 0 )
            {
                return 4; //��ʼ�ڵ�4����
            }

            throw new AlgorithmException( "Quadrant��Ӧ�õ������" );
        }
    }
}