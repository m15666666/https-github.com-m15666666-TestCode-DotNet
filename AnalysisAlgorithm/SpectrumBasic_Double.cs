using System;
using System.Collections.Generic;
using Moons.Common20;
// ������ʹ�õ���ֵ���ͣ������ֲ�����ֱ��ʹ�÷��͵����⡣
using _ValueT = System.Double;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ��Ƶ����صĻ����㷨��
    /// </summary>
    public static partial class SpectrumBasic
    {
        #region ��ù�Ƶ������Ϣ

        /// <summary>
        /// ��ù�Ƶ���ĵ���Ƶ�׵�ʵ�����鲿
        /// </summary>
        /// <param name="fs">����Ƶ��</param>
        /// <param name="f0">��Ƶ</param>
        /// <param name="timeWave">ʱ�䲨��</param>
        /// <param name="re">��Ƶ���ĵ���Ƶ�׵�ʵ��</param>
        /// <param name="im">��Ƶ���ĵ���Ƶ�׵��鲿</param>
        public static void GetBaseFreqReIm( Double fs, Double f0, _ValueT[] timeWave, out _ValueT re, out _ValueT im )
        {
            int baseFreqIndex = GetBaseFreqIndex( fs, f0, timeWave );
            _ValueT[] reArray, imArray;
            DSPBasic.ReImSpectrum( timeWave, out reArray, out imArray );

            re = reArray[baseFreqIndex];
            im = imArray[baseFreqIndex];
        }

        /// <summary>
        /// ��ù�Ƶ�����±�
        /// </summary>
        /// <param name="fs">����Ƶ��</param>
        /// <param name="f0">��Ƶ</param>
        /// <param name="timeWave">ʱ�䲨��</param>
        /// <returns>��Ƶ�����±�</returns>
        public static int GetBaseFreqIndex( Double fs, Double f0, _ValueT[] timeWave )
        {
            int centerIndex = GetFreqIndex( fs, f0, timeWave.Length );

            const int SearchOffset = 2;
            const int SearchCount = 2 * SearchOffset + 1;
            _ValueT[] ampSpectrum = AmpSpectrum( timeWave );
            return ArrayUtils.MaxIndex( ampSpectrum, centerIndex - SearchOffset, SearchCount );
        }

        /// <summary>
        /// ��ù�Ƶ�����±�
        /// </summary>
        /// <param name="fs">����Ƶ��</param>
        /// <param name="beginFrequency">Ƶ�ʷ�Χ��ʼ</param>
        /// <param name="endFrequency">Ƶ�ʷ�Χ����</param>
        /// <param name="timeWave">ʱ�䲨��</param>
        /// <param name="amplitudeOfF0">Ƶ�׵Ĺ�Ƶ��ֵ</param>
        /// <returns>��Ƶ�����±�</returns>
        public static int GetBaseFreqIndex( Double fs, Double beginFrequency, Double endFrequency, _ValueT[] timeWave, out Double amplitudeOfF0 )
        {
            int beginIndex = GetFreqIndex( fs, Math.Min( beginFrequency, endFrequency ), timeWave.Length );
            int endIndex = GetFreqIndex( fs, Math.Max( beginFrequency, endFrequency ), timeWave.Length );

            _ValueT[] ampSpectrum = AmpSpectrum(timeWave);
            var indexOfF0 =  ArrayUtils.MaxIndex( ampSpectrum, beginIndex, endIndex - beginIndex + 1 );

            amplitudeOfF0 = ampSpectrum[indexOfF0];

            return indexOfF0;
        }

        /// <summary>
        /// ��ù�Ƶ
        /// </summary>
        /// <param name="fs">����Ƶ��</param>
        /// <param name="beginFreq">Ƶ�ʷ�Χ��ʼ</param>
        /// <param name="endFreq">Ƶ�ʷ�Χ����</param>
        /// <param name="timeWave">ʱ�䲨��</param>
        /// <param name="amplitudeOfF0">Ƶ�׵Ĺ�Ƶ��ֵ</param>
        /// <returns>��Ƶ</returns>
        public static Double GetBaseFreq( Double fs, Double beginFreq, Double endFreq, _ValueT[] timeWave, out Double amplitudeOfF0 )
        {
            int index = GetBaseFreqIndex( fs, beginFreq, endFreq, timeWave, out amplitudeOfF0 );
            return GetFreqByIndex( fs, index, timeWave.Length );
        }

        #endregion

            #region �����ֵ

            /// <summary>
            /// ��ֵ. ��ӳ���źŵ�ƽ������. ����Ƶ�׼���,����Ϊ����Ӧ����λ�ơ��ٶȺͼ��ٶ��źŵ���������.
            /// </summary>
            /// <param name="xArray">ʱ�䲨��</param>
            /// <returns>��ֵ</returns>
        public static _ValueT Overall( _ValueT[] xArray )
        {
            return Overall_AmpSpectrum( AmpSpectrum( xArray ) );
        }

        /// <summary>
        /// ͨ�����߷�ֵ�׻����ֵ
        /// </summary>
        /// <param name="ampSpectrum">��ֵ��</param>
        /// <returns>��ֵ</returns>
        public static _ValueT Overall_AmpSpectrum( _ValueT[] ampSpectrum )
        {
            return Overall_AmpSpectrum( ampSpectrum, 0, ampSpectrum.Length, WindowType.Rectangular );
        }

        /// <summary>
        /// ͨ�����߷�ֵ�׻����ֵ
        /// </summary>
        /// <param name="ampSpectrum">��ֵ��</param>
        /// <param name="startIndex">��ʼ�±�</param>
        /// <param name="count">����</param>
        /// <param name="windowType">�źŴ�����</param>
        /// <returns>��ֵ</returns>
        public static _ValueT Overall_AmpSpectrum( _ValueT[] ampSpectrum, int startIndex, int count,
                                                   WindowType windowType )
        {
            if( Moons.Common20.CollectionUtils.IsNullOrEmptyG( ampSpectrum ) || count < 1 ||
                ampSpectrum.Length <= startIndex )
            {
                return 0;
            }

            startIndex = IndexUtils.GetIndexInRange( startIndex, 0, ampSpectrum.Length - 1 );
            count = Math.Min( count, ampSpectrum.Length - startIndex );
            Double ret = MathBasic.SquareSum( ArrayUtils.Slice( ampSpectrum, startIndex, count ) ) / MathConst.SqrtTwo;
            if( windowType == WindowType.Hanning )
            {
                ret *= Window.PowerScale_Hanning;
            }
// ReSharper disable RedundantCast
            return (_ValueT)ret;
// ReSharper restore RedundantCast
        }

        #endregion

        /// <summary>
        /// ���㵥�߷�ֵ��( ������ = �����źų��� / 2.56 + 1)
        /// </summary>
        /// <param name="xArray">�����ź�x[i]</param>
        /// <returns>������</returns>
        /// <remarks>
        /// ����ֱ����ʾ�� ����˻���κ�ϵ�������磬1024���ʱ�䲨�εõ�401���ס�
        /// </remarks>
        public static _ValueT[] AmpSpectrum( _ValueT[] xArray )
        {
            _ValueT[] biSideSpectrum, phaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum( xArray, out biSideSpectrum, out phaseSpectrum );

            int lineCount = DSPBasic.GetSpectrumLengthByTimeWaveLength( xArray.Length );
            _ValueT[] ampSpectrum = new _ValueT[lineCount];
            for( int index = 0; index < lineCount; index++ )
            {
                ampSpectrum[index] = 2 * biSideSpectrum[index];
            }
            ampSpectrum[0] /= 2;

            return ampSpectrum;
        }

        /// <summary>
        /// ������λ��( ������ = �����źų��� / 2.56 + 1)��-180�� ~ 180��
        /// </summary>
        /// <param name="xArray">�����ź�x[i]</param>
        /// <returns>��λ�ף�-180�� ~ 180��</returns>
        public static _ValueT[] PhaseSpectrum( _ValueT[] xArray )
        {
            _ValueT[] biSideSpectrum, phaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum( xArray, out biSideSpectrum, out phaseSpectrum );

            int lineCount = DSPBasic.GetSpectrumLengthByTimeWaveLength( xArray.Length );
            _ValueT[] ret = new _ValueT[lineCount];
            ArrayUtils.Copy( phaseSpectrum, ret, 0, ret.Length );

            return ret;
        }

        /// <summary>
        /// ����Ƶ��X������ݣ�������ʾ
        /// </summary>
        /// <param name="fs">����Ƶ�ʣ�����ǵȽǶȲ������뱶Ƶϵ��</param>
        /// <param name="dataLength">���������ݳ���</param>
        /// <param name="retLength">ϣ�����ص�����ĳ���</param>
        /// <returns>Ƶ��X������ݣ�������ʾ</returns>
        public static _ValueT[] SpectrumXAxisTicks( _ValueT fs, int dataLength, int retLength )
        {
            _ValueT delta = fs / dataLength;
            _ValueT value = 0;
            _ValueT[] ret = new _ValueT[retLength];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = value;
                value += delta;
            }
            return ret;
        }

        /// <summary>
        /// ����ʱ�䲨��X������ݣ�������ʾ����ʱ�����ʱ��λ�Ǻ���
        /// </summary>
        /// <param name="fs">����Ƶ��</param>
        /// <param name="isOrder">�Ƿ��ǵȽǶȲ���</param>
        /// <param name="dataLength">���������ݳ���</param>
        /// <returns>ʱ�䲨��X������ݣ�������ʾ</returns>
        public static _ValueT[] TimeWaveXAxisTicks( _ValueT fs, bool isOrder, int dataLength )
        {
            _ValueT[] ret = new _ValueT[dataLength];

            if( isOrder )
            {
                for( int index = 0; index < ret.Length; index++ )
                {
                    ret[index] = index + 1;
                }
                return ret;
            }

            _ValueT deltaT = 1f / fs * 1000;
            _ValueT currentTime = 0;
            for( int index = 0; index < ret.Length; index++ )
            {
                currentTime += deltaT;
                ret[index] = currentTime;
            }
            return ret;
        }

        /// <summary>
        /// ������׺Ͳ���.
        /// </summary>
        /// <param name="waveType">�����źŵ���������</param>
        /// <param name="xArray">�����ź�x[i]</param>
        /// <param name="yArray">�����ź�y[i]</param>
        /// <param name="diffSpectrum">�������</param>
        /// <param name="ratioSpectrum">�������</param>
        public static void DiffRatioSpectrum( WaveType waveType, _ValueT[] xArray, _ValueT[] yArray,
                                              out _ValueT[] diffSpectrum, out _ValueT[] ratioSpectrum )
        {
            #region ������������Լ��

            MathError.CheckLengthEqual( xArray, yArray );

            #endregion

            bool isTimeWave = ( WaveType.TimeWave == waveType );
            _ValueT[] xAmpArray = isTimeWave ? AmpSpectrum( xArray ) : xArray;
            _ValueT[] yAmpArray = isTimeWave ? AmpSpectrum( yArray ) : yArray;
            diffSpectrum = CollectionUtils.SubArray( xAmpArray, yAmpArray );
            ratioSpectrum = CollectionUtils.DivArray( xAmpArray, yAmpArray );
        }

        /// <summary>
        /// ��ò��������е���Ҫ��ֵ�����(�Ӵ�С����).
        /// </summary>
        /// <param name="xArray">��������</param>
        /// <param name="includeTwoSide">�Ƿ�������˵ķ�ֵ</param>
        /// <returns>��Ҫ��ֵ���±꣬���δ�ҵ��κη�ֵ�򷵻�null</returns>
        public static int[] MaxPeaksIndex( _ValueT[] xArray, bool includeTwoSide )
        {
            switch( xArray.Length )
            {
                case 1:
                    return includeTwoSide ? new[] {0} : null;

                case 0:
                    return null;
            }

            List<int> peakIndexList = new List<int>();
            int lineCount = xArray.Length;

            // �������˵ķ�ֵ
            if( includeTwoSide )
            {
                if( xArray[0] > xArray[1] )
                {
                    peakIndexList.Add( 0 );
                }
                if( xArray[lineCount - 1] > xArray[lineCount - 2] )
                {
                    peakIndexList.Add( lineCount - 1 );
                }
            }


            for( int index = 1; index < lineCount - 1; index++ )
            {
                if(
                    ( xArray[index - 1] < xArray[index] ) &&
                    ( xArray[index] > xArray[index + 1] )
                    )
                {
                    peakIndexList.Add( index );
                }
            }

            if( peakIndexList.Count == 0 )
            {
                return null;
            }

            int[] peakIndexes = peakIndexList.ToArray();
            _ValueT[] keys = new _ValueT[peakIndexes.Length];
            int keyIndex = 0;
            foreach( int peakIndex in peakIndexes )
            {
                keys[keyIndex++] = xArray[peakIndex];
            }
            return ArrayUtils.SortArray( keys, peakIndexes, false );
        }

        /// <summary>
        /// ��ò�������������n����Ҫ��ֵ(�Ӵ�С����).
        /// </summary>
        /// <param name="xArray">��������</param>
        /// <param name="includeTwoSide">�Ƿ�������˵ķ�ֵ</param>
        /// <param name="maxNum">����ֵ����</param>
        /// <returns>n����Ҫ��ֵ�����δ�ҵ��κη�ֵ�򷵻�null</returns>
        public static _ValueT[] MaxPeaks( _ValueT[] xArray, bool includeTwoSide, int maxNum )
        {
            if( maxNum <= 0 )
            {
                return null;
            }

            int[] peakIndex = MaxPeaksIndex( xArray, includeTwoSide );
            if( peakIndex == null )
            {
                return null;
            }

            _ValueT[] maxPeaks = new _ValueT[Math.Min( maxNum, peakIndex.Length )];
            for( int index = 0; index < maxPeaks.Length; index++ )
            {
                maxPeaks[index] = xArray[peakIndex[index]];
            }

            return maxPeaks;
        }

        /// <summary>
        /// ��������ת��Ϊ�ֱ���.
        /// </summary>
        /// <param name="linearSpectrum">������</param>
        /// <param name="vibQtyType">��������</param>
        /// <param name="isAmpSpectrum">�Ƿ��ֵ��(trueΪ��ֵ��, falseΪ������)</param>
        /// <returns>�ֱ���</returns>
        //	�������׵Ļ�׼ֵȡƽ��
        private static _ValueT[] LinearToDBSpectrum( _ValueT[] linearSpectrum, VibQtyType vibQtyType, bool isAmpSpectrum )
        {
            // ��׼ֵ
            Double x0 = 1;

            switch( vibQtyType )
            {
                case VibQtyType.Force:
                    x0 = ISOR1683.f0;
                    break;

                case VibQtyType.Displacement:
                    x0 = ISOR1683.d0;
                    break;

                case VibQtyType.Velocity:
                    x0 = ISOR1683.v0;
                    break;

                case VibQtyType.Acceleration:
                    x0 = ISOR1683.a0;
                    break;
            }

            if( isAmpSpectrum )
            {
                return CollectionUtils.Array20Log10( linearSpectrum, x0 );
            }
            return CollectionUtils.Array10Log10( linearSpectrum, MathBasic.Square( x0 ) );
        }

        /// <summary>
        /// ��������ת��Ϊ�ֱ��ף���׼ֵΪ1��ϵ��Ϊ10
        /// </summary>
        /// <param name="linearSpectrum">������</param>
        /// <returns>�ֱ���</returns>
        public static _ValueT[] LinearToDBSpectrum( _ValueT[] linearSpectrum )
        {
            return LinearToDBSpectrum( linearSpectrum, VibQtyType.Generic, false );
        }

        /// <summary>
        /// ����ʵ����( ������ = �����źų��� / 2)
        /// </summary>
        /// <param name="timeWave">ʱ�䲨��</param>
        /// <returns>���ײ���</returns>
        // ���ڹ����׼���ʵ���ס�
        // ���㹫ʽ��  C(q) = Real{IFFT{log10[Gx(f)]}}
        // �ο����ף�1990-�����-��FORTRANԴ�����ࡷ
        public static _ValueT[] Cepstrum( _ValueT[] timeWave )
        {
            //ԭʼ�ź�ȥ����ֵ, 1990-�����-��FORTRANԴ�����ࡷ
            DSPBasic.ACCoupling( timeWave );

            // �ȵõ�˫�߶���������
            _ValueT[] biAmpSpectrum, phaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum( timeWave, out biAmpSpectrum, out phaseSpectrum );
            int lineCount = biAmpSpectrum.Length;
            _ValueT[] powerSpectrum = new _ValueT[lineCount];

            for( int index = 0; index < lineCount; index++ )
            {
                Double powerSpectrumValue = biAmpSpectrum[index] * biAmpSpectrum[index];
                const int MinPower = -20;
                const Double MinValue = 1.0E-20;
                powerSpectrum[index] = ( powerSpectrumValue <= MinValue
                                             ? MinPower
// ReSharper disable RedundantCast
                                             : (_ValueT)Math.Log10( powerSpectrumValue ) );
// ReSharper restore RedundantCast
            }

            //˫��FFT��任
            _ValueT[] imArray = new _ValueT[lineCount];
            DSPBasic.CxInvFFT( powerSpectrum, imArray );
            //ȥ��������t=0����ֵ
            powerSpectrum[0] = 0;

            _ValueT[] cepstrum = new _ValueT[lineCount / 2];
            for( int index = 0; index < cepstrum.Length; index++ )
            {
                cepstrum[index] = Math.Abs( powerSpectrum[index] );
            }

            return cepstrum;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="timeWave">ʱ�䲨��</param>
        /// <param name="fl">�ͽ�ֹƵ��</param>
        /// <param name="fh">�߽�ֹƵ��</param>
        /// <param name="fs">����Ƶ��</param>
        /// <returns>����������</returns>
        public static _ValueT[] DeModSpectrum( _ValueT[] timeWave, Double fl, Double fh,
                                               Double fs )
        {
            #region ������������Լ��

            //���ʵ������ĸ����������0
            MathError.CheckLengthGTZero( timeWave );

            //������鳤���Ƿ�2���ݴ�
            MathError.CheckPowerOfTwo( timeWave.Length );

            #endregion

            return DeModSpectrum( DSPBasic.ButterworthBandPass( timeWave, 5, fl, fh, fs ) );
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="timeWave">ʱ�䲨��</param>
        /// <returns>����������</returns>
        public static _ValueT[] DeModSpectrum( _ValueT[] timeWave )
        {
            #region ������������Լ��

            //���ʵ������ĸ����������0
            MathError.CheckLengthGTZero( timeWave );

            //������鳤���Ƿ�2���ݴ�
            MathError.CheckPowerOfTwo( timeWave.Length );

            #endregion

            //���л���Hilbert�任�İ�����
            _ValueT[] envArray = Envelope.HilbertDetection( timeWave );

            //ȥ��֧������
            DSPBasic.ACCoupling( envArray );

            //��������ֵ��
            _ValueT[] ampSpectrum, phaseSpectrum;
            DSPBasic.AmpPhaseSpectrum( envArray, out ampSpectrum, out phaseSpectrum );

            return ampSpectrum;
        }

        /// <summary>
        /// ��ò���ͼ����λ����
        /// </summary>
        /// <param name="reArray">Ƶ�׵�ʵ��</param>
        /// <param name="imArray">Ƶ�׵��鲿</param>
        /// <param name="revs">����ת������</param>
        /// <returns>����ͼ����λ����</returns>
        public static _ValueT[] BodePhase( _ValueT[] reArray, _ValueT[] imArray, int[] revs )
        {
            return BodePhase( reArray, imArray, revs[0] < revs[revs.Length - 1] );
        }

        /// <summary>
        /// ��ò���ͼ����λ����
        /// </summary>
        /// <param name="reArray">Ƶ�׵�ʵ��</param>
        /// <param name="imArray">Ƶ�׵��鲿</param>
        /// <param name="isClockWise">true�����˳ʱ�룻false��ͣ����λ��������ʱ�����</param>
        /// <returns>����ͼ����λ����</returns>
        private static _ValueT[] BodePhase( _ValueT[] reArray, _ValueT[] imArray, bool isClockWise )
        {
            // ��һ����λ
            Double lastPhase = 0;

            _ValueT[] ret = new _ValueT[reArray.Length];
            for( int index = 0; index < reArray.Length; index++ )
            {
                _ValueT re = reArray[index];
                _ValueT im = imArray[index];

                Double phase = MathBasic.ReIm2Phase180( re, im );

                if( index == 0 )
                {
                    lastPhase = phase;
                }
                else
                {
                    Double offset = phase - lastPhase;
                    if( Math.Abs( offset ) <= MathConst.Deg_180 )
                    {
                        // ��λ������Ҫ�󣬱��������λ��
                    }
                    else if( Math.Abs( offset + MathConst.Deg_360 ) <= MathConst.Deg_180 )
                    {
                        phase += MathConst.Deg_360;
                    }
                    else
                    {
                        phase -= MathConst.Deg_360;
                    }

                    lastPhase = phase;
                }

// ReSharper disable RedundantCast
                ret[index] = (_ValueT)phase;
// ReSharper restore RedundantCast
            } // for( int index = 0; index < reArray.Length; index++ )

            return ret;
        }
    }
}