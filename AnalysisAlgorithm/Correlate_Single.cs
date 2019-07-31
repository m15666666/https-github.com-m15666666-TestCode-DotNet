using System;
using Moons.Common20;
// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ����㷨��
    /// </summary>
    public static partial class Correlate
    {
        /// <summary>
        /// ֱ�Ӽ��������������غ���(�����˵�ЧӦ����)
        /// </summary>
        /// <param name="xArray">�����һ������</param>
        /// <param name="yArray">����ڶ�������</param>
        /// <param name="dt">�������ʱ����</param>
        /// <param name="rxy">������ؽ������</param>
        /// <param name="tau">����ʱ���ӳ�����</param>
        /// <remarks>
        /// ���㹫ʽ����: 
        ///	���: 
        ///		n = x[]��Ԫ�ظ���      m = y[]��Ԫ�ظ��� 
        /// ��ô: 
        ///		              m-1 
        ///			Rxy[i] =  �� x[k+n-1-i] * y[k] 
        ///		              k=0 
        /// </remarks>  
        public static void DirectCorrelate( _ValueT[] xArray, _ValueT[] yArray, _ValueT dt,
                                            out _ValueT[] rxy, out _ValueT[] tau )
        {
            int xLength = xArray.Length;
            int yLength = yArray.Length;

            int rxyLength = xLength + yLength - 1;
            _ValueT[] rxyArray = new _ValueT[rxyLength];
            _ValueT[] tauArray = new _ValueT[rxyLength];

            for( int outterIndex = 0; outterIndex < rxyLength; outterIndex++ )
            {
                _ValueT rxySum = 0;

                // �ۼӵĴ���
                int count = 0;

                for( int innerIndex = 0; innerIndex < yLength; innerIndex++ )
                {
                    int xIndex = innerIndex + xLength - 1 - outterIndex;
                    if( 0 <= xIndex && xIndex < xLength )
                    {
                        count++;
                        rxySum += xArray[xIndex] * yArray[innerIndex];
                    }
                }

                tauArray[outterIndex] = ( outterIndex - xLength + 1 ) * dt;

                //���ж˵�ЧӦ����
                //rxyArray[outterIndex] = rxySum / count;

                // �����˵�����
                rxyArray[outterIndex] = rxySum;
            }

            // ����ʢ�������������ط���N��������N+M-1��NΪ�������鳤�ȵ�Сֵ
            //rxy = new _ValueT[Math.Min( xLength, yLength )];
            //tau = new _ValueT[rxy.Length];
            //for( int index = 0; index < rxy.Length; index++ )
            //{
            //    rxy[index] = rxyArray[index];
            //    tau[index] = tauArray[index];
            //}

            // ����Ҫ����������N+M-1����x����ʾtau�������޷�ͨ����ط���ȷ������ʵ����������������ʱ���
            rxy = rxyArray;
            tau = tauArray;
        }

        /// <summary>
        /// �������(�����˵�ЧӦ����)
        /// </summary>
        /// <param name="xArray">�����һ������x[n]</param>
        /// <param name="yArray">����ڶ�������y[m]</param>
        /// <param name="dt">�������ʱ����</param>
        /// <param name="rxy">������ؽ������</param>
        /// <param name="tau">����ʱ���ӳ�����</param>
        /// <remarks>
        ///���㲽�����£�
        ///1.ѡ��L���㣺L>=n+m-1;L=2^r��
        ///2.��x[n]��y[m]����,�γɳ�ΪL=2^r�����У�
        ///		x0[i]=x[i], i��[0,n-1];
        ///		x0[i]=0,    i��[n,L-1];
        ///		y0[i]=0,    i��[0,n-2];
        ///		y0[i]=y[i], i��[n-1,n+m-2];
        ///		y0[i]=0,    i��[n+m-1,L-1];
        ///3.��FFT�ֱ����x0[i]��y0[i]��X0[i]��Y0[i]��
        ///4.����X0[i]�Ĺ����Y0[i]�ĳ˻�, Z[i]=conj(X0[i])*Y0[i];
        ///5.��FFT����Z[i]����任��ȡǰn+m-1����������ء�
        /// </remarks>
        public static void FastCorrelate( _ValueT[] xArray, _ValueT[] yArray, _ValueT dt,
                                          out _ValueT[] rxy, out _ValueT[] tau )
        {
            int xLength = xArray.Length;
            int yLength = yArray.Length;

            int rxyLength = xLength + yLength - 1;
            _ValueT[] rxyArray = new _ValueT[rxyLength];
            _ValueT[] tauArray = new _ValueT[rxyLength];

            //����1-ѡ��L
            int fftLength = 1;
            for( int index = 1; index <= MathConst.FFTPowerMax; index++ )
            {
                fftLength *= 2;
                if( fftLength >= rxyLength )
                {
                    break;
                }
            }

            //����2-��x[n]��y[m]����,�γɳ�ΪL=2^r������
            _ValueT[] xr0 = new _ValueT[fftLength];
            _ValueT[] yr0 = new _ValueT[fftLength];

            // ��������
            xArray.CopyTo( xr0, 0 );
            yArray.CopyTo( yr0, xLength - 1 );

            //����3-��FFT�ֱ����x0[i]��y0[i]��X0[i]��Y0[i]
            _ValueT[] xi0, yi0;
            DSPBasic.ReFFT( xr0, out xi0 );
            DSPBasic.ReFFT( yr0, out yi0 );

            //����4-����X0[i]�Ĺ����Y0[i]�ĳ˻�, Z[i]=conj(X0[i])*Y0[i];
            _ValueT[] zr = new _ValueT[fftLength];
            _ValueT[] zi = new _ValueT[fftLength];

            for( int index = 0; index < fftLength; index++ )
            {
                zr[index] = xr0[index] * yr0[index] + xi0[index] * yi0[index];
                zi[index] = xr0[index] * yi0[index] - xi0[index] * yr0[index];
            }

            //����4-��FFT����Z[i]����任��ȡǰn+m-1�����������
            DSPBasic.CxInvFFT( zr, zi );

            //���ж˵�ЧӦ����
            for( int index = 0; index < rxyLength; index++ )
            {
                tauArray[index] = ( index - xLength + 1 ) * dt;
                //rxyArray[index] = index < xLength ? zr[index] / ( index + 1 ) : zr[index] / ( 2 * xLength - index );

                // �����˵�����
                rxyArray[index] = zr[index];
            }

            // ����ʢ�������������ط���N��������N+M-1��NΪ�������鳤�ȵ�Сֵ
            //rxy = new _ValueT[Math.Min( xLength, yLength )];
            //tau = new _ValueT[rxy.Length];
            //for( int index = 0; index < rxy.Length; index++ )
            //{
            //    rxy[index] = rxyArray[index];
            //    tau[index] = tauArray[index];
            //}

            // ����Ҫ����������N+M-1����x����ʾtau�������޷�ͨ����ط���ȷ������ʵ����������������ʱ���
            rxy = rxyArray;
            tau = tauArray;
        }

        /// <summary>
        /// ���ף�ʹ�ÿ�����ص��м�����Ϊ����
        /// </summary>
        /// <param name="xArray">�����һ������x[n]</param>
        /// <param name="yArray">����ڶ�������y[m]</param>
        /// <param name="ampSpectrum">���ط�ֵ������</param>
        /// <param name="phaseSpectrum">������λ������</param>
        /// <param name="fftLength">��fft�任��ʱ�䲨�γ���</param>
        /// <remarks>
        ///���㲽�����£�
        ///1.ѡ��L���㣺L>=n+m-1;L=2^r��
        ///2.��x[n]��y[m]����,�γɳ�ΪL=2^r�����У�
        ///		x0[i]=x[i], i��[0,n-1];
        ///		x0[i]=0,    i��[n,L-1];
        ///		y0[i]=0,    i��[0,n-2];
        ///		y0[i]=y[i], i��[n-1,n+m-2];
        ///		y0[i]=0,    i��[n+m-1,L-1];
        ///3.��FFT�ֱ����x0[i]��y0[i]��X0[i]��Y0[i]��
        ///4.����X0[i]�Ĺ����Y0[i]�ĳ˻�, Z[i]=conj(X0[i])*Y0[i];
        ///5.��FFT����Z[i]����任��ȡǰn+m-1����������ء�
        /// </remarks>
        public static void CrossSpectrum( _ValueT[] xArray, _ValueT[] yArray, out _ValueT[] ampSpectrum,
                                          out _ValueT[] phaseSpectrum, out int fftLength )
        {
            int xLength = xArray.Length;
            int yLength = yArray.Length;

            int rxyLength = xLength + yLength - 1;

            //����1-ѡ��L
            int fftWaveLength = 1;
            for( int index = 1; index <= MathConst.FFTPowerMax; index++ )
            {
                fftWaveLength *= 2;
                if( fftWaveLength >= rxyLength )
                {
                    break;
                }
            }
            fftLength = fftWaveLength;

            //����2-��x[n]��y[m]����,�γɳ�ΪL=2^r������
            _ValueT[] xr0 = new _ValueT[fftWaveLength];
            _ValueT[] yr0 = new _ValueT[fftWaveLength];

            // ��������
            xArray.CopyTo( xr0, 0 );
            yArray.CopyTo( yr0, xLength - 1 );

            //����3-��FFT�ֱ����x0[i]��y0[i]��X0[i]��Y0[i]
            _ValueT[] xi0, yi0;
            DSPBasic.ReFFT( xr0, out xi0 );
            DSPBasic.ReFFT( yr0, out yi0 );

            //����4-����X0[i]�Ĺ����Y0[i]�ĳ˻�, Z[i]=conj(X0[i])*Y0[i];
            _ValueT[] zr = new _ValueT[fftWaveLength];
            _ValueT[] zi = new _ValueT[fftWaveLength];

            for( int index = 0; index < fftWaveLength; index++ )
            {
                zr[index] = xr0[index] * yr0[index] + xi0[index] * yi0[index];
                zi[index] = xr0[index] * yi0[index] - xi0[index] * yr0[index];
            }

            ampSpectrum = new _ValueT[DSPBasic.GetSpectrumLengthByTimeWaveLength( fftLength )];
            phaseSpectrum = new _ValueT[ampSpectrum.Length];

            // �����ж˵�����
            for( int index = 0; index < ampSpectrum.Length; index++ )
            {
                var ampSquare = zr[index] * zr[index] + zi[index] * zi[index];
                if( ampSquare != 0 )
                {
                    ampSpectrum[index] = (_ValueT)Math.Sqrt( ampSquare );
                }
                phaseSpectrum[index] = (_ValueT)MathBasic.ReIm2Phase180( zr[index], zi[index] );
            }
        }

        /// <summary>
        /// ֱ�Ӽ��������������ɺ���
        /// </summary>
        /// <param name="xArray">�����һ������</param>
        /// <param name="yArray">����ڶ�������</param>
        /// <returns>������ɽ������</returns>
        public static _ValueT[] DirectCoherence( _ValueT[] xArray, _ValueT[] yArray )
        {
            #region ������������Լ��

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            int minLength = Math.Min( xArray.Length, yArray.Length );

            _ValueT[] r, tau, wave4FFT = new _ValueT[minLength];

            DirectCorrelate( xArray, yArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );
            var xyAmpSpectrum = SpectrumBasic.AmpSpectrum( wave4FFT );

            DirectCorrelate( xArray, xArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );
            var xxAmpSpectrum = SpectrumBasic.AmpSpectrum( wave4FFT );

            DirectCorrelate( yArray, yArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );
            var yyAmpSpectrum = SpectrumBasic.AmpSpectrum( wave4FFT );

            return CrossSpectrum2Coherence( xyAmpSpectrum, xxAmpSpectrum, yyAmpSpectrum );
        }

        /// <summary>
        /// ʹ�ÿ�����ص��м�����Ϊ���ף�����������������
        /// </summary>
        /// <param name="xArray">�����һ������</param>
        /// <param name="yArray">����ڶ�������</param>
        /// <param name="coherence">��ɽ������</param>
        /// <param name="fftLength">��fft�任��ʱ�䲨�γ���</param>
        /// <returns>������ɽ������</returns>
        public static void FastCoherence( _ValueT[] xArray, _ValueT[] yArray, out _ValueT[] coherence,
                                          out int fftLength )
        {
            #region ������������Լ��

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );
            MathError.CheckLengthEqual( xArray, yArray );

            #endregion

            // ʹ�ÿ�����ص��м�����Ϊ����
            _ValueT[] xyAmpSpectrum, xyPhaseSpectrum;
            int xyFftLength;
            CrossSpectrum( xArray, yArray, out xyAmpSpectrum, out xyPhaseSpectrum, out xyFftLength );

            _ValueT[] xxAmpSpectrum, xxPhaseSpectrum;
            int xxFftLength;
            CrossSpectrum( xArray, xArray, out xxAmpSpectrum, out xxPhaseSpectrum, out xxFftLength );

            _ValueT[] yyAmpSpectrum, yyPhaseSpectrum;
            int yyFftLength;
            CrossSpectrum( yArray, yArray, out yyAmpSpectrum, out yyPhaseSpectrum, out yyFftLength );

            fftLength = xyFftLength;

            coherence = CrossSpectrum2Coherence( xyAmpSpectrum, xxAmpSpectrum, yyAmpSpectrum );
        }

        /// <summary>
        /// ʹ�û��׽���������
        /// </summary>
        /// <param name="xyAmpSpectrum">Sxy��ֵ</param>
        /// <param name="xxAmpSpectrum">Sxx��ֵ</param>
        /// <param name="yyAmpSpectrum">Syy��ֵ</param>
        /// <returns>������ɽ������</returns>
        private static _ValueT[] CrossSpectrum2Coherence( _ValueT[] xyAmpSpectrum, _ValueT[] xxAmpSpectrum,
                                                          _ValueT[] yyAmpSpectrum )
        {
            var ret = new _ValueT[xyAmpSpectrum.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                var GxxGyy = xxAmpSpectrum[index] * yyAmpSpectrum[index];
                var GxyGxy = xyAmpSpectrum[index] * xyAmpSpectrum[index];
                if( GxyGxy != 0 && GxxGyy != 0 )
                {
                    ret[index] = (_ValueT)Math.Sqrt( GxyGxy / GxxGyy );
                }
            }
            return ret;
        }

        /// <summary>
        /// ֱ�Ӽ������������Ƶ�캯��
        /// </summary>
        /// <param name="xArray">�����һ������</param>
        /// <param name="yArray">����ڶ�������</param>
        /// <param name="ampSpectrum">Ƶ��ķ�Ƶ����</param>
        /// <param name="phaseSpectrum">Ƶ�����Ƶ����</param>
        public static void DirectFrequenceResponse( _ValueT[] xArray, _ValueT[] yArray, out _ValueT[] ampSpectrum,
                                                    out _ValueT[] phaseSpectrum )
        {
            #region ������������Լ��

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            int minLength = Math.Min( xArray.Length, yArray.Length );
            int lineCount = DSPBasic.GetSpectrumLengthByTimeWaveLength( minLength );

            // ʹ��ֱ�Ӽ��������ټ��㻥��
            _ValueT[] r, tau, wave4FFT = new _ValueT[minLength];
            DirectCorrelate( xArray, yArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );

            _ValueT[] xyAmpSpectrum, xyPhaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum( wave4FFT, out xyAmpSpectrum, out xyPhaseSpectrum );

            DirectCorrelate( xArray, xArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );

            _ValueT[] xxAmpSpectrum, xxPhaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum( wave4FFT, out xxAmpSpectrum, out xxPhaseSpectrum );

            ampSpectrum = new _ValueT[lineCount];
            phaseSpectrum = new _ValueT[ampSpectrum.Length];

            CrossSpectrum2FrequenceResponse( xyAmpSpectrum, xyPhaseSpectrum, xxAmpSpectrum, xxPhaseSpectrum, ampSpectrum,
                                             phaseSpectrum );
        }

        /// <summary>
        /// ʹ�ÿ�����ص��м�����Ϊ���ף��������������Ƶ�캯��
        /// </summary>
        /// <param name="xArray">�����һ������</param>
        /// <param name="yArray">����ڶ�������</param>
        /// <param name="ampSpectrum">Ƶ��ķ�Ƶ����</param>
        /// <param name="phaseSpectrum">Ƶ�����Ƶ����</param>
        /// <param name="fftLength">��fft�任��ʱ�䲨�γ���</param>
        public static void FastFrequenceResponse( _ValueT[] xArray, _ValueT[] yArray, out _ValueT[] ampSpectrum,
                                                  out _ValueT[] phaseSpectrum, out int fftLength )
        {
            #region ������������Լ��

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );
            MathError.CheckLengthEqual( xArray, yArray );

            #endregion

            // ʹ�ÿ�����ص��м�����Ϊ����
            _ValueT[] xyAmpSpectrum, xyPhaseSpectrum;
            int xyFftLength;
            CrossSpectrum( xArray, yArray, out xyAmpSpectrum, out xyPhaseSpectrum, out xyFftLength );

            _ValueT[] xxAmpSpectrum, xxPhaseSpectrum;
            int xxFftLength;
            CrossSpectrum( xArray, xArray, out xxAmpSpectrum, out xxPhaseSpectrum, out xxFftLength );

            fftLength = xyFftLength;

            ampSpectrum = new _ValueT[xyAmpSpectrum.Length];
            phaseSpectrum = new _ValueT[ampSpectrum.Length];

            CrossSpectrum2FrequenceResponse( xyAmpSpectrum, xyPhaseSpectrum, xxAmpSpectrum, xxPhaseSpectrum, ampSpectrum,
                                             phaseSpectrum );
        }

        /// <summary>
        /// ʹ�û��׽������Ƶ��
        /// </summary>
        /// <param name="xyAmpSpectrum">Sxy��ֵ</param>
        /// <param name="xyPhaseSpectrum">Sxy��λ</param>
        /// <param name="xxAmpSpectrum">Sxx��ֵ</param>
        /// <param name="xxPhaseSpectrum">Sxx��λ</param>
        /// <param name="ampSpectrum">Ƶ��ķ�Ƶ����</param>
        /// <param name="phaseSpectrum">Ƶ�����Ƶ����</param>
        private static void CrossSpectrum2FrequenceResponse( _ValueT[] xyAmpSpectrum, _ValueT[] xyPhaseSpectrum,
                                                             _ValueT[] xxAmpSpectrum, _ValueT[] xxPhaseSpectrum,
                                                             _ValueT[] ampSpectrum,
                                                             _ValueT[] phaseSpectrum )
        {
            for( int index = 0; index < ampSpectrum.Length; index++ )
            {
                phaseSpectrum[index] =
                    (_ValueT)MathBasic.DegreeToAngle180( xyPhaseSpectrum[index] - xxPhaseSpectrum[index] );
                if( xxAmpSpectrum[index] != 0 )
                {
                    ampSpectrum[index] = xyAmpSpectrum[index] / xxAmpSpectrum[index];
                }
            }
        }
    }
}