using System;
// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ���������źŴ����㷨��
    /// </summary>
    public static partial class DSPBasic
    {
        #region Ƶ���㷨

        #region ����ת�ٽ���ת��Ϊת��(����).

        /// <summary>
        /// ����ת�ٽ���ת��Ϊת��(����).
        /// </summary>
        /// <param name="second">�״�</param>
        /// <param name="rpm">����ת��</param>
        /// <returns>ת��(����)</returns>
        public static Double SecondtoCycle( _ValueT second, Double rpm )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( second * RpmtoHz( rpm ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// ����ת�ٽ�ת��(����)ת��Ϊ��.
        /// </summary>
        /// <param name="cycle">�״�</param>
        /// <param name="rpm">����ת��</param>
        /// <returns>ת��(����)</returns>
        public static Double CycletoSecond( _ValueT cycle, Double rpm )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( cycle * MathConst.SecondCountOfMinute / rpm );
            // ReSharper restore RedundantCast
        }

        #endregion

        #region Hz��״�ת��

        /// <summary>
        /// ����ת�ٽ�Xת��ΪHz.
        /// </summary>
        /// <param name="order">�״�</param>
        /// <param name="rpm">����ת��</param>
        /// <returns>Ƶ��ֵ</returns>
        public static Double XtoHz( _ValueT order, Double rpm )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( order * RpmtoHz( rpm ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// ����ת�ٽ�Hzת��ΪX.
        /// </summary>
        /// <param name="hz">Ƶ��ֵ</param>
        /// <param name="rpm">����ת��</param>
        /// <returns>�״�</returns>
        public static Double HztoX( _ValueT hz, Double rpm )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( hz * MathConst.SecondCountOfMinute / rpm );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// ����ת�ٽ�Hzת��ΪX.
        /// </summary>
        /// <param name="xArray">����Hz���飬����״�����</param>
        /// <param name="rpm">����ת��</param>
        public static void HztoX( _ValueT[] xArray, Double rpm )
        {
            // ReSharper disable RedundantCast
            CollectionUtils.ScaleArray( xArray, (_ValueT)( MathConst.SecondCountOfMinute / rpm ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// ����ת�ٽ�Xת��ΪHz.
        /// </summary>
        /// <param name="xArray">����״����飬���Hz����</param>
        /// <param name="rpm">����ת��</param>
        public static void XtoHz( _ValueT[] xArray, Double rpm )
        {
            // ReSharper disable RedundantCast
            CollectionUtils.ScaleArray( xArray, (_ValueT)( RpmtoHz( rpm ) ) );
            // ReSharper restore RedundantCast
        }

        #endregion

        /// <summary>
        /// ���㸴���л�2���ٸ���Ҷ���任(˫��), x(t) => X(f)
        /// </summary>
        /// <param name="reArray">���븴�ź�ʵ��Re(x)������FFTʵ��</param>
        /// <param name="imArray">���븴�ź��鲿Im(x)������FFT�鲿</param>
        /// <remarks>
        /// ����FFT��׼��ʽ���㣬���з���ֵ��δ��N. 
        /// </remarks>
        public static void CxFFT( _ValueT[] reArray, _ValueT[] imArray )
        {
            #region ������������Լ��

            //���ʵ�����鲿����ĸ����������
            MathError.CheckLengthEqual( reArray, imArray );

            //���ʵ�����鲿����ĸ����������0
            MathError.CheckLengthGTZero( reArray );

            //������鳤���Ƿ�2���ݴ�
            MathError.CheckPowerOfTwo( reArray.Length );

            #endregion

            Transform.ComplexFFT( reArray, imArray, reArray.Length, 1 );
        }

        /// <summary>
        /// ���㸴���л�2���ٸ���Ҷ��任(˫��), X(f) => x(t)
        /// </summary>
        /// <param name="reArray">����FFTʵ�������ظ��ź�ʵ��</param>
        /// <param name="imArray">����FFT�鲿�����ظ��ź��鲿</param>
        /// <remarks>
        /// ����ֵ������N������reArray[i]=Re{x[i]/N}, imArray[i]=Im{x[i]/N}. 
        /// </remarks>
        public static void CxInvFFT( _ValueT[] reArray, _ValueT[] imArray )
        {
            #region ������������Լ��

            //���ʵ�����鲿����ĸ����������
            MathError.CheckLengthEqual( reArray, imArray );

            //���ʵ�����鲿����ĸ����������0
            MathError.CheckLengthGTZero( reArray );

            //������鳤���Ƿ�2���ݴ�
            MathError.CheckPowerOfTwo( reArray.Length );

            #endregion

            Transform.ComplexFFT( reArray, imArray, reArray.Length, -1 );
        }


        /// <summary>
        /// ����ʵ���л�2���ٸ���Ҷ���任(˫��)
        /// </summary>
        /// <param name="reArray">���븴�ź�ʵ�����飬����FFTʵ������</param>
        /// <param name="imArray">���븴�ź��鲿���飬����FFT�鲿����</param>
        /// <remarks>
        /// ����FFT��׼��ʽ���㣬���з���ֵ��δ��N. 
        /// </remarks>
        public static void ReFFT( _ValueT[] reArray, out _ValueT[] imArray )
        {
            #region ������������Լ��

            //���ʵ�����鲿����ĸ����������0
            MathError.CheckLengthGTZero( reArray );

            //������鳤���Ƿ�2���ݴ�
            MathError.CheckPowerOfTwo( reArray.Length );

            #endregion

            imArray = new _ValueT[reArray.Length];

            Transform.RealFFT( reArray, imArray, reArray.Length, 1 );
        }

        /// <summary>
        /// ��Ƶ�׵�ʵ�����鲿�õ�˫�߷�ֵ��
        /// </summary>
        /// <param name="reArray">Ƶ�׵�ʵ��</param>
        /// <param name="imArray">Ƶ�׵��鲿</param>
        /// <param name="divLength">�Ƿ�����ݵĳ��ȣ���ҪΪ�˼�������FFT�ļ��㷽��</param>
        /// <returns>˫�߷�ֵ��</returns>
        public static _ValueT[] ReIm2AmpSpectrum( _ValueT[] reArray, _ValueT[] imArray, bool divLength )
        {
            _ValueT[] ret = new _ValueT[reArray.Length];
            int length = divLength ? reArray.Length : 1;
            for( int index = 0; index < ret.Length; index++ )
            {
                Double squareSum = MathBasic.SquareSum( reArray[index], imArray[index] );
                // ReSharper disable RedundantCast
                ret[index] = (_ValueT)( Math.Sqrt( squareSum ) / length );
                // ReSharper restore RedundantCast
            }
            return ret;
        }

        /// <summary>
        /// ��Ƶ�׵�ʵ�����鲿�õ���λ�ף���λ��Ϊ0��360��
        /// </summary>
        /// <param name="reArray">Ƶ�׵�ʵ��</param>
        /// <param name="imArray">Ƶ�׵��鲿</param>
        /// <returns>��λ��</returns>
        public static _ValueT[] ReIm2PhaseSpectrum( _ValueT[] reArray, _ValueT[] imArray )
        {
            _ValueT[] ret = new _ValueT[reArray.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                // ReSharper disable RedundantCast
                ret[index] = (_ValueT)MathBasic.ReIm2Phase180( reArray[index], imArray[index] );
                // ReSharper restore RedundantCast
            }
            return ret;
        }

        /// <summary>
        /// ����˫��Ƶ�׵�ʵ�����鲿���Ѿ���N��
        /// </summary>
        /// <param name="xArray">�����ź�</param>
        /// <param name="reArray">Ƶ�׵�ʵ��</param>
        /// <param name="imArray">Ƶ�׵��鲿</param>
        public static void BiReImSpectrum( _ValueT[] xArray, out _ValueT[] reArray, out _ValueT[] imArray )
        {
            #region ������������Լ��

            //���ʵ�����鲿����ĸ����������0
            MathError.CheckLengthGTZero( xArray );

            //������鳤���Ƿ�2���ݴ�
            MathError.CheckPowerOfTwo( xArray.Length );

            #endregion

            int length = xArray.Length;
            reArray = new _ValueT[length];
            imArray = new _ValueT[length];
            xArray.CopyTo( reArray, 0 );
            CxFFT( reArray, imArray );

            for( int index = 0; index < reArray.Length; index++ )
            {
                reArray[index] /= length;
                imArray[index] /= length;
            }
        }

        /// <summary>
        /// ���㵥��Ƶ�׵�ʵ�����鲿( ������ = �����źų��� / 2)���Ѿ���N��
        /// </summary>
        /// <param name="xArray">�����ź�</param>
        /// <param name="reArray">Ƶ�׵�ʵ��</param>
        /// <param name="imArray">Ƶ�׵��鲿</param>
        public static void ReImSpectrum( _ValueT[] xArray, out _ValueT[] reArray, out _ValueT[] imArray )
        {
            _ValueT[] re, im;
            BiReImSpectrum( xArray, out re, out im );

            reArray = new _ValueT[xArray.Length / 2];
            imArray = new _ValueT[reArray.Length];

            reArray[0] = re[0];
            imArray[0] = im[0];
            for( int index = 1; index < reArray.Length; index++ )
            {
                reArray[index] = 2 * re[index];
                imArray[index] = 2 * im[index];
            }
        }

        /// <summary>
        /// ����˫�߷�ֵ��λ��.
        /// </summary>
        /// <param name="xArray">�����ź�</param>
        /// <param name="ampSpectrum">����˫�߷�ֵ��</param>
        /// <param name="phaseSpectrum">����˫����λ��(��λ����)��-180�� ~ 180��</param>
        //	���㹫ʽ����: 
        //				AmpSpectrum= |FFT{x}| / N
        public static void BiAmpPhaseSpectrum( _ValueT[] xArray, out _ValueT[] ampSpectrum, out _ValueT[] phaseSpectrum )
        {
            #region ������������Լ��

            //���ʵ�����鲿����ĸ����������0
            MathError.CheckLengthGTZero( xArray );

            //������鳤���Ƿ�2���ݴ�
            MathError.CheckPowerOfTwo( xArray.Length );

            #endregion

            int length = xArray.Length;
            _ValueT[] reArray = new _ValueT[length];
            _ValueT[] imArray = new _ValueT[length];
            xArray.CopyTo( reArray, 0 );
            CxFFT( reArray, imArray );

            ampSpectrum = ReIm2AmpSpectrum( reArray, imArray, true );
            phaseSpectrum = ReIm2PhaseSpectrum( reArray, imArray );
        }

        /// <summary>
        /// ���㵥�߷�ֵ��λ��( ������ = �����źų��� / 2)
        /// </summary>
        /// <param name="xArray">�����ź�x[i]</param>
        /// <param name="ampSpectrum">���ص��߷�ֵ��</param>
        /// <param name="phaseSpectrum">������λ��(��λ����)</param>
        /// <remarks>
        /// ����ֱ����ʾ�� ����˻���κ�ϵ�������磬1024���ʱ�䲨�εõ�512���ס�
        /// </remarks>
        public static void AmpPhaseSpectrum( _ValueT[] xArray, out _ValueT[] ampSpectrum, out _ValueT[] phaseSpectrum )
        {
            _ValueT[] amplitude, phase;
            BiAmpPhaseSpectrum( xArray, out amplitude, out phase );

            int lineNum = xArray.Length / 2;
            ampSpectrum = new _ValueT[lineNum];
            phaseSpectrum = new _ValueT[lineNum];

            for( int index = 0; index < ampSpectrum.Length; index++ )
            {
                ampSpectrum[index] = 2 * amplitude[index];
                phaseSpectrum[index] = phase[index];
            }
            ampSpectrum[0] /= 2;
        }

        /// <summary>
        /// ����˫�߹�����.
        /// </summary>
        /// <param name="xArray">ԭʼ�ź�</param>
        /// <param name="powerSpectrum">����˫�߹�����</param>
        /// <param name="phaseSpectrum">����˫����λ��(��λ����)</param>
        //	���㹫ʽ����: 
        //				Gs(f) = |FFT{x}|^2 / N^2 = Amp{x}^2
        public static void BiPowerSpectrum( _ValueT[] xArray, out _ValueT[] powerSpectrum, out _ValueT[] phaseSpectrum )
        {
            _ValueT[] ampSpectrum;
            BiAmpPhaseSpectrum( xArray, out ampSpectrum, out phaseSpectrum );

            powerSpectrum = new _ValueT[ampSpectrum.Length];
            for( int index = 0; index < powerSpectrum.Length; index++ )
            {
                powerSpectrum[index] = ampSpectrum[index] * ampSpectrum[index];
            }
        }

        /// <summary>
        /// ���㵥�߹�����.( ������ = ԭʼ�źų��� / 2)
        /// </summary>
        /// <param name="xArray">ԭʼ�ź�</param>
        /// <returns>���߹�����</returns>
        /// <remarks>
        /// ����˻���κ�ϵ�������磺1024���ʱ�䲨�εõ�512���ס�
        /// </remarks>
        //	���㹫ʽ����: 
        //				Gs(f) = 2 * |FFT{x}|^2 / N^2 = 2 * Amp{x}^2
        public static _ValueT[] PowerSpectrum( _ValueT[] xArray )
        {
            _ValueT[] ampSpectrum, phaseSpectrum;
            BiAmpPhaseSpectrum( xArray, out ampSpectrum, out phaseSpectrum );

            _ValueT[] powerSpectrum = new _ValueT[ampSpectrum.Length / 2];
            for( int index = 0; index < powerSpectrum.Length; index++ )
            {
                powerSpectrum[index] = 2 * ampSpectrum[index] * ampSpectrum[index];
            }
            powerSpectrum[0] /= 2;

            return powerSpectrum;
        }

        /// <summary>
        /// ����ϣ�����ر任������ʵ�źţ� re[]=ʵ�źţ�im[]=0��
        /// </summary>
        /// <param name="reArray">�����źŵ�ʵ�����飬����任���ʵ������</param>
        /// <param name="imArray">�����źŵ��鲿���飬����任����鲿����</param>
        public static void HilbertT( _ValueT[] reArray, _ValueT[] imArray )
        {
            #region ������������Լ��

            //���ʵ�����鲿����ĸ����������
            MathError.CheckLengthEqual( reArray, imArray );

            //���ʵ�����鲿����ĸ����������0
            MathError.CheckLengthGTZero( reArray );

            //������鳤���Ƿ�2���ݴ�
            MathError.CheckPowerOfTwo( reArray.Length );

            #endregion

            Transform.Hilbert( reArray, imArray );
        }

        #endregion

        #region ʱ���źŴ���

        /// <summary>
        /// �������������ֱ�Ӿ��Cxy
        /// </summary>
        /// <param name="xArray">��һ������[n]</param>
        /// <param name="yArray">�ڶ�������[m]</param>
        /// <returns>����������[n+m-1]</returns>
        public static _ValueT[] DirectConvolve( _ValueT[] xArray, _ValueT[] yArray )
        {
            #region ������������Լ��

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            //ϵͳĬ��x���鳤�� >= y���鳤��
            if( xArray.Length >= yArray.Length )
            {
                return Convolve.DirectConvolve( xArray, yArray );
            }

            //���x���鳤�� < y���鳤��,����x��y����
            return Convolve.DirectConvolve( yArray, xArray );
        }

        /// <summary>
        /// ����Butterworth��ͨ�˲�
        /// </summary>
        /// <param name="xArray">�����ź�����</param>
        /// <param name="order">�˲�������</param>
        /// <param name="fl">�ͽ�ֹƵ��</param>
        /// <param name="fh">�߽�ֹƵ��</param>
        /// <param name="fs">����Ƶ��</param>
        /// <returns>�����˲��������</returns>
        public static _ValueT[] ButterworthBandPass( _ValueT[] xArray, int order, Double fl, Double fh,
                                                     Double fs )
        {
            #region ������������Լ��

            MathError.CheckLengthGTZero( xArray );

            //�������������0
            if( order <= 0 )
            {
                throw new AlgorithmException( MathError.MathErrorOrderGTZero );
            }

            //����ֹƵ�ʱ������0
            if( fl < 0 )
            {
                fl = 0;
            }
            Double freqBand = GetFreqBandBySampleFreq( fs );
            if( fh <= 0 )
            {
                fh = freqBand;
            }
            if( fh > freqBand )
            {
                fh = freqBand;
            }

            //����ֹƵ�������޹�ϵ
            if( fl > fh )
            {
                Double mid = fl;
                fl = fh;
                fh = mid;
            }

            #endregion

            return DigitalIIR.ButterworthBandPass( xArray, fl, fh, order, fs );
        }

        /// <summary>
        /// �������������ֱ�����Rxy(�����˵�ЧӦ����)
        /// </summary>
        /// <param name="xArray">�����һ������x[n]</param>
        /// <param name="yArray">����ڶ�������y[m]</param>
        /// <param name="dt">�������ʱ����</param>
        /// <param name="rxyArray">������ؽ������</param>
        /// <param name="tauArray">����ʱ���ӳ�����</param>
        /// <remarks>
        /// ���ʱ���ӳ�tau[i]����0, ��ʾ�ź�y[m]��ǰx[n];���ʱ���ӳ�tau[i]С��0, ��ʾ�ź�y[m]�ͺ�x[n].
        /// </remarks>
        public static void DirectCorrelate( _ValueT[] xArray, _ValueT[] yArray, _ValueT dt,
                                            out _ValueT[] rxyArray, out _ValueT[] tauArray )
        {
            #region ������������Լ��

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            Correlate.DirectCorrelate( xArray, yArray, dt, out rxyArray, out tauArray );
        }


        /// <summary>
        /// ������������Ŀ������Rxy(�����˵�ЧӦ����)
        /// </summary>
        /// <param name="xArray">�����һ������x[n]</param>
        /// <param name="yArray">����ڶ�������y[m]</param>
        /// <param name="dt">�������ʱ����</param>
        /// <param name="rxyArray">������ؽ������</param>
        /// <param name="tauArray">����ʱ���ӳ�����</param>
        /// <remarks>
        /// ���ʱ���ӳ�tau[i]����0, ��ʾ�ź�y[m]��ǰx[n];���ʱ���ӳ�tau[i]С��0, ��ʾ�ź�y[m]�ͺ�x[n].
        /// </remarks>
        public static void FastCorrelate( _ValueT[] xArray, _ValueT[] yArray, _ValueT dt,
                                          out _ValueT[] rxyArray, out _ValueT[] tauArray )
        {
            #region ������������Լ��

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            Correlate.FastCorrelate( xArray, yArray, dt, out rxyArray, out tauArray );
        }

        /// <summary>
        /// ȥ��ֱ������
        /// </summary>
        /// <param name="xArray">����ԭʼ�źţ�����AC�������</param>
        public static void ACCoupling( _ValueT[] xArray )
        {
            if( 0 < xArray.Length )
            {
                _ValueT average = StatisticsUtils.Mean( xArray );
                for( int index = 0; index < xArray.Length; index++ )
                {
                    xArray[index] -= average;
                }
            }
        }

        #endregion

        #region ʱ��������ָ��

        /// <summary>
        /// ����ֵ. ��ӳ���ź�˲ʱ����ǿ��, ��ӳ�˷�ֵ�仯��Χ,�Լ�ƫ���������. �������۹�ʽ����, ����Ϊλ���źŵ���������.
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>����ֵ</returns>
        //���㹫ʽ�� max(x[])-min(x[])
        public static _ValueT TruePeak2Peak( _ValueT[] xArray )
        {
            if( 0 < xArray.Length )
            {
                _ValueT min = xArray[0];
                _ValueT max = min;

                foreach( _ValueT value in xArray )
                {
                    if( value > max )
                    {
                        max = value;
                    }
                    if( value < min )
                    {
                        min = value;
                    }
                }
                return max - min;
            }
            return 0;
        }

        /// <summary>
        /// ���ֵ. ��ӳ���ź�˲ʱ����ǿ��. ����RMSֵ����,����Ϊ����Ӧ����λ���źŵ���������.
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>���ֵ</returns>
        //���㹫ʽ�� 2��sqrt(2)��RMS
        public static _ValueT Peak2Peak( _ValueT[] xArray )
        {
            return 2 * Peak( xArray );
        }

        /// <summary>
        /// ���ֵ. ��ӳ���ź�˲ʱ�������ǿ��. �������۹�ʽ����,����Ϊ���ٶ��źŵ���������.
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>���ֵ</returns>
        //	ʹ��CollectionUtils.AbsMax()����
        public static _ValueT TruePeak( _ValueT[] xArray )
        {
            return CollectionUtils.AbsMax( xArray );
        }

        /// <summary>
        /// ��ֵ. ��ӳ���ź�˲ʱ�������ǿ��. ����RMSֵ����,����Ϊ����Ӧ���м��ٶ��źŵ���������.
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>��ֵ</returns>
        //���㹫ʽ�� sqrt(2)��RMS
        public static _ValueT Peak( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)( MathConst.SqrtTwo * RMS( xArray ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// ��ֵ. ��ӳ���źŵ�ƽ������. ����RMSֵ����,����Ϊ����Ӧ����λ�ơ��ٶȺͼ��ٶ��źŵ���������.
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>��ֵ</returns>
        //	���㹫ʽ�� RMS^2
        public static _ValueT Overall( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)MathBasic.Square( RMS( xArray ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// ��Чֵ(�ֽо�����ֵ). ��ӳ���ź�����ǿ��. ����Ϊ�ٶ��źŵ���������.
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>��Чֵ</returns>
        //	ʹ��StatisticsUtils.OriginMoment�㷨
        //	���㹫ʽ�� sqrt(sum(x[t]^2/N))
        public static _ValueT RMS( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)Math.Sqrt( StatisticsUtils.OriginMoment( xArray, 2 ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// ����ֵ. ��ӳ���ź�����ǿ��. ����Ϊ�ٶ��źŵ���������.
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>����ֵ</returns>
        //���㹫ʽ�� RMS^2
        public static _ValueT MeanSquare( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)MathBasic.Square( RMS( xArray ) );
            // ReSharper restore RedundantCast
        }

        #endregion

        #region ʱ��������ָ��

        /// <summary>
        /// ����ָ��.(������ָ��)
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>����ָ��</returns>
        /// <remarks>
        /// ���źŵķ�ֵ��Ƶ�ʱ仯�����У����������������ϵ���󣩣����Թ����㹻���С�
        /// </remarks>
        //	ʹ��MathBasic.Div()����
        public static _ValueT ShapeFactor( _ValueT[] xArray )
        {
            return MathBasic.Div( RMS( xArray ), StatisticsUtils.AbsMean( xArray ) );
        }

        /// <summary>
        /// ����ָ��. (������ָ��)
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>����ָ��</returns>
        /// <remarks>
        /// ���źŵķ�ֵ��Ƶ�ʱ仯�����У����������������ϵ���󣩣����Թ����㹻���С�
        /// </remarks>
        //	ʹ��CollectionUtils.AbsMax()�����滻CollectionUtils.Max()
        //	ʹ��MathBasic.Div()������CollectionUtils.AbsMax()
        public static _ValueT ImpulseFactor( _ValueT[] xArray )
        {
            return MathBasic.Div( CollectionUtils.AbsMax( xArray ), StatisticsUtils.AbsMean( xArray ) );
        }

        /// <summary>
        /// ��ֵָ��. (������ָ��)
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>��ֵָ��</returns>
        /// <remarks>
        /// ���źŵķ�ֵ��Ƶ�ʱ仯�����У����������������ϵ���󣩣����Թ����㹻���С�
        /// </remarks>
        //	ʹ��CollectionUtils.AbsMax()�����滻CollectionUtils.Max()
        //	ʹ��MathBasic.Div()������CollectionUtils.AbsMax()
        public static _ValueT CrestFactor( _ValueT[] xArray )
        {
            return MathBasic.Div( CollectionUtils.AbsMax( xArray ), RMS( xArray ) );
        }

        /// <summary>
        /// ԣ��ָ��. (������ָ��)
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>ԣ��ָ��</returns>
        /// <remarks>
        /// ���źŵķ�ֵ��Ƶ�ʱ仯�����У����������������ϵ���󣩣����Թ����㹻���С�
        /// </remarks>
        //	ʹ��CollectionUtils.AbsMax()�����滻CollectionUtils.Max()
        //	ʹ��MathBasic.Div()������CollectionUtils.AbsMax()
        public static _ValueT ClearanceFactor( _ValueT[] xArray )
        {
            return MathBasic.Div( CollectionUtils.AbsMax( xArray ), StatisticsUtils.SMR( xArray ) );
        }

        /// <summary>
        /// ���ָ��. (������ָ��)
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>���ָ��</returns>
        /// <remarks>
        /// ���źŵķ�ֵ��Ƶ�ʱ仯�����У����������������ϵ���󣩣����Թ����㹻���С�
        /// </remarks>
        public static _ValueT SkewFactor( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)MathBasic.Div( StatisticsUtils.Moment( xArray, 3 ),
                                           MathBasic.PositivePow( StatisticsUtils.StdDeviation( xArray ), 3 ) );
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// �Ͷ�ָ��. (������ָ��)
        /// </summary>
        /// <param name="xArray">����ԭʼ�ź�</param>
        /// <returns>�Ͷ�ָ��</returns>
        /// <remarks>
        /// ���źŵķ�ֵ��Ƶ�ʱ仯�����У����������������ϵ���󣩣����Թ����㹻���С�
        /// </remarks>
        public static _ValueT KurtoFactor( _ValueT[] xArray )
        {
            // ReSharper disable RedundantCast
            return (_ValueT)MathBasic.Div( StatisticsUtils.Moment( xArray, 4 ),
                                           MathBasic.PositivePow( StatisticsUtils.StdDeviation( xArray ), 4 ) );
            // ReSharper restore RedundantCast
        }

        #endregion

        #region �źżӴ��㷨

        /// <summary>
        /// ���ź�Ӧ��һ�����Ǵ�
        /// </summary>
        /// <param name="xArray">һά�����ź�</param>
        /// <returns>���ؼӴ�����ź�</returns>
        public static _ValueT[] TriangleWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenTriWindow( xArray.Length );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// ���ź�Ӧ��һ��������
        /// </summary>
        /// <param name="xArray">һά�����ź�</param>
        /// <returns>���ؼӴ�����ź�</returns>
        public static _ValueT[] HanningWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenCosWindows( WindowType.Hanning, xArray.Length );
            return MaskWindow( xArray, win, Window.AmpScale_Hanning );
        }

        /// <summary>
        /// ���ź�Ӧ��һ��������
        /// </summary>
        /// <param name="xArray">һά�����ź�</param>
        /// <returns>���ؼӴ�����ź�</returns>
        public static _ValueT[] HammingWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenCosWindows( WindowType.Hamming, xArray.Length );
            return MaskWindow( xArray, win, Window.AmpScale_Hamming );
        }

        /// <summary>
        /// ���ź�Ӧ��һ������������
        /// </summary>
        /// <param name="xArray">һά�����ź�</param>
        /// <returns>���ؼӴ�����ź�</returns>
        public static _ValueT[] BlackmanWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenCosWindows( WindowType.Blackman, xArray.Length );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// ���ź�Ӧ��һ��ƽ����
        /// </summary>
        /// <param name="xArray">һά�����ź�</param>
        /// <returns>���ؼӴ�����ź�</returns>
        public static _ValueT[] FlatTopWindow( _ValueT[] xArray )
        {
            Single[] win = Window.GenCosWindows( WindowType.FlatTop, xArray.Length );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// ���ź�Ӧ��һ��ƽ����
        /// </summary>
        /// <param name="xArray">һά�����ź�</param>
        /// <param name="final">ָ������ֵ(�Ƽ�final=0.01)</param>
        /// <returns>���ؼӴ�����ź�</returns>
        public static _ValueT[] ExpWindow( _ValueT[] xArray, Single final )
        {
            Single[] win = Window.GenExpWindow( xArray.Length, final );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// ���ź�Ӧ��һ������
        /// </summary>
        /// <param name="xArray">һά�����ź�</param>
        /// <param name="duty">�ӳٵİٷֱ�(�Ƽ�duty=50)</param>
        /// <returns>���ؼӴ�����ź�</returns>
        public static _ValueT[] ForceWindow( _ValueT[] xArray, Single duty )
        {
            Single[] win = Window.GenForceWindow( xArray.Length, duty );
            return MaskWindow( xArray, win );
        }

        /// <summary>
        /// ���źżӴ�
        /// </summary>
        /// <param name="xArray">�ź�</param>
        /// <param name="window">��ϵ��</param>
        private static _ValueT[] MaskWindow( _ValueT[] xArray, Single[] window )
        {
            return MaskWindow( xArray, window, Window.AmpScale_Rectangle );
        }

        /// <summary>
        /// ���źżӴ�
        /// </summary>
        /// <param name="xArray">�ź�</param>
        /// <param name="window">��ϵ��</param>
        /// <param name="ampScale">��ֵ��Ȼָ�ϵ��</param>
        private static _ValueT[] MaskWindow( _ValueT[] xArray, Single[] window, Single ampScale )
        {
            _ValueT[] ret = new _ValueT[xArray.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = xArray[index] * window[index] * ampScale;
            }
            return ret;
        }

        #endregion
    }
}